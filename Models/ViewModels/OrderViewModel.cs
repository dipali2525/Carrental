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
        [Display(Name = "Car")]
        public string CarName { get; set; }
        public IEnumerable<CarViewModel> Cars { get; set; } = new List<CarViewModel>();
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.Date;
        [Display(Name = "Pick Location")]
        public string PickLocation { get; set; }
        [Display(Name = "Drop Location")]
        public string DropLocation { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNo { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
    }

    public class SearchViewModel
    {
        public DateTime StartDate { get; set; } = DateTime.UtcNow.Date;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.Date.AddDays(7);
        public string Brand { get; set; }
        public bool IsData { get; set; }
    }
    public class CalendarViewModel
    {
        public IEnumerable<DateTime> DateTimes;
        public Dictionary<int, IEnumerable<CarBookingRecord>> Records { get; set; } = new Dictionary<int, IEnumerable<CarBookingRecord>>();
    }
    public class CarBookingRecord
    {
        public DateTime DateTime { get; set; }
        public CarViewModel Car { get; set; }
        public bool IsBooked { get; set; }
    }
}
