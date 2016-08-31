using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Models
{
    public class UserLoginViewModel
    {
        public int UserId { get; set; }
        [Display(Name="Email or Username")]
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}