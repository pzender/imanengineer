using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Logic.FileReader;
using Logic.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.Database;
using System.Xml;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmltvController : ControllerBase
    {
        IRepository<GuideUpdate> GuideUpdateRepository;
        XDocument document = null;
        // POST: api/Xmltv
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            try
            {
                document = XDocument.Parse(value);
            }
            catch(XmlException e)
            {
                return BadRequest(new { message = "Cannot create an XDocument", exception = e.StackTrace});
            }
            
            IXMLParser parser = new XMLParserImpl();
            GuideUpdate result = parser.ParseAll(document);
            return Created("/api/Xmltv/"+result.Id, result);
        }

        [HttpGet]
        public IEnumerable<GuideUpdate> GetListOfUpdates()
        {
            return GuideUpdateRepository.GetAll();
        }

    }   
}
