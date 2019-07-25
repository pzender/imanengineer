using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class Feature
    {
        public Feature()
        {
            FeatureExample = new HashSet<FeatureExample>();
        }

        public long Id { get; set; }
        public long Type { get; set; }
        public string Value { get; set; }

        public virtual FeatureTypes TypeNavigation { get; set; }
        public virtual ICollection<FeatureExample> FeatureExample { get; set; }
    }
}
