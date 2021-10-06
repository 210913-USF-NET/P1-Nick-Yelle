using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using BL;
using DL;

namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string MenuString)
        {
            string connectionString = File.ReadAllText(@"../connectionString.txt");

            DbContextOptions<P0DBContext> options = new DbContextOptionsBuilder<P0DBContext>()
            .UseSqlServer(connectionString).Options;

            P0DBContext context = new P0DBContext(options);

            switch(MenuString.ToLower())
            {
                case "start menu":
                    return new StartMenu();
                case "login":
                    return new Login(new ShopLogic(new DBRepo(context)));
                case "shop menu":
                    return new ShopMenu();
                case "shopping by brewery":
                    return new ShopByBrewery(new ShopLogic(new DBRepo(context)));
                case "shopping by brews":
                    return new ShopByBrews(new ShopLogic(new DBRepo(context)));
                case "order menu":
                    return new OrderMenu(new ShopLogic(new DBRepo(context)));
                case "man brew menu":
                    return new ManBrewMenu(new ShopLogic(new DBRepo(context)));
                case "order history":
                    return new OrderHistory(new ShopLogic(new DBRepo(context)));
                default:
                    Console.WriteLine("Menu Factory Unrecognized command");
                    return null;
            }
        }
    }
}