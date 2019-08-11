using System.Collections.Generic;
using TV_App.Models;

namespace TV_App.Responses
{
    public class FeatureResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public FeatureResponse(ProgrammesFeature fe)
        {
            Id = (int)fe.RelFeature.Id;
            Value = fe.RelFeature.Value;
            Type = fe.RelFeature.RelType.TypeName;
        }

        public FeatureResponse(Feature f)
        {
            Id = (int)f.Id;
            Value = f.Value;
            Type = f.RelType.TypeName;
        }
    }
}