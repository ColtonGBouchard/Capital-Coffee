using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class HoursOfOperation
    {
        [Key]
        public int HoursId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime MyProperty { get; set; }

    }

    
}
