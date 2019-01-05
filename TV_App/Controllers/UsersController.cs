using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        // GET: api/Users/Przemek
        [HttpGet("{name}", Name = "Get")]
        public User Get(string name)
        {
            return DbContext.User.Where(u => u.Login == name).SingleOrDefault();
        }

        // POST: api/Users/Przemek/Ratings
        [HttpPost("{name}/Ratings")]
        public EFModels.Rating PostRating(string name, [FromBody] Rating body)
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

        // POST: api/Users
        [HttpPost]
        public User Post()
        {
            string name = "";
            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                name = sr.ReadToEnd();
            }
            
            User user = DbContext.User.Where(u => u.Login == name).SingleOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Login = name
                };
                DbContext.User.Add(user);
                DbContext.SaveChanges();
            }
            return user;
        }



        // PUT: api/Users/Przemek
        [HttpPut("{username}")]
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
