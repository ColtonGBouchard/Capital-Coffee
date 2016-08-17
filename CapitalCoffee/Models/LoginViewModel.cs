using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapitalCoffee.Models
{
    public class UserLoginViewModel
    {
        [Display(Name="Email or Username")]
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}