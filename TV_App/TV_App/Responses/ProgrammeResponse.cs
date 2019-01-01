using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Responses
{
    public class ProgrammeResponse
    {
        public ProgrammeResponse(EFModels.Programme src)
        {
            Id = src.Id;
            Title = src.Title;
            IconUrl = src.IconUrl;
            SeqNumber = src.SeqNumber;

            Series = null;

            Emissions = src.Emission.Select(e => new EmissionResponse(e));
            Features = src.FeatureExample.Select(fe => new FeatureResponse(fe));
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string SeqNumber { get; set; }

        public virtual SeriesResponse Series { get; set; }
        public virtual IEnumerable<EmissionResponse> Emissions { get; set; }
        public virtual IEnumerable<FeatureResponse> Features { get; set; }

    }
}
