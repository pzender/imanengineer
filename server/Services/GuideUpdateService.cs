using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TV_App.NewModels;

namespace TV_App.Services
{
    public class GuideUpdateService
    {
        private TvAppContext context;
        public GuideUpdateService(TvAppContext context)
        {
            this.context = context;
        }

        public void ParseAll(XDocument doc)
        {

        }
    }
}
