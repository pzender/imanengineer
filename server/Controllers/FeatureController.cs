using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.Models;
using TV_App.Responses;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly RecommendationService recommendations = new RecommendationService();
        private readonly ProgrammeService programmes = new ProgrammeService();

        // GET: api/Feature
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Feature/5
        [HttpGet("{id}")]
        public FeatureResponse Get(int id)
        {
            Feature feat = DbContext.Features
                .Include(f => f.RelType)
                .FirstOrDefault(f => f.Id == id);

            return new FeatureResponse(feat);
        }

        // GET: api/Feature/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id, [FromQuery] string username = null, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Filter filter = Filter.Create(from, to, date, 0);
            IEnumerable<Programme> list = programmes.GetFilteredProgrammes(filter);
            list = list.OrderBy(prog => prog.Emissions.First().Start);
            if (username != null)
            {
                User user = DbContext.Users
                    .Include(u => u.Ratings)
                    .ThenInclude(r => r.RelProgramme)
                    .ThenInclude(p => p.ProgrammesFeatures)
                    .ThenInclude(fe => fe.RelFeature)
                    .ThenInclude(f => f.RelType)
                    .First(u => u.Login == username);

                if (recommendations.GetPositivelyRated(user).Count() > 0)
                {
                    list = recommendations.GetRecommendations(user, list);
                }
            }

            Request.HttpContext.Response.Headers.Add("X-Total-Count", list.Count().ToString());
            return list
                .Where(prog => prog.ProgrammesFeatures.Any(fe => fe.FeatureId == id))
                .Select(prog => new ProgrammeResponse(prog));

        }


        // POST: api/Feature
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Feature/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
