using System;
using System.Collections.Generic;
using TV_App.EFModels;

namespace TV_App.Responses
{
    public class EmissionResponse
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public string Channel { get; set; }

        public EmissionResponse(Emission e)
        {
            Start = DateTime.Parse(e.Start);
            Stop = DateTime.Parse(e.Stop);
            Channel = e.Channel.Name;
        }
    }
}