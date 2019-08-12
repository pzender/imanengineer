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
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly RecommendationService recommendations = new RecommendationService();
        private readonly ProgrammeService programmes = new ProgrammeService();

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
            User user = DbContext.Users.Where(u => u.Login == name).FirstOrDefault();
            return user != null ? StatusCode(200, user) : StatusCode(404, "No such user!");
        }

        // POST: api/Users/Przemek/Ratings
        [HttpPost("{name}/Ratings")]
        public async Task<ObjectResult> PostRating(string name, [FromBody] RatingJson body)
        {
            Programme ratedProgramme = DbContext.Programmes.FirstOrDefault(prog => prog.Id == body.ProgrammeId);
            if (ratedProgramme != null)
            {
                Rating existing_rating = DbContext.Ratings.SingleOrDefault(r => r.ProgrammeId == body.ProgrammeId && r.UserLogin == name);
                Rating new_rating = new Rating()
                {
                    ProgrammeId = body.ProgrammeId,
                    RelProgramme = ratedProgramme,
                    RatingValue = body.RatingValue,
                    UserLogin = name

                };
                if (existing_rating == null)
                    DbContext.Ratings.Add(new_rating);
                else
                    existing_rating.RatingValue = body.RatingValue;
                await DbContext.SaveChangesAsync();
                return StatusCode(200, new RatingJson() { ProgrammeId = new_rating.ProgrammeId, RatingValue = new_rating.RatingValue });
            }

            return StatusCode(404, "Program nie istnieje");
            
        }

        // GET: api/Users/Przemek/Ratings
        [HttpGet("{name}/Ratings")]
        public IEnumerable<ProgrammeResponse> GetRatings(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            User user = DbContext.Users
                .Include(u => u.Ratings)
                .ThenInclude(r => r.RelProgramme)
                .ThenInclude(p => p.ProgrammesFeatures)
                .ThenInclude(fe => fe.RelFeature)
                .ThenInclude(f => f.RelType)
                .First(u => u.Login == name);

            var rated = recommendations.GetRated(user);

            Request.HttpContext.Response.Headers.Add("X-Total-Count", rated.Count().ToString());
            return rated.Select(reco => new ProgrammeResponse(reco));
        }

        [HttpGet("{name}/Recommended")]
        public IEnumerable<ProgrammeResponse> GetRecommendations(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            User user = DbContext.Users
                .Include(u => u.Ratings)
                .ThenInclude(r => r.RelProgramme)
                .ThenInclude(p => p.ProgrammesFeatures)
                .ThenInclude(fe => fe.RelFeature)
                .ThenInclude(f => f.RelType)
                .First(u => u.Login == name);
            if (recommendations.GetPositivelyRated(user).Count() == 0) return new List<ProgrammeResponse>();

            Filter filter = Filter.Create(from, to, date, 0);
            IEnumerable<Programme> programmes = this.programmes.GetFilteredProgrammes(filter);
            programmes = programmes.Except(recommendations.GetRated(user));
            var list = recommendations.GetRecommendations(user, programmes);

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
            
            User user = DbContext.Users.Where(u => u.Login == name).FirstOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Login = name
                };
                DbContext.Users.Add(user);
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
