using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class Description
    {
        public long Id { get; set; }
        public long IdProgramme { get; set; }
        public long? Source { get; set; }
        public string Content { get; set; }

        public virtual Programme IdProgrammeNavigation { get; set; }
        public virtual GuideUpdate SourceNavigation { get; set; }
    }
}
