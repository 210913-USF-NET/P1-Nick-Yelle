using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        //Properties.
        public int Id {get; set;}
        public int CustomerId{get; set;}
        public bool OrderPlaced{get; set;}
        public DateTime DateTimePlaced {get; set;}
        //Add for sake of EFCore.
        public List<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }

        //Constructors.
        public Order(){}
        public Order(int CustomerId)
        {
            this.CustomerId = CustomerId;
        }

        public override string ToString()
        {
            return $"OrderId {Id}, CustomerId {CustomerId}, OrderPlace {OrderPlaced}";
        }
    }
}