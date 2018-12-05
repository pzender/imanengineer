using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Logic;
using Logic.FileReader;
using System.Xml.Linq;
using System.Linq;
using Logic.Entities;
using Logic.Database;

namespace LogicTests
{
    public class XMLParserTests
    {
        [Fact]
        public void ParseChannel_ShouldAddToChannels()
        {
            //Arrange
            FakeRepository<Channel> FakeChannels = new FakeRepository<Channel>();
            FakeRepository<GuideUpdate> FakeUpdates = new FakeRepository<GuideUpdate>();

            XMLParser test = new XMLParser(FakeChannels, FakeUpdates);
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

            Assert.NotNull(FakeChannels.content.Single());
        }

        [Fact]
        public void ParseChannel_ShouldAddToChannels_OnlyOne()
        {
            //Arrange
            FakeRepository<Channel> FakeChannels = new FakeRepository<Channel>();
            FakeRepository<GuideUpdate> FakeUpdates = new FakeRepository<GuideUpdate>();

            XMLParser test = new XMLParser(FakeChannels, FakeUpdates);
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

            Assert.NotNull(FakeChannels.content.Single());
        }

        [Fact]
        public void ParseProgramme_ShouldAddToProgrammes()
        {
            //Arrange
            FakeRepository<Programme> FakeProgrammes = new FakeRepository<Programme>();
            FakeRepository<Feature> FakeFeatures = new FakeRepository<Feature>();
            FakeConnectionRepository<FeatureExample, Programme, Feature> FakeExamples = new FakeConnectionRepository<FeatureExample, Programme, Feature>();

            XMLParser test = new XMLParser(programmeRepository: FakeProgrammes, featureRepository: FakeFeatures, featureExampleRepository: FakeExamples);
            test.currentUpdate = new GuideUpdate()
            {
                id = 1,
                posted = DateTime.Now,
                source = "http://www.programtv.onet.pl"
            };

            XElement programme = XElement.Parse("<programme start=\"20181023202500 + 0200\" stop=\"20181023223000 + 0200\" channel=\"Eleven\">" +
                "<title lang=\"pl\">Koszykówka mężczyzn: Liga Mistrzów - mecz fazy grupowej: Sidigas Avellino - Anwil Włocławek</title>" +
                "<category lang = \"pl\">Koszykówka</category>" +
                "<icon src = \"http://ocdn.eu/images/program-tv/YWU7MDA_/315b6d4433c5ce9dbd2b3180f3819021.jpg\"/>" +
                "</programme>"
            );

            //Act
            test.ParseProgramme(programme);

            //Assert
            Assert.NotNull(FakeProgrammes.content.Single());

        }

        [Fact]
        public void ParseProgramme_ShouldAddToFeatures_OnlyOnce()
        {
            //Arrange
            FakeRepository<Programme> FakeProgrammes = new FakeRepository<Programme>();
            FakeRepository<Feature> FakeFeatures = new FakeRepository<Feature>();
            FakeConnectionRepository<FeatureExample, Programme, Feature> FakeExamples = new FakeConnectionRepository<FeatureExample, Programme, Feature>();

            XMLParser test = new XMLParser(programmeRepository: FakeProgrammes, featureRepository: FakeFeatures, featureExampleRepository: FakeExamples);
            test.currentUpdate = new GuideUpdate()
            {
                id = 1,
                posted = DateTime.Now,
                source = "http://www.programtv.onet.pl"
            };

            XElement programme = XElement.Parse("<programme start=\"20181023202500 + 0200\" stop=\"20181023223000 + 0200\" channel=\"Eleven\">" +
                "<title lang=\"pl\">Koszykówka mężczyzn: Liga Mistrzów - mecz fazy grupowej: Sidigas Avellino - Anwil Włocławek</title>" +
                "<category lang = \"pl\">Koszykówka</category>" +
                "<icon src = \"http://ocdn.eu/images/program-tv/YWU7MDA_/315b6d4433c5ce9dbd2b3180f3819021.jpg\"/>" +
                "</programme>"
            );

            //Act
            test.ParseProgramme(programme);
            test.ParseProgramme(programme);

            //Assert
            Assert.NotNull(FakeFeatures.content.Single());

        }


