using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    [Table("Emissions")]
    public partial class Emission : IEntityWithID
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("start", TypeName = "datetime")]
        public DateTime Start { get; set; }

        [Column("stop", TypeName = "datetime")]
        public DateTime Stop { get; set; }

        [Column("programme_id")]
        public long ProgrammeId { get; set; }

        [Column("channel_id")]
        public long ChannelId { get; set; }

        [ForeignKey(nameof(ChannelId))]
        [InverseProperty(nameof(Channel.Emissions))]
        public virtual Channel ChannelEmitted { get; set; }

        [ForeignKey(nameof(ProgrammeId))]
        [InverseProperty(nameof(Programme.Emissions))]
        public virtual Programme RelProgramme { get; set; }

        [InverseProperty(nameof(Notification.RelEmission))]
        public virtual ICollection<Notification> Notifications { get; set; }

    }
}
