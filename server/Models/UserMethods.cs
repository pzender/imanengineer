using System;
using System.Collections.Generic;
using System.Linq;

namespace TV_App.Models
{

    public partial class User
    {
        //public 

        public IEnumerable<Programme> GetRecommendations(IEnumerable<Programme> available_programmes)
        {
            Dictionary<Programme, double> recom_supports = available_programmes.ToDictionary(p => p, p => RecommendationSupport(p));
            recom_supports = recom_supports
                .Where(rs => rs.Value > 0.1)
                .OrderByDescending(rs => rs.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return recom_supports
                .Keys
                .Except(GetRated())
                .Take(20);
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
            return GetPositivelyRated().Select(pos => pos.TotalSimilarity(p, WeightActor, WeightCategory, WeightKeyword, WeightDirector, WeightCountry, WeightYear)).Average();
        }

    }
}