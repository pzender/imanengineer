using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.NewModels
{
    public partial class Series
    {
        public Series()
        {
            Programmes = new HashSet<Programme>();
        }

        [Column("id")]
        public long Id { get; set; }

        [Column("title")]
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [InverseProperty(nameof(Programme.RelSeries))]
        public virtual ICollection<Programme> Programmes { get; set; }
    }
}
