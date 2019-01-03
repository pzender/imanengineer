using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.EFModels;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly testContext DbContext = new testContext();

        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users/Przemek/
        [HttpPost("{name}")]
        public EFModels.Rating Post(string name, [FromBody] Rating body)
        {
            EFModels.Rating rating = new EFModels.Rating()
            {
                ProgrammeId = body.programme_id,
                RatingValue = body.rating_value,
                UserLogin = name
            };
            DbContext.Rating.Add(rating);
            DbContext.SaveChanges();
            return rating;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public struct Rating
        {
            public int programme_id;
            public int rating_value;
        }
    }
}
