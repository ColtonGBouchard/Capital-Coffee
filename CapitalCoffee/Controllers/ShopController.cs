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
    public class ShopController : Controller
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

        // GET: /Shop/
        public ActionResult Index()
        {
            return View(db.Shops.ToList());
        }

        // GET: /Shop/Details/5
        public ActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();
            var shopDao = new ShopDao(db);
            var userDao = new UserDao(db);
            var photoDao = new PhotoDao(db);
            var userName = (string)(Session["userName"]);
            var user = userDao.GetByEmailOrUser(userName);

            //if (user == null)
            //{
            //    hasReviewed = false;
            //}
            //else
            if(user != null)
            {
                model.UserHasReviewedShop = shopDao.UserHasReviewedShop(id, user.UserId);
            }

            model.SelectedShop = shopDao.GetById(id);
            model.Reviews = shopDao.GetReviews(id);
            model.ReviewPictures = shopDao.GetReviewPictures(id);
            model.AverageRating = shopDao.GetAverageRating(id);
            model.HoursOfOperation = shopDao.GetHoursOfOperation(id);
            model.DefaultPicture = photoDao.GetDefaultPicture(id);
            
            return View(model);
        }

        // GET: /Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Shop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShopCreateViewModel shop, HttpPostedFileBase ShopPicture)
        {
            if (ModelState.IsValid)
            {
                var shopDao = new ShopDao(db);
                var photoDao = new PhotoDao(db);
                var hoursDao = new HoursOfOperationDao(db);
                var newShop = new Shop()
                    {
                        Name = shop.Name,
                        Address1 = shop.Address1,
                        Address2 = shop.Address2,
                        City = shop.City,
                        State = shop.State,
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
                
                shopDao.Create(newShop);
                if (ShopPicture != null)
                {
                    if (ShopPicture.ContentLength <= 5000000 && (ShopPicture.ContentType == "image/gif" || ShopPicture.ContentType == "image/jpeg" || ShopPicture.ContentType == "image/png"))
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
                        shopDao.Delete(newShop.ShopId);
                        return View(shop);
                    }
                }
                return RedirectToAction("Details", "Shop", new { id = newShop.ShopId });
            }

            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

            return RedirectToAction("Index", "Home");
        }

        // GET: /Shop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: /Shop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ShopId,Name,Address1,Address2,City,State,Zip,WebsiteUrl,MenuUrl,FacebookUrl,TwitterUrl,InstagramUrl,IsLocal,IsActive")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        // GET: /Shop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = db.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        // POST: /Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shop shop = db.Shops.Find(id);
            db.Shops.Remove(shop);
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
