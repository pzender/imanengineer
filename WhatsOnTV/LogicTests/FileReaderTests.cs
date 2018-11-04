using System;
using System.Collections.Generic;
using System.Text;
using Logic;
using Logic.FileReader;
using Xunit;

namespace LogicTests
{
    public class FileReaderTests
    {
        [Fact]
        public void ReadFile_ShouldReturnString()
        {
            //Arrange
            IFileReader fileReader = new FileReaderImpl();
            //Act
            string actual = fileReader.ReadFile("guide.xml");
            //Assert
            Assert.True(actual.Length > 0);
        }
    }
}
