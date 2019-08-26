using System;
using System.Collections.Generic;
using System.Text;
using TV_App.Models;
using TV_App.Services;
using Xunit;

namespace TV_AppTests
{
    public class SimilarityCalculatorTests
    {
        private SimilarityCalculator calculator = new SimilarityCalculator();

        [Fact]
        public void TotalSimilarityOfSameProgrammes()
        {
            // arrange
            User testUser = new User()
            {
                Login = "test",
                WeightActor = 0.3,
                WeightCategory = 0.3,
                WeightCountry = 0.1,
                WeightDirector = 0.1,
                WeightKeyword = 0.1,
                WeightYear = 0.1
            };

            Programme prog1 = new Programme()
            {
                Id = 0,
                Title = "Test1",
                ProgrammesFeatures = new List<ProgrammesFeature>()
                {
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 0, RelFeature = EXAMPLE_FEATURES[0] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 2, RelFeature = EXAMPLE_FEATURES[2] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 8, RelFeature = EXAMPLE_FEATURES[8] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 4, RelFeature = EXAMPLE_FEATURES[4] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 10, RelFeature = EXAMPLE_FEATURES[10] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 12, RelFeature = EXAMPLE_FEATURES[12] },
                }
            };
            // act
            //double sim = calculator.TotalSimilarity(testUser, prog1, prog1);
            //// assert
            //Assert.Equal(1.0, sim, 3);
        }

        [Fact]
        public void TotalSimilarityOfSameEmptyProgrammes()
        {
            // arrange
            User testUser = new User()
            {
                Login = "test",
                WeightActor = 0.3,
                WeightCategory = 0.3,
                WeightCountry = 0.1,
                WeightDirector = 0.1,
                WeightKeyword = 0.1,
                WeightYear = 0.1
            };

            Programme prog1 = new Programme()
            {
                Id = 0,
                Title = "Test1",
                ProgrammesFeatures = new List<ProgrammesFeature>() {  }
            };
            // act
            //double sim = calculator.TotalSimilarity(testUser, prog1, prog1);
            //// assert
            //Assert.Equal(1.0, sim, 3);
        }




        private readonly Feature[] EXAMPLE_FEATURES = new Feature[]
        {
            new Feature() { Id =  0, Value = "USA", Type = 1 },
            new Feature() { Id =  1, Value = "Japan", Type = 1 },
            new Feature() { Id =  2, Value = "2016", Type = 2 },
            new Feature() { Id =  3, Value = "2006", Type = 2 },
            new Feature() { Id =  4, Value = "Sophie Turner", Type = 4 },
            new Feature() { Id =  5, Value = "Jennifer Lawrence", Type = 4 },
            new Feature() { Id =  6, Value = "Michael Fassbender", Type = 4 },
            new Feature() { Id =  7, Value = "Bruce Willis", Type = 4 },
            new Feature() { Id =  8, Value = "Bryan Singer", Type = 5 },
            new Feature() { Id =  9, Value = "Quentin Tarantino", Type = 5 },
            new Feature() { Id = 10, Value = "Film", Type = 7 },
            new Feature() { Id = 11, Value = "Serial", Type = 7 },
            new Feature() { Id = 12, Value = "Sensacyjny", Type = 7 },
            new Feature() { Id = 13, Value = "Dokumentalny", Type = 7 },
        };
    }
}
