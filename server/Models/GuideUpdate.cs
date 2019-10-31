using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class GuideUpdate : IEntityWithID
    {
        public GuideUpdate()
        {
            Descriptions = new HashSet<Description>();
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("source")]
        [Required]
        [StringLength(200)]
        public string Source { get; set; }

        [Column("started", TypeName = "datetime")]
        public DateTime Started { get; set; }

        [Column("finished", TypeName = "datetime")]
        public DateTime? Finished { get; set; }


        [InverseProperty(nameof(Description.RelGuideUpdate))]
        public virtual ICollection<Description> Descriptions { get; set; }
    }
}
