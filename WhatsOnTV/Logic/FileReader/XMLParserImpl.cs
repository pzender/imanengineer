using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Logic.Entities;
using Logic.Database;

namespace Logic.FileReader
{
    public class XMLParser
    {
        private GuideUpdate currentUpdate = new GuideUpdate();

        public IRepository<Channel> ChannelRepo { get ; protected set ; }
        public IRepository<Description> DescriptionRepo { get ; protected set ; }
        public IRepository<Emission> EmissionRepo { get ; protected set ; }
        public IRepository<Feature> FeatureRepo { get ; protected set; }
        public IRepository<Programme> ProgrammeRepo { get ; protected set ; }
        public IRepository<GuideUpdate> GuideUpdateRepo { get; protected set; }
        public IRepository<Series> SeriesRepo { get; protected set; }

        public Dictionary<string, string> sourceByChannel = new Dictionary<string, string>();


        public GuideUpdate ParseAll(XDocument arg)
        {
            currentUpdate = GuideUpdateRepo.Insert(new GuideUpdate(){
                posted = DateTime.Now,
                source = (
                    from element in arg.Root.Elements()
                    where element.Name == "channel"
                    select (
                        from child in element.Elements()
                        where child.Name == "url"
                        select child.Value
                        )
                    ).First().First()
            }).Last();

            foreach (XElement e in 
                from element in arg.Root.Elements() where element.Name == "channel" select element)
                ParseChannel(e);

            foreach (XElement e in
                from element in arg.Root.Elements() where element.Name == "programme" select element)
                ParseProgramme(e);

            


            return currentUpdate;
        }

        public void ParseChannel(XElement arg)
        {
            if (arg.Attribute("id") == null) throw new ArgumentException("Channel id doesnt exist!");
            string channel_name = arg.Attribute("id").Value;
            Channel channel = (from Channel c 
                               in ChannelRepo.GetAll()
                               where c.name == channel_name
                               select c).SingleOrDefault();
            if (channel == default(Channel))
            {
                ChannelRepo.Insert(new Channel()
                {
                    name = channel_name,
                    icon_url = arg.Element("icon") != null ? arg.Element("icon").Value : ""
                });

            }
        }

        public void ParseProgramme(XElement arg)
        {
            if (arg.Attribute("start") == null || arg.Attribute("stop") == null || arg.Attribute("channel") == null)
            {
                throw new ArgumentException("Incorrect arg");

            }
            string title = arg.Elements().Where(e => e.Name == "title" && e.Attribute("lang").Value == "pl").Select(e => e.Value).Single();
            Programme programme = (
                from Programme p in ProgrammeRepo.Get(prog => prog.title == title) select p).SingleOrDefault();
            if(programme == default(Programme))
            {
                programme = new Programme()
                {
                    title = title,
                    id = (from Programme p in ProgrammeRepo.GetAll() select p.id).Max() + 1,

                };
                if (arg.Element("icon") != null)
                    programme.icon_url = arg.Element("icon").Attribute("src").Value;
                if (arg.Element("episode-num") != null)
                {
                    programme.seq_number = arg.Element("icon").Value;
                    programme.series_id = SeriesRepo.Get(s => s.title == programme.title).Single().id;
                }

                if (arg.Element("credits") != null)
                {
                    ParseCredits(arg.Element("credits"));
                }
                
                if (currentUpdate.source.Contains("filmweb") && arg.Element("rating") != null)
                {
                    FeatureRepo.Insert(new Feature()
                    {
                        id = programme.id,
                        type = "Filmweb rating",
                        value = arg.Element("rating").Value
                    });
                }
                if(arg.Element("category") != null)
                    foreach(XElement e in arg.Elements("category"))
                    {
                        FeatureRepo.Insert(new Feature()
                        {
                            id = programme.id,
                            type = "category",
                            value = arg.Element("category").Value
                        });
                    }
                if (arg.Element("date") != null)
                    FeatureRepo.Insert(new Feature()
                    {
                        id = programme.id,
                        type = "year",
                        value = arg.Element("date").Value
                    });
                if (arg.Element("country") != null)
                    FeatureRepo.Insert(new Feature()
                    {
                        id = programme.id,
                        type = "country",
                        value = arg.Element("country").Value
                    });
                ProgrammeRepo.Insert(programme);
            }

        }


        public void ParseCredits(XElement arg)
        {
            throw new NotImplementedException();
        }

        public void ParseDescription(XElement arg, int programme_id, int source_id)
        {
            throw new NotImplementedException();
        }


        public XMLParser(IRepository<Channel> channelRepository, IRepository<GuideUpdate> guideUpdateRepository)
        {
            ChannelRepo = channelRepository;
            GuideUpdateRepo = guideUpdateRepository;

        }
    }
}
