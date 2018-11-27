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
        public void BuildInsert_ShouldReturnCorrectSQL()
        {
            //Arrange
            Channel tst = new Channel()
            {
                id = 1,
                name = "TVP1",
                icon_url = "http://bla.bla.bla.pl"
            };
            string expected = "INSERT INTO Channel (id, name, icon_url) VALUES\n" +
                "(1, 'TVP1', 'http://bla.bla.bla.pl')\n" +
                ";";
            SQLBuilder<Channel> tstBuilder = new SQLBuilder<Channel>();
            //Act
            string actual = tstBuilder.BuildInsert(new List<Channel>() { tst});
            //Assert

            Assert.True(expected == actual);
        }
    }
}
    