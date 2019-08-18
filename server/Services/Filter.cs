using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;

namespace TV_App.Services
{
    [NotMapped]
    public class Filter
    {
        public TimeSpan? From { get; set; }
        public TimeSpan? To { get; set; }
        public DateTime? Date { get; set; }
        public long? OfferId { get; set; }
        public long? ChannelId { get; set; }

        public static Filter Create(string from, string to, long date, long offer_id, long? channel_id = null)
        {
            TimeSpan? from_ts = null;
            if (from != "0:0" && from != "undefined")
                from_ts = new TimeSpan(int.Parse(from.Split(':')[0]), int.Parse(from.Split(':')[1]), 0);
            TimeSpan? to_ts = null;
            if (to != "0:0" && to != "undefined")
                to_ts = new TimeSpan(int.Parse(from.Split(':')[0]), int.Parse(from.Split(':')[1]), 0);
            DateTime? desiredDate = null;
            if (date != 0)
                desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;

            return new Filter()
            {
                OfferId = offer_id,
                From = from_ts,
                To = to_ts,
                Date = desiredDate,
                ChannelId = channel_id
            };
        }
    }
}
