using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name="Email Address")]
        public string EmailAddress { get; set; }
    }
}