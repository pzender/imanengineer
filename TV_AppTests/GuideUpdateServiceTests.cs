using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using TV_App.Models;
using TV_App.Services;
using Xunit;

namespace TV_AppTests
{
    public class GuideUpdateServiceTests
    {
        
        
        [Fact]
        public void ParseAllShouldAddChannelsOnce()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestChannels.xml");
            // act
            service.ParseAll(mockData);
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Channels,
                el => Assert.Equal("TTV", el.Name),
                el => Assert.Equal("Stopklatka TV", el.Name)
            );
        }

        [Fact]
        public void ParseAllShouldAddProgrammeOnce()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");
            // act
            service.ParseAll(mockData);
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Programmes,
                el => Assert.Equal("X-Men: Apocalypse", el.Title)
            );
        }

        [Fact]
        public void ParseAllShouldAddEmission()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");
            // act
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Emissions,
                el => Assert.Equal(0, el.ChannelId)
            );

        }

        [Fact]
        public void ParseAllShouldThrowExceptionIfNoEmission()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesMissingEmission.xml");
            // act
            Assert.Throws<DataException>(() => service.ParseAll(mockData));

        }

        [Fact]
        public void ParseAllShouldAddSeparateEmissions()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesSeparateEmissions.xml");
            // act
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Emissions,
                el => Assert.Equal(0, el.ChannelId),
                el => Assert.Equal(0, el.ChannelId)
            );

        }

        [Fact]
        public void ParseAllShouldAddFeatures()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");
            // act
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Features,
                el => {
                    Assert.Equal("Jennifer Lawrence", el.Value);
                    Assert.Equal(4, el.Type);
                },
                el => {
                    Assert.Equal("USA", el.Value);
                    Assert.Equal(1, el.Type);
                },

                el => {
                    Assert.Equal("Film", el.Value);
                    Assert.Equal(7, el.Type);
                },
                el => {
                    Assert.Equal("2016", el.Value);
                    Assert.Equal(2, el.Type);
                }
            );

        }

        [Fact]
        public void ParseAllShouldConnectNewFeaturesToProgrammes()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");
            // act
            service.ParseAll(mockData);
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.ProgrammesFeatures,
                el => { },
                el => { },
                el => { },
                el => { }
            );

        }

        [Fact]
        public void ParseAllShouldConnectExistingFeaturesToProgrammes()
        {
            // arrange
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");

            context.Features.AddRange(new List<Feature>()
            {
                new Feature() { Id = 0, Type = 4, Value = "Jennifer Lawrence" },
                new Feature() { Id = 1, Type = 1, Value = "USA" },
                new Feature() { Id = 2, Type = 7, Value = "Film" },
                new Feature() { Id = 3, Type = 2, Value = "2016" },
            });
            // act
            service.ParseAll(mockData);
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.ProgrammesFeatures,
                el => { Assert.Equal(0, el.RelFeature.Id); },
                el => { Assert.Equal(1, el.RelFeature.Id); },
                el => { Assert.Equal(2, el.RelFeature.Id); },
                el => { Assert.Equal(3, el.RelFeature.Id); }
            );

        }


        [Fact]
        public void ParseAllShouldAddDescriptionOnce()
        {
            // arrange 
            TvAppContext context = new MockContext();
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesBasic.xml");
            // act
            service.ParseAll(mockData);
            service.ParseAll(mockData);
            // assert
            Assert.Collection(context.Descriptions,
                el => Assert.Equal(0, el.ProgrammeId)
            );

        }

    }
}
