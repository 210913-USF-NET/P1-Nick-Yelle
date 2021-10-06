using System;
using Models;
using BL;
using System.Collections.Generic;

namespace UI
{
    public class ManBrewMenu : IMenu
    {
        private ISBL _bl;

        public ManBrewMenu(ISBL bl)
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
                Console.WriteLine("[1] See all Breweries");
                Console.WriteLine("[2] Create New Brewery");
                Console.WriteLine("[3] See all Brews");
                Console.WriteLine("[4] Create New Brew");
                Console.WriteLine("[5] Add Inventory");
                Console.WriteLine("[x] Back to Manager Menu");

                switch(Console.ReadLine())
                {
                    case "1":
                        ListAllBreweries();
                        break;

                    case "2":
                        CreateBrewery();
                        break;

                    case "3":
                        ListBrews();
                        break;

                    case "4":
                        // CreateBrew();
                        break;

                    case "5":
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

        private void CreateBrewery()
        {
            //Get Name, City, State.
            Console.WriteLine();
            Console.WriteLine("Brewery Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Brewery Location:");
            Console.WriteLine("City");
            string city = Console.ReadLine();
            Console.WriteLine("State");
            string state = Console.ReadLine();
            

            //Create Brewery.
            Brewery newBrewery = new Brewery(name, city, state);
            Console.WriteLine($"You created {name}, in {city}, {state}");
            _bl.AddBrewery(newBrewery);
        }

        private void ListAllBreweries()
        {
            List<Brewery> breweries = _bl.GetBreweries();
            Console.WriteLine();
            Console.WriteLine(":::: All Breweries ::::");
            for(int i = 0; i < breweries.Count; i++)
            {
                Console.WriteLine($"--{breweries[i].ToString()}--");
            }
        }
        // public Brew ChangeInventory()
        // {
        //     //Get Brew to change.
        //     Console.WriteLine("Choose a Brew to Add/Subtract Inventory from.");
        //     List<Brew> brews = _bl.GetBrews();
        //     for(int i = 0; i < brews.Count; i++)
        //     {
        //         Console.WriteLine($"[{i}] {brews[i].ToDescription()}");
        //     }
        //     int chosenIndex = Int32.Parse(Console.ReadLine());
        //     Brew brewToChange = brews[chosenIndex];

        //     Input: 
        //     //Add/Subtract from chosen brew.
        //     Console.WriteLine($"How many {brewToChange.Name} would you like to add/subtract?");
        //     try
        //     {
        //         brewToChange.Quantity += Int32.Parse(Console.ReadLine());
        //     }
        //     catch (System.FormatException)
        //     {
        //         Console.WriteLine("Please input a number");
        //         goto Input;
        //     }

        //     //Send to change to DL.
        //     Brew changedBrew = _bl.UpdateBrewQuantity(brewToChange);

        //     return changedBrew;
        // }
        // private Brew CreateBrew()
        // {
        //     Console.WriteLine("Creating new Brew.");
        //     Console.WriteLine("Brew Name:");
        //     string name = Console.ReadLine();
        //     Brewery chosenBrewery = ChooseBrewery();
        //     Console.WriteLine("Price of Brew: ");
        //     int chosenPrice = Int32.Parse(Console.ReadLine());
        //     Brew newBrew = new Brew(chosenBrewery, name, chosenPrice);
        //     Console.WriteLine($"You created {newBrew.ToString()}");

        //     Console.WriteLine(newBrew.Id);

        //     return _bl.AddBrew(newBrew);
        // }

        public void ListBrews()
        {
            Console.WriteLine();
            Console.WriteLine(":::All Brews:::");
            List<Brew> allBrews = _bl.GetBrews();
            if(allBrews.Count == 0)
            {
                Console.WriteLine("There are no Brews.");
            }
            else
            {
                foreach(Brew brew in allBrews)
                {
                    Console.WriteLine($"{brew.ToDescription()} -- {_bl.GetBreweryById(brew.Id)}");
                }
            }
        }

        public Brewery ChooseBrewery()
        {
            //List Breweries.
            Console.WriteLine("Choose a Brewery.");
            List<Brewery> allBreweries = _bl.GetBreweries();
            for(int i = 0; i < allBreweries.Count; i++)
            {
                Console.WriteLine($"[{i}] {allBreweries[i].ToString()}");
            }
            int chosenBreweryIndex = Int32.Parse(Console.ReadLine());
            //Return Selected Brewery.
            Console.WriteLine($"{allBreweries[chosenBreweryIndex].ToString()}");
            return allBreweries[chosenBreweryIndex];
            
        }
    }
}