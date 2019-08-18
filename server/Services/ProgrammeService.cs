using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;

namespace TV_App.Services
{
    public class ProgrammeService
    {
        readonly string[] IGNORED_TITLES = { "Zakonczenie programu" };
        private readonly TvAppContext db = new TvAppContext();
        public IEnumerable<Programme> GetFilteredProgrammes(Filter filter)
        {
            IEnumerable<Programme> programmes = db.Programmes
                .Include("Emissions.EmittedChannel")
                .Include("ProgrammesFeatures.RelFeature.RelType")
                ;

            if(filter.From.HasValue && filter.To.HasValue)
                programmes = programmes.Where(prog => EmissionsBetween(prog, filter.From.Value, filter.To.Value).Any());
            if (filter.Date.HasValue)
                programmes = programmes.Where(prog => EmittedOn(prog, filter.Date.Value));
            if (filter.ChannelId.HasValue)
                programmes = programmes.Where(prog => prog.Emissions.Any(em => em.ChannelId == filter.ChannelId));

            if (filter.OfferId != 0)
            {
                var channel_ids = db.OfferedChannels
                    .Where(oc => oc.OfferId == filter.OfferId)
                    .Select(oc => oc.ChannelId)
                    .ToList();
                programmes = programmes.Where(prog => prog.Emissions.Any(em => channel_ids.Contains(em.ChannelId)));
            }

            return programmes.Where(prog => !IGNORED_TITLES.Any(title => title == prog.Title));
        }

        private IEnumerable<Emission> EmissionsBetween(Programme prog, TimeSpan from, TimeSpan to)
        {
            return prog.Emissions.Where(e => EmissionBetween(e, from, to));
        }

        private bool EmittedOn(Programme prog, DateTime date)
        {
            return prog.Emissions.Any(em => em.Start.ToShortDateString() == date.ToShortDateString());
        }

        public bool EmissionBetween(Emission e, TimeSpan from, TimeSpan to)
        {
            int hours = e.Start.Hour;
            int minutes = e.Start.Minute;
            TimeSpan start_ts = new TimeSpan(hours, minutes, 0);

            if (from.TotalMinutes > 0 && start_ts.TotalMinutes < from.TotalMinutes)
                return false;
            else if (to.TotalMinutes > 0 && start_ts.TotalMinutes > to.TotalMinutes)
                return false;
            else return true;

        }

    }
}
