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

        public void UploadProfilePicture()
        {

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

        public ReviewPicture GetBinaryData(int id)
        {
            return context.ReviewPictures.Find(id);
        }
    }
}
