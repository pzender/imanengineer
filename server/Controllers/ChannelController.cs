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

        [HttpGet("Offers")]
        public ObjectResult GetOffers()
        {
            var offers = DbContext.Offers.Select(o => new
            {
                id = o.Id,
                name = o.Name
            });

            return StatusCode(200, offers);
        }

        [HttpGet]
        public IEnumerable<ChannelResponse> Get([FromQuery] long offer_id = 0)
        {
            IEnumerable<Channel> channels = DbContext.Channels
                .Include(ch => ch.OfferedChannels);
            if (offer_id != 0)
                channels = channels.Where(ch => ch.OfferedChannels.Any(oc => oc.OfferId == offer_id));
            return channels.Select(ch => new ChannelResponse(ch));
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
            Filter filter = Filter.Create(from, to, date, 0, id);
            IEnumerable<Programme> programmes = programmeService.GetFilteredProgrammes(filter);

            programmes = programmes
                .OrderBy(prog => prog.Emissions.First().Start);

            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes.Select(prog => new ProgrammeResponse(prog));

        }

        [HttpGet("{id}/Emissions")]
        public IEnumerable<EmissionResponse> GetEmissions(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Channel channel = DbContext.Channels
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
