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
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 0, RelFeature = TestData.SampleFeatures[0] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 2, RelFeature = TestData.SampleFeatures[2] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 8, RelFeature = TestData.SampleFeatures[8] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 4, RelFeature = TestData.SampleFeatures[4] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 10, RelFeature = TestData.SampleFeatures[10] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 12, RelFeature = TestData.SampleFeatures[12] },
                }
            };
            // act
            double sim = calculator.TotalSimilarity(testUser, prog1, prog1);
            // assert
            Assert.Equal(1.0, sim, 3);
        }

        [Fact]
        public void TotalSimilarityWithDifferentCountry()
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
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 0, RelFeature = TestData.SampleFeatures[0] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 2, RelFeature = TestData.SampleFeatures[2] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 8, RelFeature = TestData.SampleFeatures[8] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 4, RelFeature = TestData.SampleFeatures[4] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 10, RelFeature = TestData.SampleFeatures[10] },
                    new ProgrammesFeature() { ProgrammeId = 0, FeatureId = 12, RelFeature = TestData.SampleFeatures[12] },
                }
            };

            Programme prog2 = new Programme()
            {
                Id = 1,
                Title = "Test2",
                ProgrammesFeatures = new List<ProgrammesFeature>()
                {
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 1, RelFeature = TestData.SampleFeatures[1] },
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 2, RelFeature = TestData.SampleFeatures[2] },
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 8, RelFeature = TestData.SampleFeatures[8] },
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 4, RelFeature = TestData.SampleFeatures[4] },
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 10, RelFeature = TestData.SampleFeatures[10] },
                    new ProgrammesFeature() { ProgrammeId = 1, FeatureId = 12, RelFeature = TestData.SampleFeatures[12] },
                }
            };

            // act
            double sim = calculator.TotalSimilarity(testUser, prog1, prog2);
            // assert
            Assert.Equal(0.9, sim, 3);
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
            //act
            double sim = calculator.TotalSimilarity(testUser, prog1, prog1);
            // assert
            Assert.Equal(1.0, sim, 3);
        }
    }
}
