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
    public class ReviewController : BaseController
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

      
        [HttpGet]
        public ActionResult Add(int shopId)
        {
            var userDao = new UserDao(db);
            var user = userDao.GetByEmailOrUser((string)(Session["userName"]));

            if(user == null)
            {
                return RedirectToAction("Login", "User");            
            }
            
            var vm = new AddReviewViewModel();
            vm.ShopId = shopId;
            vm.UserId = user.UserId;
            return View(vm);
                     
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddReviewViewModel review, List<HttpPostedFileBase> ReviewPictures)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    var reviewDao = new ReviewDao(db);
                    var photoDao = new PhotoDao(db);

                    review.Review = new Review();

                    review.Review.UserId = review.UserId;
                    review.Review.ShopId = review.ShopId;
                    review.Review.Rating = review.Rating;
                    review.Review.ReviewText = review.ReviewText;

                    reviewDao.AddReview(review.Review);

                    if (ReviewPictures[0] == null)
                    {
                        return RedirectToAction("Details", "Shop", new { id = review.ShopId });
                    }
                    else if (ReviewPictures.Count > 5)
                    {
                        TempData["notice"] = "Too many pictures. Please include 5 pictures or less";
                        dbContextTransaction.Rollback();
                        return View(review);
                    }

                    foreach (var p in ReviewPictures)
                    {
                        if (VerifyPhoto(p) == true)
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
                        else
                        {
                            TempData["notice"] = "Invalid file type or file size. Please upload jpeg, gif or png that is 5mb or less.";
                            dbContextTransaction.Rollback();
                            return View(review);
                        }
                    }

                    dbContextTransaction.Commit();
                    return RedirectToAction("Details", "Shop", new { id = review.ShopId });
                }
            }

            return View(review);
        }

        [HttpGet]
        public ActionResult Edit(int shopId)
        {
            var userDao = new UserDao(db);
            var user = userDao.GetByEmailOrUser((string)(Session["userName"]));
            if(user == null)
            {
                return RedirectToAction("Index", "Home", null); 
            }

            var reviewDao = new ReviewDao(db);
            Review review = reviewDao.GetReviewToEdit(user.UserId, shopId);

            return View(review); 
        }

        
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

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var reviewDao = new ReviewDao(db);

        //    var review = reviewDao.GetById(id);

        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(review);
        //}

        
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var reviewDao = new ReviewDao(db);
        //    reviewDao.Delete(id);

        //    return RedirectToAction("Index");
        //}

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
