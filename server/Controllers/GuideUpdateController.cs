using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TV_App.Models;
using TV_App.Services;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideUpdateController : ControllerBase
    {
        private readonly TvAppContext context = new TvAppContext();
        private readonly GuideUpdateService service = new GuideUpdateService();
        // GET: api/GuideUpdate/Last
        [HttpGet("Last")]
        public GuideUpdateJson Get()
        {
            var last = context.GuideUpdates
                .Where(gu => gu.Finished.HasValue)
                .OrderBy(gu => gu.Finished.Value)
                .Last();
            return new GuideUpdateJson()
            {
                Id = last.Id,
                Started = last.Started,
                Finished = last.Finished.GetValueOrDefault(),
                Source = last.Source
            };
        }

        // POST: api/GuideUpdate
        [HttpPost]
        public async Task PostAsync()
        {
            LogService.Log($"start");
            string body = "";
            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await sr.ReadToEndAsync();
            }

            if (body != "")
            {
                service.ParseAll(XDocument.Parse(body));
            }
        }
        public class GuideUpdateJson
        {
            public long Id { get; set; }
            public DateTime Started { get; set; }
            public DateTime Finished { get; set; }
            public string Source { get; set; }
        }

    }
}
