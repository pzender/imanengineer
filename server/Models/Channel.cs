﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    [Table("Channels")]
    public partial class Channel : IEntityWithID
    {
        public Channel()
        {
            Emissions = new HashSet<Emission>();
            OfferedChannels = new HashSet<GroupedChannel>();
        }

        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Column("icon_url")]
        [StringLength(200)]
        public string IconUrl { get; set; }

        [InverseProperty(nameof(Emission.ChannelEmitted))]
        public virtual ICollection<Emission> Emissions { get; set; }

        [InverseProperty(nameof(GroupedChannel.RelChannel))]
        public virtual ICollection<GroupedChannel> OfferedChannels { get; set; }
    }
}
