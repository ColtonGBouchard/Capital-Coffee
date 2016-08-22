﻿using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CapitalCoffee.Data.Access
{
    public class ReviewDao
    {
        private readonly CapitalCoffeeContext context;
        
        public ReviewDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public Review GetById(int id)
        {
            return context.Reviews.Find(id);
        }
        public Review GetReviewToEdit(int userId, int shopId)
        {
            return context.Reviews.Where(r => r.ShopId == shopId && r.UserId == userId).FirstOrDefault();
        }

        public void EditReview(Review review)
        {
            review.UpdatedAt = DateTime.Now;
            context.Entry(review).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void AddReview(Review review)
        {
            review.CreatedAt = DateTime.Now;
            context.Reviews.Add(review);
            context.SaveChanges();
        }

        public Review GetReviewByUser(int userId)
        {
            return context.Reviews.Where(r => r.UserId == userId).FirstOrDefault();
        }

    }
}