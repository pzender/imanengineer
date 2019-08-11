using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.Models;
using LemmaSharp;
using TV_App.DataLayer;
using System.Diagnostics;
using TV_App.Responses;
using Microsoft.AspNetCore.SignalR;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        TvAppContext DbContext = new TvAppContext();
        IHubContext<NotificationHub> hubContext;

        public TestController(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        // GET: api/Test
        [HttpGet]
        public IEnumerable<ProgrammeResponse> Get()
        {
            //Channel channel = DbContext.Channel
            //    .Include(ch => ch.Emission)
            //    .ThenInclude(em => em.Programme)
            //    .ThenInclude(pr => pr.FeatureExample)
            //    .ThenInclude(fe => fe.Feature)
            //    .ThenInclude(ft => ft.TypeNavigation)
            //    .Single(ch => ch.Id == 1);

            //IEnumerable<Emission> emissions = 
            //    channel.Emission.OrderBy(em => em.Start);

            //return emissions.Select(em => new ProgrammeResponse(em.Programme));
            return null;
        }

        [HttpPost]
        public string Notify()
        {
            try
            {
                hubContext.Clients.All.SendAsync("notify", "test notification");
                return "success!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
