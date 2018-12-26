using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class FeatureExample// : IConnection<Programme, Feature>
    {
        public int t1_id { get ; set ; }
        public int t2_id { get ; set ; }

        public string table => this.GetType().Name;
    }
}
