using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;
using Microsoft.EntityFrameworkCore;
using TV_App.Responses;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        readonly TvAppContext DbContext = new TvAppContext();

        private int TermMatches(Programme prog, IEnumerable<string> search_terms)
        {
            IEnumerable<string> prog_terms = prog
                .FeatureExample.Select(fe => fe.Feature.Value)
                .Concat(prog.Title.Split(' '));

            int count = prog_terms
                .Where(prog_term => search_terms
                    .Any(search_term => prog_term.ToLower().Contains(search_term.ToLower())))
                .Count();

            return count;
        }

        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<ProgrammeResponse> Get([FromQuery] string search = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0")
        {
            var list = DbContext.Programme
                .Include(prog => prog.Description)
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation)
                .AsEnumerable();

            if(search != "")
            {
                IEnumerable<string> search_terms = search.Split(' ');
                IDictionary<Programme, int> list_matches = list
                    .ToDictionary(prog => prog, prog => TermMatches(prog, search_terms))
                    .OrderByDescending(prog_match => prog_match.Value)
                    .Where(prog_match => prog_match.Value > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value);
                list = list_matches
                    .Select(prog_match => prog_match.Key);
            }

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

            IEnumerable<ProgrammeResponse> preparedResponse = list.Select(prog => new ProgrammeResponse(prog));
            Request.HttpContext.Response.Headers.Add("X-Total-Count", preparedResponse.Count().ToString());
            return preparedResponse;
        }


        // GET: api/Programmes/5/Similar
        [HttpGet("{id}/Similar")]
        public IEnumerable<ProgrammeResponse> GetSimilar(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Programme programme = DbContext.Programme
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(f => f.TypeNavigation)
                .Single(prog => prog.Id == id);

            double avg_w_act = DbContext.User.Average(u => u.WeightActor);
            double avg_w_cat = DbContext.User.Average(u => u.WeightCategory);
            double avg_w_keyw = DbContext.User.Average(u => u.WeightKeyword);
            double avg_w_dir = DbContext.User.Average(u => u.WeightDirector);
            double avg_w_country = DbContext.User.Average(u => u.WeightCountry);
            double avg_w_year = DbContext.User.Average(u => u.WeightYear);

            IEnumerable<Programme> list = programme.GetSimilar(avg_w_act, avg_w_cat, avg_w_keyw, avg_w_dir, avg_w_country, avg_w_year);

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
            Request.HttpContext.Response.Headers.Add("X-Total-Count", list.Count().ToString());

            return list
                .Select(prog => new ProgrammeResponse(prog));
        }


        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public ProgrammeResponse Get(int id)
        {
            Programme programme = DbContext.Programme
                .Include(prog => prog.Description)
                .Include(prog => prog.Emission)
                    .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                        .ThenInclude(f => f.TypeNavigation)
                .SingleOrDefault(prog => prog.Id == id);
            return new ProgrammeResponse(programme);
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
