using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CapitalCoffee.Models
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
        }

        //public DetailsViewModel()
        //{

        //}
        
        public int ShopId { get; set; }
        public List<int> ReviewIds { get; set; }
        public List<int> ReviewPictureIds { get; set; }
        public double AverageRating { get; set; }
        public Shop SelectedShop { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ReviewPicture> ReviewPictures { get; set; }
        public List<HoursOfOperation> HoursOfOperation { get; set; }
        public bool UserHasReviewedShop { get; set; }
    }
}