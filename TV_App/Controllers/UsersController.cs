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
using TV_App.Responses;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static readonly testContext DbContext = new testContext();

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

        // GET: api/Users/Przemek/Ratings
        [HttpGet("{name}/Ratings")]
        public IEnumerable<ProgrammeResponse> GetRatings(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            User user = DbContext.User
                .Include(u => u.Rating)
                .ThenInclude(r => r.Programme)
                .ThenInclude(p => p.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation)
                .Single(u => u.Login == name);

            var list = user.GetRated();
            return list.Select(reco => new ProgrammeResponse(reco));
        }

        [HttpGet("{name}/Recommended")]
        public IEnumerable<ProgrammeResponse> GetRecommendations(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            User user = DbContext.User
                .Include(u => u.Rating)
                .ThenInclude(r => r.Programme)
                .ThenInclude(p => p.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation)
                .Single(u => u.Login == name);

            IEnumerable<Programme> programmes = DbContext.Programme
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.Description)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation);

            var list = user.GetRecommendations(programmes);

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

            if (date != 0)
            {
                DateTime desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;
                list = list.Where(prog => prog.EmittedOn(desiredDate));
            }

            return list.Select(reco => new ProgrammeResponse(reco));
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
