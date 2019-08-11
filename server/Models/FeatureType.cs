using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    [Table("FeatureTypes")]
    public partial class FeatureType
    {
        public FeatureType()
        {
            Features = new HashSet<Feature>();
        }

        [Column("id")]
        public long Id { get; set; }
        [Required]
        [Column("type_name")]
        [StringLength(200)]
        public string TypeName { get; set; }

        [InverseProperty(nameof(Feature.RelType))]
        public virtual ICollection<Feature> Features { get; set; }
    }
}
