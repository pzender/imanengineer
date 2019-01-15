using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using TV_App.EFModels;
using Microsoft.EntityFrameworkCore;
using LemmaSharp;
using Microsoft.Extensions.Logging;

namespace TV_App.DataLayer
{
    public class XMLParser
    {
        private readonly testContext DbContext = new testContext();
        private readonly ILogger logger;

        public XMLParser(ILogger logger)
        {
            this.logger = logger;
        }

        public void ParseAll(XDocument doc)
        {
            IEnumerable<XElement> channels_in_xml = doc.Root.Elements("channel");
            logger.LogInformation($"{channels_in_xml.Count()} channels found. Parsing channels.");
            //guideupdate
            long new_id = DbContext.GuideUpdate.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);

            GuideUpdate new_gu = new GuideUpdate()
            {
                Id = new_id,
                Posted = DateTime.Now.ToString(),
                Source = channels_in_xml.First().Element("url").Value
            };
            DbContext.GuideUpdate.Add(new_gu);

            DbContext.SaveChanges();

            //kanały
            new_id = DbContext.Channel.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

            foreach (XElement channel in channels_in_xml)
            {
                if (!DbContext.Channel.Where(ch => ch.Name == channel.Attribute("id").Value).Any())
                {
                    Channel new_channel = new Channel()
                    {
                        Id = new_id,
                        Name = channel.Attribute("id").Value,
                        IconUrl = channel.Element("icon")?.Attribute("src").Value,
                    };
                    DbContext.Channel.Add(new_channel);
                    new_id++;

                }
            }
            DbContext.SaveChanges();
            logger.LogInformation($"Channels done. ");

            //programy
            IEnumerable<XElement> programmes_in_xml = doc.Root.Elements("programme").ToList();

            int count = programmes_in_xml.Count(), i = 0;
            logger.LogInformation($"{count} programmes found. Parsing programmes.");


            List<Programme> new_programmes = new List<Programme>();
            new_id = DbContext.Programme.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

            List<Emission> new_emissions = new List<Emission>();
            long em_id = DbContext.Emission.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

            List<Description> new_descriptions = new List<Description>();
            long desc_id = DbContext.Description.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

            List<Feature> new_features = new List<Feature>();
            long feat_id = DbContext.Feature.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

