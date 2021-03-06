﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }

        [Display(Name="Email")]
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Tagline { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public List<int> ReviewIds { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
