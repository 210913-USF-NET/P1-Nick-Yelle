using System;
using System.Collections.Generic;
using Serilog;

namespace Models
{
    public class Brew
    {
        //Properties.
        public string Name { get; set; }
        public int Id {get; set;}
        public int BreweryId {get; set;}
        public int Price { get; set; }
        public int Quantity { get; set; }
        public Brewery Brewery { get; set; }

        //Contructors.
        public Brew(){}
        public Brew(string name)
        {   
            Log.Debug("Brew constructor called.");
            this.Name = name;
        }

        //Overriding ToString method.
        public override string ToString()
        {
            return $"{Name}";
        }

        public string ToDescription()
        {
            return $"${Name})";
        }
    }
}
