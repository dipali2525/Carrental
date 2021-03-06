using Carrental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarTypeRepository _carTypeRepository;

        public CarController(ICarRepository carRepository, ICarTypeRepository carTypeRepository)
        {
            this._carRepository = carRepository;
            this._carTypeRepository = carTypeRepository;
        }
        public ActionResult Index()
        {
            var list = _carRepository.GetAll();
            return View(list);
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            var car = _carRepository.Find(id);
            if (car != null)
            {
                return View(car);
            }
            return RedirectToAction("Index");
        }

        // GET: CarController/Create
        public ActionResult Create()
        {
            var carTypes = _carTypeRepository.GetAll();
            var car = new CarViewModel() { CarTypes = carTypes };
            return View(car);
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarViewModel car)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isAdded = _carRepository.Add(car);

                    if (isAdded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.ErrorMessage = "Error in saving Data";
                }

                var carTypes = _carTypeRepository.GetAll();
                car.CarTypes = carTypes;

                return View(car);
            }
            catch (Exception ex)
            {
                return View(car);
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            var car = _carRepository.Find(id);
            if (car != null)
            {
                var carTypes = _carTypeRepository.GetAll();
                car.CarTypes = carTypes;
                return View(car);
            }
            return RedirectToAction("Index");
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarViewModel car)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var isUpdated = _carRepository.Update(car);
                    if (isUpdated)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.ErrorMessage = "Error in updating Data";
                }

                var carTypes = _carTypeRepository.GetAll();
                car.CarTypes = carTypes;

                return View(car);
            }
            catch (Exception ex)
            {
                return View(car);
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            var car = _carRepository.Find(id);
            if (car != null)
            {
                return View(car);
            }
            return RedirectToAction("Index");
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CarViewModel car)
        {
            try
            {
               var isDeleted = _carRepository.Delete(car);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ErrorMessage = "Error in deleting Data";
                return View(car);
            }
            catch
            {
                return View(car);
            }
        }
    }
}
