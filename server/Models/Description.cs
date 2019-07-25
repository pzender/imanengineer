using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class Description
    {
        public long Id { get; set; }
        public long IdProgramme { get; set; }
        public string Content { get; set; }
        public long? GuideUpdateId { get; set; }

        public virtual GuideUpdate GuideUpdate { get; set; }
        public virtual Programme IdProgrammeNavigation { get; set; }
    }
}
