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

    public class BreweryController : Controller
    {
        private ISBL _bl;
        public BreweryController(ISBL bl)
        {
            _bl = bl;
        }
        // GET: BreweryController
        public ActionResult Index()
        {
            List<Brewery> allBreweries = _bl.GetBreweries();
            return View(allBreweries);
        }

        // GET: BreweryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BreweryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brewery brewery)
        {
            try
            {
                //Ensure valid data.
                if(ModelState.IsValid)
                {
                    _bl.AddBrewery(brewery);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_bl.GetBreweryById(id));
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brewery brewery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateBrewery(brewery);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_bl.GetBreweryById(id));
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Brewery brewery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.RemoveBrewery(brewery);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}