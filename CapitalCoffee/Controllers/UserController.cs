using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapitalCoffee.Data.Models;
using CapitalCoffee.Models;
using CapitalCoffee.Data.Access;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace CapitalCoffee.Controllers
{
    public class UserController : BaseController
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
                if (IsValidEmail(user.Email))
                {
                    newUser.EmailAddress = user.Email;
                }
                else
                {
                    TempData["notice"] = "Invalid Email Address";
                    return View(user);
                }
                userDao.RegisterAccount(newUser, password);
                return RedirectToAction("Profile", "User", new { id = newUser.UserId });
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
                    return View(login);
                }

                var user = userDao.GetByEmailOrUser(login.EmailOrUsername);
                Session["userName"] = user.Username;
                Session["userId"] = user.UserId;
               
                

                Session.Timeout = 90;
                return RedirectToAction("Index", "Home", null); 
        }


        public ActionResult LogOff()
        {
            Session.Remove("userName");
            Session.Remove("userId");
            if (Request.Cookies["sortCookie"] != null)
            {
                Response.Cookies["sortCookie"].Expires = DateTime.Now.AddDays(-1);
            }

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
            vm.User.UserId = user.UserId;
            if (reviews != null)
            {
                vm.User.Reviews = reviews;
            }
             
            return PartialView("~/Views/User/_Edit.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfilePageViewModel vm, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {

                    var userDao = new UserDao(db);
                    var photoDao = new PhotoDao(db);
                    var user = userDao.GetById(vm.User.UserId);

                    user.Username = vm.User.Username;
                    Session["userName"] = user.Username;
                    user.Tagline = vm.User.Tagline;
                    user.EmailAddress = vm.User.EmailAddress;

                    userDao.Edit(user);

                    if (photo != null)
                    {
                        photoDao.DeleteProfilePicture(vm.User.UserId);

                        if (VerifyPhoto(photo) == true)
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
                            dbContextTransaction.Rollback();
                            return RedirectToAction("Profile", "User", new { id = vm.User.UserId });
                        }
                    }

                    dbContextTransaction.Commit();
                    return RedirectToAction("Profile", "User", new { id = vm.User.UserId });
                }
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
            var shopDao = new ShopDao(db);
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
                foreach(var r in vm.Reviews)
                {
                    r.Shop = shopDao.GetById(r.ShopId);
                }
            }
            
            return View(vm);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var vm = new ChangePasswordViewModel();
            if (Session["userName"] != null)
            {
                vm.UserId = Convert.ToInt32(Session["userId"]);
                return View(vm);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var userDao = new UserDao(db);
                var validPassword = userDao.CheckPassword(vm.UserId, vm.OldPassword);
                if(validPassword == true)
                {
                    if(vm.NewPassword == vm.VerifyNewPassword)
                    {
                        userDao.ChangePassword(vm.UserId, vm.NewPassword);
                        return RedirectToAction("Profile", "User", new { id = vm.UserId });
                    }
                }
                
            }
            TempData["notice"] = "One or more of the password fields was incorrect, please try again.";
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel vm)
        {
            var userDao = new UserDao(db);
            var validEmail = userDao.GetEmail(vm.EmailAddress);
            if(validEmail != null)
            {
                var newPassword = GetPassword();
                var body = "<p>Your new password is " + newPassword + "</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(vm.EmailAddress));
                message.Subject = "Forgot Password";
                message.Body = string.Format(body, vm.Message);
                message.IsBodyHtml = true;
                userDao.ChangePassword(validEmail.UserId, newPassword);

                SmtpClient client = new SmtpClient();

                client.Send(message);

                return RedirectToAction("Login", "User");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


    }
}
