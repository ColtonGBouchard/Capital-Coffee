﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CapitalCoffee.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [DisplayName("Review")]
        public string ReviewText { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ? UpdatedAt { get; set; }

        public List<ReviewPicture> ReviewPictures { get; set; }

        public int? ProfilePictureId { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
    }
}
