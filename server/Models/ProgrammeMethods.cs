using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.Models
{
    public partial class Programme
    {
        private readonly static TvAppContext DbContext = new TvAppContext();

        public IEnumerable<Emission> EmissionsBetween(TimeSpan from, TimeSpan to)
        {
            return Emission.Where(e => e.Between(from, to));
        }

        public bool EmittedOn(DateTime date)
        {
            return Emission.Any(em => em.Start.ToShortDateString() == date.ToShortDateString());
        }

        public IEnumerable<string> GetFeatureNames()
        {
            return FeatureExample.Select(fe => fe.Feature.Value);
        }

        public IEnumerable<Programme> GetSimilar(double w_actor, double w_category, double w_keyword, double w_director, double w_country, double w_year)
        {
            IDictionary<Programme, double> similar = DbContext.Programme
                .Include(prog => prog.Emission)
                .ThenInclude(em => em.Channel)
                .Include(prog => prog.FeatureExample)
                .ThenInclude(fe => fe.Feature)
                .ThenInclude(ft => ft.TypeNavigation)
                .ToDictionary(prog => prog, prog => TotalSimilarity(prog, w_actor, w_category, w_keyword, w_director, w_country, w_year));

            IEnumerable<Programme> list = similar
                .Where(prog => prog.Value > 0.5 && prog.Key.Id != this.Id)
                .OrderByDescending(prog => prog.Value)
                .Select(prog => prog.Key);

            return list;
        }

        public double TotalSimilarity(Programme other, double w_actor, double w_category, double w_keyword, double w_director, double w_country, double w_year)
        {
            IEnumerable<Feature> features = FeatureExample.Select(fe => fe.Feature),
                other_features = other.FeatureExample.Select(fe => fe.Feature);
            double sim_act = Feature.SetSimilarity(features.Where(f => f.Type == Feature.ACT_TYPE), other_features.Where(f => f.Type == Feature.ACT_TYPE));
            double sim_cat = Feature.SetSimilarity(features.Where(f => f.Type == Feature.CAT_TYPE), other_features.Where(f => f.Type == Feature.CAT_TYPE));
            double sim_keyw = Feature.SetSimilarity(features.Where(f => f.Type == Feature.KEYW_TYPE), other_features.Where(f => f.Type == Feature.KEYW_TYPE));
            double sim_dir = Feature.SetSimilarity(features.Where(f => f.Type == Feature.DIRECTOR_TYPE), other_features.Where(f => f.Type == Feature.DIRECTOR_TYPE));
            double sim_year = Feature.DateSimilarity(features.LastOrDefault(f => f.Type == Feature.DATE_TYPE), other_features.LastOrDefault(f => f.Type == Feature.DATE_TYPE));
            double sim_country = Feature.SetSimilarity(features.Where(f => f.Type == Feature.COUNTRY_TYPE), other_features.Where(f => f.Type == Feature.COUNTRY_TYPE));

            return w_actor * sim_act
                + w_category * sim_cat
                + w_keyword * sim_keyw
                + w_director * sim_dir
                + w_year * sim_year
                + w_country * sim_country;

        }

    }
}
