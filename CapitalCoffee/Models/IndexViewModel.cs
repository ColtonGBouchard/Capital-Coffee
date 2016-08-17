using CapitalCoffee.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapitalCoffee.Models
{
    public class IndexViewModel
    {
        public List<int> ShopIds { get; set; }

        public List<Shop> Shops { get; set; }

        //public List<String> ShopNames { get; set; }
        //public List<String> Addresses { get; set; }
        //public List<String> Ratings { get; set; }
    }
}