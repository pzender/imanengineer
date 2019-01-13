using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class Emission
    {
        public long Id { get; set; }
        public string Start { get; set; }
        public string Stop { get; set; }
        public long ProgrammeId { get; set; }
        public long ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual Programme Programme { get; set; }
    }
}
