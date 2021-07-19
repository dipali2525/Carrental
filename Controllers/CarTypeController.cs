using Carrental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Controllers
{
    public class CarTypeController : Controller
    {
        private readonly ICarTypeRepository _carTypeRepository;

        public CarTypeController(ICarTypeRepository carTypeRepository)
        {
            this._carTypeRepository = carTypeRepository;
        }
        // GET: CarTypeController
        public ActionResult Index()
        {
            var list = _carTypeRepository.GetAll();
            return View(list);
        }

        // GET: CarTypeController/Details/5
        public ActionResult Details(int id)
        {
            var carType = _carTypeRepository.Find(id);
            return RedirectToAction("Index");
        }

        // GET: CarTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarTypeViewModel carType)
        {
            try
            {
                if(ModelState.IsValid){
                    _carTypeRepository.Add(carType);
                  return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: CarTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var carType = _carTypeRepository.Find(id);
            if (carType != null)
            {
                return View(carType);
            }
            return RedirectToAction("Index");
        }

        // POST: CarTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarTypeViewModel carType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _carTypeRepository.Update(carType);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: CarTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var carType = _carTypeRepository.Find(id);
            if (carType != null)
            {
                return View(carType);
            }
            return RedirectToAction("Index");
        }

        // POST: CarTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CarTypeViewModel carType)
        {
            try
            {
                _carTypeRepository.Delete(carType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
