using System.Collections.Generic;
using TV_App.EFModels;

namespace TV_App.Responses
{
    public class FeatureResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public FeatureResponse(FeatureExample fe)
        {
            Id = (int)fe.Feature.Id;
            Value = fe.Feature.Value;
            Type = fe.Feature.TypeNavigation.TypeName;
        }

        public FeatureResponse(Feature f)
        {
            Id = (int)f.Id;
            Value = f.Value;
            Type = f.TypeNavigation.TypeName;
        }
    }
}