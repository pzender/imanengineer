using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.EFModels;
using Microsoft.EntityFrameworkCore;
using TV_App.Responses;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        readonly testContext DbContext = new testContext();
        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<ProgrammeResponse> Get([FromQuery] string channel = "", [FromQuery] string username = "")
        {
            var list = DbContext.Programme
                .Include(prog => prog.Emission)
                    .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                        .ThenInclude(f => f.TypeNavigation)
                .AsEnumerable();
;           
            if(username != "")
            {
                User user = DbContext.User
                    .Include(u => u.Rating)
                        .ThenInclude(rat => rat.Programme)
                            .ThenInclude(prog => prog.FeatureExample)
                                .ThenInclude(fe => fe.Feature)
                                    .ThenInclude(feat => feat.TypeNavigation)
                    .Where(u => u.Login == username)
                    .Single();
                RecommendationBuilder r = new RecommendationBuilder(user);
                list = r.Build(list);
            }

            if (channel != "")
            {
                list = (
                    from prog in list
                    where prog.Emission.Any(e => e.Channel.Name == channel)
                    select prog
                );
            }

            return list
                .Select(prog => new ProgrammeResponse(prog));//.ToList();
        }
        // GET: api/Programmes/5/similar
        [HttpGet("{id}/Programmes")]
        public IEnumerable<ProgrammeResponse> GetSimilar(int id)
        {
            var list = DbContext.Programme
                .Include(prog => prog.Emission)
                    .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                        .ThenInclude(f => f.TypeNavigation)
                .AsEnumerable();

            RecommendationBuilder r = new RecommendationBuilder(null);
            return r.Similar(list, list.Where(p => p.Id == id).Single())
                .Select(p => new ProgrammeResponse(p));
        }


        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public ProgrammeResponse Get(int id)
        {
            return new ProgrammeResponse(DbContext.Programme.Where(p => p.Id == id).Single());
        }

        // POST: api/Programmes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Programmes/5
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
