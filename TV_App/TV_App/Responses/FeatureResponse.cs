using System.Collections.Generic;
using TV_App.EFModels;

namespace TV_App.Responses
{
    public class FeatureResponse
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public FeatureResponse(FeatureExample fe)
        {
            Value = fe.Feature.Value;
            Type = fe.Feature.TypeNavigation.TypeName;
        }
    }
}