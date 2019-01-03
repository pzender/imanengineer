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

        public bool Between(TimeSpan from, TimeSpan to)
        {
            int hours = int.Parse(Start.Substring(11, 2));
            int minutes = int.Parse(Start.Substring(14, 2));
            TimeSpan start_ts = new TimeSpan(hours, minutes, 0);

            if (from.TotalMinutes > 0 && start_ts.TotalMinutes < from.TotalMinutes)
                return false;
            else if (to.TotalMinutes > 0 && start_ts.TotalMinutes > to.TotalMinutes)
                return false;
            else return true;

        }

    }
}
