using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class GroupedChannel
    {
        [Column("offer_id")]
        [Key]
        public long OfferId { get; set; }
        [Column("channel_id")]
        [Key]
        public long ChannelId { get; set; }

        [ForeignKey(nameof(ChannelId))]
        [InverseProperty(nameof(Channel.OfferedChannels))]
        public virtual Channel RelChannel { get; set; }

        [ForeignKey(nameof(OfferId))]
        [InverseProperty(nameof(ChannelGroup.OfferedChannels))]
        public virtual ChannelGroup RelOffer { get; set; }
    }
}
