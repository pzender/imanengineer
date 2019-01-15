using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class User
    {
        public User()
        {
            Rating = new HashSet<Rating>();
        }

        public string Login { get; set; }
        public double WeightActor { get; set; }
        public double WeightCategory { get; set; }
        public double WeightCountry { get; set; }
        public double WeightYear { get; set; }
        public double WeightKeyword { get; set; }
        public double WeightDirector { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}
