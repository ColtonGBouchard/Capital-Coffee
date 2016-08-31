using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Data.Models
{
    public class HoursOfOperation
    {
        [Key]
        public int HoursId { get; set; }
        public int DayOfWeek { get; set; }

        [Display(Name="Open")]
        public string OpenTimeHour { get; set; }
        public string OpenTimeMinute { get; set; }

        [Display(Name="Close")]
        public string CloseTimeHour { get; set; }
        public string CloseTimeMinute { get; set; }
        public int ShopId { get; set; }
        public bool IsActive { get; set; }
        public string OpenAmOrPm { get; set; }
        public string CloseAmOrPm { get; set; }


        public Shop Shop { get; set; }
    }

    
}
