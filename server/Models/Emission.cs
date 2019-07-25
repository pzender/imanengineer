using System;
using System.Collections.Generic;

namespace TV_App.Models
{
    public partial class Emission
    {
        public long Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public long ProgrammeId { get; set; }
        public long ChannelId { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual Programme Programme { get; set; }
    }
}
