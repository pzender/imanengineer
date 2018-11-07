using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Description
    {
        public int id { get; set; }
        public string content { get; set; }
        public int source_id { get; set; }
        public int programme_id { get; set; }
    }
}
