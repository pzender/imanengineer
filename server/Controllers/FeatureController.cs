using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.Models;
using TV_App.DataTransferObjects;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly ProgrammeService programmeService = new ProgrammeService();
        private readonly ChannelService channelService = new ChannelService();

        // GET: api/Feature
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Feature/5
        [HttpGet("{id}")]
        public FeatureDTO Get(int id)
        {
            Feature feat = DbContext.Features
                .Include(f => f.RelType)
                .FirstOrDefault(f => f.Id == id);

            return new FeatureDTO(feat);
        }

        // GET: api/Feature/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeDTO> GetProgrammes(int id, [FromQuery] string username = null, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            var channels = channelService.GetGroup(offer_id).Select(ch => ch.Id);
            Feature f = DbContext.Features.Single(f => f.Id == id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetWithFeature(f, filter, username);
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }
    }
}
