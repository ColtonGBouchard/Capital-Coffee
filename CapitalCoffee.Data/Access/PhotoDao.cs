using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace CapitalCoffee.Data.Access
{
    public class PhotoDao
    {
        private readonly CapitalCoffeeContext context;

        public PhotoDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public void UploadProfilePicture()
        {

        }

        public void UploadReviewPicture(ReviewPicture reviewPicture)
        {
            context.ReviewPictures.Add(reviewPicture);
            context.SaveChanges();
        }

        public void UploadDefaultPicture()
        {

        }

        public ReviewPicture GetBinaryData(int id)
        {
            return context.ReviewPictures.Find(id);
        }
    }
}
