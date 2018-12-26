using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class GuideUpdate : AbstractEntity
    {
        public DateTime posted { get; set; }
        public string source { get; set; }
    }
}
