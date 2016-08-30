﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapitalCoffee.Data.Models;
using System.Data.Entity;
using PagedList;

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

        //public IPagedList<Shop> GetAll(string searchTerm = " ", int page = 1, string orderTerm = " ")
        //{
        //    var shopList = context.Shops.Where(s => s.Name.Contains(searchTerm)).OrderBy(s => s.Name).ToList();
        //    var pagedList = shopList.ToPagedList(page, 5);
        //    foreach(var s in pagedList)
        //    {
        //        s.Reviews = context.Reviews.Where(r => r.ShopId == s.ShopId).ToList();

        //        if (s.Reviews.Count() > 0)
        //        {
        //            var ratingSum = 0;

        //            foreach (var r in s.Reviews)
        //            {
        //                ratingSum += r.Rating;
        //            }

        //            var averageRating = ratingSum / s.Reviews.Count();

        //            s.AverageRating = averageRating;
        //        }
        //        else
        //        {
        //            s.AverageRating = 0;
        //        }
        //    }

        //    if(orderTerm == "rating")
        //    {
        //        shopList = shopList.OrderByDescending(s => s.AverageRating).ToList();
        //        var finalList = shopList.ToPagedList(page, 5);
        //        return finalList;
        //    }

        //    return pagedList;    
        //}

        public List<Shop> GetAll(string searchTerm = " ", string orderTerm = " ")
        {
            var shopList = context.Shops.Where(s => s.Name.Contains(searchTerm)).OrderBy(s => s.Name).ToList();
            foreach (var s in shopList)
            {
                s.Reviews = context.Reviews.Where(r => r.ShopId == s.ShopId).ToList();

                if (s.Reviews.Count() > 0)
                {
                    var ratingSum = 0;

                    foreach (var r in s.Reviews)
                    {
                        ratingSum += r.Rating;
                    }

                    var averageRating = ratingSum / s.Reviews.Count();

                    s.AverageRating = averageRating;
                }
                else
                {
                    s.AverageRating = 0;
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
            return context.Reviews.Where(r => r.Shop.ShopId == id).ToList();
        }

        public List<ReviewPicture> GetReviewPictures(int id)
        {
            return context.ReviewPictures.Where(p => p.Review.Shop.ShopId == id).ToList();
           
        }

        public int GetAverageRating(int id)
        {
            var reviews = context.Reviews.Where(r => r.Shop.ShopId == id).ToList();
            var ratingSum = 0;

            if (reviews.Count() == 0)
            {
                return ratingSum;
            }
            else
            {
                foreach (var r in reviews)
                {
                    ratingSum += r.Rating;
                }

                var averageRating = ratingSum / reviews.Count();

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

    }
}
