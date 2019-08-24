﻿using System;
using System.Collections.Generic;
using TV_App.Models;

namespace TV_App.DataTransferObjects
{
    public class EmissionDTO
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public ChannelDTO Channel { get; set; }

        public EmissionDTO(Emission e)
        {
            Start = e.Start;
            Stop = e.Stop;
            Channel = new ChannelDTO(e.ChannelEmitted);
        }
    }
}