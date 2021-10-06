using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;

namespace UI
{
    public class ShopByBrews : IMenu
    {
        private ISBL _bl;

        public ShopByBrews(ISBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {

            List<Brew> brewList = GetBrews();
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
            Brew chosenBrew = brewList[brewIndex];

            Console.WriteLine();
            Console.WriteLine($"You have chosen {chosenBrew.Name}");
            Console.WriteLine();
            Console.WriteLine("How many would you like?");
            int chosenQuantity = Int32.Parse(Console.ReadLine());

            AddBrewToOrder(Login.CurrentOrder, chosenBrew, chosenQuantity);
            }
            catch (System.ArgumentOutOfRangeException){}
        }
        public OrderItem AddBrewToOrder(Order order, Brew brew, int quantity)
        {
            return _bl.AddBrewToOrder(order, brew, quantity);
        }
        private List<Brew> GetBrews()
        {
            Console.WriteLine();
            Console.WriteLine(":::: All Brews ::::");
            List<Brew> allBrews = _bl.GetBrews();
            if(allBrews.Count == 0)
            {
                Console.WriteLine("There are no Brews.");
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
    }
}