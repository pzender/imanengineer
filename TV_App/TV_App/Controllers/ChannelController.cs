using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using DataLayer;
using System.Xml.Linq;
using System.IO;

namespace Controllers
{
    [Route("api/[controller]")]
    public class ChannelsController : Controller
    {
        [HttpGet]
        public IEnumerable<Channel> Get()
        {
            QueryExecutor q = new QueryExecutor();
            return q.Query<Channel>("SELECT * FROM Channel");
        }

        //// GET: api/GuideUpdate/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
            
        //    return 
        //}

    }
}
