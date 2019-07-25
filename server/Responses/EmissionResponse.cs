using System;
using System.Collections.Generic;
using TV_App.Models;

namespace TV_App.Responses
{
    public class EmissionResponse
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public ChannelResponse Channel { get; set; }
        public string Title { get; set; }

        public EmissionResponse(Emission e)
        {
            Start = e.Start;
            Stop = e.Stop;
            Channel = new ChannelResponse(e.Channel);
            Title = e.Programme?.Title;
        }
    }
}