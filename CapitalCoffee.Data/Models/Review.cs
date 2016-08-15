using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int ReviewerId { get; set; }
        public User Reviewer { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ReviewPicture> ReviewPictures { get; set; }
    }
}
