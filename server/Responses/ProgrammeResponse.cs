using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Controllers;
using TV_App.Models;

namespace TV_App.Responses
{
    public class ProgrammeResponse
    {
        public ProgrammeResponse(Programme src, User user)
        {
            long? rating = user?.Ratings.SingleOrDefault(rating => rating.ProgrammeId == src.Id)?.RatingValue;
            Rating = user != null && rating.HasValue ? (int)user.Ratings.Single(rating => rating.ProgrammeId == src.Id).RatingValue : 10000;
            Id = src.Id;
            Title = src.Title;
            IconUrl = src.IconUrl;
            SeqNumber = src.SeqNumber;
            Description = src.Descriptions.FirstOrDefault()?.Content;
            Series = null;
            Emissions = src.Emissions.Select(e => new EmissionResponse(e));
            Features = src.ProgrammesFeatures.Select(fe => new FeatureResponse(fe));
        }

        public ProgrammeResponse(Programme src) : this(src, null) { }



        public long Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string SeqNumber { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; } 

        public virtual SeriesResponse Series { get; set; }
        public virtual IEnumerable<EmissionResponse> Emissions { get; set; }
        public virtual IEnumerable<FeatureResponse> Features { get; set; }
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
