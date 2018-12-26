using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class ProgrammeListElement
    {
        public string Title { get; set; }
        public IEnumerable<EmissionModel> Emissions { get; set; }
        public IEnumerable<FeatureModel> Features { get; set; }


        public ProgrammeListElement()
        {
            Emissions = new List<EmissionModel>();
            Features = new List<FeatureModel>();
        }
    }
}
