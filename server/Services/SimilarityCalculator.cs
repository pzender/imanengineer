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
        public const int COUNTRY_TYPE = 1, DATE_TYPE = 2, ACT_TYPE = 4, DIRECTOR_TYPE = 5, CAT_TYPE = 7, KEYW_TYPE = 8;

        public double TotalSimilarity(User user, Programme p1, Programme p2)
        {
            IEnumerable<Feature> p1_features = p1.ProgrammesFeatures.Select(fe => fe.RelFeature);
            IEnumerable<Feature> p2_features = p2.ProgrammesFeatures.Select(fe => fe.RelFeature);
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

        private double SetSimilarity(IEnumerable<Feature> one, IEnumerable<Feature> other)
        {

            int and = 0;
            foreach (Feature f_one in one)
            {
                foreach (Feature f_oth in other)
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

        private double DateSimilarity(Feature one, Feature other)
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

        private double SingleSimilarity(Feature one, Feature other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
                return one.Value == other.Value ? 1 : 0;
            else return 0;
        }

    }
}
