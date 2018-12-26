using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        // GET: api/Programmes
        [HttpGet]
        public ProgrammeListModel Get([FromQuery] string channel = "", [FromQuery] string user = "")
        {
            ProgrammeListModel plm = new ProgrammeListModel();
            ProgrammeListBuilder builder = new ProgrammeListBuilder();
            if (channel != "")
                plm = builder.BuildForChannel(channel);
            else if (user != "")
                plm = builder.BuildForUser(user);
            
            return plm;
        }

        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
