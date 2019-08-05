using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TV_App.Models;
using TV_App.Responses;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly ILoggerFactory loggerFactory;

        public UsersController(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;

        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/Przemek
        [HttpGet("{name}", Name = "Get")]
        public ObjectResult Get(string name)
        {
            User user = DbContext.User.Where(u => u.Login == name).SingleOrDefault();
            return user != null ? StatusCode(200, user) : StatusCode(404, "No such user!");
        }

        // POST: api/Users/Przemek/Ratings
        [HttpPost("{name}/Ratings")]
        public async Task<ObjectResult> PostRating(string name, [FromBody] RatingJson body)
        {
            Programme ratedProgramme = DbContext.Programme.SingleOrDefault(prog => prog.Id == body.ProgrammeId);
            if (ratedProgramme != null)
            {
                Rating existing_rating = DbContext.Rating.SingleOrDefault(r => r.ProgrammeId == body.ProgrammeId && r.UserLogin == name);
                Rating new_rating = new Rating()
                {
                    ProgrammeId = body.ProgrammeId,
                    Programme = ratedProgramme,
                    RatingValue = body.RatingValue,
                    UserLogin = name

                };
                if (existing_rating == null)
                    DbContext.Rating.Add(new_rating);
                else
                    existing_rating.RatingValue = body.RatingValue;
                await DbContext.SaveChangesAsync();
                return StatusCode(200, new RatingJson() { ProgrammeId = new_rating.ProgrammeId, RatingValue = new_rating.RatingValue });
            }

            return StatusCode(404, "Program nie istnieje?");
            
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
            Request.HttpContext.Response.Headers.Add("X-Total-Count", list.Count().ToString());
            return list.Select(reco => new ProgrammeResponse(reco));
        }

        [HttpGet("{name}/Recommended")]
        public IEnumerable<ProgrammeResponse> GetRecommendations(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            var logger = loggerFactory.CreateLogger("UsersControllerGET");
            logger.LogInformation("UsersController.GetRecommendations() called");

            User user = DbContext.User
                .Include(u => u.Rating)
                .ThenInclude(r => r.Programme)
                .ThenInclude(p => p.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation)
                .Single(u => u.Login == name);

            if (user.GetPositivelyRated().Count() == 0) return new List<ProgrammeResponse>();

            IEnumerable<Programme> programmes = DbContext.Programme
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.Description)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation);

            logger.LogInformation($"{programmes.Count()} programmes initially");

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

                programmes = programmes
                    .Where(prog => prog.EmissionsBetween(from_ts, to_ts).Count() > 0);
            }

            if (date != 0)
            {
                DateTime desiredDate = DateTime.UnixEpoch.AddMilliseconds(date).Date;
                programmes = programmes.Where(prog => prog.EmittedOn(desiredDate));
            }

            logger.LogInformation($"date&time: down to {programmes.Count()}");

            var list = user.GetRecommendations(programmes);

            logger.LogInformation($"recommendations: down to {list.Count()}");
            Request.HttpContext.Response.Headers.Add("X-Total-Count", list.Count().ToString());
            return list.Select(reco => new ProgrammeResponse(reco));
        }



        // POST: api/Users
        [HttpPost]
        public async Task<ObjectResult> PostAsync()
        {
            string name = "";
            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                name = await sr.ReadToEndAsync();
            }
            
            User user = DbContext.User.Where(u => u.Login == name).SingleOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Login = name
                };
                DbContext.User.Add(user);
                await DbContext.SaveChangesAsync();
                return StatusCode(200, user);
            }
            else return StatusCode(409, "Username exists!");
            
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

        public class RatingJson
        {
            public long ProgrammeId { get; set; }
            public long RatingValue { get; set; }
        }
    }
}
