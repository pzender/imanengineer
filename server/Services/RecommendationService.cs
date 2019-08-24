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
        private readonly SimilarityCalculator similarityCalculator = new SimilarityCalculator();
        private readonly ProgrammeService programmeService = new ProgrammeService();
        private readonly TvAppContext db = new TvAppContext();
        public IEnumerable<ProgrammeDTO> GetRecommendations(string username, IEnumerable<ProgrammeDTO> available_programmes)
        {
            User user = db.Users
                .Include(u => u.Ratings)
                .AsNoTracking().Single(u => u.Login == username);

            IEnumerable<ProgrammeDTO> rated_programmes = programmeService.GetRatedBy(username);

            Dictionary<ProgrammeDTO, double> recom_supports = available_programmes
                .ToDictionary(p => p, p => RecommendationSupport(user, p, rated_programmes));
                
            recom_supports = recom_supports
                .Where(rs => rs.Value > 0.5)
                .OrderByDescending(rs => rs.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return recom_supports
                .Keys
                .Except(rated_programmes)
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
            return user.Ratings.Select(rat => rat.RelProgramme);

        }

        public double RecommendationSupport(User user, ProgrammeDTO programme, IEnumerable<ProgrammeDTO> rated_programmes)
        {
            if (!rated_programmes.Any())
                return 0;
            else
            {
                return rated_programmes
                    .Select(rate => similarityCalculator.TotalSimilarity(user, rate, programme))
                    .Average();
            }
        }
    }
}
