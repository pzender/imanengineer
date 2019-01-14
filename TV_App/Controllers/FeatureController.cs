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
        readonly testContext DbContext = new testContext();

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
