using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideUpdateController : ControllerBase
    {
        // GET: api/GuideUpdate
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GuideUpdate/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GuideUpdate
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //XMLParser p = new XMLParser();
            string guide_content = value;
            //p.ParseAll(XDocument.Parse(guide_content));

        }
    }
}
