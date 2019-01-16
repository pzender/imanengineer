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
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id, [FromQuery] string username = "")
        {
            Channel channel = DbContext.Channel
                .Include(ch => ch.Emission)
                .ThenInclude(em => em.Programme)
                .ThenInclude(pr => pr.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(ft => ft.TypeNavigation)
                .Single(ch => ch.Id == 1);

            IEnumerable<Emission> emissions =
                channel.Emission.OrderBy(em => em.StartToDate());

            return emissions.Select(em => new ProgrammeResponse(em.Programme));

        }

    }
}
