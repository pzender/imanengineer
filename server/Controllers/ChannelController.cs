using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entities;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using System.Xml.Linq;
using System.IO;
using TV_App.Models;
using TV_App.Responses;
using Microsoft.EntityFrameworkCore;
using TV_App.Services;

namespace Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly ProgrammeService programmeService = new ProgrammeService();

        [HttpGet]
        public IEnumerable<ChannelResponse> Get()
        {
            return DbContext.Channels.Select(ch => new ChannelResponse(ch));
        }

        // GET: api/Channel/5
        [HttpGet("{id}")]
        public ChannelResponse Get(int id)
        {

            return new ChannelResponse(DbContext.Channels.Where(ch => ch.Id == id).Single());
        }

        // GET: api/Channel/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Filter filter = Filter.Create(from, to, date, 0);
            IEnumerable<Programme> programmes = programmeService.GetFilteredProgrammes(filter);

            programmes = programmes
                .Where(prog => prog.Emissions.Any(em => em.ChannelId == id))
                .OrderBy(prog => prog.Emissions.First().Start);

            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes.Select(prog => new ProgrammeResponse(prog));

        }

        [HttpGet("{id}/Emissions")]
        public IEnumerable<EmissionResponse> GetEmissions(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Channel channel = DbContext.Channels
                .Include(ch => ch.Emissions)
                .ThenInclude(em => em.RelProgramme)
                .ThenInclude(pr => pr.ProgrammesFeatures)
                .ThenInclude(fe => fe.RelFeature)
                .ThenInclude(ft => ft.Type)
                .First(ch => ch.Id == id);

            IEnumerable<Emission> list =
                channel.Emissions.OrderBy(em => em.Start);

            if (from != to)
            {
                TimeSpan from_ts = new TimeSpan(
                    int.Parse(from.Split(':')[0]),
                    int.Parse(from.Split(':')[1]),
                    0
                );
                TimeSpan to_ts = new TimeSpan(
                    int.Parse(to.Split(':')[0]),
                    int.Parse(to.Split(':')[1]),
                    0
                );

                list = list
                    .Where(em => programmeService.EmissionBetween(em, from_ts, to_ts));
            }

            if (date != 0)
            {
                DateTime desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;
                list = list.Where(em => em.Start.Date == desiredDate);
            }

            return list.Select(em => new EmissionResponse(em));

        }

    }
}
