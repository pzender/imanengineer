using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.NewModels
{
    public partial class OfferedChannel
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
        [InverseProperty(nameof(Offer.OfferedChannels))]
        public virtual Offer RelOffer { get; set; }
    }
}
