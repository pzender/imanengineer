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
        private readonly TvAppContext db = new TvAppContext();
        public IEnumerable<Programme> GetFilteredProgrammes(Filter filter)
        {
            IEnumerable<Programme> programmes = db.Programmes
                .Include(prog => prog.Emissions)
                    .ThenInclude(em => em.ChannelEmitted)
                .Include(prog => prog.Descriptions)
                .Include(prog => prog.ProgrammesFeatures)
                    .ThenInclude(fe => fe.RelFeature)
                        .ThenInclude(f => f.RelType);

            if(filter.From.HasValue && filter.To.HasValue)
                programmes = programmes.Where(prog => EmissionsBetween(prog, filter.From.Value, filter.To.Value).Any());

            if (filter.Date.HasValue)
                programmes = programmes.Where(prog => EmittedOn(prog, filter.Date.Value));
            

            return programmes;
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
