using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapitalCoffee.Data.Models;

namespace CapitalCoffee.Models
{
    public class ProfilePageViewModel
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProfilePictureId { get; set; }
        public ProfilePicture ProfilePicture { get; set; }

        public List<int> ReviewIds { get; set; }
        public List<Review> Reviews { get; set; }
    }
}