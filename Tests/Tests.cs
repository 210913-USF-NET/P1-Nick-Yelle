using System;
using Models;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void BrewsShouldSetValidData()
        {
            //Arrange.
            Brew test = new Brew();
            string testName = "test brew";

            //Act.
            test.Name = testName;

            //Assert.
            Assert.Equal(testName, test.Name);
        }

        // [Theory]
        // public void BrewShouldNotSetInvalidData(string input)
        // {
        //     //Arrange.
        // }
    }
}
