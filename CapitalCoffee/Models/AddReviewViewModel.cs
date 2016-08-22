using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CapitalCoffee.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace CapitalCoffee.Models
{
    public class AddReviewViewModel
    {
        public Review Review { get; set; }
        //public List<ReviewPicture> ReviewPictures { get; set; }
        public HttpPostedFileBase ReviewPicture { get; set; }
        public int ShopId { get; set; }
        public int UserId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        public string ReviewText { get; set; }
    }
}