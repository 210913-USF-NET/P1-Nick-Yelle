using System;
using BL;
using DL;

namespace UI
{
    public class ManMenu
    {
        public void Start()
        {
            bool exit = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to manage?");
                Console.WriteLine("[1] Manage Breweries");
                Console.WriteLine("[2] View Order History");
                Console.WriteLine("[x] Back to Start Menu");

                switch(Console.ReadLine().ToLower())
                {
                    case "1":
                        MenuFactory.GetMenu("Man Brew Menu").Start();
                        break;
                    case "2":
                        MenuFactory.GetMenu("Order History").Start();
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
    }
}