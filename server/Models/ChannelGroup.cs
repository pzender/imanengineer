using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class ChannelGroup : IEntityWithID
    {
        public ChannelGroup()
        {
            OfferedChannels = new HashSet<GroupedChannel>();
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

        [Column("type")]
        [StringLength(10)]
        public string GroupType { get; set; }

        [InverseProperty(nameof(GroupedChannel.RelOffer))]
        public virtual ICollection<GroupedChannel> OfferedChannels { get; set; }

        public const string TYPE_OFFER = "OFFER";
        public const string TYPE_THEME = "THEME";
    }
}
