using System.ComponentModel.DataAnnotations;

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
