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

        // GET: api/GuideUpdate/5
        [HttpGet("{id}")]
        public ChannelResponse Get(int id)
        {

            return new ChannelResponse(DbContext.Channel.Where(ch => ch.Id == id).Single());
        }

        // GET: api/GuideUpdate/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id)
        {
            var list = DbContext.Programme
                .Include(prog => prog.Emission)
                    .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                        .ThenInclude(f => f.TypeNavigation)
                .AsEnumerable();

            return list
                .Where(prog => prog.Emission.Any(e => e.ChannelId == id))
                .Select(prog => new ProgrammeResponse(prog));

        }

    }
}
