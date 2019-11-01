﻿using Microsoft.EntityFrameworkCore;
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
        private readonly TvAppContext context;
        private long lastGuideUpdateId;
        public GuideUpdateService(TvAppContext context)
        {
            this.context = context;
        }
        private void InitGuideUpdate(string src)
        {
            lastGuideUpdateId = GetNewId(context.GuideUpdates);

            GuideUpdate new_gu = new GuideUpdate()
            {
                Id = lastGuideUpdateId,
                Started = DateTime.Now,
                Source = src
            };
            context.GuideUpdates.Add(new_gu);
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - entry added");
            context.SaveChanges();

        }

        public void ParseChannels(IEnumerable<XElement> channels_in_xml)
        {
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - parsing channels");
            long new_id = GetNewId(context.Channels);

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
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - saving channels");
            context.SaveChanges();
        }

        public void ParseAll(XDocument doc)
        {
            
            if (!context.FeatureTypes.Any())
            {
                FillFeatureTypes();
            }

            IEnumerable<XElement> channels_in_xml = doc.Root.Elements("channel");

            ParseChannels(channels_in_xml);
            InitGuideUpdate(channels_in_xml.First().Element("url").Value); 

            //programy
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - parsing programmes");
            IEnumerable<XElement> programmes_in_xml = doc.Root.Elements("programme").ToList();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - {programmes_in_xml.Count()} found");
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - first at {programmes_in_xml.First().Attribute("start")}");
            List<Programme> new_programmes = new List<Programme>();
            long new_id = GetNewId(context.Programmes);
            List<Emission> new_emissions = new List<Emission>();
            long em_id = GetNewId(context.Emissions);

            List<Description> new_descriptions = new List<Description>();
            long desc_id = GetNewId(context.Descriptions);
            List<Feature> new_features = new List<Feature>();
            long feat_id = GetNewId(context.Features);

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
                    new_prog = context.Programmes.SingleOrDefault(prog => prog.Title.ToLower() == new_title.ToLower());
                if (new_prog == null)
                    new_prog = new_programmes.SingleOrDefault(prog => prog.Title.ToLower() == new_title.ToLower());

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

                Emission new_em = null;
                if (programme.Attribute("start") == null || programme.Attribute("stop") == null)
                    throw new DataException($"Missing emission hours ({new_prog.Title})");

                DateTime em_start = ParseDateTime(programme.Attribute("start").Value);
                DateTime em_stop = ParseDateTime(programme.Attribute("stop").Value);
                Channel em_channel = context.Channels.Where(ch => ch.Name == programme.Attribute("channel").Value).Single();

                if (context.Emissions.Any())
                    new_em = context.Emissions.FirstOrDefault(e => e.Start == em_start && e.Stop == em_stop && e.ChannelId == em_channel.Id);
                if (new_em == null)
                    new_em = new_prog.Emissions.FirstOrDefault(e => e.Start == em_start && e.Stop == em_stop && e.ChannelId == em_channel.Id);

                if (new_em == null)
                {
                    
                    new_em = new Emission()
                    {
                        ChannelEmitted = em_channel,
                        ChannelId = em_channel.Id,
                        Start = em_start,
                        Stop = em_stop,
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
                    features.Add(new XElement("date", $"{DateTime.Now.Year}"));

                foreach (XElement feat in features)
                {
                    string type = feat.Name.LocalName;
                   
                    long type_id = context.FeatureTypes
                        .FirstOrDefault(ft => ft.TypeName == type)
                        .Id;

                    string value = feat.Value;
                    Feature new_feat = null;
                    if (context.Features != null && context.Features.Count() > 0)
                        new_feat = context.Features.SingleOrDefault(f => f.Type == type_id && f.Value.ToLower() == value.ToLower());
                    if (new_feat == null)
                        new_feat = new_features.SingleOrDefault(f => f.Type == type_id && f.Value.ToLower() == value.ToLower());
                    if (new_feat == null)
                    {
                        new_feat = new Feature()
                        {
                            Id = feat_id,
                            RelType = context.FeatureTypes.Single(ft => ft.TypeName == type),
                            Type = type_id,
                            Value = value
                        };
                        new_features.Add(new_feat);
                        feat_id++;
                    }

                    ProgrammesFeature pf_value_found = null;

                    if (context.ProgrammesFeatures != null && context.ProgrammesFeatures.Any())
                        pf_value_found = context.ProgrammesFeatures.SingleOrDefault(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == new_prog.Id);
                    if (new_prog.ProgrammesFeatures != null && new_prog.ProgrammesFeatures.Any())
                        pf_value_found = new_prog.ProgrammesFeatures.SingleOrDefault(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == new_prog.Id);
                    if (pf_value_found == null)
                    {
                        new_prog.ProgrammesFeatures.Add(new ProgrammesFeature()
                        {
                            FeatureId = new_feat.Id,
                            ProgrammeId = new_prog.Id,
                        });
                    }
                }
            }
            context.Features.AddRange(new_features);
            context.Programmes.AddRange(new_programmes);
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - saving programmes");
            context.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - programmes done");

            Cleanup();
            FinishGuideUpdate();
        }

        private void Cleanup()
        {
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - notifications");
            var old_notifications = context.Notifications
                .Include(n => n.RelEmission)
                .Where(n => n.RelEmission.Stop < DateTime.Today);
            context.Notifications.RemoveRange(old_notifications);
            context.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - emissions");
            context.Emissions.RemoveRange(
                context.Emissions.Where(em => em.Stop < DateTime.Today)
            );
            context.SaveChanges();

            List<long> emptyProgrammeIds = context.Programmes
                .Include(prog => prog.Emissions)
                .Include(prog => prog.Ratings)
                .Where(prog => prog.Emissions.Count == 0 && prog.Ratings.Count == 0)
                .Select(prog => prog.Id)
                .ToList();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - {emptyProgrammeIds.Count} empty");
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - descriptions");
            context.Descriptions
                .RemoveRange(context.Descriptions.Where(desc => emptyProgrammeIds.Contains(desc.ProgrammeId)));
            context.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - programme-featues");
            context.ProgrammesFeatures
                .RemoveRange(context.ProgrammesFeatures.Where(pf => emptyProgrammeIds.Contains(pf.ProgrammeId)));
            context.SaveChanges();

            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - programmes");
            context.Programmes
                .RemoveRange(context.Programmes.Where(prog => emptyProgrammeIds.Contains(prog.Id)));
            context.SaveChanges();
        }

        private void FinishGuideUpdate()
        {
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - cleanup - finished");
            GuideUpdate last = context.GuideUpdates.Single(gu => gu.Id == lastGuideUpdateId);
            last.Finished = DateTime.Now;
            context.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] GuideUpdate processing - finish");
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

        private long GetNewId<T> (DbSet<T> dbset) where T : class, IEntityWithID
        {
            long id = 0;
            if (dbset != null && dbset.Count() > 0)
            {
                var ids = dbset.Select(entity => entity.Id).ToList();
                id = ids.Max() + 1;
            }
            return id;
        }


    }
}
