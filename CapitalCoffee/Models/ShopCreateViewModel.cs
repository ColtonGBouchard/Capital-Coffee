using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CapitalCoffee.Models
{
    public class ShopCreateViewModel
    {
        public int ShopId { get; set; }
        public string Name { get; set; }

        [DisplayName("Address 1")]
        public string Address1 { get; set; }

        [DisplayName("Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [DisplayName("Website URL")]
        public string WebsiteUrl { get; set; }

        [DisplayName("Menu URL")]
        public string MenuUrl { get; set; }

        [DisplayName("Facebook URL")]
        public string FacebookUrl { get; set; }

        [DisplayName("Twitter URL")]
        public string TwitterUrl { get; set; }

        [DisplayName("Instagram URL")]
        public string InstagramUrl { get; set; }

        [DisplayName("Check if this is a local shop:")]
        public bool IsLocal { get; set; }

        public List<HoursOfOperation> HoursOfOperation { get; set; }
     
    }
}