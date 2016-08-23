using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class DefaultShopPicture
    {
        [Key]
        public int DefaultShopPictureId { get; set; }
        public byte[] Picture { get; set; }
        public string MimeType { get; set; }
        public int ShopId { get; set; }
 
    }
}
