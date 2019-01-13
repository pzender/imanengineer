using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.EFModels;
using LemmaSharp;
using TV_App.DataLayer;
using System.Diagnostics;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        testContext DbContext = new testContext();
        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            KeywordExtractor ke = new KeywordExtractor();

            Programme p = DbContext.Programme
                .Include(prog => prog.Description)
                .Include(prog => prog.FeatureExample)
                    .ThenInclude(fe => fe.Feature)
                .First(prog => prog.Id == 4189);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = ke.ProcessKeywords(p);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return result;
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
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
