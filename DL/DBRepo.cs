using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
//using Entity = DL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class DBRepo : ISRepo
    {

        //Fields.
        private P1DBContext _context;

        //Constructor.
        public DBRepo(P1DBContext context)
        {
            _context = context;
        }

        //Methods.
        public List<OrderItem> GetOrderItems(int orderId)
        {
            return (from oi in _context.OrderItems 
                        where oi.OrderId == orderId 
                        select new Models.OrderItem{
                            Id = oi.Id,
                            OrderId = oi.OrderId,
                            BrewId = oi.BrewId,
                            Quantity = oi.Quantity
                        }).ToList();
        }
        public List<Brewery> GetBreweries()
        {
            return _context.Breweries.Select(
                Brewery => new Models.Brewery() {
                    Id = Brewery.Id,
                    Name = Brewery.Name,
                    State = Brewery.State,
                    City = Brewery.City
                }
            ).ToList();
        }

        public List<Brew> GetBrews()
        {
            return _context.Brews.Select(
                Brew => new Models.Brew() {
                    Id = Brew.Id,
                    Name = Brew.Name,
                    Price = Brew.Price,
                    Quantity = Brew.Quantity,
                    BreweryId = Brew.BreweryId,
                    Brewery = Brew.Brewery
                }
            ).ToList();
        }

        public Customer AddCustomer(Customer cust)
        {   //Check if customer with that username already exists.
            Customer existingCust = (from c in _context.Customers 
                                where c.UserName == cust.UserName 
                                select c).SingleOrDefault();
            
            if (existingCust == null)
            {
                Customer newCust = new()
                {
                    UserName = cust.UserName,
                    Password = cust.Password
                };

                newCust = _context.Add(newCust).Entity;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();

                return newCust;
            } else
            {
                return null;
            }
        }

        public Customer Login(Customer cust)
        {
            Customer custToCheck = (from c in _context.Customers
                                    where cust.UserName == c.UserName
                                    select c).SingleOrDefault();
            return custToCheck;
        }

        public Order GetOrder(Customer cust)
        {
            Order retrievedOrder = (from o in _context.Orders
                                    where o.CustomerId == cust.Id && o.OrderPlaced == false
                                    select o).SingleOrDefault();

            if(retrievedOrder != null)
            {
                return retrievedOrder;
            } 
            else
            {
                Order createdOrder = new Order()
                {
                    CustomerId = cust.Id,
                    OrderPlaced = false
                };
                createdOrder = _context.Add(createdOrder).Entity;
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
                return createdOrder;
            }
        }
        /// <summary>
        /// Returns an Order given its Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Order</returns>
        public Order GetOrderById(int Id)
        {
            return (from o in _context.Orders
                    where o.Id == Id
                    select o).SingleOrDefault();
        }

        public OrderItem AddBrewToOrder(Order order, Brew brew, int quantity)
        {
            OrderItem eoi = new OrderItem()
            {
                OrderId = order.Id,
                BrewId = brew.Id,
                Quantity = quantity
            };

            eoi = _context.Add(eoi).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            OrderItem returnOrderItem = new OrderItem()
            {
                Id = eoi.Id,
                OrderId = eoi.OrderId,
                BrewId = eoi.BrewId,
                Quantity = eoi.Quantity
            };
            return returnOrderItem;
        }

        public OrderItem AddBrewToOrder(int orderId, int brewId, int quantity)
        {
            OrderItem eoi = new OrderItem()
            {
                OrderId = orderId,
                BrewId = brewId,
                Quantity = quantity
            };

            eoi = _context.Add(eoi).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return eoi;
        }



        public Brew GetBrewById(int brewId)
        {
            return (from oi in _context.Brews
                         where oi.Id == brewId
                         select new Models.Brew()
                         {
                             Name = oi.Name,
                             Id = oi.Id,
                             Price = oi.Price,
                             Quantity = oi.Quantity,
                             BreweryId =oi.BreweryId
                         }).SingleOrDefault();
        }

        public Brew AddBrew(Brew brew)
        {
            Brew b = new Brew()
            {
                Name = brew.Name,
                Price = brew.Price,
                Quantity = brew.Quantity,
                BreweryId = brew.BreweryId,
                Brewery = brew.Brewery
            };

            b = _context.Add(b).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return b;
        }

        public Order PlaceOrder(int orderId)
        {
            Order order = (from o in _context.Orders 
                                where o.Id == orderId
                                select o).SingleOrDefault();

            order.OrderPlaced = true;
            order.DateTimePlaced = DateTime.Now;

            List<OrderItem> oiList = GetOrderItems(orderId);

            //Go through the list of order items and decrement each one based on the quantity in the cart.
            foreach(OrderItem i in oiList)
            {
                Brew b = GetBrewById(i.BrewId);

            }

            Brew brew = (from b in _context.Brews
                         where b.Id == )

            _context.SaveChanges();
            _context.ChangeTracker.Clear();



            return order;
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.Select(
                Customer => new Models.Customer() {
                    Id = Customer.Id,
                    UserName = Customer.UserName,
                    Password = Customer.Password
                }
            ).ToList();
        }

        public List<Order> GetOrders(Customer c)
        {
            return (from o in _context.Orders
                    where o.CustomerId == c.Id && 
                    o.OrderPlaced == true
                    select new Models.Order(){
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        OrderPlaced = o.OrderPlaced
                    }).ToList();
        }

        public Brewery AddBrewery(Brewery brewery)
        {
            Brewery b = new Brewery()
            {
                Name = brewery.Name,
                City = brewery.City,
                State = brewery.State
            };

            b = _context.Add(b).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return brewery;
        }

        public Brewery GetBreweryById(int id)
        {
            Brewery brewery = (from b in _context.Breweries 
                    where b.Id == id
                    select new Models.Brewery(){
                        Name = b.Name,
                        Id = b.Id,
                        City = b.City,
                        State = b.State
                    }).SingleOrDefault();

            //Returning first item in list because I know the query will return exactly 1 Brewery.
            return brewery;
        }

        public List<Brew> GetBrews(int BreweryId)
        {
            throw new NotImplementedException();
        }

        public Brew UpdateBrew(Brew brew)
        {
            Brew updatedBrew = (from b in _context.Brews
                                where b.Id == brew.Id
                                select b).SingleOrDefault();

            updatedBrew.Quantity = brew.Quantity;
            updatedBrew.Price = brew.Price;

            Brew newerBrew = new Models.Brew()
            {
                Id = updatedBrew.Id,
                Name = updatedBrew.Name,
                BreweryId = updatedBrew.BreweryId,
                Price = updatedBrew.Price,
                Quantity = updatedBrew.Quantity,
                Brewery = updatedBrew.Brewery
            };

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return newerBrew;
        }

        public void UpdateBrewery(Brewery brewery)
        {
            Brewery breweryToUpdate = new()
            {
                Id = brewery.Id,
                Name = brewery.Name,
                City = brewery.City,
                State = brewery.State
            };

            breweryToUpdate = _context.Breweries.Update(breweryToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void RemoveBrewery(Brewery brewery)
        {
            Brewery breweryToDelete = new()
            {
                Id = brewery.Id,
                Name = brewery.Name,
                City = brewery.City,
                State = brewery.State
            };

            breweryToDelete = _context.Breweries.Remove(breweryToDelete).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public void RemoveBrew(Brew brew)
        {
            Brew brewToDelete = new()
            {
                Id = brew.Id,
                Name = brew.Name,
                Price = brew.Price,
                Quantity = brew.Quantity,
                BreweryId = brew.BreweryId
            };

            brewToDelete = _context.Brews.Remove(brewToDelete).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }
    }
}