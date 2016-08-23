using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class Shop
    {
        [Key]
        public int ShopId { get; set; }
        public string Name { get; set; }

        [Display(Name="Address 1")]
        public string Address1 { get; set; }
        [Display(Name="Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [Display(Name="Website URL")]
        public string WebsiteUrl { get; set; }

        [Display(Name="Menu URL")]
        public string MenuUrl { get; set; }

        [Display(Name="Facebook URL")]
        public string FacebookUrl { get; set; }

        [Display(Name="Twitter URL")]
        public string TwitterUrl { get; set; }

        [Display(Name="Instagram URL")]
        public string InstagramUrl { get; set; }
        
        [Display(Name="Check if this is a local shop:")]
        public bool IsLocal { get; set; }
        public bool IsActive { get; set; }

        public List<int> HoursIds { get; set; }
        public List<HoursOfOperation> HoursOfOperation { get; set; }

        public List<int> ReviewIds { get; set; }
        public List<Review> Reviews { get; set; }

        public double ? AverageRating { get; set; }
    }
}
