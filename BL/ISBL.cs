using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace BL
{
    /// <summary>
    /// Interface designed to interact with customers
    /// </summary>
    public interface ISBL
    {   
        Brewery GetBreweryById(int id);
        Brewery AddBrewery(Brewery b);
        List<Brewery> GetBreweries();

        List<Brew> GetBrews();

        List<Brew> GetBrews(int BreweryId);

        Customer AddCustomer(Customer cust);

        Customer Login(Customer cust);

        Order CreateOrder(Order order);

        OrderItem AddBrewToOrder(Order order, Brew brew, int quantity);

        List<OrderItem> GetOrderItems(int orderId);

        Brew GetBrewById(int brewId);

        Brew UpdateBrew(Brew brew);

        Brew AddBrew(Brew brew);

        Order PlaceOrder(int orderId);

        List<Customer> GetCustomers();

        List<Order> GetOrders(Customer cust);

        void UpdateBrewery(Brewery brewery);

        void RemoveBrewery(Brewery brewery);

        void RemoveBrew(Brew brew);

    }
}