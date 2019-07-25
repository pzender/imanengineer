using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class FeatureExample
    {
        public long FeatureId { get; set; }
        public long ProgrammeId { get; set; }

        public virtual Feature Feature { get; set; }
        public virtual Programme Programme { get; set; }
    }
}
