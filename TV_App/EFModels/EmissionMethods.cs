using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.EFModels
{
    public partial class Emission
    {
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

        public DateTime StartToDate()
        {
            return DateTime.ParseExact(Start, "dd.MM.yyyy HH:mm:ss", null);
        }
    }
}
