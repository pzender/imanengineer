using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
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
            return new Filter()
            {
                OfferId = offer_id,
                From = new TimeSpan(
                    int.Parse(from.Split(':')[0]),
                    int.Parse(from.Split(':')[1]),
                    0
                ),
                To = new TimeSpan(
                    int.Parse(to.Split(':')[0]),
                    int.Parse(to.Split(':')[1]),
                    0
                ),
                Date = DateTime.UnixEpoch.AddMilliseconds(date).Date,
            };
        }
    }
}
