﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TV_App.DataLayer;
using TV_App.EFModels;

namespace TV_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideUpdateController : ControllerBase
    {
        testContext DbContext = new testContext();
        KeywordExtractor keywordExtractor = new KeywordExtractor();

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
                long feat_id = DbContext.Feature.OrderByDescending(gu => gu.Id).Select(gu => gu.Id).FirstOrDefault() + 1;

                foreach (Programme p in DbContext.Programme.Include(prog => prog.Description))
                {
                    IEnumerable<string> keywords = keywordExtractor.ProcessKeywords(p);
                    foreach (string keyword in keywords)
                    {
                        string type = "keyword";

                        Feature new_feat = DbContext.Feature
                            .Include(f => f.TypeNavigation)
                            .Where(f => f.TypeNavigation.TypeName == type && f.Value == keyword)
                            .SingleOrDefault();
                        if (new_feat == null)
                        {
                            new_feat = new Feature()
                            {
                                Id = feat_id,
                                TypeNavigation = DbContext.FeatureTypes.First(ft => ft.TypeName == type),
                                Value = keyword
                            };
                            DbContext.Feature.Add(new_feat);
                            DbContext.SaveChanges();
                            feat_id++;
                        }

                        FeatureExample new_fe = DbContext.FeatureExample
                            .Where(fe => fe.FeatureId == new_feat.Id && fe.ProgrammeId == p.Id)
                            .SingleOrDefault();
                        if (new_fe == default(FeatureExample))
                        {
                            new_fe = new FeatureExample()
                            {
                                FeatureId = new_feat.Id,
                                ProgrammeId = p.Id,
                                Feature = new_feat,
                                Programme = p
                            };
                            DbContext.FeatureExample.Add(new_fe);
                            DbContext.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
