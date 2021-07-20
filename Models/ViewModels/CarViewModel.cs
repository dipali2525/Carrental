using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Models
{
    public class CarViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Type")]
        public int TypeId { get; set; }
        [Display(Name ="Type")]
        public string TypeName { get; set; }
        public IEnumerable<CarTypeViewModel> CarTypes { get; set; } = new List<CarTypeViewModel>();
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Photo{ get; set; }
        [Display(Name = "Car Name")]
        public string CarName { get; set; }
    }

}
