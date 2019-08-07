using System;
using System.Collections.Generic;
using System.Linq;

namespace TV_App.Models
{
    public partial class Feature
    {
        public const int DATE_SIMILARITY_HALF = 20;
        public const double SET_SIMILARITY_COEFF = 4;
        public const int COUNTRY_TYPE = 1, DATE_TYPE = 2, ACT_TYPE = 4, DIRECTOR_TYPE = 5, CAT_TYPE = 7, KEYW_TYPE = 8;

        public static double SetSimilarity(IEnumerable<Feature> one, IEnumerable<Feature> other)
        {
            int and = 0;
            foreach(Feature f_one in one)
            {
                foreach(Feature f_oth in other)
                {
                    if (f_one.Value == f_oth.Value && f_one.Type == f_oth.Type)
                        and++;
                }
            }
            int or = one.Count() + other.Count() - and;
            if (or == 0) return 0;
            else
            {
                return (SET_SIMILARITY_COEFF * and) / ((SET_SIMILARITY_COEFF - 1) * and + or);
            }
        }

        public static double DateSimilarity(Feature one, Feature other)
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

        public static double SingleSimilarity(Feature one, Feature other)
        {
            if (one == null && other == null)
                return 1;
            else if (one != null && other != null)
                return one.Value == other.Value ? 1 : 0;
            else return 0;
        }

    }
}
