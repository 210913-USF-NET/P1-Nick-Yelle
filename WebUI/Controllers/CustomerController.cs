using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Models;
using WebUI.Models;

namespace WebUI.Controllers
{

    public class CustomerController : Controller
    {
        private ISBL _bl;
        public CustomerController(ISBL bl)
        {
            _bl = bl;
        }
        public static Customer CurrentCustomer;
        public static Order CurrentOrder;

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer cust)
        {
            if(cust.UserName == "admin")
            {
                return RedirectToAction("Index", "Brewery");
            }
            Customer confirmedCust = _bl.Login(cust);
            if (confirmedCust == null)
            {
                bool failedLogin = true;
                return RedirectToAction("Register", failedLogin);
            }
            else
            {
                CurrentCustomer = confirmedCust;
                CurrentOrder = _bl.GetOrder(confirmedCust);
                return RedirectToAction("Index", "Home", confirmedCust);
            }
        }

        public ActionResult OrderHistory(int id, string sortOrder)
        {
            Customer cust = _bl.GetCustomer(id);
            List<Order> orders = new List<Order>();
            switch(sortOrder)
            {
                case "HighToLow":
                    orders = _bl.GetOrdersHighToLow(cust);
                    break;
                case "LowToHigh":
                    orders = _bl.GetOrdersLowToHigh(cust);
                    break;
                case "NewToOld":
                    orders = _bl.GetOrdersNewToOld(cust);
                    break;
                case "OldToNew":
                    orders = _bl.GetOrdersOldToNew(cust);
                    break;
                default:
                    orders = _bl.GetOrders(cust);
                    break;
            }
            return View(orders);
        }

        public ActionResult ViewItems(int id)
        {
            List<OrderItem> oiList = _bl.GetOrderItems(id);
            foreach(OrderItem oi in oiList)
            {
                oi.Brew = _bl.GetBrewById(oi.BrewId);
            }
            return View(oiList);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer cust, bool failedLogin)
        {
            try
            {
                //Ensure valid data.
                if (ModelState.IsValid)
                {
                    Customer customer = _bl.AddCustomer(cust);
                    if(customer != null)
                    {
                        CurrentCustomer = customer;
                        CurrentOrder = _bl.GetOrder(customer);
                        return RedirectToAction("Index", "Home", customer);
                    }
                }
                return RedirectToAction(nameof(Register));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            List<Customer> allCustomers = _bl.GetCustomers();
            return View(allCustomers);
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_bl.GetCustomer(id));
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Customer cust)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.Remove(cust);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return RedirectToAction(nameof(Delete));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: BreweryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer cust)
        {
            try
            {
                //Ensure valid data.
                if (ModelState.IsValid)
                {
                    _bl.AddCustomer(cust);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
