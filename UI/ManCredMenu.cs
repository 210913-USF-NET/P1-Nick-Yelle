using System;

namespace UI
{
    public class ManCredMenu : IMenu
    {
        public void Start()
        {
            bool exit = false;
            bool beenHere = false;

            while (!beenHere && !exit)
            {   
                beenHere = true;
                Console.WriteLine();
                Console.WriteLine("Enter Manager ID:");
                Console.WriteLine("[x] Exit");


                switch(Console.ReadLine().ToUpper())
                {
                    case "1":
                        new ManMenu().Start();
                        break;

                    case "X":
                        exit = true;
                        Console.WriteLine("Bye.");
                        break;

                    default:
                        exit = true;
                        Console.WriteLine("Not recognized.");
                        break;
                }
            }
        }
    }
}