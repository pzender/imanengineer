using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV_App.Models
{
    public partial class User
    {
        public User()
        {
            Ratings = new HashSet<Rating>();
        }

        [Column("login")]
        [StringLength(20)]
        public string Login { get; set; }
        [Column("weight_actor")]
        public double WeightActor { get; set; }
        [Column("weight_category")]
        public double WeightCategory { get; set; }
        [Column("weight_country")]
        public double WeightCountry { get; set; }
        [Column("weight_year")]
        public double WeightYear { get; set; }
        [Column("weight_keyword")]
        public double WeightKeyword { get; set; }
        [Column("weight_director")]
        public double WeightDirector { get; set; }

        [InverseProperty(nameof(Rating.RelUser))]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
