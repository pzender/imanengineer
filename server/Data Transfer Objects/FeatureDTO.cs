using System.Collections.Generic;
using TV_App.Models;

namespace TV_App.DataTransferObjects
{
    public class FeatureDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public FeatureDTO(ProgrammesFeature fe)
        {
            Id = (int)fe.RelFeature.Id;
            Value = fe.RelFeature.Value;
            Type = fe.RelFeature.RelType.TypeName;
        }

        public FeatureDTO(Feature f)
        {
            Id = (int)f.Id;
            Value = f.Value;
            Type = f.RelType.TypeName;
        }
    }
}