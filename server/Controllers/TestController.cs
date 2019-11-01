using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.Models;
using LemmaSharp;
using System.Diagnostics;
using TV_App.DataTransferObjects;
using Microsoft.AspNetCore.SignalR;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        TvAppContext DbContext = new TvAppContext();
        ProgrammeService service = new ProgrammeService();
        IHubContext<NotificationHub> hubContext;

        public TestController(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        // GET: api/Test
        [HttpGet]
        public IHubClients Get()
        {
            return hubContext.Clients;
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
