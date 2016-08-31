using CapitalCoffee.Data.Models;
using System.Linq;

namespace CapitalCoffee.Data.Access
{
    public class PhotoDao
    {
        private readonly CapitalCoffeeContext context;

        public PhotoDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public void UploadReviewPicture(ReviewPicture reviewPicture)
        {
            context.ReviewPictures.Add(reviewPicture);
            context.SaveChanges();
        }

        public void UploadDefaultPicture(DefaultShopPicture shopPicture)
        {
            context.DefaultShopPictures.Add(shopPicture);
            context.SaveChanges();
        }

        public DefaultShopPicture GetDefaultPicture(int shopId)
        {
            return context.DefaultShopPictures.Where(p => p.ShopId == shopId).FirstOrDefault();
        }

        
        public ProfilePicture GetPictureForUser(int id)
        {
            return context.ProfilePictures.Where(p => p.UserId == id).FirstOrDefault();
        }

        public void UploadProfilePicture(ProfilePicture picture)
        {
            context.ProfilePictures.Add(picture);
            context.SaveChanges();
        }

        public void DeleteProfilePicture(int id)
        {
            var picture = context.ProfilePictures.Where(p => p.UserId == id).FirstOrDefault();
            if (picture != null)
            {
                context.ProfilePictures.Remove(picture);
                context.SaveChanges();
            }
        }

    }
}
