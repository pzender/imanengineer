using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class GuideUpdate
    {
        public GuideUpdate()
        {
            Description = new HashSet<Description>();
        }

        public long Id { get; set; }
        public string Source { get; set; }
        public string Posted { get; set; }

        public virtual ICollection<Description> Description { get; set; }
    }
}
