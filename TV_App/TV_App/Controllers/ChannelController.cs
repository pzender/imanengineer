using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Entities;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using System.Xml.Linq;
using System.IO;
using TV_App.EFModels;

namespace Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        readonly testContext DbContext = new testContext();

        [HttpGet]
        public IEnumerable<Channel> Get()
        {
            return DbContext.Channel;
        }

        //// GET: api/GuideUpdate/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
            
        //    return 
        //}

    }
}
