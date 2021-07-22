using Carrental.Factories;
using Carrental.Models;
using Carrental.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Controllers
{
    public class BulkOperationController : Controller
    {
        private readonly IBulkDataServiceFactory _bulkDataServiceFactory;

        public BulkOperationController(IBulkDataServiceFactory bulkDataServiceFactory)
        {
            this._bulkDataServiceFactory = bulkDataServiceFactory;
        }
        // GET: BulkInsertController

        // GET: BulkInsertController/Add/Type
        public ActionResult Add(string id)
        {
            ViewBag.Message = GetTitle(id);
            return View();
        }
        private string GetTitle(string id)
        {
            var title = "Upload Excel for";
            switch (id.ToLower())
            {
                case "type": title = $"{title} Car Types";
                    break;
                case "car":
                    title = $"{title} Cars";
                    break;
                case "order":
                    title = $"{title} Orders";
                    break;

            }
            return title;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(string id, IFormFile formFile)
        {
            try
            {
                ViewBag.Message = GetTitle(id);
                var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                var bulkOperation = _bulkDataServiceFactory.GetBulkOperator(id);
                var isAdded = bulkOperation.Add(memoryStream);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Data updated sucessfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Error in updating Data";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
