using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;

namespace UI
{
    public class Login : IMenu
    {
        private ISBL _bl;

        public Login(ISBL bl)
        {
            _bl = bl;
        }

        public static Customer CurrentCustomer;
        public static Order CurrentOrder;
        public void Start()
        {
            bool exit = false;
            bool beenHere = false;

            while (!exit && !beenHere)
            {
                Restart:
                beenHere = true;
                Console.WriteLine();
                Console.WriteLine("Have you been here before?");
                Console.WriteLine("[0] No");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("[X] Exit");


                switch(Console.ReadLine().ToUpper())
                {
                    case "0":
                        CreateNewCustomer();
                        Console.WriteLine($"Great, you are now logged in as {CurrentCustomer.ToString()}.");
                        Console.WriteLine("Happy Brew Finding!");
                        MenuFactory.GetMenu("shop menu").Start();
                        break;
                    case "1":
                        Console.WriteLine("Welcome Back! What is your Name?");
                        string n = Console.ReadLine();
                        
                        //Relating the Current customer to an order. **Creates a new order
                        //each time app is run. Can I figure out how to save an order?
                        CurrentCustomer = _bl.CheckCustomerExists(n);

                        try {
                            CurrentOrder = new Order(CurrentCustomer.Id);
                            CurrentOrder = _bl.CreateOrder(CurrentOrder);
                        } 
                        catch (NullReferenceException)
                        {
                            Console.WriteLine($"A {n} has never been here...but...");
                            goto case "0";
                        }

                        if(CurrentCustomer == null)
                        {   
                            Console.WriteLine("No one with that name has ever been here...but....");
                            goto case "0";
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Great, you are now logged in as {CurrentCustomer.ToString()}.");
                            Console.WriteLine("Happy Brew Finding!");
                        }

                        MenuFactory.GetMenu("shop menu").Start();
                        break;
                    case "X":
                        exit = true;
                        Console.WriteLine("Bye.");
                        break;
                    default:
                        Console.WriteLine("I don't understand.");
                        goto Restart;
                }
            } 
        }

        private Customer CreateNewCustomer()
        {
            bool tryAgain = false;
            Start:
            //Collect name.
            if(tryAgain) 
            {
                Console.WriteLine("That username already exists. Try another.");
            }
            else 
            {
                Console.WriteLine("Welcome, let's create an account!");
            }
            Console.WriteLine("Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();
            //Check if name already exists in DB.
            if (_bl.CheckCustomerExists(username) == null)
            {
                Console.WriteLine();
                Customer c = new Customer(username, password);
                CurrentCustomer = _bl.AddCustomer(c);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Sorry a customer with that name already exists.");
                Console.WriteLine();
                goto Start;
            }

            //Set static CurrentCustomer to the customer that just logged in.
            Order o = new Order(CurrentCustomer.Id);
            CurrentOrder = _bl.CreateOrder(o);

            return CurrentCustomer;
        }
    }
}