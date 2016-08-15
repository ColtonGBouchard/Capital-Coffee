using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class ReviewPicture
    {
        [Key]
        public int ReviewPictureId { get; set; }
        public int ReviewId { get; set; }
        public byte[] Picture { get; set; }

        public Review Review { get; set; }
    }
}
