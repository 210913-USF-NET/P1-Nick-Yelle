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
            Customer confiremedCust = _bl.Login(cust);
            if (confiremedCust == null)
            {
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("Index", "Home", cust);
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer cust)
        {
            try
            {
                //Ensure valid data.
                if (ModelState.IsValid)
                {
                    if(_bl.AddCustomer(cust) != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                return RedirectToAction(nameof(Register));
            }
            catch
            {
                return View();
            }
        }
    }
}