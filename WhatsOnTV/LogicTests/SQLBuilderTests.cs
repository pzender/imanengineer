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
            string actual = tstBuilder.BuildInsert(new List<Channel>() { tst});
            //Assert

            Assert.True(expected == actual);
        }

        [Fact]
        public void BuildInsert_ShouldReturnCorrectSQLWithSelectID()
        {
            //Arrange
            Description tst = new Description()
            {
                content = "W Bochni swoją pizzerię prowadzi Renata. Kiedyś już zarządzała ona tego typu lokalem, ale w innym mieście. Nazwa restauracji ma zatem symboliczne znaczenie. Niestety w lokalu jest brudno, a używane do dań produkty są słabej jakości. Właścicielka za to chce jak najbardziej oszczędzać na jedzeniu. Tę sytuację może zmienić tylko Magda Gessler",
                programme_id = "Kuchenne rewolucje",
                guideupdate_id = "http://www.programtv.onet.pl"

            };
            string expected = "INSERT INTO Description (content, source_id, programme_id) VALUES\n" +
                "('W Bochni swoją pizzerię prowadzi Renata. Kiedyś już zarządzała ona tego typu lokalem, ale w innym mieście. Nazwa restauracji ma zatem symboliczne znaczenie. Niestety w lokalu jest brudno, a używane do dań produkty są słabej jakości. Właścicielka za to chce jak najbardziej oszczędzać na jedzeniu. Tę sytuację może zmienić tylko Magda Gessler', " +
                "(SELECT id from source WHERE name LIKE 'http://www.programtv.onet.pl'), " +
                "(SELECT id from programme WHERE name LIKE 'Kuchenne rewolucje')" +
                ")\n;";
            SQLBuilder<Description> tstBuilder = new SQLBuilder<Description>();
            //Act
            string actual = tstBuilder.BuildInsert(new List<Description>() { tst });
            //Assert

            Assert.True(expected == actual);
        }



    }
}
    