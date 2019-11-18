using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TV_App.Models;
using TV_App.Services;
using Xunit;

namespace TV_AppTests
{
    public class RecommendationServiceTests
    {
        

        [Fact]
        public void ShouldReturnRatings()
        {
            var db = setupDb();
            var user = db.Users.Single(u => u.Login == "user1");
            var service = new RecommendationService(db);

            var actual = service.GetRated(user);

            Assert.Collection(actual,
                r1 => Assert.Equal(0, r1.Id),
                r2 => Assert.Equal(1, r2.Id)
            );
            db.Database.EnsureDeleted();   
        }

        [Fact]
        public void ShouldReturnPositiveRatings()
        {
            var db = setupDb();

            var user = db.Users.Single(u => u.Login == "user1");
            var service = new RecommendationService(db);

            var actual = service.GetPositivelyRated(user);

            Assert.Collection(actual,
                r1 => Assert.Equal(0, r1.Id)
            );
            db.Database.EnsureDeleted();
        }



        private static MockContext setupDb()
        {
            MockContext context = new MockContext();
            context.Features.AddRange(TestData.SampleFeatures);
            User testUser1 = new User()
            {
                Login = "user1",
                WeightActor = 0.3,
                WeightCategory = 0.3,
                WeightCountry = 0.1,
                WeightDirector = 0.1,
                WeightKeyword = 0.1,
                WeightYear = 0.1
            };

            User testUser2 = new User()
            {
                Login = "user2",
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
                }
            };
            Programme prog3 = new Programme()
            {
                Id = 2,
                Title = "Test3",
                ProgrammesFeatures = new List<ProgrammesFeature>()
                {
                    new ProgrammesFeature() { ProgrammeId = 2, FeatureId = 1, RelFeature = TestData.SampleFeatures[0] },
                    new ProgrammesFeature() { ProgrammeId = 2, FeatureId = 2, RelFeature = TestData.SampleFeatures[3] },
                }
            };
            Programme prog4 = new Programme()
            {
                Id = 3,
                Title = "Test4",
                ProgrammesFeatures = new List<ProgrammesFeature>()
                {
                    new ProgrammesFeature() { ProgrammeId = 3, FeatureId = 1, RelFeature = TestData.SampleFeatures[1] },
                    new ProgrammesFeature() { ProgrammeId = 3, FeatureId = 2, RelFeature = TestData.SampleFeatures[3] },
                }
            };

            Programme prog5 = new Programme()
            {
                Id = 4,
                Title = "Test5",
                ProgrammesFeatures = new List<ProgrammesFeature>()
                {
                    new ProgrammesFeature() { ProgrammeId = 4, FeatureId = 1, RelFeature = TestData.SampleFeatures[1] },
                    new ProgrammesFeature() { ProgrammeId = 4, FeatureId = 2, RelFeature = TestData.SampleFeatures[3] },
                }
            };

            context.Programmes.Add(prog1);
            context.Programmes.Add(prog2);
            context.Programmes.Add(prog3);
            context.Programmes.Add(prog4);
            context.Programmes.Add(prog5);
            context.Users.Add(testUser1);
            context.Users.Add(testUser2);


            context.Ratings.Add(new Rating()
            {
                ProgrammeId = 0,
                RelProgramme = prog1,
                UserLogin = "test1",
                RelUser = testUser1,
                RatingValue = 1
            });

            context.Ratings.Add(new Rating()
            {
                ProgrammeId = 1,
                RelProgramme = prog2,
                UserLogin = "test1",
                RelUser = testUser1,
                RatingValue = -1
            });

            context.SaveChanges();

            return context;
        }
    }
}
