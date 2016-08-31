using System.Web;
using CapitalCoffee.Data.Models;
using System.ComponentModel.DataAnnotations;
namespace CapitalCoffee.Models
{
    public class AddReviewViewModel
    {
        public Review Review { get; set; }
        [Display(Name="Add pictures to your review")]
        public HttpPostedFileBase ReviewPicture { get; set; }
        public int ShopId { get; set; }
        public int UserId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [Display(Name="Review (optional)")]
        public string ReviewText { get; set; }
    }
}