        [Fact]
        public void ParseDescription_ShouldAddToDescriptions_OnlyOnce()
        {
            //Arrange
            FakeRepository<Programme> FakeProgrammes = new FakeRepository<Programme>();
            FakeRepository<Description> FakeDescriptions = new FakeRepository<Description>();

            XMLParser test = new XMLParser(programmeRepository: FakeProgrammes, descriptionRepository : FakeDescriptions);
            test.currentUpdate = new GuideUpdate()
            {
                id = 1,
                posted = DateTime.Now,
                source = "http://www.programtv.onet.pl"
            };

            XElement description = new XElement(
                "desc",
                new XAttribute("lang", "pl"),
                "Aulus prosi Antedię o cierpliwość. Wszystko przez Kerrę, która zgodziła się negocjować w sprawie uwolnienia Vitusa. Podczas gdy Rzymianie i Celtowie omawiają szczegóły ugody, Aulus zaczyna wypytywać o Cait, czym wzbudza podejrzenia Kerry. Nowa królowa decyduje się na kłamstwo i twierdzi, że nie poznała nigdy dziewczynki. Tymczasem z krainy umarłych powraca Divis, którego w zaświatach zaatakowało stworzenie podobne do węża. Po przebudzeniu szybko orientuje się, że władzę nad jego umysłem przejął demon Pwykka. Duch zna położenie Cait, znajdującej się właśnie w Crugdunon, gdzie trwają rozmowy Aulusa i Kerry. Ta ostatnia uświadamia sobie, że przepowiednia o córce ślepego ojca mówi nie o niej samej, ale o Cait. Przy okazji nagłego zamieszania młoda władczyni pomaga dziewczynce i Sawyerowi w ucieczce przed Pwykką. Widząc Cait i Kerrę razem, Aulus zdaje sobie sprawę z oszustwa królowej plemienia Cantii. Szybko wraca do obozu, aby rozkazać Antendii rozpoczęcie oblężenia(n)"
            );

            //Act
            test.ParseDescription(description, 1, 1);
            test.ParseDescription(description, 1, 1);

            //Assert
            Assert.NotNull(FakeDescriptions.content.Single());
        }

        [Fact]
        public void ParseCredits_ShouldAddToFeatures_OnlyOnce()
        {
            //Arrange
            FakeRepository<Feature> FakeFeatures = new FakeRepository<Feature>();
            FakeConnectionRepository<FeatureExample, Programme, Feature> FakeExamples = new FakeConnectionRepository<FeatureExample, Programme, Feature>();


            XMLParser test = new XMLParser(featureRepository: FakeFeatures, featureExampleRepository: FakeExamples);
            XElement credits = new XElement("credits",
                new XElement("director", "Sheree Folkson"),
                new XElement("actor", "Kelly Reilly"),
                new XElement("writer", "Barry Ward")
            );

            //Act
            test.ParseCredits(credits, 1);
            test.ParseCredits(credits, 1);
            //Assert
            Assert.Collection(FakeFeatures.content,
                item => { Assert.Contains("director", item.type); Assert.Contains("Sheree Folkson", item.value); },
                item => { Assert.Contains("actor", item.type); Assert.Contains("Kelly Reilly", item.value); },
                item => { Assert.Contains("writer", item.type); Assert.Contains("Barry Ward", item.value); }
            );
        }


        [Fact]
        public void ParseFeature_ShouldAddToFeatures_OnlyOnce()
        {
            //Arrange
            FakeRepository<Feature> FakeFeatures = new FakeRepository<Feature>();
            FakeConnectionRepository<FeatureExample, Programme, Feature> FakeExamples = new FakeConnectionRepository<FeatureExample, Programme, Feature>();

            XMLParser test = new XMLParser(featureRepository: FakeFeatures, featureExampleRepository: FakeExamples);
            //Act
            test.ParseFeature(XElement.Parse("<country>USA</country>"), 1);
            test.ParseFeature(XElement.Parse("<country>USA</country>"), 1);
            //Assert
            Assert.Collection(FakeFeatures.content,
                item => { Assert.Contains("country", item.type); Assert.Contains("USA", item.value); }
            );
        }

        [Fact]
        public void ParseFeature_ShouldAddToExamples()
        {
            //Arrange
            FakeRepository<Feature> FakeFeatures = new FakeRepository<Feature>();
            FakeConnectionRepository<FeatureExample, Programme, Feature> FakeExamples = new FakeConnectionRepository<FeatureExample, Programme, Feature>();

            XMLParser test = new XMLParser(featureRepository: FakeFeatures, featureExampleRepository: FakeExamples);
            //Act
            test.ParseFeature(XElement.Parse("<country>USA</country>"), 1);
            //Assert
            Assert.Collection(FakeExamples.content,
                item => { Assert.Equal(1, item.t1_id); Assert.Equal(1, item.t2_id); }
            );
        }

    }
}
