using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Logic;
using Logic.FileReader;
using System.Xml.Linq;
using System.Linq;

namespace LogicTests
{
    public class XMLParserTests
    {
        [Fact]
        public void ParseChannel_ShouldAddChannelToList_IfValid()
        {
            //Arrange

            IXMLParser test = new XMLParserImpl();
            XElement channel = new XElement(
                "channel", 
                new XAttribute("id", "13th Street"),
                new XElement("display-name",
                    new XAttribute("lang", "pl"),
                    "13th Street"
                ),
                new XElement("icon", 
                    new XAttribute("src", "blablabla")
                ),
                new XElement("url", "http://www.programtv.onet.pl")
            );

            //Act

            test.ParseChannel(channel);

            //Assert

            Assert.NotNull(test.ParsedChannels.Single(item => item.name == "13th Street"));
        }

        [Fact]
        public void ParseChannel_ShouldNotAddChannelToList_IfExists()
        {
            //Arrange

            IXMLParser test = new XMLParserImpl();
            XElement channel = new XElement(
                "channel",
                new XAttribute("id", "13th Street"),
                new XElement("display-name",
                    new XAttribute("lang", "pl"),
                    "13th Street"
                ),
                new XElement("icon",
                    new XAttribute("src", "blablabla")
                ),
                new XElement("url", "http://www.programtv.onet.pl")
            );

            //Act

            test.ParseChannel(channel);
            test.ParseChannel(channel);

            //Assert

            Assert.Single(test.ParsedChannels);
        }

        [Fact]
        public void ParseChannel_ShouldAddSourceToDictionary_IfNotThere()
        {
            //Arrange

            XMLParserImpl test = new XMLParserImpl();
            XElement channel = new XElement(
                "channel",
                new XAttribute("id", "13th Street"),
                new XElement("display-name",
                    new XAttribute("lang", "pl"),
                    "13th Street"
                ),
                new XElement("icon",
                    new XAttribute("src", "blablabla")
                ),
                new XElement("url", "http://www.programtv.onet.pl")
            );

            //Act

            test.ParseChannel(channel);
            test.ParseChannel(channel);

            //Assert

            Assert.Equal("http://www.programtv.onet.pl", test.sourceByChannel["13th Street"]);

        }

        [Fact]
        public void ParseDescription_ShouldAddDescriptionToList_IfValid()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");
        }



        [Fact]
        public void ParseEmission_ShouldAddEmissionToList_IfValid()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }

        [Fact]
        public void ParseEmission_ShouldNotAddEmissionToList_IfExists()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }



        [Fact]
        public void ParseFeature_ShouldAddFeatureToListIfValid()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }

        [Fact]
        public void ParseFeature_ShouldAddFeatureToListWithExistingProgrammeId_IfAvailable()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }


        [Fact]
        public void ParseProgramme_ShouldAddProgrammeToListIfValid()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }

        [Fact]
        public void ParseProgramme_ShouldNotAddProgrammeToListIfExists()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }


        [Fact]
        public void ParseCredits_ShouldAddCreditsToListIfValid()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }

        [Fact]
        public void ParseChannel_ShouldThrowExceptionIfInvaild()
        {
            //Arrange

            IXMLParser test = new XMLParserImpl();
            XElement channel = new XElement(
                "channel",
                new XElement("display-name",
                    new XAttribute("lang", "pl"),
                    "13th Street"
                ),
                new XElement("icon",
                    new XAttribute("src", "blablabla")
                ),
                new XElement("url", "http://www.programtv.onet.pl")
            );

            //Act


            //Assert

            Assert.Throws<ArgumentException>(()=>test.ParseChannel(channel));

        }

        [Fact]
        public void ParseDescriptionSource_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }
        [Fact]
        public void ParseDescription_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }
        [Fact]
        public void ParseEmission_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }
        [Fact]
        public void ParseFeature_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }
        [Fact]
        public void ParseProgramme_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }
        [Fact]
        public void ParseCredits_ShouldThrowExceptionIfInvaild()
        {
            //Arrange
            //Act
            //Assert
            Assert.True(false, "Test not implemented");

        }


    }
}
