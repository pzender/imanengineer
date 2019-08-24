using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Controllers;
using TV_App.Models;

namespace TV_App.DataTransferObjects
{
    public class ProgrammeDTO
    {
        public ProgrammeDTO(Programme src, User user)
        {
            Rating = user?.Ratings.SingleOrDefault(rating => rating.ProgrammeId == src.Id)?.RatingValue;
            Id = src.Id;
            Title = src.Title;
            IconUrl = src.IconUrl;
            SeqNumber = src.SeqNumber;
            Description = src.Descriptions.FirstOrDefault()?.Content;
            Series = null;
            Emissions = src.Emissions.Select(e => new EmissionDTO(e));
            Features = src.ProgrammesFeatures.Select(fe => new FeatureDTO(fe));
        }

        public ProgrammeDTO(Programme src) : this(src, null) { }

        public ProgrammeDTO() { }

        public long Id { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public string SeqNumber { get; set; }
        public string Description { get; set; }
        public long? Rating { get; set; } 

        public virtual SeriesDTO Series { get; set; }
        public virtual IEnumerable<EmissionDTO> Emissions { get; set; }
        public virtual IEnumerable<FeatureDTO> Features { get; set; }
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
