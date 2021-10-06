using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Customer
    {
        //Properties.
        public int Id { get; set; }
        public string UserName {get; set;}
        public string Password {get; set;}
        //Added for sake of EFCore.
        public List<Order> Orders { get; set; }

        //Constructors.
        public Customer(){}
        public Customer(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public Customer(int Id, string UserName, string Password)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Password = Password;
        }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
}