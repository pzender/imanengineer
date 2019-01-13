using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using TV_App.EFModels;
using Microsoft.EntityFrameworkCore;
using LemmaSharp;

namespace TV_App.DataLayer
{
    public class XMLParser
    {
        private readonly testContext DbContext = new testContext();

        public void ParseAll(XDocument doc)
        {

            IEnumerable<XElement> channels_in_xml = doc.Root.Elements("channel");

            //guideupdate
            long new_id = DbContext.GuideUpdate.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
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

            //programy
            IEnumerable<XElement> programmes_in_xml = doc.Root.Elements("programme");
            new_id = DbContext.Programme.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            long em_id = DbContext.Emission.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            long desc_id = DbContext.Description.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            long feat_id = DbContext.Feature.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;
            foreach (XElement programme in programmes_in_xml)
            {
                Programme new_prog = DbContext.Programme.Where(prog => prog.Title == programme.Elements("title").First().Value).SingleOrDefault();
                if (new_prog == null)
                {
                    new_prog = new Programme()
                    {
                        Id = new_id,
                        Title = programme.Elements("title").First().Value,
                        IconUrl = programme.Element("icon")?.Attribute("src").Value
                    };
                    DbContext.Programme.Add(new_prog);
                    DbContext.SaveChanges();
                    new_id++;
                }

                Emission new_em = DbContext.Emission
                    .Where(e => DateTime.Parse(e.Start) == ParseDateTimeXml(programme.Attribute("start").Value) 
                             && DateTime.Parse(e.Stop) == ParseDateTimeXml(programme.Attribute("stop").Value))
                    .SingleOrDefault();
                if(new_em == null)
                {
                    new_em = new Emission()
                    {
                        Id = em_id,
                        Channel = DbContext.Channel.Where(ch => ch.Name == programme.Attribute("channel").Value).Single(),
                        Start = ParseDateTimeXml(programme.Attribute("start").Value).ToString(),
                        Stop = ParseDateTimeXml(programme.Attribute("stop").Value).ToString(),
                        Programme = new_prog
                    };
                    DbContext.Emission.Add(new_em);
                    DbContext.SaveChanges();
                    em_id++;

                }

                Description new_desc = DbContext.Description
                    .Include(desc => desc.IdProgrammeNavigation)
                    .Include(desc => desc.SourceNavigation)
                    .Where(desc => desc.IdProgrammeNavigation == new_prog)
                    .SingleOrDefault();
                if(new_desc == null)
                {
                    new_desc = new Description()
                    {
                        Id = desc_id,
                        IdProgrammeNavigation = new_prog,
                        SourceNavigation = new_gu,
                        Content = programme.Element("desc")?.Value ?? ""
                    };
                    DbContext.Description.Add(new_desc);
                    DbContext.SaveChanges();
                    desc_id++;
                }

                string[] feat_names = { "date", "category", "country" };
                List<XElement> features = programme.Elements().Where(elem => feat_names.Contains(elem.Name.LocalName)).ToList();
                if(programme.Element("credits") != null)
                    features.AddRange(programme.Element("credits").Elements());
                if (features.Where(el => el.Name.LocalName == "date").Count() == 0)
                    features.Add(new XElement("date", $"{DateTime.Now.Year-1}"));

                foreach(XElement feat in features)
                {
                    ILemmatizer lemmatizer = new LemmatizerPrebuiltCompact(LanguagePrebuilt.Polish);

                    string type = feat.Name.LocalName;
                    string value = feat.Value;
                    if (type == "category")
                        value = lemmatizer.Lemmatize(value);

                    Feature new_feat = DbContext.Feature
                        .Include(f => f.TypeNavigation)
                        .Where(f => f.TypeNavigation.TypeName == type && f.Value == value)
                        .SingleOrDefault();
                    if(new_feat == null)
                    {
                        new_feat = new Feature()
                        {
                            Id = feat_id,
                            TypeNavigation = DbContext.FeatureTypes.First(ft => ft.TypeName == type),
                            Value = value
                        };
                        DbContext.Feature.Add(new_feat);
                        DbContext.SaveChanges();
                        feat_id++;
                    }

                    FeatureExample new_fe = DbContext.FeatureExample
                        .Where(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == new_prog.Id)
                        .SingleOrDefault();
                    if(new_fe == default(FeatureExample))
                    {
                        new_fe = new FeatureExample()
                        {
                            FeatureId = new_feat.Id,
                            ProgrammeId = new_prog.Id,
                            Feature = new_feat,
                            Programme = new_prog
                        };
                        DbContext.FeatureExample.Add(new_fe);
                        DbContext.SaveChanges();
                    }
                    
                }

            }
            DbContext.SaveChanges();
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
