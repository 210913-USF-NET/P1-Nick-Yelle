using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DL;
using Models;

namespace Tests
{
    public class DLTests
    {
        private readonly DbContextOptions<P1DBContext> options;

        public DLTests()
        {
            options = new DbContextOptionsBuilder<P1DBContext>()
            .UseSqlite("Filename=Test.db").Options;

            Seed();
        }

        [Fact]
        public void GetBreweriesWorks()
        {
            using(var context = new P1DBContext(options))
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
            using(var context = new P1DBContext(options))
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
            using(var context = new P1DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);

                //Act
                var Brews = repo.GetBrews(1);

                //Assert
                Assert.Single(Brews);
            }
        }

        [Fact]
        public void AddCustomerWorks()
        {
            using(var context = new P1DBContext(options))
            {
                //Arrange
                ISRepo repo = new DBRepo(context);
                Models.Customer custToAdd = new Models.Customer(){
                    UserName = "Nancy"
                };

                //Act
                repo.AddCustomer(custToAdd);
            }

            using(var context = new P1DBContext(options))
            {
                //Assert
                Customer cust = context.Customers.FirstOrDefault(c => c.Id == 1);

                Assert.NotNull(cust);
                Assert.Equal("Nancy", cust.UserName);
            }
        }

        private void Seed()
        {
            using(var context = new P1DBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Breweries.Add(
                    new Brewery(){
                        Id = 1,
                        Name = "Lord Hobo",
                        City = "Boston",
                        State = "MA"
                    }
                );

                context.Breweries.Add(
                    new Brewery(){
                        Id = 2,
                        Name = "Harpoon",
                        City = "Boston",
                        State = "MA"
                    }
                );

                context.Brews.AddRange(
                    new Brew(){
                        Id = 1,
                        Name = "Boomsauce",
                        BreweryId = 1,
                        Price = 10,
                        Quantity = 50
                    }
                );

                context.Brews.AddRange(
                    new Brew(){
                        Id = 2,
                        Name = "Hazy",
                        BreweryId = 2,
                        Price = 11,
                        Quantity = 50
                    }
                );
                context.SaveChanges();
            }
        }
    }
}