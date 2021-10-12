using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using DL;

namespace BL
{
    public class ShopLogic : ISBL
    {
        //Properties.
        private ISRepo _repo;

        //Constructor.
        public ShopLogic(ISRepo repo)
        {
            _repo = repo;
        }

        public List<Brewery> GetBreweries()
        {
            return _repo.GetBreweries();
        }

        public List<Brew> GetBrews()
        {
            return _repo.GetBrews();
        }

        public List<Brew> GetBrews(int BreweryId)
        {
            return _repo.GetBrews(BreweryId);
        }

        public Customer AddCustomer(Customer cust)
        {
            return _repo.AddCustomer(cust);
        }

        public Customer Login(Customer cust)
        {
            return _repo.Login(cust);
        }


        public Customer GetCustomer(Order o)
        {
            return _repo.GetCustomer(o);
        }
        public Customer GetCustomer(int id)
        {
            return _repo.GetCustomer(id);
        }

        public Order GetOrder(Customer cust)
        {
            return _repo.GetOrder(cust);
        }
        public Order GetOrderById(int Id)
        {
            return _repo.GetOrderById(Id);
        }

        public OrderItem AddBrewToOrder(Order order, Brew brew, int quantity)
        {
            return _repo.AddBrewToOrder(order, brew, quantity);
        }
        public OrderItem AddBrewToOrder(int OrderId, int BrewId, int quantity)
        {
            return _repo.AddBrewToOrder(OrderId, BrewId, quantity);
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            return _repo.GetOrderItems(orderId);
        }

        public Brew GetBrewById(int Id)
        {
            return _repo.GetBrewById(Id);
        }

        public Brew UpdateBrew(Brew brew)
        {
            return _repo.UpdateBrew(brew);
        }

        public Brew AddBrew(Brew brew)
        {
            return _repo.AddBrew(brew);
        }

        public Order PlaceOrder(int orderId)
        {
            return _repo.PlaceOrder(orderId);
        }

        public List<Customer> GetCustomers()
        {
            return _repo.GetCustomers();
        }

        public List<Order> GetOrders(Customer cust)
        {
            return _repo.GetOrders(cust);
        }

        public Brewery AddBrewery(Brewery b)
        {
            return _repo.AddBrewery(b);
        }

        public Brewery GetBreweryById(int id)
        {
            return _repo.GetBreweryById(id);
        }

        public void UpdateBrewery(Brewery brewery)
        {
            _repo.UpdateBrewery(brewery);
        }

        public void RemoveBrewery(Brewery brewery)
        {
            _repo.RemoveBrewery(brewery);
        }

        public void RemoveBrew(Brew brew)
        {
            _repo.RemoveBrew(brew);
        }

        public object Update(Object thing)
        {
            return _repo.Update(thing);
        }

        public object Add(Object thing)
        {
            return _repo.Add(thing);
        }

        public void Remove(Object thing)
        {
            _repo.Remove(thing);
        }

        public List<Order> GetOrdersNewToOld(Customer cust)
        {
            return _repo.GetOrdersNewToOld(cust);
        }

        public List<Order> GetOrdersOldToNew(Customer cust)
        {
            return _repo.GetOrdersOldToNew(cust);
        }

        public List<Order> GetOrdersHighToLow(Customer cust)
        {
            return _repo.GetOrdersHighToLow(cust);
        }

        public List<Order> GetOrdersLowToHigh(Customer cust)
        {
            return _repo.GetOrdersLowToHigh(cust);
        }
    }
}