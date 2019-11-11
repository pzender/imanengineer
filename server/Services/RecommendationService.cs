using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;
using TV_App.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace TV_App.Services
{
    public class RecommendationService
    {
        private const double WEIGHT_UPDATE = 0.2;
        private readonly SimilarityCalculator similarityCalculator = new SimilarityCalculator();
        private readonly TvAppContext db = new TvAppContext();

        public IEnumerable<Programme> GetSimilar(Programme prog, User user)
        {
            var available_programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                    .ThenInclude(pf => pf.RelFeature)
                    .ThenInclude(feat => feat.RelType)
                .Include(prog => prog.Ratings)
                    .ThenInclude(r => r.RelUser)
                .Include(prog => prog.Emissions)
                    .ThenInclude(em => em.ChannelEmitted)
                .AsNoTracking();

            var recom_supports = available_programmes
                .ToDictionary(p => p, p => similarityCalculator.TotalSimilarity(user, p, prog))
                .OrderByDescending(rs => rs.Value)
                .Skip(1)
                .Take(5)
                .ToDictionary(kv => kv.Key, kv => kv.Value)
                .Keys
                .Where(prog => prog.Emissions.Any());

            return recom_supports;
        }

        public IEnumerable<Programme> GetRecommendations(User user)
        {
            var available_programmes = db.Programmes
                .Include(prog => prog.ProgrammesFeatures)
                    .ThenInclude(pf => pf.RelFeature)
                    .ThenInclude(feat => feat.RelType)
                .Include(prog => prog.Ratings)
                    .ThenInclude(r => r.RelUser)
                .Include(prog => prog.Emissions)
                    .ThenInclude(em => em.ChannelEmitted)
                .AsNoTracking();
            Console.WriteLine($"[{DateTime.Now}] Recommendations - {available_programmes.Count()} programmes");

            Dictionary<Programme, double> recom_supports = available_programmes
                .ToDictionary(p => p, p => RecommendationSupport(p, user));

            recom_supports = recom_supports
                .OrderByDescending(rs => rs.Value)
                .Take(20)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            var rated_ids = GetRated(user).Select(prog => prog.Id);

            return recom_supports
                .Keys
                .Where(prog => !rated_ids.Contains(prog.Id))
                .Where(prog => prog.Emissions.Any());
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

        public User UpdateWeights(Programme programme, User user, long actual_rating)
        {
            double weight_sum = user.WeightActor + user.WeightCategory + user.WeightCountry + user.WeightDirector + user.WeightYear + user.WeightKeyword;
            double expected_rating = RecommendationSupport(programme, user);
            user.WeightActor += WEIGHT_UPDATE * user.WeightActor * (actual_rating - expected_rating) / weight_sum;
            user.WeightCategory += WEIGHT_UPDATE * user.WeightCategory * (actual_rating - expected_rating) / weight_sum;
            user.WeightCountry += WEIGHT_UPDATE * user.WeightCountry * (actual_rating - expected_rating) / weight_sum;
            user.WeightDirector += WEIGHT_UPDATE * user.WeightDirector * (actual_rating - expected_rating) / weight_sum;
            user.WeightKeyword += WEIGHT_UPDATE * user.WeightKeyword * (actual_rating - expected_rating) / weight_sum;
            user.WeightYear += WEIGHT_UPDATE * user.WeightYear * (actual_rating - expected_rating) / weight_sum;

            return user;
        }
    }
}
