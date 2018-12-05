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
        IRepository<Channel> channelRepository = null;
        IRepository<GuideUpdate> guideUpdateRepository = null;
        IRepository<Programme> programmeRepository = null;
        IRepository<Feature> featureRepository = null;
        IRepository<Description> descriptionRepository = null;
        IRepository<FeatureExample> featureExampleRepository = null;
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
            
            XMLParser parser = new XMLParser(channelRepository, guideUpdateRepository, programmeRepository, featureRepository, descriptionRepository, featureExampleRepository);

            GuideUpdate result = parser.ParseAll(document);

            return Created("/api/Xmltv/", result);
        }

        [HttpGet]
        public IEnumerable<GuideUpdate> GetListOfUpdates()
        {
            return guideUpdateRepository.GetAll();
        }

        [HttpGet("{id}")]
        public IEnumerable<GuideUpdate> Get(int id)
        {
            throw new NotImplementedException();
        }

        public XmltvController(
            IRepository<Channel> channelRepository = null,
            IRepository<GuideUpdate> guideUpdateRepository = null,
            IRepository<Programme> programmeRepository = null,
            IRepository<Feature> featureRepository = null,
            IRepository<Description> descriptionRepository = null,
            IRepository<FeatureExample> featureExampleRepository = null
        )
        {
            this.channelRepository = channelRepository;
            this.guideUpdateRepository = guideUpdateRepository;
            this.programmeRepository = programmeRepository;
            this.featureRepository = featureRepository;
            this.descriptionRepository = descriptionRepository;
            this.featureExampleRepository = featureExampleRepository;
        }
    }   
}
