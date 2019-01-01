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

        public virtual ICollection<Rating> Rating { get; set; }
    }
}
