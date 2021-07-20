using Carrental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;

        public OrderController(IOrderRepository orderRepository,ICarRepository carRepository)
        {
            this._orderRepository = orderRepository;
            this._carRepository = carRepository;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            var list = _orderRepository.GetAll();
            return View(list);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var car = _orderRepository.Find(id);
            if (car != null)
            {
                return View(car);
            }
            return RedirectToAction("Index");
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var order = new OrderViewModel();
            var cars = _carRepository.GetAll();
            order.Cars = cars;
            return View(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _orderRepository.Add(order);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var cars = _carRepository.GetAll();
                    order.Cars = cars;
                }
                return View(order);
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _orderRepository.Find(id);
            if (order != null)
            {
                var cars = _carRepository.GetAll();
                order.Cars = cars;
                return View(order);
            }

            return RedirectToAction("Index");
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _orderRepository.Update(order);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var cars = _carRepository.GetAll();
                    order.Cars = cars;
                }
                return View(order);
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            var order = _orderRepository.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return RedirectToAction("Index");
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(OrderViewModel order)
        {
            try
            {
                _orderRepository.Delete(order);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
