using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Logic.Entities;

namespace Logic.FileReader
{
    public class XMLParserImpl : IXMLParser
    {
        public ICollection<Channel> ParsedChannels { get ; protected set ; }
        public ICollection<Description> ParsedDescriptions { get ; protected set ; }
        public ICollection<DescriptionSource> ParsedDescriptionSources { get ; protected set ; }
        public ICollection<Emission> ParsedEmissions { get ; protected set ; }
        public ICollection<Feature> ParsedFeatures { get ; protected set; }
        public ICollection<Programme> ParsedProgrammes { get ; protected set ; }

        public Dictionary<string, string> sourceByChannel = new Dictionary<string, string>();

        public GuideUpdate ParseAll(XDocument arg)
        {
            IEnumerable<XElement> elements = (
                from element in arg.Root.Elements()
                where element.Name == "channel"
                select element
                );
            foreach (XElement e in elements)
                ParseChannel(e);
            
            return new GuideUpdate()
            {
                Id = 1,
                Source = "FUCK.",
                UpdateDate = DateTime.Now
            };
            //throw new NotImplementedException();
        }

        public void ParseChannel(XElement arg)
        {
            if (arg.Attribute("id") == null) throw new ArgumentException("Channel id doesnt exist!");
            string channel_name = arg.Attribute("id").Value;
            Channel channel = (from Channel c 
                               in ParsedChannels
                               where c.name == channel_name
                               select c).SingleOrDefault();
            if (channel == default(Channel))
            {
                ParsedChannels.Add(new Channel()
                {
                    id = 3,
                    name = channel_name,
                    icon_url = arg.Element("icon") != null ? arg.Element("icon").Value : ""
                });

                if (!sourceByChannel.ContainsKey(channel_name))
                    sourceByChannel.Add(channel_name, arg.Element("url").Value);
            }
        }

        public void ParseCredits(XElement arg)
        {
            throw new NotImplementedException();
        }


        public void ParseDescriptionSource(XElement arg)
        {
            throw new NotImplementedException();
        }

        public void ParseFeature(XElement arg)
        {
            throw new NotImplementedException();
        }

        public void ParseProgramme(XElement arg)
        {
            throw new NotImplementedException();
        }

        public void ParseDescription(XElement arg, int programme_id, int source_id)
        {
            throw new NotImplementedException();
        }

        public void ParseEmission(XElement arg, int programme_id)
        {
            throw new NotImplementedException();
        }

        public XMLParserImpl()
        {
            ParsedChannels = new List<Channel>();
            ParsedDescriptions = new List<Description>();
            ParsedDescriptionSources = new List<DescriptionSource>();
            ParsedEmissions = new List<Emission>();
            ParsedFeatures = new List<Feature>();
            ParsedProgrammes = new List<Programme>();
        }
    }
}
