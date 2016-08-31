using CapitalCoffee.Data.Models;
using System.Collections.Generic;

namespace CapitalCoffee.Models
{
    public class IndexViewModel
    {
        public List<int> ShopIds { get; set; }

        public List<Shop> Shops { get; set; }
    }
}