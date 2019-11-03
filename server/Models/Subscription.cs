using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public class Subscription: IEntityWithID
    {
        [Column("id")]
        [Key]
        public long Id { get; set; }

        [Column("user_login")]
        [Required]
        [StringLength(20)]
        public string UserLogin { get; set; }

        [Column("push_endpoint")]
        [StringLength(250)]
        public string PushEndpoint { get; set; }

        [Column("push_p256dh")]
        [StringLength(250)]
        public string PushP256dh { get; set; }

        [Column("push_auth")]
        [StringLength(250)]
        public string PushAuth { get; set; }

        [ForeignKey(nameof(UserLogin))]
        [InverseProperty(nameof(User.Subscriptions))]
        public virtual User RelUser { get; set; }

    }
}
