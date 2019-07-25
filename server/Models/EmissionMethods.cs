using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public partial class Emission
    {
        public bool Between(TimeSpan from, TimeSpan to)
        {
            int hours = Start.Hour;
            int minutes = Start.Minute;
            TimeSpan start_ts = new TimeSpan(hours, minutes, 0);

            if (from.TotalMinutes > 0 && start_ts.TotalMinutes < from.TotalMinutes)
                return false;
            else if (to.TotalMinutes > 0 && start_ts.TotalMinutes > to.TotalMinutes)
                return false;
            else return true;

        }
    }
}
