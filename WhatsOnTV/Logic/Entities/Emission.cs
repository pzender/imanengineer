using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Emission
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public string channel_id { get; set; }
        public string programme_id { get; set; }
    }
}
