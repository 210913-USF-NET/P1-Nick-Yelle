using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebUI.Models;
using BL;
using Models;

namespace WebUI.Controllers
{
	public class HomeController : Controller
	{
		private ISBL _bl;

		public HomeController(ISBL bl)
		{
			_bl = bl;
		}

		public ActionResult Index()
		{
			return View(CustomerController.CurrentCustomer);
		}

		public ActionResult ShopBrews(Customer cust)
        {
			List<Brew> allBrews = _bl.GetBrews();
			return View(allBrews);
        }

        // GET: RestaurantController/Edit/5
        public ActionResult AddToCart(int id)
        {
            OrderItem newOi = new OrderItem()
            {
                OrderId = CustomerController.CurrentOrder.Id,
                BrewId = id,
                Brew = _bl.GetBrewById(id),
                Order = _bl.GetOrderById(id)
            };
            return View(newOi);
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(OrderItem oi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.AddBrewToOrder(oi.OrderId, oi.BrewId, oi.Quantity);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(AddToCart));
            }
            catch
            {
                return RedirectToAction(nameof(AddToCart));
            }
        }
        //GET
        public ActionResult Cart()
        {

            List<OrderItem> oiList = _bl.GetOrderItems(CustomerController.CurrentOrder.Id);
            foreach(OrderItem oi in oiList)
            {
                oi.Brew = _bl.GetBrewById(oi.BrewId);
            }
            return View(oiList);

        }

        public ActionResult PlaceOrder()
        {
            _bl.PlaceOrder(CustomerController.CurrentOrder.Id);
            CustomerController.CurrentOrder = _bl.GetOrder(CustomerController.CurrentCustomer);
            return View(nameof(Index));
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
