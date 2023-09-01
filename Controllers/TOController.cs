using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db_TO_web.Models.TO;
using db_TO_web.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace db_TO_web.Controllers
{
    public class TOController : Controller
    {
        // GET: HomeController1
        AutoParkDBStorage _storage = new AutoParkDBStorage(new TOContext());
        public ActionResult CarList(int ModelId = -1)
        {
            Model car = new Model();

            car.ModelId = -1;
            car.ModelName = "Все";

            ViewBag.Models = new SelectList(_storage.GetAllModels().ToList().Append(car), "ModelId", "ModelName");
            //_storage.GetAllModels();
            return View(_storage.GetAllCars(ModelId));
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult CreateCar()
        {
            //mode.LastToDate = DateTime.Now;
            //if (ModelState.IsValid)
            //{
            //    _storage.Add(model);
            //    return RedirectToAction("NewsList");
            //}
            ViewBag.Models = new SelectList(_storage.GetAllModels(), "ModelId", "ModelName");

            return View(/*new CarList()*/);
        }
        [HttpPost]
        public ActionResult CreateCar(CarList model)
        {

            if (ModelState.IsValid)
            {
                _storage.AddCar(model);
                return RedirectToAction("CarList");
            }
            return View(model);
        }

        // GET: HomeController1/Edit/5
        public ActionResult EditCar(int id)
        {
            ViewBag.Models = new SelectList(_storage.GetAllModels(), "ModelId", "ModelName");
            CarList mod = _storage.GetCarWithId(id);
            return View(mod);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(int id, CarList car)
        {
            if (ModelState.IsValid)
            {
                _storage.UpdateCar(id, car);
                return RedirectToAction("CarList");
            }
            return View(car);
        }

        // GET: HomeController1/Delete/5
        public ActionResult DeleteCar(int id)
        {
            ViewBag.Models = new SelectList(_storage.GetAllModels(), "ModelId", "ModelName");
            CarList mod = _storage.GetCarWithId(id);
            return View(mod);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCar(int id, CarList car)
        {
            _storage.DeleteCar(id);
            return RedirectToAction("CarList");
        }
    }
}
