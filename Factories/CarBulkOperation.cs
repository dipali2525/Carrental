using Carrental.Models;
using Carrental.Utility;
using System.IO;

namespace Carrental.Factories
{
    public class CarBulkOperation : IBulkOperation
    {
        private readonly ICarRepository _carRepository;

        public CarBulkOperation(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }
        public bool Add(Stream stream)
        {
            var cars = ExcelUtility.GetExcelData<CarViewModel>(stream);
            _carRepository.Add(cars);
            return true;
        }
    }

}
