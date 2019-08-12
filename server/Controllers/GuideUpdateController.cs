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

        // GET: api/GuideUpdate
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
            Console.WriteLine("GuideUpdateController.Post() called");
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
                //long feat_id = DbContext.Feature.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            //    KeywordExtractor keywordExtractor = new KeywordExtractor(logger);

            //    IEnumerable<Programme> list = DbContext.Programme.Include(prog => prog.Description).ToList();
            //    int count = list.Count(), i = 0;

            //    foreach (Programme p in list)
            //    {
            //        i++;
            //        logger.LogInformation($"Processing keywords for programme {i} of {count}");
            //        List<string> keywords = keywordExtractor.ProcessKeywords(p);
            //        foreach (string keyword in keywords)
            //        {
            //            string type = "keyword";

            //            Feature new_feat = DbContext.Feature
            //                .Include(f => f.TypeNavigation)
            //                .Where(f => f.TypeNavigation.TypeName == type && f.Value == keyword)
            //                .SingleOrDefault();
            //            if (new_feat == null)
            //            {
            //                new_feat = new Feature()
            //                {
            //                    Id = feat_id,
            //                    TypeNavigation = DbContext.FeatureTypes.First(ft => ft.TypeName == type),
            //                    Value = keyword
            //                };
            //                DbContext.Feature.Add(new_feat);
            //                DbContext.SaveChanges();
            //                feat_id++;
            //            }

            //            FeatureExample new_fe = DbContext.FeatureExample
            //                .Where(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == p.Id)
            //                .SingleOrDefault();
            //            if (new_fe == default(FeatureExample))
            //            {
            //                new_fe = new FeatureExample()
            //                {
            //                    FeatureId = new_feat.Id,
            //                    ProgrammeId = p.Id,
            //                    Feature = new_feat,
            //                    Programme = p
            //                };
            //                DbContext.FeatureExample.Add(new_fe);
            //                DbContext.SaveChanges();
            //            }
            //        }
            //    }
            //}

            //DbContext.Emissions
            //    .RemoveRange(DbContext.Emissions.Where(em => em.Stop < DateTime.Today));
            List<long> emptyProgrammeIds = DbContext.Programmes
                .Include(prog => prog.Emissions)
                .Include(prog => prog.Ratings)
                .Where(prog => prog.Emissions.Count == 0 && prog.Ratings.Count == 0)
                .Select(prog => prog.Id)
                .ToList();
            //DbContext.Programmes
            //    .RemoveRange(DbContext.Programmes.Where(prog => emptyProgrammeIds.Contains(prog.Id)));
            DbContext.SaveChanges();
        }
    }
}
