using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        [Display(Name = "Car")]
        public int CarId { get; set; }
        [Display(Name ="Car")]
        public string CarName { get; set; }
        public IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Pick Location")]
        public string PickLocation { get; set; }
        [Display(Name = "Drop Location")]
        public string DropLocation { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }
}
