using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Services
{
    [NotMapped]
    public class Filter
    {
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
        public DateTime? Date { get; set; }
        public long? OfferId { get; set; }

        public static Filter Create(string from, string to, long date, long offer_id)
        {
            TimeSpan? from_ts = null;
            if (from != "0:0")
                from_ts = new TimeSpan(int.Parse(from.Split(':')[0]), int.Parse(from.Split(':')[1]), 0);
            TimeSpan? to_ts = null;
            if (to != "0:0")
                to_ts = new TimeSpan(int.Parse(from.Split(':')[0]), int.Parse(from.Split(':')[1]), 0);
            DateTime? desiredDate = null;
            if (date != 0)
                desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;

            return new Filter()
            {
                OfferId = offer_id,
                From = from_ts,
                To = to_ts,
                Date = desiredDate
            };
        }
    }
}