            List<FeatureExample> new_feature_examples = new List<FeatureExample>();
            foreach (XElement programme in programmes_in_xml)
            {
                //i++;
                //logger.LogInformation($"Programme {i} of {count}");
                Programme new_prog = DbContext.Programme
                    .Include(prog => prog.Description)
                    .Include(prog => prog.Emission)
                    .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                    .SingleOrDefault(prog => prog.Title == programme.Elements("title").First().Value);
                if (new_prog == null)
                    new_prog = new_programmes.SingleOrDefault(prog => prog.Title == programme.Elements("title").First().Value);
                if (new_prog == null)
                {
                    new_prog = new Programme()
                    {
                        Id = new_id,
                        Title = programme.Elements("title").First().Value,
                        IconUrl = programme.Element("icon")?.Attribute("src").Value
                    };
                    new_programmes.Add(new_prog);
                    new_id++;
                }

                Emission new_em = new_prog.Emission
                    .SingleOrDefault(e => DateTime.ParseExact(e.Start, "dd.MM.yyyy HH:mm:ss", null) == ParseDateTimeXml(programme.Attribute("start").Value)
                                       && DateTime.ParseExact(e.Stop, "dd.MM.yyyy HH:mm:ss", null) == ParseDateTimeXml(programme.Attribute("stop").Value));
                if(new_em == null)
                {
                    DateTime start = ParseDateTimeXml(programme.Attribute("start").Value);
                    DateTime stop = ParseDateTimeXml(programme.Attribute("stop").Value);
                    Channel channel = DbContext.Channel.Where(ch => ch.Name == programme.Attribute("channel").Value).Single();
                    new_em = new Emission()
                    {
                        Id = em_id,
                        Channel = channel,
                        ChannelId = channel.Id,
                        Start = $"{start:dd.MM.yyyy HH:mm:ss}",
                        Stop = $"{stop:dd.MM.yyyy HH:mm:ss}",
                        Programme = new_prog,
                        ProgrammeId = new_prog.Id
                    };

                    if (!new_prog.Emission.Contains(new_em))
                    {
                        new_prog.Emission.Add(new_em);
                        em_id++;
                    }
                }

                Description new_desc = DbContext.Description
                    .Include(desc => desc.IdProgrammeNavigation)
                    .Include(desc => desc.SourceNavigation)
                    .SingleOrDefault(desc => desc.IdProgrammeNavigation == new_prog);

                if(new_desc == null)
                {
                    new_desc = new Description()
                    {
                        Id = desc_id,
                        IdProgramme = new_prog.Id,
                        IdProgrammeNavigation = new_prog,
                        SourceNavigation = new_gu,
                        Source = new_gu.Id,
                        Content = programme.Element("desc")?.Value ?? ""
                    };
                    if (!new_prog.Description.Contains(new_desc))
                    {
                        new_prog.Description.Add(new_desc);
                        desc_id++;
                    }
                }

                string[] feat_names = { "date", "category", "country" };
                List<XElement> features = programme.Elements().Where(elem => feat_names.Contains(elem.Name.LocalName)).ToList();
                if(programme.Element("credits") != null)
                    features.AddRange(programme.Element("credits").Elements());
                if (features.Where(el => el.Name.LocalName == "date").Count() == 0)
                    features.Add(new XElement("date", $"{DateTime.Now.Year-1}"));

                foreach(XElement feat in features)
                {

                    string type = feat.Name.LocalName;
                    string value = feat.Value;
                    if (type == "category")
                        value = lemmatizer.Lemmatize(value);

                    Feature new_feat = DbContext.Feature
                        .Include(f => f.TypeNavigation)
                        .SingleOrDefault(f => f.TypeNavigation.TypeName == type && f.Value == value);
                    if (new_feat == null)
                        new_feat = new_features.SingleOrDefault(f => f.TypeNavigation.TypeName == type && f.Value == value);
                    if(new_feat == null)
                    {
                        new_feat = new Feature()
                        {
                            Id = feat_id,
                            TypeNavigation = DbContext.FeatureTypes.Single(ft => ft.TypeName == type),
                            Value = value
                        };
                        new_features.Add(new_feat);
                        DbContext.Feature.Add(new_feat);
                        feat_id++;
                    }

                    if (new_prog.FeatureExample.SingleOrDefault(fe => fe.Feature == new_feat) == null)
                    {
                        new_prog.FeatureExample.Add(new FeatureExample()
                        {
                            ProgrammeId = new_prog.Id,
                            FeatureId = new_feat.Id,
                            Programme = new_prog,
                            Feature = new_feat
                        });
                    }                    
                }

            }

            //DbContext.Database.OpenConnection();
            //DbContext.Database.ExecuteSqlCommand("PRAGMA foreign_keys=OFF;");
            DbContext.AddRange(new_programmes);
            DbContext.SaveChanges();
            //DbContext.Database.CloseConnection();

            try
            {

                //DbContext.Programme.AddRange(new_programmes);
                //DbContext.SaveChanges();

                //DbContext.Feature.AddRange(new_features);
                //DbContext.SaveChanges();

                //DbContext.Emission.AddRange(new_emissions);
                //DbContext.SaveChanges();

                //DbContext.FeatureExample.AddRange(new_feature_examples);
                //DbContext.SaveChanges();

                //DbContext.Description.AddRange(new_descriptions);
                //DbContext.SaveChanges();

            }
            catch (DbUpdateException e)
            {
                logger.LogError(e.InnerException.Message);
                logger.LogError(e.StackTrace);
            }
            //throw new NotImplementedException();
        }


        //01234567 89
        //20181017 03 25 00 +0200
        private DateTime ParseDateTimeXml(string inp)
        {
            return new DateTime(
                year: int.Parse(inp.Substring(0, 4)),
                month: int.Parse(inp.Substring(4, 2)),
                day: int.Parse(inp.Substring(6, 2)),
                hour: int.Parse(inp.Substring(8, 2)),
                minute: int.Parse(inp.Substring(10, 2)),
                second: int.Parse(inp.Substring(12, 2))
            );
        }

        private DateTime ParseDateTimeDb(string inp)
        {
            return new DateTime(
                year: int.Parse(inp.Substring(0, 4)),
                month: int.Parse(inp.Substring(5,2)),
                day: int.Parse(inp.Substring(8,2)),
                hour: int.Parse(inp.Substring(11,2)),
                minute: int.Parse(inp.Substring(14,2)),
                second: int.Parse(inp.Substring(17, 2))
            );
        }
    }
}
