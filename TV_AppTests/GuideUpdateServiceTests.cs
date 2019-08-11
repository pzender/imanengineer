using System;
using System.Data;
using System.Xml.Linq;
using TV_App.Models;
using TV_App.Services;
using Xunit;

namespace TV_AppTests
{
    public class GuideUpdateServiceTests
    {
        TvAppContext context = new MockContext();
        
        [Fact]
        public void ParseAllShouldAddChannelsOnce()
        {
            // arrange
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
            GuideUpdateService service = new GuideUpdateService(context);
            XDocument mockData = XDocument.Load("TestData/TestProgrammesMissingEmission.xml");
            // act
            Assert.Throws<DataException>(() => service.ParseAll(mockData));

        }

        [Fact]
        public void ParseAllShouldAddSeparateEmissions()
        {
            // arrange
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
            Assert.True(false);
        }

        [Fact]
        public void ParseAllShouldConnectExistingFeaturesToProgrammes()
        {
            Assert.True(false);
        }

        [Fact]
        public void ParseAllShouldAddDescriptionOnce()
        {
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
