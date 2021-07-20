using Carrental.Models;
using Carrental.Services;
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
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            var list = _orderService.GetAllOrders();
            return View(list);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var car = _orderService.GetOrder(id);
            if (car != null)
            {
                return View(car);
            }
            return RedirectToAction("Index");
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var order = _orderService.GetEmptyOrder();
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

                    var message = string.Empty;
                    var isAdded = _orderService.Add(order, out message);
                    ViewBag.ErrorMessage = message;
                    if (isAdded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                order.Cars = _orderService.GetAllCars();
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
            var order = _orderService.GetOrder(id);
            if (order != null)
            {
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
                ViewBag.ErrorMessage = string.Empty;
                if (ModelState.IsValid)
                {
                    var isModified = _orderService.ModifyOrder(order);
                    if (isModified)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.ErrorMessage = "Error in updating the data";
                }
                else
                {
                    order.Cars = _orderService.GetAllCars();
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
            var order = _orderService.GetOrder(id);
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
                ViewBag.ErrorMessage = string.Empty;
                var isDeleted = _orderService.RemoveOrder(order);
                if (isDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ErrorMessage = "Error in deleting the data";
                return View(order);
            }
            catch
            {
                return View(order);
            }
        }
    }
}
