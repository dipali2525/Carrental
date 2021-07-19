using Carrental.Models;
using Carrental.Utility;
using System.IO;

namespace Carrental.Factories
{
    public class OrderBulkOperation : IBulkOperation
    {
        private readonly IOrderRepository _orderRepository;

        public OrderBulkOperation(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public bool Add(Stream stream)
        {
            var orders = ExcelUtility.GetExcelData<OrderViewModel>(stream);
            _orderRepository.Add(orders);
            return true;
        }
    }

}
