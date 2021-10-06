using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;

namespace UI
{
    public class ShopByBrewery : IMenu
    {
        private ISBL _bl;

        public ShopByBrewery(ISBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {

            Console.WriteLine();
            GetBreweries();


            Console.WriteLine();
            Console.WriteLine("Which Brewery would you like to Shop?");
            Console.WriteLine("[x] Back");

            string chosenBrewery = Console.ReadLine();

            if(chosenBrewery.ToLower() == "x")
            {
                return;
            }

            int breweryId = Int32.Parse(chosenBrewery);
            Console.WriteLine();
            List<Brew> BreweryBrews = GetBrews(breweryId);
            Console.WriteLine("[x] Back");

            Console.WriteLine("Select a Brew to add to your order.");
            
            string input = Console.ReadLine();
            if(input.ToLower() == "x")
            {
                return;
            }
            int brewIndex = Int32.Parse(input);

            //When 20 is used as input, it throws an exception...Idk why...
            try
            {
            Brew chosenBrew = BreweryBrews[brewIndex];

            Console.WriteLine();
            Console.WriteLine($"You have chosen {chosenBrew.Name}");
            Console.WriteLine();
            Console.WriteLine("How many would you like?");
            int chosenQuantity = Int32.Parse(Console.ReadLine());

            _bl.AddBrewToOrder(Login.CurrentOrder, chosenBrew, chosenQuantity);
            }
            catch (System.ArgumentOutOfRangeException){}
        }

        List<Brew> GetBrews(int BreweryId)
        {
            List<Brew> allBrews = _bl.GetBrews(BreweryId);

            if(allBrews.Count == 0)
            {
                Console.WriteLine("Currently No Brews Here.");
            }
            else
            {
                for(int i = 0; i < allBrews.Count; i++)
                {
                    Console.WriteLine($"[{i}] {allBrews[i].ToDescription()}");
                }
            }
            return allBrews;
        }
        private List<Brewery> GetBreweries()
        {
            List<Brewery> allBreweries = _bl.GetBreweries();
            if(allBreweries.Count == 0)
            {
                Console.WriteLine("There are no Breweries.");
            }
            else
            {
                for(int i = 0; i < allBreweries.Count; i++)
                {
                    Console.WriteLine($"[{allBreweries[i].Id}] {allBreweries[i].ToString()}");
                }
            }
            return allBreweries;
        }
    }
}