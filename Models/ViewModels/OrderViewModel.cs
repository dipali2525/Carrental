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
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string PickLocation { get; set; }
        public string DropLocation { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
    }
}
