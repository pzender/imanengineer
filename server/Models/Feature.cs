using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    [Table("Features")]
    public partial class Feature : IEntityWithID
    {
        public Feature()
        {
            ProgrammesFeatures = new HashSet<ProgrammesFeature>();
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("type")]
        public long Type { get; set; }

        [Column("value")]
        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        [ForeignKey(nameof(Type))]
        [InverseProperty(nameof(FeatureType.Features))]
        public virtual FeatureType RelType { get; set; }

        [InverseProperty(nameof(ProgrammesFeature.RelFeature))]
        public virtual ICollection<ProgrammesFeature> ProgrammesFeatures { get; set; }
    }
}
