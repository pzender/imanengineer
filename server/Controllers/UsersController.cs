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
using TV_App.DataTransferObjects;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly RecommendationService recommendations = new RecommendationService();
        private readonly ProgrammeService programmeService = new ProgrammeService();
        private readonly ChannelService channelService = new ChannelService();

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
        public IEnumerable<ProgrammeDTO> GetRatings(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            var channels = channelService.GetOffer(offer_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetRatedBy(name);
            programmes = programmes.Where(prog => prog.Rating.HasValue).OrderBy(prog => prog.Rating).ToList();
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }

        // GET: api/Users/Przemek/Ratings
        [HttpGet("{name}/Notifications")]
        public IEnumerable<ProgrammeDTO> GetNotifications(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, long offer_id = 0)
        {
            var channels = channelService.GetOffer(offer_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetRatedBy(name);
            programmes = programmes.Where(prog => prog.Rating.HasValue).OrderBy(prog => prog.Rating).ToList();
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }


        [HttpGet("{name}/Recommended")]
        public IEnumerable<ProgrammeDTO> GetRecommendations(string name, [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0, [FromQuery] long offer_id = 0)
        {
            var channels = channelService.GetOffer(offer_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            // IEnumerable<ProgrammeDTO> programmes = programmeService.GetFilteredProgrammes(filter, name);
            User user = DbContext.Users
                .Include(u => u.Ratings)
                    .ThenInclude(r => r.RelProgramme)
                        .ThenInclude(prog => prog.ProgrammesFeatures)
                            .ThenInclude(pf => pf.RelFeature)
                .AsNoTracking()
                .Single(u => u.Login == name);
            var programmes = recommendations.GetRecommendations(user).Select(prog => new ProgrammeDTO(prog));
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
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

        public class RatingJson
        {
            public long ProgrammeId { get; set; }
            public long RatingValue { get; set; }
        }
    }
}
