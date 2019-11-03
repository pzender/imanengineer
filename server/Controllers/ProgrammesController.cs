using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;
using Microsoft.EntityFrameworkCore;
using TV_App.DataTransferObjects;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        private readonly ProgrammeService programmeService = new ProgrammeService();
        private readonly ChannelService channelService = new ChannelService();
        private readonly RecommendationService recommendationService = new RecommendationService();
        private readonly TvAppContext db = new TvAppContext();


        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<ProgrammeDTO> Get([FromQuery] string username = "", [FromQuery] string search = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            var channels = channelService.GetGroup(offer_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetBySearchTerm(search, filter, username);
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }


        // GET: api/Programmes/5/Similar
        [HttpGet("{id}/Similar")]
        public IEnumerable<ProgrammeDTO> GetSimilar(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            
            var channels = channelService.GetGroup(offer_id).Select(ch => ch.Id);
            var target = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                    .ThenInclude(pf => pf.RelFeature)
                    .ThenInclude(feat => feat.RelType)
                .Include(prog => prog.Ratings)
                    .ThenInclude(r => r.RelUser)
                .Include(prog => prog.Emissions)
                    .ThenInclude(em => em.ChannelEmitted)
                .AsNoTracking()
                .Single(prog => prog.Id == id);
            Filter filter = Filter.Create(from, to, date, channels);

            var results = recommendationService.GetSimilar(target, DUMMY_USER);
            int count = results.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return results.Select(prog => new ProgrammeDTO(prog));

        }


        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public ProgrammeDTO Get(int id)
        {
            return programmeService.GetById(id);
        }

        private static readonly User DUMMY_USER = new User() { Login = "DUMMY", WeightActor = 0.3, WeightCategory = 0.3, WeightCountry = 0.1, WeightDirector = 0.1, WeightKeyword = 0.1, WeightYear = 0.1 };

    }
}
