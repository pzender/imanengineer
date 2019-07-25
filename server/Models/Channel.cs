using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class Channel
    {
        public Channel()
        {
            Emission = new HashSet<Emission>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }

        public virtual ICollection<Emission> Emission { get; set; }
    }
}
