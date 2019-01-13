using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Controllers;
using TV_App.EFModels;

namespace TV_App.Responses
{
    public class ProgrammeResponse
    {
        public ProgrammeResponse(Programme src)
        {
            Id = src.Id;
            Title = src.Title;
            IconUrl = src.IconUrl;
            SeqNumber = src.SeqNumber;
            Description = src.Description.FirstOrDefault()?.Content;

            Series = null;

            Emissions = src.Emission.Select(e => new EmissionResponse(e));
            Features = src.FeatureExample.Select(fe => new FeatureResponse(fe));
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string SeqNumber { get; set; }
        public string Description { get; set; }


        public virtual SeriesResponse Series { get; set; }
        public virtual IEnumerable<EmissionResponse> Emissions { get; set; }
        public virtual IEnumerable<FeatureResponse> Features { get; set; }
        public IEnumerable<ShortProgrammeLink> Similar { get; set; }
    }

    public class ShortProgrammeLink
    {
        public ShortProgrammeLink(Programme src)
        {
            Id = src.Id;
            Title = src.Title;
        }

        public long Id { get; set; }
        public string Title { get; set; }
    }
}
