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

    public class BrewController : Controller
    {
        private ISBL _bl;
        public BrewController(ISBL bl)
        {
            _bl = bl;
        }
        // GET: BreweryController
        public ActionResult Index()
        {
            List<Brew> allBrews = _bl.GetBrews();
            return View(allBrews);
        }

        // GET: BreweryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BreweryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brew brew)
        {
            try
            {
                //Ensure valid data.
                if (ModelState.IsValid)
                {
                    _bl.AddBrew(brew);
                    List<Brewery> allBreweries= new List<Brewery>();
                    allBreweries = _bl.GetBreweries();
                    ViewData["breweries"] = allBreweries;
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
            return View(_bl.GetBrewById(id));
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brew brew)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.UpdateBrew(brew);
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
            return View(_bl.GetBrewById(id));
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Brew brew)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.RemoveBrew(brew);
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
