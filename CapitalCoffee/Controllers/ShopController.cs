using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapitalCoffee.Data.Models;
using CapitalCoffee.Data.Access;
using CapitalCoffee.Models;

namespace CapitalCoffee.Controllers
{
    public class ShopController : BaseController
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

        [HttpGet]
        public ActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();
            var shopDao = new ShopDao(db);
            var userDao = new UserDao(db);
            var photoDao = new PhotoDao(db);
            var user = userDao.GetByEmailOrUser((string)(Session["userName"]));
            
          
            if(user != null)
            {
                model.UserHasReviewedShop = shopDao.UserHasReviewedShop(id, user.UserId);
                model.UserIsAdmin = userDao.IsAdmin(user);
            }

            model.SelectedShop = shopDao.GetById(id);
            model.Reviews = shopDao.GetReviews(id);

            if (model.Reviews.Any())
            {
                foreach (var r in model.Reviews)
                {
                    r.User = userDao.GetById(r.UserId);
                    r.ProfilePicture = photoDao.GetPictureForUser(r.UserId);
                }
            }

            model.ReviewPictures = shopDao.GetReviewPictures(id);
            model.AverageRating = shopDao.GetAverageRating(id);
            model.HoursOfOperation = shopDao.GetHoursOfOperation(id);
            model.DefaultPicture = photoDao.GetDefaultPicture(id);
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if(Session["userName"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View(new ShopCreateViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopCreateViewModel shop, HttpPostedFileBase ShopPicture)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    var shopDao = new ShopDao(db);
                    var photoDao = new PhotoDao(db);
                    var hoursDao = new HoursOfOperationDao(db);
                    var newShop = new Shop()
                        {
                            Name = shop.Name,
                            Address1 = shop.Address1,
                            Address2 = shop.Address2,
                            City = "Tallahassee",
                            State = "FL",
                            Zip = shop.Zip,
                            WebsiteUrl = shop.WebsiteUrl,
                            MenuUrl = shop.MenuUrl,
                            FacebookUrl = shop.FacebookUrl,
                            TwitterUrl = shop.TwitterUrl,
                            InstagramUrl = shop.InstagramUrl,
                            IsLocal = shop.IsLocal,
                            HoursOfOperation = shop.HoursOfOperation
                        };


                    for (int i = 0; i < 7; i++)
                    {
                        newShop.HoursOfOperation[i].DayOfWeek = i;
                    }

                    if (shopDao.IsDuplicate(newShop) == false)
                    {
                        shopDao.Create(newShop);
                    }
                    else
                    {
                        TempData["notice"] = "That shop already exists.";
                        return View(shop);
                    }

                    if (ShopPicture != null)
                    {
                        if (VerifyPhoto(ShopPicture) == true)
                        {
                            var image = new DefaultShopPicture()
                            {
                                ShopId = newShop.ShopId,
                                MimeType = ShopPicture.ContentType,
                                Picture = new byte[ShopPicture.ContentLength]
                            };

                            ShopPicture.InputStream.Read(image.Picture, 0, ShopPicture.ContentLength);

                            photoDao.UploadDefaultPicture(image);
                        }
                        else
                        {
                            TempData["notice"] = "Invalid file type or file size. Please upload jpeg, gif or png that is 5mb or less.";
                            dbContextTransaction.Rollback();
                            return View(shop);
                        }
                    }

                    dbContextTransaction.Commit();
                    return RedirectToAction("Details", "Shop", new { id = newShop.ShopId });
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var shopDao = new ShopDao(db);
            var userDao = new UserDao(db);
            
            if (Session["userName"] == null)
            {
                return View("Login", "User");
            }
            
            var user = userDao.GetByEmailOrUser((string)Session["userName"]);
            if(userDao.IsAdmin(user) == false)
            {
                return RedirectToAction("Forbidden", "Error");
            }
            
            Shop shop = shopDao.GetById(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shop shop)
        {
            if (ModelState.IsValid)
            {
                var shopDao = new ShopDao(db);
                shopDao.Edit(shop);
                return RedirectToAction("Details", "Shop", new { id = shop.ShopId });
            }
            return View(shop);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var shopDao = new ShopDao(db);
            var shop = shopDao.GetById(id);
            return View(shop);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var shopDao = new ShopDao(db);
            shopDao.Delete(id);
            return RedirectToAction("Index", "Home");
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
