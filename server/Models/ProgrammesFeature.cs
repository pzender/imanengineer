using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class ProgrammesFeature
    {
        [Column("feature_id")]
        public long FeatureId { get; set; }

        [Column("programme_id")]
        public long ProgrammeId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        [InverseProperty(nameof(Feature.ProgrammesFeatures))]
        public virtual Feature RelFeature { get; set; }

        [ForeignKey(nameof(ProgrammeId))]
        [InverseProperty(nameof(Programme.ProgrammesFeatures))]
        public virtual Programme RelProgramme { get; set; }
    }
}
