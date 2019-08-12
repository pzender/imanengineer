using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TV_App.Models;

namespace TV_App.Services
{
    public class GuideUpdateService
    {
        private TvAppContext context;
        public GuideUpdateService(TvAppContext context)
        {
            this.context = context;
        }

        public void ParseAll(XDocument doc)
        {
            if (!context.FeatureTypes.Any())
            {
                FillFeatureTypes();
            }

            IEnumerable<XElement> channels_in_xml = doc.Root.Elements("channel");
            //guideupdate
            long new_id = context.GuideUpdates.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;

            GuideUpdate new_gu = new GuideUpdate()
            {
                Id = new_id,
                Posted = DateTime.Now,
                Source = channels_in_xml.First().Element("url").Value
            };
            context.GuideUpdates.Add(new_gu);

            context.SaveChanges();

            //kanały
            new_id = context.Channels.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;

            foreach (XElement channel in channels_in_xml)
            {
                if (!context.Channels.Where(ch => ch.Name == channel.Attribute("id").Value).Any())
                {
                    Channel new_channel = new Channel()
                    {
                        Id = new_id,
                        Name = channel.Attribute("id").Value,
                        IconUrl = channel.Element("icon")?.Attribute("src").Value,
                    };
                    context.Channels.Add(new_channel);
                    new_id++;

                }
            }
            context.SaveChanges();

            //programy
            IEnumerable<XElement> programmes_in_xml = doc.Root.Elements("programme").ToList();

            int count = programmes_in_xml.Count();


            List<Programme> new_programmes = new List<Programme>();
            new_id = context.Programmes.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;
            List<Emission> new_emissions = new List<Emission>();
            long em_id = context.Emissions.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;

            List<Description> new_descriptions = new List<Description>();
            long desc_id = context.Descriptions.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;
            List<Feature> new_features = new List<Feature>();
            long feat_id = context.Features.OrderBy(gu => gu.Id).LastOrDefault()?.Id + 1 ?? 0;

            List<ProgrammesFeature> new_feature_examples = new List<ProgrammesFeature>();
            foreach (XElement programme in programmes_in_xml)
            {
                string new_title = programme.Elements("title").First().Value;
                if (new_title.Length > 180)
                {
                    new_title = new_title.Substring(0, 180);
                    int last_space = new_title.LastIndexOf(' ');
                    new_title = new_title.Substring(0, last_space);
                    new_title += "...";
                }
                Programme new_prog = null;
                if (context.Programmes.Any())
                {
                    new_prog = context.Programmes.SingleOrDefault(prog => prog.Title.ToLower() == new_title.ToLower());
                }
                if (new_prog == null)
                    new_prog = new_programmes
                        .SingleOrDefault(prog => prog.Title.ToLower() == new_title.ToLower());

                if (new_prog == null)
                {
                    new_prog = new Programme()
                    {
                        Id = new_id,
                        Title = new_title,
                        IconUrl = programme.Element("icon")?.Attribute("src").Value
                    };
                    new_programmes.Add(new_prog);
                    new_id++;
                }

                Emission new_em = new_prog.Emissions
                    .SingleOrDefault(e => e.Start == ParseDateTime(programme.Attribute("start").Value)
                                       && e.Stop == ParseDateTime(programme.Attribute("stop").Value));
                if (new_em == null)
                {
                    if (programme.Attribute("start") == null || programme.Attribute("stop") == null)
                        throw new DataException($"Missing emission hours ({new_prog.Title})");
                    DateTime start = ParseDateTime(programme.Attribute("start").Value);
                    DateTime stop = ParseDateTime(programme.Attribute("stop").Value);
                    Channel channel = context.Channels.Where(ch => ch.Name == programme.Attribute("channel").Value).Single();
                    new_em = new Emission()
                    {
                        ChannelEmitted = channel,
                        ChannelId = channel.Id,
                        Start = start,
                        Stop = stop,
                        RelProgramme = new_prog,
                        ProgrammeId = new_prog.Id
                    };

                    if (!new_prog.Emissions.Contains(new_em))
                    {
                        new_prog.Emissions.Add(new_em);
                        em_id++;
                    }
                }

                Description new_desc = context.Descriptions.Any() ? context.Descriptions.FirstOrDefault(desc => desc.ProgrammeId == new_prog.Id) : null;

                if (new_desc == null)
                {
                    new_desc = new Description()
                    {
                        Id = desc_id,
                        ProgrammeId = new_prog.Id,
                        RelProgramme = new_prog,
                        Content = programme.Element("desc")?.Value ?? ""
                    };
                    if (new_prog.Descriptions.Where(description => description.Content == new_desc.Content).Count() == 0)
                    {
                        new_prog.Descriptions.Add(new_desc);
                        desc_id++;
                    }
                }

                string[] feat_names = { "date", "category", "country" };
                List<XElement> features = programme.Elements().Where(elem => feat_names.Contains(elem.Name.LocalName)).ToList();
                if (programme.Element("credits") != null)
                    features.AddRange(programme.Element("credits").Elements());
                if (features.Where(el => el.Name.LocalName == "date").Count() == 0)
                    features.Add(new XElement("date", $"{DateTime.Now.Year - 1}"));

                foreach (XElement feat in features)
                {

                    string type = feat.Name.LocalName;
                    long type_id = context.FeatureTypes
                        .FirstOrDefault(ft => ft.TypeName == type)
                        .Id;

                    string value = feat.Value;
                    //if (type == "category")
                    //    value = lemmatizer.Lemmatize(value);

                    Feature new_feat = context.Features.Any() ?
                        context.Features.FirstOrDefault(f => f.Type == type_id && f.Value == value)
                        : null;

                    if (new_feat == null)
                        new_feat = new_features.SingleOrDefault(f => f.RelType.TypeName == type && f.Value == value);
                    if (new_feat == null)
                    {
                        new_feat = new Feature()
                        {
                            Id = feat_id,
                            RelType = context.FeatureTypes.Single(ft => ft.TypeName == type),
                            Value = value
                        };
                        new_features.Add(new_feat);
                        context.Features.Add(new_feat);
                        feat_id++;
                    }

                    if (new_prog.ProgrammesFeatures.SingleOrDefault(fe => fe.RelFeature == new_feat) == null)
                    {
                        new_prog.ProgrammesFeatures.Add(new ProgrammesFeature()
                        {
                            ProgrammeId = new_prog.Id,
                            FeatureId = new_feat.Id,
                            RelProgramme = new_prog,
                            RelFeature = new_feat
                        });
                    }
                }
            }

            context.AddRange(new_programmes);
            context.SaveChanges();

        }

        public void ClearOldProgrammes(DateTime olderThan)
        {

        }

        private DateTime ParseDateTime(string inp)
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
        private void FillFeatureTypes()
        {
            context.FeatureTypes.AddRange(new List<FeatureType>
            {
                new FeatureType() { Id = 1, TypeName = "country" },
                new FeatureType() { Id = 2, TypeName = "date" },
                new FeatureType() { Id = 3, TypeName = "writer" },
                new FeatureType() { Id = 4, TypeName = "actor" },
                new FeatureType() { Id = 5, TypeName = "director" },
                new FeatureType() { Id = 6, TypeName = "presenter" },
                new FeatureType() { Id = 7, TypeName = "category" },
                new FeatureType() { Id = 8, TypeName = "keyword" },

            });
            context.SaveChanges();
        }



    }
}
