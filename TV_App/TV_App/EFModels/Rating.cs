using System;
using System.Collections.Generic;

namespace TV_App.EFModels
{
    public partial class Rating
    {
        public string UserLogin { get; set; }
        public long ProgrammeId { get; set; }
        public long RatingValue { get; set; }

        public virtual Programme Programme { get; set; }
        public virtual User UserLoginNavigation { get; set; }
    }
}
