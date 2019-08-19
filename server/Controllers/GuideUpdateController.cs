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
using TV_App.DataLayer;
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
            var last = DbContext.GuideUpdates.Last();
            return new GuideUpdateJson()
            {
                Id = last.Id,
                Posted = last.Posted,
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

            DbContext.Emissions
                .RemoveRange(DbContext.Emissions.Where(em => em.Stop < DateTime.Today));
            List<long> emptyProgrammeIds = DbContext.Programmes
                .Include(prog => prog.Emissions)
                .Include(prog => prog.Ratings)
                .Where(prog => prog.Emissions.Count == 0 && prog.Ratings.Count == 0)
                .Select(prog => prog.Id)
                .ToList();
            DbContext.Descriptions
                .RemoveRange(DbContext.Descriptions.Where(desc => emptyProgrammeIds.Contains(desc.ProgrammeId)));
            DbContext.ProgrammesFeatures
                .RemoveRange(DbContext.ProgrammesFeatures.Where(pf => emptyProgrammeIds.Contains(pf.ProgrammeId)));
            DbContext.Programmes
                .RemoveRange(DbContext.Programmes.Where(prog => emptyProgrammeIds.Contains(prog.Id)));
            DbContext.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - finish");
        }
        public class GuideUpdateJson
        {
            public long Id { get; set; }
            public DateTime Posted { get; set; }
            public string Source { get; set; }
        }

    }
}
