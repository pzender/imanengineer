using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class EmissionModel
    {
        public string ChannelName { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
}
