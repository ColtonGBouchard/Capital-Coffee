﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapitalCoffee.Data.Models;
using CapitalCoffee.Models;
using CapitalCoffee.Data.Access;

namespace CapitalCoffee.Controllers
{
    public class ReviewController : Controller
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

      
        [HttpGet]
        public ActionResult Add(int shopId)
        {
            var userName = (string)(Session["userName"]);
            var userDao = new UserDao(db);
            var user = userDao.GetByEmailOrUser(userName);

            var vm = new AddReviewViewModel();
            vm.ShopId = shopId;
            vm.UserId= user.UserId;
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddReviewViewModel review, List<HttpPostedFileBase> ReviewPictures)
        {
            if(ModelState.IsValid)
            {
                var reviewDao = new ReviewDao(db);
                var photoDao = new PhotoDao(db);
                review.Review = new Review();
                review.Review.UserId = review.UserId;
                review.Review.ShopId = review.ShopId;
                review.Review.Rating = review.Rating;
                review.Review.ReviewText = review.ReviewText;
                reviewDao.AddReview(review.Review);

                if (ReviewPictures.Count < 6)
                {
                    foreach (var p in ReviewPictures)
                    {
                        var image = new ReviewPicture()
                        {
                            ReviewId = review.Review.ReviewId,
                            MimeType = p.ContentType,
                            Picture = new byte[p.ContentLength]
                        };

                        p.InputStream.Read(image.Picture, 0, p.ContentLength);

                        photoDao.UploadReviewPicture(image);
                    }
                    return RedirectToAction("Details", "Shop", new { id = review.ShopId });
                }
            }

            return View(review);
        }

        // GET: /Review/Edit/5
        public ActionResult Edit(int shopId)
        {
            var userName = (string)(Session["userName"]);
            if(userName != null)
            {
                var userDao = new UserDao(db);
                var user = userDao.GetByEmailOrUser(userName);
                var reviewDao = new ReviewDao(db);
                Review review = reviewDao.GetReviewToEdit(user.UserId, shopId);

                return View(review);  
            }
         
            return RedirectToAction("Index", "Home", null); 
        }

        // POST: /Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                var reviewDao = new ReviewDao(db);
                reviewDao.EditReview(review);
                return RedirectToAction("Details", "Shop", new {id = review.ShopId});
            }
            
            return View(review);
        }

        // GET: /Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: /Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}