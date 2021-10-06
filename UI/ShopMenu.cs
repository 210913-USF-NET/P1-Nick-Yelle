using System;
using System.Collections.Generic;
using BL;
using Models;

namespace UI
{
    public class ShopMenu : IMenu
    {
        public void Start()
        {
            bool exit = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to the Shopping Menu.");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Shop By Brewery");
                Console.WriteLine("[2] Shop By Brews");
                Console.WriteLine("[3] View Current Order");
                Console.WriteLine("[x] Back to Start Menu");

                switch(Console.ReadLine())
                {
                    case "1":
                        MenuFactory.GetMenu("shopping by brewery").Start();
                        break;
                    case "2":
                        MenuFactory.GetMenu("shopping by brews").Start();
                        break;
                    case "3":
                        MenuFactory.GetMenu("order menu").Start();
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