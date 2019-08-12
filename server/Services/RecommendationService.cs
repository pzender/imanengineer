using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;

namespace TV_App.Services
{
    public class RecommendationService
    {
        private SimilarityCalculator similarityCalculator = new SimilarityCalculator();
        public IEnumerable<Programme> GetRecommendations(User user, IEnumerable<Programme> available_programmes)
        {
            Dictionary<Programme, double> recom_supports = available_programmes
                .ToDictionary(p => p, p => RecommendationSupport(p, user));
                
            recom_supports = recom_supports
                .Where(rs => rs.Value > 0.5)
                .OrderByDescending(rs => rs.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return recom_supports
                .Keys
                .Except(GetRated(user))
                .Take(20);
        }

        public IEnumerable<Programme> GetPositivelyRated(User user)
        {
            return user.Ratings
                .Where(rat => rat.RatingValue > 0)
                .Select(rat => rat.RelProgramme);

        }

        public IEnumerable<Programme> GetRated(User user)
        {
            return user.Ratings
                .Select(rat => rat.RelProgramme);

        }

        public double RecommendationSupport(Programme programme, User user)
        {
            IEnumerable<Programme> positivelyRated = GetPositivelyRated(user);
            if (!positivelyRated.Any())
                return 0;
            else
            {
                return positivelyRated
                    .Select(rate => similarityCalculator.TotalSimilarity(user, rate, programme))
                    .Average();
            }
        }
    }
}
