using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class OrderItem
    {
        //Properties.
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BrewId { get; set; }
        public int Quantity { get; set; }
        //Added for sake of EFCore.
        public Brew Brew { get; set; }
        public Order Order { get; set; }
        //Added to use methods.


        //Constructors.
        public OrderItem(){}
        public OrderItem(int OrderId, int BrewId)
        {
            this.OrderId = OrderId;
            this.BrewId = BrewId;
        }
    }
}