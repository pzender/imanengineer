using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class FeatureTypes
    {
        public FeatureTypes()
        {
            Feature = new HashSet<Feature>();
        }

        public long Id { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Feature> Feature { get; set; }
    }
}
