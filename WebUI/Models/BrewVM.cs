using System;
using System.Collections.Generic;
using Serilog;

namespace WebUI
{
    public class BrewVM
    {
        //Properties.
        public string Name { get; set; }
        public int Id { get; set; }
        public string BreweryName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //Contructors.
        public BrewVM() { }
        public BrewVM(string Name, int Id, string BreweryName, double Price, int Quantity)
        {
            Log.Debug("Brew constructor called.");
            this.Name = Name;
            this.Id = Id;
            this.BreweryName = BreweryName;
            this.Price = Price;
            this.Quantity = Quantity;
        }
    }
}