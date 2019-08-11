using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class Offer
    {
        public Offer()
        {
            OfferedChannels = new HashSet<OfferedChannel>();
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Column("icon_url")]
        [StringLength(150)]
        public string IconUrl { get; set; }

        [InverseProperty(nameof(OfferedChannel.RelOffer))]
        public virtual ICollection<OfferedChannel> OfferedChannels { get; set; }
    }
}
