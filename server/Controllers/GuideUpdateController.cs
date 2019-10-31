using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TV_App.Models;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideUpdateController : ControllerBase
    {

        private readonly ILoggerFactory loggerFactory;
        private readonly TvAppContext DbContext = new TvAppContext();

        public GuideUpdateController(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
            
        }

        // GET: api/GuideUpdate/Last
        [HttpGet("Last")]
        public GuideUpdateJson Get()
        {
            var last = DbContext.GuideUpdates
                .Where(gu => gu.Finished.HasValue)
                .OrderBy(gu => gu.Finished.Value)
                .Last();
            return new GuideUpdateJson()
            {
                Id = last.Id,
                Started = last.Started,
                Finished = last.Finished.GetValueOrDefault(),
                Source = last.Source
            };
        }

        // GET: api/GuideUpdate/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GuideUpdate
        [HttpPost]
        public async Task PostAsync()
        {
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - start");
            string body = "";
            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await sr.ReadToEndAsync();
            }

            if (body != "")
            {
                GuideUpdateService service = new GuideUpdateService(DbContext);
                service.ParseAll(XDocument.Parse(body));
            }

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - notifications");
            var old_notifications = DbContext.Notifications
                .Include(n => n.RelEmission)
                .Where(n => n.RelEmission.Stop < DateTime.Today);
            DbContext.Notifications.RemoveRange(old_notifications);
            DbContext.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - emissions");
            DbContext.Emissions.RemoveRange(
                DbContext.Emissions.Where(em => em.Stop < DateTime.Today)
            );
            DbContext.SaveChanges();
            
            List<long> emptyProgrammeIds = DbContext.Programmes
                .Include(prog => prog.Emissions)
                .Include(prog => prog.Ratings)
                .Where(prog => prog.Emissions.Count == 0 && prog.Ratings.Count == 0)
                .Select(prog => prog.Id)
                .ToList();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - {emptyProgrammeIds.Count} empty");
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - descriptions");
            DbContext.Descriptions
                .RemoveRange(DbContext.Descriptions.Where(desc => emptyProgrammeIds.Contains(desc.ProgrammeId)));
            DbContext.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - programme-featues");
            DbContext.ProgrammesFeatures
                .RemoveRange(DbContext.ProgrammesFeatures.Where(pf => emptyProgrammeIds.Contains(pf.ProgrammeId)));
            DbContext.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - programmes");
            DbContext.Programmes
                .RemoveRange(DbContext.Programmes.Where(prog => emptyProgrammeIds.Contains(prog.Id)));
            DbContext.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - finished");
            GuideUpdate last = DbContext.GuideUpdates.Last();
            last.Finished = DateTime.Now;
            DbContext.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - finish");
        }
        public class GuideUpdateJson
        {
            public long Id { get; set; }
            public DateTime Started { get; set; }
            public DateTime Finished { get; set; }
            public string Source { get; set; }
        }

    }
}
