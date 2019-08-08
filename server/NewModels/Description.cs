using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.NewModels
{
    [Table("Descriptions")]
    public partial class Description
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("programme_id")]
        public long ProgrammeId { get; set; }

        [Column("content", TypeName = "text")]
        [Required]
        public string Content { get; set; }

        [Column("guide_update_id")]
        public long? GuideUpdateId { get; set; }

        [ForeignKey(nameof(GuideUpdateId))]
        [InverseProperty(nameof(GuideUpdate.Descriptions))]
        public virtual GuideUpdate RelGuideUpdate { get; set; }

        [ForeignKey(nameof(ProgrammeId))]
        [InverseProperty(nameof(Programme.Descriptions))]
        public virtual Programme RelProgramme { get; set; }
    }
}
