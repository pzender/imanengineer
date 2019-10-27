using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.IO;
using TV_App.Models;
using TV_App.DataTransferObjects;
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
            var offers = DbContext.Offers
                .Where(o => o.GroupType == ChannelGroup.TYPE_OFFER)
                .Select(o => new {
                    id = o.Id,
                    name = o.Name
                });
            return StatusCode(200, offers);
        }

        [HttpGet("Themes")]
        public ObjectResult GetThemes()
        {
            var themes = DbContext.Offers
                .Where(o => o.GroupType == ChannelGroup.TYPE_THEME)
                .Select(o => new {
                    id = o.Id,
                    name = o.Name
                });
            return StatusCode(200, themes);
        }


        [HttpGet]
        public IEnumerable<ChannelDTO> Get([FromQuery] long offer_id = 0, [FromQuery] long theme_id = 0)
        {
            IEnumerable<Channel> channels = DbContext.Channels.Include(ch => ch.OfferedChannels);
            if (offer_id != 0)
                channels = channels.Where(ch => ch.OfferedChannels.Any(oc => oc.OfferId == offer_id));
            if (theme_id != 0)
                channels = channels.Where(ch => ch.OfferedChannels.Any(oc => oc.OfferId == theme_id));
            return channels.Select(ch => new ChannelDTO(ch));
        }

        // GET: api/Channel/5
        [HttpGet("{id}")]
        public ChannelDTO Get(int id)
        {

            return new ChannelDTO(DbContext.Channels.Where(ch => ch.Id == id).Single());
        }

        // GET: api/Channel/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeDTO> GetProgrammes(int id, [FromQuery] string username = null, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Filter filter = Filter.Create(from, to, date, new long[] { id });
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetFilteredProgrammes(filter, username);
            programmes = programmes.OrderBy(prog => prog.Emissions.First().Start);
            programmes = programmes.ToList();
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;

        }
    }
}
