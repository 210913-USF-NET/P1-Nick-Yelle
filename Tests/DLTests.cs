using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Xunit;
using DL;
using Models;

namespace Tests
{
    public class DLTests
    {
        private readonly DbContextOptions<Entity.P0DBContext> options;

        public DLTests()
        {
            options = new DbContextOptionsBuilder<Entity.P0DBContext>()
            .UseSqlite("Filename=Test.db").Options;

            Seed();
        }

        [Fact]
        public void GetBreweriesWorks()
        {
            using(var context = new Entity.P0DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);

                //Act
                var Breweries = repo.GetBreweries();

                //Assert
                Assert.Equal(2, Breweries.Count);
            }
        }

        [Fact]
        public void GetBrewsWorks()
        {
            using(var context = new Entity.P0DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);

                //Act
                var Brews = repo.GetBrews();

                //Assert
                Assert.Equal(2, Brews.Count);
            }
        }

        [Fact]
        public void GetBrewsGivenBreweryIdWorks()
        {
            using(var context = new Entity.P0DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);

                //Act
                var Brews = repo.GetBrews(1);

                //Assert
                Assert.Equal(1, Brews.Count);
            }
        }

        [Fact]
        public void AddCustomerWorks()
        {
            using(var context = new Entity.P0DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);
                Models.Customer custToAdd = new Models.Customer(){
                    UserName = "Nancy"
                };

                //Act
                repo.AddCustomer(custToAdd);
            }

            using(var context = new Entity.P0DBContext(options))
            {
                //Assert
                Entity.Customer cust = context.Customers.FirstOrDefault(c => c.Id == 1);

                Assert.NotNull(cust);
                Assert.Equal(cust.UserName, "Nancy");
            }
        }

        private void Seed()
        {
            using(var context = new Entity.P0DBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Breweries.Add(
                    new Entity.Brewery(){
                        BreweryId = 1,
                        Name = "Lord Hobo",
                        City = "Boston",
                        State = "MA"
                    }
                );

                context.Breweries.Add(
                    new Entity.Brewery(){
                        BreweryId = 2,
                        Name = "Harpoon",
                        City = "Boston",
                        State = "MA"
                    }
                );

                context.Brews.AddRange(
                    new Entity.Brew(){
                        BrewId = 1,
                        Name = "Boomsauce",
                        BreweryId = 1,
                        Price = 10,
                        BrewQuantity = 50
                    }
                );

                context.Brews.AddRange(
                    new Entity.Brew(){
                        BrewId = 2,
                        Name = "Hazy",
                        BreweryId = 2,
                        Price = 11,
                        BrewQuantity = 50
                    }
                );
                context.SaveChanges();
            }
        }
    }
}