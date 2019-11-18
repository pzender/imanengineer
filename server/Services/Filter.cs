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
        
        private TimeSpan? From { get; set; }
        private TimeSpan? To { get; set; }
        private DateTime? Date { get; set; }
        private IEnumerable<long> ChannelIds { get; set; }
        public static Filter Create(string from, string to, long date, IEnumerable<long> channel_ids)
        {
            TimeSpan? from_ts = null;
            if (from != null && from != "00:00" && from != "undefined")
                from_ts = new TimeSpan(int.Parse(from.Split(':')[0]), int.Parse(from.Split(':')[1]), 0);
            TimeSpan? to_ts = null;
            if (to != null && to != "00:00" && to != "undefined")
                to_ts = new TimeSpan(int.Parse(to.Split(':')[0]), int.Parse(to.Split(':')[1]), 0);
            DateTime? desiredDate = null;
            if (date != 0)
                desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;

            Filter f = new Filter()
            {
                From = from_ts,
                To = to_ts,
                Date = desiredDate,
                ChannelIds = channel_ids
            };
            LogService.Log($"{f.Log()}");
            return f;
        }

        public bool Apply(Emission em)
        {
            bool from_fits = !From.HasValue || em.Start.TimeOfDay > From.Value;
            bool to_fits = !To.HasValue || em.Stop.TimeOfDay < To.Value;
            bool date_fits = !Date.HasValue || em.Start.Date == Date.Value.Date;
            bool channels_fits = !ChannelIds.Any() || ChannelIds.Contains(em.ChannelId);

            return from_fits && to_fits && date_fits && channels_fits;
        }

        public string Log()
        {
            return $"Filter(From: {From}, To: {To}, Date: {Date}, ChannelIds: {string.Join(',', ChannelIds)})";
        }
    }
}
