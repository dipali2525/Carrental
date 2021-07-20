using Carrental.Models;
using Carrental.Services;
using Carrental.Utility;
using System.IO;

namespace Carrental.Factories
{
    public class OrderBulkOperation : IBulkOperation
    {
        private readonly IOrderService _orderService;

        public OrderBulkOperation(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        public bool Add(Stream stream)
        {
            var orders = ExcelUtility.GetExcelData<OrderViewModel>(stream);
            _orderService.Add(orders);
            return true;
        }
    }

}
