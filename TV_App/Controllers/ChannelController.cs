using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entities;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using System.Xml.Linq;
using System.IO;
using TV_App.EFModels;
using TV_App.Responses;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        readonly testContext DbContext = new testContext();

        [HttpGet]
        public IEnumerable<ChannelResponse> Get()
        {
            return DbContext.Channel.Select(ch => new ChannelResponse(ch));
        }

        // GET: api/Channel/5
        [HttpGet("{id}")]
        public ChannelResponse Get(int id)
        {

            return new ChannelResponse(DbContext.Channel.Where(ch => ch.Id == id).Single());
        }

        // GET: api/Channel/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0")
        {
            Channel channel = DbContext.Channel
                .Include(ch => ch.Emission)
                .ThenInclude(em => em.Programme)
                .ThenInclude(pr => pr.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(ft => ft.TypeNavigation)
                .Single(ch => ch.Id == 1);

            IEnumerable<Emission> list =
                channel.Emission.OrderBy(em => em.StartToDate());

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
                .Where(em => em.Programme.EmissionsBetween(from_ts, to_ts).Count() > 0);


            return list.Select(em => new ProgrammeResponse(em.Programme));

        }

    }
}
