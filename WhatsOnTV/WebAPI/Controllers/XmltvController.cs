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
using System.IO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmltvController : ControllerBase
    {
        IRepository<GuideUpdate> GuideUpdateRepository;
        IRepository<Channel> ChannelRepository;
        XDocument document = null;
        // POST: api/Xmltv
        [HttpPost]
        public ActionResult Post()
        {
            try
            {
                using (var reader = new StreamReader(Request.Body))
                    document = XDocument.Parse(reader.ReadToEnd());
            }
            catch(XmlException e)
            {
                return BadRequest(new { message = "Cannot create an XDocument", exception = e.StackTrace});
            }
            
            XMLParser parser = new XMLParser(ChannelRepository, GuideUpdateRepository);
            GuideUpdate result = parser.ParseAll(document);

            return Created("/api/Xmltv/", result);
        }

        [HttpGet]
        public IEnumerable<GuideUpdate> GetListOfUpdates()
        {
            return GuideUpdateRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IEnumerable<GuideUpdate> Get(int id)
        {
            throw new NotImplementedException();
        }

        public XmltvController(IRepository<Channel> channelRepository, IRepository<GuideUpdate> guideUpdateRepository)
        {
            ChannelRepository = channelRepository;
            GuideUpdateRepository = guideUpdateRepository;
        }
    }   
}
