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
        private readonly GuideUpdateService updateService = new GuideUpdateService();

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
            Programme ratedProgramme = DbContext.Programmes.FirstOrDefault(prog => prog.Id == body.Id);
            if (ratedProgramme != null)
            {
                User user = DbContext.Users
                    .Include(u => u.Ratings)
                        .ThenInclude(r => r.RelProgramme)
                            .ThenInclude(prog => prog.ProgrammesFeatures)
                                .ThenInclude(pf => pf.RelFeature)
                    .Single(u => u.Login == name);

                Rating existing_rating = DbContext.Ratings.SingleOrDefault(r => r.ProgrammeId == body.Id && r.UserLogin == name);
                Rating new_rating = new Rating()
                {
                    ProgrammeId = body.Id,
                    RelProgramme = ratedProgramme,
                    RatingValue = body.RatingValue,
                    UserLogin = name

                };
                if (existing_rating == null)
                    DbContext.Ratings.Add(new_rating);
                else
                    existing_rating.RatingValue = body.RatingValue;
                user = recommendations.UpdateWeights(ratedProgramme, user, body.RatingValue);
                Console.WriteLine($"[{DateTime.Now}] Weights: {user.WeightCategory} {user.WeightActor} {user.WeightCountry} {user.WeightDirector} {user.WeightYear}");
                DbContext.SaveChanges();
                return StatusCode(200, new RatingJson() { Id = new_rating.ProgrammeId, RatingValue = new_rating.RatingValue });
            }

            return StatusCode(404, "Program nie istnieje");
        }

        // POST: api/Users/Przemek/Notifications
        [HttpPost("{name}/Notifications")]
        public async Task<ObjectResult> PostNotification(string name, [FromBody] RatingJson body)
        {
            Emission remindMeOf = DbContext.Emissions.FirstOrDefault(em => em.Id == body.Id);
            if (remindMeOf != null)
            {
                Notification new_reminder = new Notification()
                {
                    Id = updateService.GetNewId(DbContext.Notifications),
                    EmissionId = body.Id,
                    RelEmission = remindMeOf,
                    UserLogin = name
                };
                DbContext.Notifications.Add(new_reminder);
                DbContext.SaveChanges();
                return StatusCode(200, new RatingJson() { Id = new_reminder.EmissionId, RatingValue = 1 });
            }

            return StatusCode(404, "Program nie istnieje");
        }



        // GET: api/Users/Przemek/Ratings
        [HttpGet("{name}/Ratings")]
        public IEnumerable<ProgrammeDTO> GetRatings(string name, [FromQuery] string from = "00:00", [FromQuery] string to = "00:00", [FromQuery] long date = 0, long offer_id = 0, long theme_id = 0)
        {
            var channels = channelService.GetGroup(offer_id, theme_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetRatedBy(name);
            programmes = programmes.Where(prog => prog.Rating.HasValue).OrderBy(prog => prog.Rating).ToList();
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }

        // GET: api/Users/Przemek/Ratings
        [HttpGet("{name}/Notifications")]
        public IEnumerable<ProgrammeDTO> GetNotifications(string name, [FromQuery] string from = "00:00", [FromQuery] string to = "00:00", [FromQuery] long date = 0, long offer_id = 0, long theme_id = 0)
        {
            var channels = channelService.GetGroup(offer_id, theme_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
            IEnumerable<ProgrammeDTO> programmes = programmeService.GetNotificationsFor(name);
            //programmes = programmes.Where(prog => prog.Rating.HasValue).OrderBy(prog => prog.Rating).ToList();
            int count = programmes.Count();
            Response.Headers.Add("X-Total-Count", count.ToString());
            return programmes;
        }


        [HttpGet("{name}/Recommended")]
        public IEnumerable<ProgrammeDTO> GetRecommendations(string name, [FromQuery] string from = "00:00", [FromQuery] string to = "00:00", [FromQuery] long date = 0, [FromQuery] long offer_id = 0, long theme_id = 0)
        {
            var channels = channelService.GetGroup(offer_id, theme_id).Select(ch => ch.Id);
            Filter filter = Filter.Create(from, to, date, channels);
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

        // POST: api/Users/Przemek/Subscribe
        [HttpPost("{name}/Subscribtions")]
        public ObjectResult Subscribe(string name, [FromBody] PushSubscriptionJson subscription)
        {
            User user = DbContext.Users
                .Include(u => u.Subscriptions)
                .FirstOrDefault(u => u.Login == name);
            if (user != null)
            {
                Subscription sub = new Subscription()
                {
                    PushEndpoint = subscription.endpoint,
                    PushAuth = subscription.keys.auth,
                    PushP256dh = subscription.keys.p256dh,
                    UserLogin = user.Login,
                    RelUser = user
                };
                user.Subscriptions.Add(sub);
                DbContext.SaveChanges();
                subscription.id = sub.Id;
                return StatusCode(200, subscription);
            }
            else return StatusCode(404, new { error = "No such user!" });
        }

        [HttpDelete("{name}/Subscribtions/{id}")]
        public StatusCodeResult Unsubscribe(string name, int id)
        {
            Subscription sub = DbContext.Subscriptions.SingleOrDefault(s => s.Id == id);
            if (sub != null)
            {
                DbContext.Subscriptions.Remove(sub);
                DbContext.SaveChanges();
                return StatusCode(204);
            }
            else return StatusCode(404);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ObjectResult> CreateUser()
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
            public long Id { get; set; }
            public long RatingValue { get; set; }
        }

        public class PushSubscriptionJson
        {
            public long? id { get; set; }
            public string endpoint { get; set; }
            public string expirationTime { get; set; }
            public KeysJson keys { get; set; }
            public class KeysJson
            {
                public string p256dh { get; set; }
                public string auth { get; set; }
            }
        }

    }
}
