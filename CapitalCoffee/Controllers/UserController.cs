using System;
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
using System.Web.Security;

namespace CapitalCoffee.Controllers
{
    public class UserController : Controller
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterUserViewModel user)
        { 
            if(ModelState.IsValid)
            {
                var userDao = new UserDao(db);
                var newUser = new User();
                var password = user.Password;
                newUser.Username = user.Username;
                newUser.EmailAddress = user.Email;
                userDao.RegisterAccount(newUser, password);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel login)
        {
                var userDao = new UserDao(db);
                bool loginResult = userDao.CheckLogin(login.EmailOrUsername, login.Password);
                if (loginResult != true)
                {
                    ViewBag.Error = "Invalid Username or Password";
                    return View("Login", "User", null);
                }

                var user = userDao.GetByEmailOrUser(login.EmailOrUsername);
                Session["userName"] = user.Username;

                Session.Timeout = 90;
                return RedirectToAction("Index", "Home", null); 
        }


        public ActionResult LogOff()
        {
            Session.Remove("userName");
            return RedirectToAction("Index", "Home", null);
        }


     
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            
            var userDao = new UserDao(db);
            var photoDao = new PhotoDao(db);
            var reviewDao = new ReviewDao(db);
            var vm = new ProfilePageViewModel();

            var user = userDao.GetById(id);
            var reviews = reviewDao.GetAllReviewsByUser(id);
            
            vm.User = user;
            vm.UserId = user.UserId;

            if (reviews != null)
            {
                vm.Reviews = reviews;
            }
             
            //if (picture != null)
            //{
            //    vm.ProfilePicture = picture;
            //    vm.ProfilePictureId = picture.ProfilePictureId;
            //}

            return PartialView("~/Views/User/_Edit.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfilePageViewModel vm, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var userDao = new UserDao(db);
                var photoDao = new PhotoDao(db);
                var user = vm.User;
                userDao.Edit(user.UserId);

                if(photo != null)
                {
                    photoDao.DeleteProfilePicture(vm.User.UserId);
                    if (photo.ContentLength <= 5000000 && (photo.ContentType == "image/gif" || photo.ContentType == "image/jpeg" || photo.ContentType == "image/png"))
                    {
                        var image = new ProfilePicture()
                        {
                            UserId = vm.User.UserId,
                            MimeType = photo.ContentType,
                            Picture = new byte[photo.ContentLength]
                        };

                        photo.InputStream.Read(image.Picture, 0, photo.ContentLength);

                        photoDao.UploadProfilePicture(image);
                    }
                    else
                    {
                        TempData["notice"] = "Invalid file type or file size. Please upload jpeg, gif or png that is 5mb or less.";
                        return View(vm);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Profile", "User", new { id = vm.User.UserId });
            }

            return View(vm);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        [HttpGet]
        public ActionResult Profile(int id)
        {
            var userDao = new UserDao(db);
            var photoDao = new PhotoDao(db);
            var reviewDao = new ReviewDao(db);
            var vm = new ProfilePageViewModel();

            var user = userDao.GetById(id);
            vm.User = user;

            var profilePicture = photoDao.GetPictureForUser(user.UserId);
            if(profilePicture != null)
            {
              vm.ProfilePicture = profilePicture;
            }

            var reviews = reviewDao.GetAllReviewsByUser(id);
            if(reviews.Any())
            {
                vm.Reviews = reviews;
            }
            
            return View(vm);
        }


    }
}
