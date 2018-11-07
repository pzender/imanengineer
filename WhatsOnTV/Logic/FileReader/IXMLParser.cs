using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Logic.Entities;

namespace Logic.FileReader
{
    public interface IXMLParser
    {
        ICollection<Channel> ParsedChannels { get; }
        ICollection<Description> ParsedDescriptions { get;  }
        ICollection<DescriptionSource> ParsedDescriptionSources { get;  }
        ICollection<Emission> ParsedEmissions { get;  }
        ICollection<Feature> ParsedFeatures { get;  }
        ICollection<Programme> ParsedProgrammes { get;  }

        void ParseAll(XDocument arg); //void?
        void ParseChannel(XElement arg);
        void ParseDescription(XElement arg, int programme_id, int source_id);
        void ParseDescriptionSource(XElement arg);
        void ParseEmission(XElement arg, int programme_id);
        void ParseFeature(XElement arg);
        void ParseProgramme(XElement arg);
        void ParseCredits(XElement arg);
    }
}
