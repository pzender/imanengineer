using System;
using System.Collections.Generic;
using System.Linq;

namespace TV_App.EFModels
{

    public partial class User
    {
        //public 

        public IEnumerable<Programme> GetRecommendations()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Programme> GetPositivelyRated()
        {
            return Rating
                .Where(rat => rat.RatingValue > 0)
                .Select(rat => rat.Programme);

        }

        public IEnumerable<Programme> GetRated()
        {
            return Rating
                .Select(rat => rat.Programme);

        }

        public double RecommendationSupport(Programme p)
        {
            throw new NotImplementedException();
        }

    }
}