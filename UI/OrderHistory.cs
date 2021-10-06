using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;

namespace UI
{
    public class OrderHistory : IMenu
    {
        private ISBL _bl;

        public OrderHistory(ISBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            bool exit = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] See order history of a Brewery");
                Console.WriteLine("[2] See order history of a Customer");
                Console.WriteLine("[x] Back to Manager Menu");

                switch(Console.ReadLine())
                {
                    case "1":
                        
                        break;
                    case "2":
                        ViewCustOrderHistory();
                        break;

                    case "3":
                        // ChangeInventory();
                        break;

                    case "x":
                        exit = true;
                        Console.WriteLine("Bye.");
                        break;

                    default:
                        Console.WriteLine("I don't understand.");
                        break;
                }
            } while (!exit);
        }

        private void ViewCustOrderHistory()
        {
            Console.WriteLine();
            //Prints customers.
            List<Customer> custs = _bl.GetCustomers();
            for(int i = 0; i < custs.Count; i++)
            {
                Console.WriteLine($"[{i}] {custs[i].UserName} {custs[i].Id}");
            }
            //Gets index of customer to search.
            Console.WriteLine("Choose a Customer.");
            int custIndex = Int32.Parse(Console.ReadLine());
            Customer custToView = custs[custIndex];

            //Get all orders of customer to search.
            List<Order> orders = _bl.GetOrders(custToView);

            Console.WriteLine($":: All Orders of {custToView.UserName} ::");
            if(orders.Count == 0)
            {
                Console.WriteLine("No Previous Orders Filled.");
            }
            for(int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"---Order Number {i + 1}---");
                List<OrderItem> oi = _bl.GetOrderItems(orders[i].Id);
                for(int j = 0; j < oi.Count; j++)
                {
                    //Log to console the order quantity and the name of the Brew ordered.
                    Console.WriteLine($"{oi[j].Quantity} {_bl.GetBrewById(oi[j].BrewId).ToString()}");
                }
            }
        }
    }
}