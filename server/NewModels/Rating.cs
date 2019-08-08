using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.NewModels
{
    public partial class Rating
    {
        [Column("user_login")]
        [Required]
        [StringLength(20)]
        public string UserLogin { get; set; }

        [Column("programme_id")]
        [Required]
        public long ProgrammeId { get; set; }

        [Column("rating_value")]
        [Required]
        public long RatingValue { get; set; }

        [ForeignKey(nameof(ProgrammeId))]
        [InverseProperty(nameof(Programme.Ratings))]
        public virtual Programme RelProgramme { get; set; }

        [ForeignKey(nameof(UserLogin))]
        [InverseProperty(nameof(User.Ratings))]
        public virtual User RelUser { get; set; }
    }
}
