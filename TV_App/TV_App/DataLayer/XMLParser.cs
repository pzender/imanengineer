using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using Entities;

namespace DataLayer
{
    public class XMLParser
    {
        public void ParseAll(XDocument doc)
        {
            //guideupdate
            QueryExecutor updateExecutor = new QueryExecutor();
            SQLBuilder<GuideUpdate> updateSql = new SQLBuilder<GuideUpdate>();
            updateExecutor.Query<GuideUpdate>(updateSql.BuildInsert(new List<GuideUpdate>(){ new GuideUpdate() {
                posted = DateTime.Now,
                source = doc.Root.Elements("channel").First().Element("url").Value
            }}));
            GuideUpdate last = updateExecutor.Query<GuideUpdate>(
                "select * from GuideUpdate where id = (select MAX(id) from GuideUpdate);").Single();

            //kanały
            SQLBuilder<Channel> channelSql = new SQLBuilder<Channel>();
            QueryExecutor channelExecutor = new QueryExecutor();
            List<Channel> channels = new List<Channel>();
            foreach(XElement e in doc.Root.Elements("channel"))
            {
                if (e.Attribute("id") == null) throw new ArgumentException("Channel id doesnt exist!");
                channels.Add(new Channel()
                {
                    name = e.Attribute("id").Value,
                    icon_url = e.Element("icon") != null ? e.Element("icon").Value : null
                });
                
            }
            if (channels.Count > 0)
            {
                channelExecutor.Query<Channel>(channelSql.BuildInsert(channels));
            }

            //programy
            QueryExecutor programmeExecutor = new QueryExecutor();
            List<Programme> programmes = new List<Programme>();
            SQLBuilder<Programme> programmeSql = new SQLBuilder<Programme>();

            foreach (XElement e in doc.Root.Elements("programme"))
            {
                string title = e.Elements("title").First().Value;
                programmes.Add(new Programme()
                {
                    title = title,
                    icon_url = (e.Element("icon") != null && e.Element("icon").Attribute("src") != null) ?
                        e.Element("icon").Attribute("src").Value : null,
                    seq_number = null,
                });
                
            }
            if(programmes.Count > 0)
            {
                programmeExecutor.Query<Programme>(programmeSql.BuildInsert(programmes));
            }

            //emisje, ficzery, opisy itepe
            QueryExecutor featureExecutor = new QueryExecutor();
            QueryExecutor exampleExecutor = new QueryExecutor();
            List<Feature> features = new List<Feature>();
            SQLBuilder<Feature> featureSql = new SQLBuilder<Feature>();

            QueryExecutor emissionExecutor = new QueryExecutor();
            List<Emission> emissions = new List<Emission>();
            string emissionQuery = "INSERT INTO Emission (start, stop, channel_id, programme_id) VALUES ";

            foreach (XElement e in doc.Root.Elements("programme"))
            {
                string title = e.Elements("title").First().Value;
                emissionQuery += "(";
                emissionQuery += $"'{ParseDateTime(e.Attribute("start").Value)}', ";
                emissionQuery += $"'{ParseDateTime(e.Attribute("stop").Value)}', ";
                emissionQuery += $"(SELECT id FROM Channel WHERE name like '{e.Attribute("channel").Value.Replace("'", "''")}'), ";
                emissionQuery += $"(SELECT id FROM Programme WHERE title like '{title.Replace("'", "''")}')";

                emissionQuery += "), ";


                if (e.Element("country") != null)
                    features.Add(new Feature()
                    {
                        type = "country",
                        value = e.Element("country").Value
                    });
                if (e.Element("date") != null)
                    features.Add(new Feature()
                    {
                        type = "date",
                        value = e.Element("date").Value
                    });

                if (e.Element("credits") != null && e.Element("credits").HasElements)
                {
                    features.AddRange(e.Element("credits").Elements().Select(
                        el => new Feature()
                        {
                            type = el.Name.LocalName,
                            value = el.Value
                        }
                        ));
                }

                if (e.Elements("category") != null)
                {
                    features.AddRange(e.Elements("category").Select(
                    el => new Feature()
                    {
                        type = el.Name.LocalName,
                        value = el.Value
                    }
                    ));
                }
                if (features.Count > 0)
                {
                    featureExecutor.Query<Feature>(featureSql.BuildInsert(features));
                    foreach (Feature f in features)
                    {
                        exampleExecutor.Query<FeatureExample>($"INSERT INTO FeatureExample VALUES (" +
                            $"(SELECT id FROM Feature WHERE type LIKE '{f.type.Replace("'", "''")}' AND value LIKE '{f.value.Replace("'", "''")}'), " +
                            $"(SELECT id FROM Programme WHERE title LIKE '{title.Replace("'", "''")}')" +
                            $")");
                    }
                }
                features.Clear();
            }
            emissionQuery += ";";
            if (!emissionQuery.EndsWith("VALUES ;"))
            {
                emissionQuery = emissionQuery.Replace(", ;", ";");
                emissionExecutor.Query<Emission>(emissionQuery);
            }

            //throw new NotImplementedException();
        }
        //01234567 89
        //20181017 03 25 00 +0200
        private DateTime ParseDateTime(string inp)
        {
            return new DateTime(
                year: int.Parse(inp.Substring(0,4)),
                month: int.Parse(inp.Substring(4,2)),
                day: int.Parse(inp.Substring(6,2)),
                hour: int.Parse(inp.Substring(8,2)),
                minute: int.Parse(inp.Substring(10,2)),
                second: int.Parse(inp.Substring(12,2))
            );
        }
    }
}
