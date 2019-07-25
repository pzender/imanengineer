using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class Series
    {
        public Series()
        {
            Programme = new HashSet<Programme>();
        }

        public long Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Programme> Programme { get; set; }
    }
}
