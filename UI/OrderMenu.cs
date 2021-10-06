using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using BL;

namespace UI
{
    public class OrderMenu : IMenu
    {
        private ISBL _bl;
        public OrderMenu(ISBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            int total = 0;
            Console.WriteLine();
            Console.WriteLine("---Current Order---");
            int orderId = Login.CurrentOrder.Id;

            //Search orderitems table for all the orders with this order id.
            List<OrderItem> orderItems = GetOrderItems(orderId);
            if (orderItems.Count == 0)
            {
                Console.WriteLine("No Brews in Order :/");
            }
            else
            {
                // foreach(OrderItem oi in orderItems)
                // {
                //     Console.WriteLine($"{oi.Quantity} {GetBrewById(oi.BrewId)} - ${oi.Quantity*GetBrewById(oi.BrewId).Price}");
                //     total += oi.Quantity*_bl.GetBrewById(oi.BrewId).Price;
                // }
                Console.WriteLine();
                Console.WriteLine($"TOTAL : ${total}");
                Console.WriteLine("[$] To Place Current Order");
                Console.WriteLine("[x] Exit Order Menu");
            }

            switch(Console.ReadLine())
            {
                case "$":
                    // PlaceOrder(orderItems);
                    break;
                default:
                    break;
            }
            
        }
        // // private List<Brew> PlaceOrder(List<OrderItem> oiList)
        // // {
        // //     List<Brew> UpdatedBrews = new List<Brew>();
        // //     //new strategy: run through each brew here. Update 1 brew at a time in db. ***
        // //     foreach(OrderItem oi in oiList)
        // //     {
        // //         //Update the BrewQuantity of each brew.
        // //         int currentBrewId = oi.BrewId;
        // //         Brew currentBrew = _bl.GetBrewById(currentBrewId);

        // //         //Update Models.Brew.BrewQuantity.
        // //         currentBrew.Quantity -= oi.Quantity;

        // //         Console.WriteLine($"{currentBrew.Quantity} {currentBrew.Name} left.");

        // //         //Send updated Brew.BrewQuantity to DL to be updated in the DB.
        // //         Brew UpdatedBrew = _bl.UpdateBrewQuantity(currentBrew);
        // //         UpdatedBrews.Add(UpdatedBrew);
        // //     }

        //     //Update OrderPlaced Variable of the Order.
        //     int orderId = oiList[0].OrderId;
        //     _bl.PlaceOrder(orderId);

        //     //Clear Current Order and Create new one. 
        //     Order nextOrder = new Order(){
        //         CustomerId = Login.CurrentCustomer.Id
        //     };
        //     Login.CurrentOrder = _bl.CreateOrder(nextOrder);

        //     return UpdatedBrews;
        // }
        private List<OrderItem> GetOrderItems(int orderId)
        {
            return _bl.GetOrderItems(orderId);
        }
        private Brew GetBrewById(int brewId)
        {
            return _bl.GetBrewById(brewId);
        }
    }
}