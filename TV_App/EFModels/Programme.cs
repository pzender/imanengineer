using System;
using System.Collections.Generic;
using System.Linq;

namespace TV_App.EFModels
{
    public partial class Programme
    {
        public Programme()
        {
            Description = new HashSet<Description>();
            Emission = new HashSet<Emission>();
            FeatureExample = new HashSet<FeatureExample>();
            Rating = new HashSet<Rating>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string SeqNumber { get; set; }
        public long? SeriesId { get; set; }

        public virtual Series Series { get; set; }
        public virtual ICollection<Description> Description { get; set; }
        public virtual ICollection<Emission> Emission { get; set; }
        public virtual ICollection<FeatureExample> FeatureExample { get; set; }
        public virtual ICollection<Rating> Rating { get; set; }

        public IEnumerable<Emission> EmissionsBetween(TimeSpan from, TimeSpan to)
        {
            return this.Emission.Where(e => e.Between(from, to));
        }
    }
}
