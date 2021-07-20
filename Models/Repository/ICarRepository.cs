using System.Collections.Generic;

namespace Carrental.Models
{
    public interface ICarRepository
    {
        IEnumerable<CarViewModel> GetAll();
        CarViewModel Find(int id);
        bool Add(CarViewModel car);
        bool Add(IEnumerable<CarViewModel> cars);
        bool Delete(CarViewModel car);
        bool Update(CarViewModel car);
        IEnumerable<CarViewModel> FindByBrand(string brand);
    }
}
