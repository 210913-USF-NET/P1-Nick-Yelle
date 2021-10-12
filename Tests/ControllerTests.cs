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
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public void BreweryControllerIndexReturnListOfBreweries()
        {
            //Arrange
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

            //Act.
            var result = controller.Index();

            //Assert.
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Brewery>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

        }

        [Fact]
        public void BrewControllerIndexReturnListOfBrews()
        {
            //Arrange
            var mockBl = new Mock<ISBL>();
            mockBl.Setup(x => x.GetBrews()).Returns(
                new List<Brew>()
                {
                    new Brew()
                    {
                        Id = 1,
                        Name = "Goose Island",
                        Price = 1,
                        Quantity = 5
                    },
                    new Brew()
                    {
                        Id = 2,
                        Name = "Super Brew",
                        Price = 2,
                        Quantity = 5
                    }
                }
            );
            var controller = new BrewController(mockBl.Object);

            //Act.
            var result = controller.Index();

            //Assert.
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Brew>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

        }

        [Fact]
        public void CustomerControllerIndexReturnListOfBrews()
        {
            //Arrange
            var mockBl = new Mock<ISBL>();
            mockBl.Setup(x => x.GetCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer()
                    {
                        Id = 1,
                        UserName = "Goose Island",
                        Password = "Yup"
                    },
                    new Customer()
                    {
                        Id = 2,
                        UserName = "Super Brew",
                        Password = "Yup"
                    }
                }
            );
            var controller = new CustomerController(mockBl.Object);

            //Act.
            var result = controller.Index();

            //Assert.
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());

        }

        [Fact]
        public void CustomerControllerLoginReturnsCustomer()
        {
            //Arrange
            var mockBl = new Mock<ISBL>();
            mockBl.Setup(x => x.GetCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer()
                    {
                        Id = 1,
                        UserName = "Nick Yelle",
                        Password = "Yup"
                    },
                }
            );
            var controller = new CustomerController(mockBl.Object);

            //Act.
            var result = controller.Login();

            //Assert.
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<Customer>(viewResult.ViewData.Model);

            Assert.Equal("Nick Yelle", model.UserName);
        }

        public void MockTesting()
        {
            var mockBl = new Mock<ISBL>();
            mockBl.Setup(x => x.GetCustomers()).Returns(
               new List<Customer>()
               {
                    new Customer()
                    {
                        Id = 1,
                        UserName = "Nick Yelle",
                        Password = "Yup"
                    },
               }
           );
        }
    }
}
