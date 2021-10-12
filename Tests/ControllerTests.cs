using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using BL;
using Models;

namespace Tests
{
    public class ControllerTests
    {
        public void BreweryControllerIndexReturnListOfBreweries()
        {
            var mockBl = new Mock<ISBL>();
            mockBl.Setup(x => x.GetBreweries()).Returns(
                new List<Brewery>()
                {
                    new Brewery()
                    {
                        Id = 1,
                        Name = "Goose Island",
                        City = "Boston",
                        State = "MA"
                    },
                    new Brewery()
                    {
                        Id = 2,
                        Name = "Super Brews",
                        City = "Tampa Bay",
                        State = "Florida"
                    }
                }
            );
            var controller = new BreweryController(mockBl.Object);
        }
    }
}
