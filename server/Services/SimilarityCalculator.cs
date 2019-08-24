using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_App.Models;
using TV_App.DataTransferObjects;

namespace TV_App.Services
{
    public class SimilarityCalculator
    {
        public const int DATE_SIMILARITY_HALF = 20;
        public const double SET_SIMILARITY_COEFF = 4;
        public const string COUNTRY_TYPE = "country", DATE_TYPE = "date", ACT_TYPE = "actor", DIRECTOR_TYPE = "director", CAT_TYPE = "category", KEYW_TYPE = "keyword";

        public double TotalSimilarity(User user, ProgrammeDTO p1, ProgrammeDTO p2)
        {
            IEnumerable<FeatureDTO> p1_features = p1.Features;
            IEnumerable<FeatureDTO> p2_features = p2.Features;
            user ??= DUMMY_USER;

            double sim_act = SetSimilarity(
                p1_features.Where(f => f.Type == ACT_TYPE),
                p2_features.Where(f => f.Type == ACT_TYPE));
            double sim_cat = SetSimilarity(
                p1_features.Where(f => f.Type == CAT_TYPE),
                p2_features.Where(f => f.Type == CAT_TYPE));
            double sim_keyw = SetSimilarity(
                p1_features.Where(f => f.Type == KEYW_TYPE),
                p2_features.Where(f => f.Type == KEYW_TYPE));
            double sim_dir = SetSimilarity(
                p1_features.Where(f => f.Type == DIRECTOR_TYPE),
                p2_features.Where(f => f.Type == DIRECTOR_TYPE));
            double sim_year = DateSimilarity(
                p1_features.LastOrDefault(f => f.Type == DATE_TYPE),
                p2_features.LastOrDefault(f => f.Type == DATE_TYPE));
            double sim_country = SetSimilarity(
                p1_features.Where(f => f.Type == COUNTRY_TYPE),
                p2_features.Where(f => f.Type == COUNTRY_TYPE));

            return user.WeightActor * sim_act
                + user.WeightCategory * sim_cat
                + user.WeightDirector * sim_dir
                + user.WeightYear * sim_year
                + user.WeightCountry * sim_country
                + user.WeightKeyword * sim_keyw;

        }

        private double SetSimilarity(IEnumerable<FeatureDTO> one, IEnumerable<FeatureDTO> other)
        {
            
            int and = 0;
            foreach (FeatureDTO f_one in one)
            {
                foreach (FeatureDTO f_oth in other)
                {
                    if (f_one.Value == f_oth.Value && f_one.Type == f_oth.Type)
                        and++;
                }
            }
            int or = one.Count() + other.Count() - and;
            if (or == 0) return 1;
            else
            {
                return (SET_SIMILARITY_COEFF * and) / ((SET_SIMILARITY_COEFF - 1) * and + or);
            }
        }

        private double DateSimilarity(FeatureDTO one, FeatureDTO other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
            {
                double n_one = double.Parse(one.Value), n_other = double.Parse(other.Value);
                return 1 / ((1 / DATE_SIMILARITY_HALF) * Math.Abs(n_one - n_other) + 1);
            }
            else return 0;
        }

        private double SingleSimilarity(FeatureDTO one, FeatureDTO other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
                return one.Value == other.Value ? 1 : 0;
            else return 0;
        }

        private static readonly User DUMMY_USER = new User() { Login = "DUMMY", WeightActor = 0.3, WeightCategory = 0.3, WeightCountry = 0.1, WeightDirector = 0.1, WeightKeyword = 0.1, WeightYear = 0.1 };

    }
}
