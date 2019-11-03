using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TV_App.Models;
using Microsoft.AspNetCore.SignalR;
using TV_App.Services;
using WebPush;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TvAppContext db = new TvAppContext();
        private readonly ProgrammeService service = new ProgrammeService();
        private readonly PushService push = new PushService();

        // GET: api/Test
        [HttpGet]
        public void Get()
        {
            
        }

        //POST: api/Test
        [HttpPost("{name}")]
        public void Notify(string name)
        {
            push.Notify(name, "test", "test");
        }
    }
}
