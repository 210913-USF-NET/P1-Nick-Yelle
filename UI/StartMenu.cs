using System;

namespace UI
{
    public class StartMenu : IMenu
    {
        public void Start()
        {
            bool exit = false;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to Brew Finder!");
                Console.WriteLine("Are you here to shop or manage?");
                Console.WriteLine("[S] Shop");
                Console.WriteLine("[M] Manage");
                Console.WriteLine("[x] Exit");


                switch(Console.ReadLine().ToUpper())
                {
                    case "S":
                        MenuFactory.GetMenu("login").Start();
                        //MenuFactory.GetMenu("shop menu").Start();
                        break;
                    case "M":
                        new ManCredMenu().Start();
                        break;
                    case "X":
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