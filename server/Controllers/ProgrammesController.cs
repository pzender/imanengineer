﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;
using Microsoft.EntityFrameworkCore;
using TV_App.Responses;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammesController : ControllerBase
    {
        private readonly TvAppContext DbContext = new TvAppContext();
        private readonly ProgrammeService programmes = new ProgrammeService();
        private readonly RecommendationService recommendations = new RecommendationService();
        private readonly SimilarityCalculator similarity = new SimilarityCalculator();

        private int TermMatches(Programme prog, IEnumerable<string> search_terms)
        {
            IEnumerable<string> prog_terms = prog
                .ProgrammesFeatures.Select(fe => fe.RelFeature.Value)
                .Concat(prog.Title.Split(' '));

            int count = prog_terms
                .Where(prog_term => search_terms
                    .Any(search_term => prog_term.ToLower().Contains(search_term.ToLower())))
                .Count();

            return count;
        }

        // GET: api/Programmes
        [HttpGet]
        public IEnumerable<ProgrammeResponse> Get([FromQuery] string username = "", [FromQuery] string search = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            Filter filter = Filter.Create(from, to, date, 0);
            IEnumerable<Programme> programmes = this.programmes.GetFilteredProgrammes(filter);


            if (search != "")
            {
                IEnumerable<string> search_terms = search.Split(' ');
                IDictionary<Programme, int> list_matches = programmes
                    .ToDictionary(prog => prog, prog => TermMatches(prog, search_terms))
                    .OrderByDescending(prog_match => prog_match.Value)
                    .Where(prog_match => prog_match.Value > 0)
                    .ToDictionary(kv => kv.Key, kv => kv.Value);
                programmes = list_matches
                    .Select(prog_match => prog_match.Key);
            }

            IEnumerable<ProgrammeResponse> preparedResponse = programmes.Select(prog => new ProgrammeResponse(prog));
            Request.HttpContext.Response.Headers.Add("X-Total-Count", preparedResponse.Count().ToString());
            return preparedResponse;
        }


        // GET: api/Programmes/5/Similar
        [HttpGet("{id}/Similar")]
        public IEnumerable<ProgrammeResponse> GetSimilar(int id, [FromQuery] string username = "", [FromQuery] string from = "0:0", [FromQuery] string to = "0:0", [FromQuery] long date = 0)
        {
            User user = DbContext.Users.Single(user => user.Login == username);

            Programme programme = DbContext.Programmes
                .Include(prog => prog.Emissions)
                .ThenInclude(em => em.ChannelEmitted)
                .Include(prog => prog.ProgrammesFeatures)
                .ThenInclude(fe => fe.RelFeature)
                .ThenInclude(f => f.Type)
                .Single(prog => prog.Id == id);

            Filter filter = Filter.Create(from, to, date, 0);
            IEnumerable<Programme> programmes = this.programmes.GetFilteredProgrammes(filter);
            programmes = programmes.OrderBy(prog => similarity.TotalSimilarity(user, prog, programme));

            Request.HttpContext.Response.Headers.Add("X-Total-Count", programmes.Count().ToString());

            return programmes
                .Select(prog => new ProgrammeResponse(prog));
        }


        // GET: api/Programmes/5
        [HttpGet("{id}")]
        public ProgrammeResponse Get(int id)
        {
            Programme programme = DbContext.Programmes
                .Include(prog => prog.Descriptions)
                .Include(prog => prog.Emissions)
                    .ThenInclude(em => em.ChannelEmitted)
                .Include(prog => prog.ProgrammesFeatures)
                    .ThenInclude(fe => fe.RelFeature)
                        .ThenInclude(f => f.Type)
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
