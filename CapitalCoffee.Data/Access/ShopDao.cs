using System.Collections.Generic;
using System.Linq;
using CapitalCoffee.Data.Models;
using System.Data.Entity;

namespace CapitalCoffee.Data.Access
{
    public class ShopDao
    {
        private readonly CapitalCoffeeContext context;

        public ShopDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public Shop GetById(int id)
        {
            return context.Shops.Find(id);
        }

     
        public List<Shop> GetAll(string searchTerm = " ", string orderTerm = "")
        {
            var shopList = context.Shops.Where(s => s.Name.Contains(searchTerm)).OrderBy(s => s.Name).ToList();
            foreach (var s in shopList)
            {
                s.Reviews = context.Reviews.Where(r => r.ShopId == s.ShopId).ToList();

                s.Reviews = context.Reviews.Where(r => r.ShopId == s.ShopId).ToList();

                if (s.Reviews.Count() == 0)
                {
                    return null;
                }
                else
                {
                    var averageRating = s.Reviews.Sum(r => r.Rating) / s.Reviews.Count();
                    s.AverageRating = averageRating;
                }
            }

            if (orderTerm == "rating")
            {
                shopList = shopList.OrderByDescending(s => s.AverageRating).ToList();
                return shopList;
            }

            return shopList;
        }

        public void Create(Shop shop)
        {
            shop.IsActive = true;
            context.Shops.Add(shop);
            context.SaveChanges();
        }

        public void Delete(int shopId)
        {
            Shop shop = context.Shops.Find(shopId);

            var hours = context.HoursOfOperation.Where(h => h.ShopId == shopId);
            var reviews = context.Reviews.Where(r => r.ShopId == shopId);

            foreach(var h in hours)
            {
                context.HoursOfOperation.Remove(h);
            }

            foreach(var r in reviews)
            {
                context.Reviews.Remove(r);
            }

            context.Shops.Remove(shop);

            context.SaveChanges();
        }

        public void Edit(Shop shop)
        {
            context.Entry(shop).State = EntityState.Modified;
            context.SaveChanges();
        }

        public List<Review> GetReviews(int id)
        {
            return context.Reviews.Where(r => r.Shop.ShopId == id).OrderByDescending(r=>r.ReviewId).ToList();
        }

        public List<ReviewPicture> GetReviewPictures(int id)
        {
            return context.ReviewPictures.Where(p => p.Review.Shop.ShopId == id).ToList();
        }

        public int? GetAverageRating(int id)
        {
            var reviews = context.Reviews.Where(r => r.Shop.ShopId == id).ToList();

            if (reviews.Count() == 0)
            {
                return null;
            }
            else
            {
                var averageRating = reviews.Sum(r => r.Rating) / reviews.Count();
                return averageRating;
            }
        }

        public List<HoursOfOperation> GetHoursOfOperation(int id)
        {
            return context.HoursOfOperation.OrderBy(r=>r.DayOfWeek).Where(h => h.ShopId == id).ToList();
        }

        public bool UserHasReviewedShop(int shopId, int userId)
        {
            var reviews = context.Reviews.Where(r => r.ShopId == shopId && r.UserId == userId).ToList();
            if (reviews.Count() == 0)
                return false;
            else
                return true;
        }

        public ReviewPicture GetSelectedPicture(int id)
        {
            return context.ReviewPictures.Find(id);
        }

        public bool IsDuplicate(Shop shop)
        {
            var duplicates = context.Shops.Where(s => s.Name == shop.Name && s.Address1 == shop.Address1).ToList();
            if (!duplicates.Any())
                return false;
            return true;
        }
    }
}
