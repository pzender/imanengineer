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

    }
}
