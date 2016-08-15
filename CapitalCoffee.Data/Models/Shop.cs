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

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string WebsiteUrl { get; set; }
        public string MenuUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        
        public bool IsLocal { get; set; }
        public bool IsActive { get; set; }

        public List<int> HoursIds { get; set; }
        public List<HoursOfOperation> HoursOfOperation { get; set; }

        public List<int> ReviewIds { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
