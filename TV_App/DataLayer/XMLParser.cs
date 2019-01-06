using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using TV_App.EFModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class XMLParser
    {
        private readonly testContext DbContext = new testContext();

        public void ParseAll(XDocument doc)
        {

            IEnumerable<XElement> channels_in_xml = doc.Root.Elements("channel");

            //guideupdate
            GuideUpdate new_gu = new GuideUpdate()
            {
                Id = DbContext.GuideUpdate.Select(gu => gu.Id).Max() + 1,
                Posted = DateTime.Now.ToString(),
                Source = channels_in_xml.First().Element("url").Value
            };
            DbContext.GuideUpdate.Add(new_gu);

            DbContext.SaveChanges();

            //kanały
            foreach (XElement channel in channels_in_xml)
            {
                if (!DbContext.Channel.Where(ch => ch.Name == channel.Attribute("id").Value).Any())
                {
                    Channel new_channel = new Channel()
                    {
                        Id = DbContext.Channel.Select(ch => ch.Id).Max() + 1,
                        Name = channel.Attribute("id").Value,
                        IconUrl = channel.Element("icon")?.Attribute("src").Value,
                    };
                    DbContext.Channel.Add(new_channel);
                    DbContext.SaveChanges();

                }
            }

            //programy
            IEnumerable<XElement> programmes_in_xml = doc.Root.Elements("programme");
            foreach (XElement programme in programmes_in_xml)
            {
                Programme new_prog = DbContext.Programme.Where(prog => prog.Title == programme.Elements("title").First().Value).SingleOrDefault();
                if (new_prog == null)
                {
                    new_prog = new Programme()
                    {
                        Id = DbContext.Programme.Select(gu => gu.Id).Max() + 1,
                        Title = programme.Elements("title").First().Value,
                        IconUrl = programme.Element("icon")?.Attribute("src").Value
                    };
                    DbContext.Programme.Add(new_prog);
                    DbContext.SaveChanges();

                }
                Emission new_em = DbContext.Emission
                    .Where(e => DateTime.Parse(e.Start) == ParseDateTimeXml(programme.Attribute("start").Value) 
                             && DateTime.Parse(e.Stop) == ParseDateTimeXml(programme.Attribute("stop").Value))
                    .SingleOrDefault();
                if(new_em == null)
                {
                    new_em = new Emission()
                    {
                        Id = DbContext.Emission.Select(gu => gu.Id).Max() + 1,
                        Channel = DbContext.Channel.Where(ch => ch.Name == programme.Attribute("channel").Value).Single(),
                        Start = ParseDateTimeXml(programme.Attribute("start").Value).ToString(),
                        Stop = ParseDateTimeXml(programme.Attribute("stop").Value).ToString(),
                        Programme = new_prog
                    };
                    DbContext.Emission.Add(new_em);
                    DbContext.SaveChanges();

                }

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
