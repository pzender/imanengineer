using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    class Programme
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TitlePL { get; set; }
        public Dictionary<string, string> Features { get; set; }
    }
}
