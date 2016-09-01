using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name="Email Address")]
        public string EmailAddress { get; set; }

        //public string FromName { get; set; }
        //public string FromEmail { get; set; }
        public string Message { get; set; }
    }
}