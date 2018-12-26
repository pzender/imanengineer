using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Description : AbstractEntity
    {
        public string content { get; set; }
        public int guideupdate_id { get; set; }
        public int programme_id { get; set; }
    }
}
