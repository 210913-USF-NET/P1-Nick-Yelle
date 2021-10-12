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

        static OrderItem CurrentOrderItem = null;

		public ActionResult Index()
		{
			return View(CustomerController.CurrentCustomer);
		}

		public ActionResult ShopBrews()
        {
			List<Brew> allBrews = _bl.GetBrews();
			return View(allBrews);
        }

        public ActionResult ShopBreweries()
        {
            List<Brewery> allBreweries = _bl.GetBreweries();
            return View(allBreweries);
        }

        public ActionResult ShopBrewsByBrewery(int Id)
        {
            List<Brew> brews = _bl.GetBrews(Id);
            foreach(Brew b in brews)
            {
                b.Brewery = _bl.GetBreweryById(b.BreweryId);
            }
            return View(brews);
        }

        public ActionResult ViewItems(int id)
        {
            List<OrderItem> oiList = _bl.GetOrderItems(id);
            foreach (OrderItem oi in oiList)
            {
                oi.Brew = _bl.GetBrewById(oi.BrewId);
            }
            return View(oiList);
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
            CurrentOrderItem = newOi;
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
                    
                    _bl.AddBrewToOrder(CurrentOrderItem.OrderId, CurrentOrderItem.BrewId, oi.Quantity);
                    CurrentOrderItem = null;
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
                oi.Order = _bl.GetOrderById(oi.OrderId);
            }
            return View(oiList);

        }

        public ActionResult PlaceOrder()
        {
            _bl.PlaceOrder(CustomerController.CurrentOrder.Id);
            CustomerController.CurrentOrder = _bl.GetOrder(CustomerController.CurrentCustomer);
            return RedirectToAction(nameof(Index));
        }
        
        public ActionResult Orders()
        {
            List<Order> orders = _bl.GetOrders(CustomerController.CurrentCustomer);
            return View(orders);
        }
	}
}
