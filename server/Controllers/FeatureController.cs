using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.EFModels;
using TV_App.Responses;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        readonly TvAppContext DbContext = new TvAppContext();

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
            Feature feat = DbContext.Feature
                .Include(f => f.TypeNavigation)
                .SingleOrDefault(f => f.Id == id);

            return new FeatureResponse(feat);
        }

        // GET: api/Feature/5/Programmes
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetProgrammes(int id, [FromQuery] string username = null, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            IEnumerable<Programme> list = DbContext.Programme
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation);

            list = list.OrderBy(prog => prog.Emission.First().StartToDate());

            if (username != null)
            {
                User user = DbContext.User
                    .Include(u => u.Rating)
                    .ThenInclude(r => r.Programme)
                    .ThenInclude(p => p.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                    .ThenInclude(f => f.TypeNavigation)
                    .Single(u => u.Login == username);

                IEnumerable<Programme> reco = user.GetRecommendations(list);
                if(reco.Count() > 0) 
                    list = user.GetRecommendations(list);

            }

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
                    .Where(prog => prog.EmissionsBetween(from_ts, to_ts).Count() > 0);
            }

            //if (date != 0)
            //{
            //    DateTime desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;
            //    list = list.Where(prog => prog.EmittedOn(desiredDate));
            //}

            Request.HttpContext.Response.Headers.Add("X-Total-Count", list.Count().ToString());
            return list
                .Where(prog => prog.FeatureExample.Any(fe => fe.FeatureId == id))
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
