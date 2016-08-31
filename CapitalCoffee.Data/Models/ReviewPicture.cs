using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Data.Models
{
    public class ReviewPicture
    {
        [Key]
        public int ReviewPictureId { get; set; }
        public int ReviewId { get; set; }
        public byte[] Picture { get; set; }
        public string MimeType { get; set; }
        public Review Review { get; set; }
    }
}
