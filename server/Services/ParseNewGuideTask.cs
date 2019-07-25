using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TV_App.DataLayer;
using TV_App.Models;
using TV_App.Scheduler;

namespace TV_App.Services
{
    public class ParseNewGuideTask : IScheduledTask
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly TvAppContext DbContext = new TvAppContext();

        public string Schedule => "* * * * *";

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var logger = loggerFactory.CreateLogger("ParseNewGuideTask");
            logger.LogInformation("ParseNewGuide task started!");

            string filepath = "/data/guide.xml";
            string data = string.Join(" ", File.ReadAllLines(filepath));
            DbContext.GuideUpdate.Add(new GuideUpdate()
            {
                Description = null,
                Id = DbContext.GuideUpdate.Select(update => update.Id).Max() + 1,
                Posted = DateTime.Now,
                Source = "TEST!"
            }) ;
            await DbContext.SaveChangesAsync();

            // await Task.Run(() => { Import(logger, data); });
            // Import(logger, data);
        }

        private async void Import(ILogger logger, string body)
        {
            logger.LogInformation("Request body found. Parsing.");
            XMLParser parser = new XMLParser(logger);
            parser.ParseAll(XDocument.Parse(body));
            long feat_id = DbContext.Feature.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            logger.LogInformation("Parsing done. ");
            KeywordExtractor keywordExtractor = new KeywordExtractor(logger);

            IEnumerable<Programme> list = DbContext.Programme.Include(prog => prog.Description);
            int count = list.Count(), i = 0;

            foreach (Programme p in list)
            {
                i++;
                logger.LogInformation($"Processing keywords for programme {i} of {count}");
                List<string> keywords = keywordExtractor.ProcessKeywords(p);
                foreach (string keyword in keywords)
                {
                    string type = "keyword";

                    Feature new_feat = DbContext.Feature
                        .Include(f => f.TypeNavigation)
                        .Where(f => f.TypeNavigation.TypeName == type && f.Value == keyword)
                        .SingleOrDefault();
                    if (new_feat == null)
                    {
                        new_feat = new Feature()
                        {
                            Id = feat_id,
                            TypeNavigation = DbContext.FeatureTypes.First(ft => ft.TypeName == type),
                            Value = keyword
                        };
                        DbContext.Feature.Add(new_feat);
                        DbContext.SaveChanges();
                        feat_id++;
                    }

                    FeatureExample new_fe = DbContext.FeatureExample
                        .Where(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == p.Id)
                        .SingleOrDefault();
                    if (new_fe == default(FeatureExample))
                    {
                        new_fe = new FeatureExample()
                        {
                            FeatureId = new_feat.Id,
                            ProgrammeId = p.Id,
                            Feature = new_feat,
                            Programme = p
                        };
                        DbContext.FeatureExample.Add(new_fe);
                        DbContext.SaveChanges();
                    }
                }
            }
        }
    }
}

