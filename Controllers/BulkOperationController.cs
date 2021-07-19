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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(string id, IFormFile formFile)
        {
            try
            {
                var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                var bulkOperation = _bulkDataServiceFactory.Add(id);
                bulkOperation.Add(memoryStream);
                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
