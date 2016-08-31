using CapitalCoffee.Data.Models;
using System.Collections.Generic;

namespace CapitalCoffee.Models
{
    public class DetailsViewModel
    {
        public int ShopId { get; set; }
        public double? AverageRating { get; set; }
        
     
        public bool UserHasReviewedShop { get; set; }
        public bool UserIsAdmin { get; set; }

        public Shop SelectedShop { get; set; }
        public DefaultShopPicture DefaultPicture { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ReviewPicture> ReviewPictures { get; set; }
        public List<HoursOfOperation> HoursOfOperation { get; set; }
    }
}