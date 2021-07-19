using Carrental.Models;
using Carrental.Utility;
using System.IO;

namespace Carrental.Factories
{
    public class CarTypeBulkOperation : IBulkOperation
    {
        private readonly ICarTypeRepository _carTypeRepository;

        public CarTypeBulkOperation(ICarTypeRepository carTypeRepository)
        {
            this._carTypeRepository = carTypeRepository;
        }
        public bool Add(Stream stream)
        {
            var carTypes = ExcelUtility.GetExcelData<CarTypeViewModel>(stream);
            _carTypeRepository.Add(carTypes);
            return true;
        }
    }

}
