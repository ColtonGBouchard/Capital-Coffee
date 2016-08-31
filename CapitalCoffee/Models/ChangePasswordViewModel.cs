using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Models
{
    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name="Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name="New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Verify New Password")]
        public string VerifyNewPassword { get; set; }
    }
}