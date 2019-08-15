using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class Programme : IEntityWithID
    {
        public Programme()
        {
            Descriptions = new HashSet<Description>();
            Emissions = new HashSet<Emission>();
            ProgrammesFeatures = new HashSet<ProgrammesFeature>();
            Ratings = new HashSet<Rating>();
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }
        
        [Column("title")]
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Column("icon_url")]
        [StringLength(200)]
        public string IconUrl { get; set; }

        [Column("seq_number")]
        [StringLength(20)]
        public string SeqNumber { get; set; }

        [Column("series_id")]
        public long? SeriesId { get; set; }

        [ForeignKey(nameof(SeriesId))]
        [InverseProperty(nameof(Series.Programmes))]
        public virtual Series RelSeries { get; set; }
        [InverseProperty(nameof(Description.RelProgramme))]
        public virtual ICollection<Description> Descriptions { get; set; }
        [InverseProperty(nameof(Emission.RelProgramme))]
        public virtual ICollection<Emission> Emissions { get; set; }
        [InverseProperty(nameof(ProgrammesFeature.RelProgramme))]
        public virtual ICollection<ProgrammesFeature> ProgrammesFeatures { get; set; }
        [InverseProperty(nameof(Rating.RelProgramme))]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
