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

                return RedirectToAction("Index", "Home", null); 
        }


        // GET: /User/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
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



 
        //// GET: /User/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
        //    return View(user);
        //}

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="UserId,Username,EmailAddress,PasswordHash,Salt,Tagline,RoleId")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
        //    return View(user);
        //}

        // GET: /User/Delete/5
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
    }
}
