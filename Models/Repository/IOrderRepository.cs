using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Models
{
    public interface IOrderRepository
    {
        IEnumerable<OrderViewModel> GetAll();
        OrderViewModel Find(int id);
        bool Add(OrderViewModel order);
        bool Add(IEnumerable<OrderViewModel> orders);
        bool Delete(OrderViewModel order);
        bool Update(OrderViewModel order);
        IEnumerable<OrderViewModel> FindByCarId(int id);
    }
}
