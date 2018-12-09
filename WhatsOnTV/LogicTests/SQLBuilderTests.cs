using DataAccess;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LogicTests
{
    public class SQLBuilderTests
    {
        [Fact]
        public void BuildInsert_ShouldReturnCorrectSQLSimple()
        {
            //Arrange
            Channel tst = new Channel()
            {
                name = "TVP1",
                icon_url = "http://bla.bla.bla.pl"
            };
            string expected = "INSERT INTO Channel (name, icon_url) VALUES\n" +
                "('TVP1', 'http://bla.bla.bla.pl')\n" +
                ";";
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            //Act
            string actual = tstBuilder.BuildInsert(new List<Channel>() { tst });
            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildInsert_ShouldReturnCorrectSQLWithSelectID()
        {
            //Arrange
            Description tst = new Description()
            {
                content = "W Bochni swoją pizzerię prowadzi Renata. Kiedyś już zarządzała ona tego typu lokalem, ale w innym mieście. Nazwa restauracji ma zatem symboliczne znaczenie. Niestety w lokalu jest brudno, a używane do dań produkty są słabej jakości. Właścicielka za to chce jak najbardziej oszczędzać na jedzeniu. Tę sytuację może zmienić tylko Magda Gessler",
                programme_id = 1, // "Kuchenne rewolucje",
                guideupdate_id = 3 //"http://www.programtv.onet.pl"
            };
            string expected = "INSERT INTO Description (content, guideupdate_id, programme_id) VALUES\n" +
                "('W Bochni swoją pizzerię prowadzi Renata. Kiedyś już zarządzała ona tego typu lokalem, ale w innym mieście. Nazwa restauracji ma zatem symboliczne znaczenie. Niestety w lokalu jest brudno, a używane do dań produkty są słabej jakości. Właścicielka za to chce jak najbardziej oszczędzać na jedzeniu. Tę sytuację może zmienić tylko Magda Gessler', " +
                "3, " +
                "1" +
                ")\n;";
            SQLBuilder<Description> tstBuilder = new SQLBuilder<Description>();
            //Act
            string actual = tstBuilder.BuildInsert(new List<Description>() { tst });
            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BuildInsert_ShouldReturnCorrectSQLEscapedQuote()
        {
            //Arrange
            Description tst = new Description()
            {
                content = "Let's GO!",
                programme_id = 1,
                guideupdate_id = 3
            };
            string expected = $"INSERT INTO Description (content, guideupdate_id, programme_id) VALUES\n" +
                "('Let''s GO!', 3, 1)\n" +
                ";";
            SQLBuilder<Description> tstBuilder = new SQLBuilder<Description>();
            //Act
            string actual = tstBuilder.BuildInsert(new List<Description>() { tst });
            //Assert

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void BuildSelect_NoFieldNoFilterNoOrder_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT * FROM Channel ;";
            //Act
            string actual = tstBuilder.BuildSelect();
            //Assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void BuildSelect_FieldNoFilterNoOrder_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT id FROM Channel ;";
            //Act
            string actual = tstBuilder.BuildSelect(field: "id");
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void BuildSelect_NoFieldFilter_StringNoOrder_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT * FROM Channel WHERE 1=1 AND Channel.name LIKE 'POLSAT' ;";
            //Act
            string actual = tstBuilder.BuildSelect(where: new Dictionary<string, string>() { { "name", "POLSAT" } });
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void BuildSelect_NoFieldFilter_NumberNoOrder_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT * FROM Channel WHERE 1=1 AND Channel.id = 7 ;";
            //Act
            string actual = tstBuilder.BuildSelect(where: new Dictionary<string, string>() { { "id", "7" } });
            //Assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void BuildSelect_NoFieldNoFilterOrder_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT * FROM Channel ORDER BY name ;";
            //Act
            string actual = tstBuilder.BuildSelect(orderby: "name");
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void BuildSelect_FULL_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            string expected = "SELECT id FROM Channel WHERE 1=1 AND Channel.id = 7 AND Channel.name LIKE 'POLSAT' ORDER BY name ;";
            //Act
            string actual = tstBuilder.BuildSelect(field: "id", where: new Dictionary<string, string>() { { "id", "7" }, { "name", "POLSAT" } }, orderby: "name");
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void BuildUpdate_ShouldReturnCorrectSQL()
        {
            //Arrange
            SQLBuilder<Series> tstBuilder = new SQLBuilder<Series>();
            string expected = "UPDATE Series SET Series.title = 'Kung Fu Panda'\nWHERE 1=1 AND Series.title LIKE 'Kung Fu Panda - legenda o niezwykłości' ;";
            //Act
            string actual = tstBuilder.BuildUpdate(set: new Dictionary<string, string>() { { "title", "Kung Fu Panda" } }, where: new Dictionary<string, string>() { { "title", "Kung Fu Panda - legenda o niezwykłości" } });
            //Assert
            Assert.Equal(expected, actual);

        }


    }
}
    