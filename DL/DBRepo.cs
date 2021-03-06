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
            return (from c in _context.Customers
                    where cust.UserName == c.UserName
                    select c).SingleOrDefault();
        }

        public Customer GetCustomer(Order o)
        {
            return (from c in _context.Customers
                    where o.CustomerId == c.Id
                    select c).SingleOrDefault();
        }
        public Customer GetCustomer(int id)
        {
            return (from c in _context.Customers
                    where c.Id == id
                    select c).SingleOrDefault();
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
            UpdateOrderTotal(order);
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
            UpdateOrderTotal(orderId);
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
            //Get order based on Id, changed it, order placed variable to true,
            //and add the date/time when placed.
            Order order = (from o in _context.Orders 
                                where o.Id == orderId
                                select o).SingleOrDefault();
            
            UpdateOrderTotal(orderId);
            order.OrderPlaced = true;
            order.DateTimePlaced = DateTime.Now;

            _context.Update(order);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            List<OrderItem> oiList = GetOrderItems(orderId);
            Brew brew;

            //Go through the list of order items and decrement each one based on the quantity in the cart.
            foreach(OrderItem i in oiList)
            {

                brew = (from b in _context.Brews
                        where b.Id == i.BrewId
                        select b).SingleOrDefault();

                brew.Quantity -= i.Quantity;

                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }

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
                        OrderPlaced = o.OrderPlaced,
                        DateTimePlaced = o.DateTimePlaced,
                        Total = o.Total
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
            return (from b in _context.Brews
                    where b.BreweryId == BreweryId
                    select b).ToList();
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

        public object Update(object thing)
        {
            thing = _context.Update(thing).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return thing;
        }

        public object Add(object thing)
        {
            thing = _context.Add(thing).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return thing;
        }

        public void Remove(object thing)
        {
            thing = _context.Remove(thing).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public Order UpdateOrderTotal(Order o)
        {
            List<OrderItem> oiList = GetOrderItems(o.Id);
            int total = 0;
            foreach(OrderItem oi in oiList)
            {
                total += GetBrewById(oi.BrewId).Price * oi.Quantity;
            }
            o.Total = total;
            o = _context.Update(o).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return o;
        }

        public Order UpdateOrderTotal(int OrderId)
        {
            List<OrderItem> oiList = GetOrderItems(OrderId);
            int total = 0;
            foreach (OrderItem oi in oiList)
            {
                total += GetBrewById(oi.BrewId).Price * oi.Quantity;
            }
            Order o = GetOrderById(OrderId);
            o.Total = total;
            o = _context.Update(o).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return o;
        }

        public List<Order> GetOrdersNewToOld(Customer cust)
        {
            return (from o in _context.Orders
                    where o.CustomerId == cust.Id &&
                    o.OrderPlaced == true
                    orderby o.DateTimePlaced descending
                    select new Models.Order()
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        OrderPlaced = o.OrderPlaced,
                        DateTimePlaced = o.DateTimePlaced,
                        Total = o.Total
                    }).ToList();
        }

        public List<Order> GetOrdersOldToNew(Customer cust)
        {
            return (from o in _context.Orders
                    where o.CustomerId == cust.Id &&
                    o.OrderPlaced == true
                    orderby o.DateTimePlaced ascending
                    select new Models.Order()
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        OrderPlaced = o.OrderPlaced,
                        DateTimePlaced = o.DateTimePlaced,
                        Total = o.Total
                    }).ToList();
        }

        public List<Order> GetOrdersHighToLow(Customer cust)
        {
            return (from o in _context.Orders
                    where o.CustomerId == cust.Id &&
                    o.OrderPlaced == true
                    orderby o.Total descending
                    select new Models.Order()
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        OrderPlaced = o.OrderPlaced,
                        DateTimePlaced = o.DateTimePlaced,
                        Total = o.Total
                    }).ToList();
        }

        public List<Order> GetOrdersLowToHigh(Customer cust)
        {
            return (from o in _context.Orders
                    where o.CustomerId == cust.Id &&
                    o.OrderPlaced == true
                    orderby o.Total ascending
                    select new Models.Order()
                    {
                        Id = o.Id,
                        CustomerId = o.CustomerId,
                        OrderPlaced = o.OrderPlaced,
                        DateTimePlaced = o.DateTimePlaced,
                        Total = o.Total
                    }).ToList();
        }
    }
}