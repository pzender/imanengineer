using System;
using System.Collections.Generic;
using TV_App.EFModels;

namespace TV_App.Responses
{
    public class EmissionResponse
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public ChannelResponse Channel { get; set; }

        public EmissionResponse(Emission e)
        {
            Start = DateTime.ParseExact(e.Start, "dd.MM.yyyy HH:mm:ss", null);
            Stop = DateTime.ParseExact(e.Stop, "dd.MM.yyyy HH:mm:ss", null);
            Channel = new ChannelResponse(e.Channel);
        }
    }
}