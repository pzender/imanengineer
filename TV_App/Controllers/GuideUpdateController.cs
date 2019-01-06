using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public void Post()
        {

            string body = "";
            using (StreamReader sr = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = sr.ReadToEnd();
            }
            if(body != "")
            {
                XMLParser parser = new XMLParser();
                parser.ParseAll(XDocument.Parse(body));
            }
        }
    }
}
