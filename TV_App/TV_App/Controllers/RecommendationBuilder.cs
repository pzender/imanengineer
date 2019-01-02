using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.EFModels;

namespace TV_App.Controllers
{
    public class RecommendationBuilder
    {
        public User User { get; set; }
        private IEnumerable<Programme> positively_rated;
        private const double DATE_SIMILARITY_HALF = 0.05;
        private readonly double w_act = 0.2, w_country = 0.1, w_year = 0.1, w_dir = 0.2, w_cat = 0.4, w_keyw = 0;
        private readonly int ACT_TYPE = 4, CAT_TYPE = 7, KEYW_TYPE = 8, DATE_TYPE = 2, COUNTRY_TYPE = 1, DIRECTOR_TYPE = 5;

        public IEnumerable<Programme> Similar(IEnumerable<Programme> available_programmes, Programme given)
        {
            Dictionary<Programme, double> recom_supports = available_programmes.ToDictionary(p => p, p => TotalSimilarity(p, given));
            return recom_supports
                .Where(rs => rs.Value > 0)
                .OrderByDescending(rs => rs.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value)
                .Keys
                .Except(new List<Programme>() { given});
        }

        public IEnumerable<Programme> Build(IEnumerable<Programme> available_programmes)
        {
            Dictionary<Programme, double> recom_supports = available_programmes.ToDictionary(p => p, p => RecommendationSupport(p));
            return recom_supports
                .Where(rs => rs.Value > 0)
                .OrderByDescending(rs => rs.Value)
                .ToDictionary(kv => kv.Key, kv => kv.Value)
                .Keys
                .Except(User.Rating.Select(r => r.Programme));
        }

        private double SetSimilarity(IEnumerable<Feature> one, IEnumerable<Feature> other)
        {
            int and = one.Intersect(other).Count(), or = one.Union(other).Count(), diff = (or - and);
            if (or == 0) return 0;
            else
            {
                return (2.0 * and) / (and + or);
            }
        }

        private double DateSimilarity(Feature one, Feature other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
            {
                double n_one = double.Parse(one.Value), n_other = double.Parse(other.Value);
                return 1 / (DATE_SIMILARITY_HALF * Math.Abs(n_one - n_other) + 1);
            }
            else return 0;
        }

        private double SingleSimilarity(Feature one, Feature other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
                return one.Value == other.Value ? 1 : 0;
            else return 0;
        }

        public double TotalSimilarity(Programme one, Programme other)
        {
            IEnumerable<Feature> one_features = one.FeatureExample.Select(fe => fe.Feature),
                other_features = other.FeatureExample.Select(fe => fe.Feature);
            double sim_act = SetSimilarity(one_features.Where(f => f.Type == ACT_TYPE), other_features.Where(f => f.Type == ACT_TYPE));
            double sim_cat = SetSimilarity(one_features.Where(f => f.Type == CAT_TYPE), other_features.Where(f => f.Type == CAT_TYPE));
            double sim_keyw = SetSimilarity(one_features.Where(f => f.Type == KEYW_TYPE), other_features.Where(f => f.Type == KEYW_TYPE));
            double sim_dir = SetSimilarity(one_features.Where(f => f.Type == DIRECTOR_TYPE), other_features.Where(f => f.Type == DIRECTOR_TYPE));
            double sim_year = DateSimilarity(one_features.Where(f => f.Type == DATE_TYPE).SingleOrDefault(), other_features.Where(f => f.Type == DATE_TYPE).SingleOrDefault());
            double sim_country = SingleSimilarity(one_features.Where(f => f.Type == COUNTRY_TYPE).SingleOrDefault(), other_features.Where(f => f.Type == COUNTRY_TYPE).SingleOrDefault());

            return w_act * sim_act
                + w_cat * sim_cat
                + w_keyw * sim_keyw
                + w_dir * sim_dir
                + w_year * sim_year
                + w_country * sim_cat;
        }

        private double RecommendationSupport(Programme p)
        {
            return positively_rated.Select(pos => TotalSimilarity(pos, p)).Sum();
        }

        public RecommendationBuilder(User user)
        {
            User = user;
            positively_rated = User?.Rating.Where(r => r.RatingValue == 1).Select(r => r.Programme);
        }


    }
}
