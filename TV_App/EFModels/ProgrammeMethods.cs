using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TV_App.EFModels
{
    public partial class Programme
    {
        public IEnumerable<Emission> EmissionsBetween(TimeSpan from, TimeSpan to)
        {
            return this.Emission.Where(e => e.Between(from, to));
        }

        public IEnumerable<string> GetFeatureNames()
        {
            return FeatureExample.Select(fe => fe.Feature.Value);
        }

        public IEnumerable<Programme> GetSimilar()
        {
            throw new NotImplementedException();
        }

        public double GetSimilarity(Programme other, User u = null)
        {

            throw new NotImplementedException();
        }

        private double GetSetSimilarity(Programme other, FeatureTypes featureGroup)
        {
            throw new NotImplementedException();
        }

        private double GetSingleSimilarity(Programme other, FeatureTypes featureGroup)
        {
            throw new NotImplementedException();
        }

        private double GetDateSimilarity(Programme other)
        {
            throw new NotImplementedException();
        }

    }
}
