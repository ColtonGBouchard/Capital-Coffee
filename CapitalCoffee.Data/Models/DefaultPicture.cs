using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    class DefaultPicture
    {
        public int ShopId { get; set; }
        public byte[] Picture { get; set; }
        public string MimeType { get; set; }
        public Shop Shop { get; set; }
    }
}
