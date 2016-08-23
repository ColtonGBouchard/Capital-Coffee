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
        public string OpenTimeHour { get; set; }
        public string OpenTimeMinute { get; set; }
        public string CloseTimeHour { get; set; }
        public string CloseTimeMinute { get; set; }
        public int ShopId { get; set; }
        public bool IsActive { get; set; }
        public string OpenAmOrPm { get; set; }
        public string CloseAmOrPm { get; set; }


        public Shop Shop { get; set; }
    }

    
}
