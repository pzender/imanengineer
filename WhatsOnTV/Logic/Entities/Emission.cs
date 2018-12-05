using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Entities
{
    public class Emission : AbstractEntity
    {
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public int channel_id { get; set; }
        public int programme_id { get; set; }
    }
}
