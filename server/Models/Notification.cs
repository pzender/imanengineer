using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class Notification : IEntityWithID
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("user_login")]
        [Required]
        [StringLength(20)]
        public string UserLogin { get; set; }

        [Column("emission_id")]
        [Required]
        public long EmissionId { get; set; }

        [Column("rating_value")]
        [Required]
        public long RatingValue { get; set; }

        [ForeignKey(nameof(EmissionId))]
        [InverseProperty(nameof(Emission.Notifications))]
        public virtual Emission RelEmission { get; set; }

        [ForeignKey(nameof(UserLogin))]
        [InverseProperty(nameof(User.Notifications))]
        public virtual User RelUser { get; set; }

    }
}
