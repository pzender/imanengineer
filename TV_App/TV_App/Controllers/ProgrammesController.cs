using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.EFModels;
using Microsoft.EntityFrameworkCore;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        readonly testContext DbContext = new testContext();
        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<Programme> Get([FromQuery] string channel = "", [FromQuery] string user = "")
        {
            var list = DbContext.Programme.AsQueryable();
            if (channel != "")
            {
                list = (
                    from prog in list
                    where prog.Emission.Any(e => e.Channel.Name == channel)
                    select prog
                );
            }

            return list
                .Include(p => p.Emission)
                    .ThenInclude(e => e.Channel)
                .Include(p => p.FeatureExample);
        }

        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public Programme Get(int id)
        {
            return DbContext.Programme.Where(p => p.Id == id).Single();
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
