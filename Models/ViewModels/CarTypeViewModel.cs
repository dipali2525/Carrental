using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Models
{
    public class CarTypeViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
}
