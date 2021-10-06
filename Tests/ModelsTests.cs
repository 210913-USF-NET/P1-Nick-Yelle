using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Xunit;

namespace Tests
{
    public class ModelsTests
    {
        [Fact]
        public void BrewShouldCreate()
        {
            //Arrange and Act.
            Brew brew = new Brew();
            
            //Assert.
            Assert.NotNull(brew);
        }

        [Fact]
        public void BreweryShouldCreate()
        {
            //Arrange and Act.
            Brewery brewery = new Brewery();

            //Assert.
            Assert.NotNull(brewery);
        }
        [Fact]
        public void CustomerShouldCreate()
        {
            //Arrange and Act.
            Customer customer = new Customer();
        
            //Assert.
            Assert.NotNull(customer);
        }

        [Fact]
        public void OrderShouldCreate()
        {
            //Arrange and Act.
            Order order = new Order();

            //Assert.
            Assert.NotNull(order);
        }

        [Fact]
        public void OrderItemShouldCreate()
        {
            //Arrange and Act.
            OrderItem oi = new OrderItem();

            //Assert.
            Assert.NotNull(oi);
        }
    }
}
