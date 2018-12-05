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
        public GuideUpdate currentUpdate = new GuideUpdate();

        public IRepository<Channel> ChannelRepo { get ; protected set ; }
        public IRepository<Description> DescriptionRepo { get ; protected set ; }
        public IRepository<Emission> EmissionRepo { get ; protected set ; }
        public IRepository<Feature> FeatureRepo { get ; protected set; }
        public IRepository<Programme> ProgrammeRepo { get ; protected set ; }
        public IRepository<GuideUpdate> GuideUpdateRepo { get; protected set; }
        public IRepository<Series> SeriesRepo { get; protected set; }

        public IRepository<FeatureExample> FeatureExampleRepo { get; protected set; }

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
                    id = (from Programme p in ProgrammeRepo.GetAll() select p.id).DefaultIfEmpty().Max() + 1,

                };

                if (arg.Element("icon") != null)
                    programme.icon_url = arg.Element("icon").Attribute("src").Value;
                if (arg.Element("episode-num") != null)
                {
                    programme.seq_number = arg.Element("episode-num").Value;
                    programme.series_id = SeriesRepo.Get(s => s.title == programme.title).Single().id;
                }

                programme = ProgrammeRepo.Insert(programme).Last();

                //other relevant info
                if (arg.Element("credits") != null)
                {
                    ParseCredits(arg.Element("credits"), programme.id);
                }
                
                if(arg.Element("category") != null)
                    foreach(XElement e in arg.Elements("category"))
                    {
                        ParseFeature(e, programme.id);
                    }
                if (arg.Element("date") != null)
                    ParseFeature(arg.Element("date"), programme.id);
                if (arg.Element("country") != null)
                    ParseFeature(arg.Element("country"), programme.id);
                if (arg.Element("desc") != null)
                    ParseDescription(arg.Element("desc"), programme.id, currentUpdate.id);

                if (currentUpdate.source.Contains("filmweb") && arg.Element("rating") != null)
                {
                    FeatureRepo.Insert(new Feature()
                    {
                        type = "Filmweb rating",
                        value = arg.Element("rating").Value
                    });
                }

            }

        }

        public void ParseFeature(XElement arg, int programme_id)
        {
            Feature feature = (
                    from Feature f in FeatureRepo.Get(feat => feat.value == arg.Value) select f).SingleOrDefault();
            if (feature == default(Feature))
                feature = FeatureRepo.Insert(new Feature() { type = arg.Name.LocalName, value = arg.Value }).Last();

            FeatureExampleRepo.Insert(new FeatureExample() { t1_id = programme_id, t2_id = feature.id });
            //connections.Add(feature.id, programme_id)
        }

        public void ParseCredits(XElement arg, int programme_id)
        {
            foreach(XElement credit in arg.Elements())
            {
                ParseFeature(credit, programme_id);
            }
        }



        public void ParseDescription(XElement arg, int programme_id, int source_id)
        {
            Description description = (
                from Description d in DescriptionRepo.Get(desc => desc.programme_id == programme_id && desc.guideupdate_id == source_id) select d).SingleOrDefault();
            if (description == default(Description))
            {
                Description desc = new Description()
                {
                    //id = lastId + 1,
                    content = arg.Value,
                    guideupdate_id = source_id,
                    programme_id = programme_id

                };

                DescriptionRepo.Insert(desc);
            }
            //throw new NotImplementedException();
        }


        public XMLParser(
            IRepository<Channel> channelRepository = null, 
            IRepository<GuideUpdate> guideUpdateRepository = null,
            IRepository<Programme> programmeRepository = null,
            IRepository<Feature> featureRepository = null,
            IRepository<Description> descriptionRepository = null,
            IRepository<FeatureExample> featureExampleRepository = null
        )
        {
            ChannelRepo = channelRepository;
            GuideUpdateRepo = guideUpdateRepository;
            ProgrammeRepo = programmeRepository;
            FeatureRepo = featureRepository;
            DescriptionRepo = descriptionRepository;
            FeatureExampleRepo = featureExampleRepository;
        }
    }
}
