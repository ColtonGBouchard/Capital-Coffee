using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Models
{
    public class RegisterUserViewModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name="Verify Password")]
        public string VerifyPassword { get; set; }
        public string Email { get; set; }
    }
}