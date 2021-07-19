using System.Collections;
using System.Collections.Generic;

namespace Carrental.Models
{
    public interface ICarTypeRepository
    {
        IEnumerable<CarTypeViewModel> GetAll();
        CarTypeViewModel Find(int id);
        bool Add(CarTypeViewModel carType);
        bool Add(IEnumerable<CarTypeViewModel> carTypes);
        bool Delete(CarTypeViewModel carType);
        bool Update(CarTypeViewModel carType);
    }
}