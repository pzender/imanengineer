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
        private readonly RecommendationService recommendations = new RecommendationService();
        private readonly SimilarityCalculator similarity = new SimilarityCalculator();

        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<ProgrammeDTO> Get([FromQuery] string username = "", [FromQuery] string search = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            throw new NotImplementedException();
        }


        // GET: api/Programmes/5/Similar
        [HttpGet("{id}/Similar")]
        public IEnumerable<ProgrammeDTO> GetSimilar(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            var channels = channelService.GetOffer(offer_id).Select(ch => ch.Id);
            var target = programmeService.GetById(id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetFilteredProgrammes(filter);
            IDictionary<ProgrammeDTO, double> similarityRanking = programmes.ToDictionary(prog => prog, prog => similarity.TotalSimilarity(null, prog, target));

            var results = similarityRanking.OrderByDescending(item => item.Value).Select(item => item.Key).Where(prog => prog.Id != target.Id).Take(8);
            int count = results.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return results;

        }


        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public ProgrammeDTO Get(int id)
        {
            return programmeService.GetById(id);
        }
    }
}
