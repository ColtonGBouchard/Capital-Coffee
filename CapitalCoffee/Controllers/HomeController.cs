using CapitalCoffee.Data.Access;
using CapitalCoffee.Data.Models;
using CapitalCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CapitalCoffee.Controllers
{
    public class HomeController : Controller
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

        [HttpGet]
        public ActionResult Index(string currentFilter, int? page, string searchTerm = " ", string sortTerm="")
        {
            
            var pageSize = 2;
            var pageNumber = page ?? 1;

            if(this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("sortCookie"))
            {
                if (sortTerm == "")
                {
                    sortTerm = HttpContext.Request.Cookies["sortCookie"].Value;
                }
            }

            var shopDao = new ShopDao(db);
            var shopList = shopDao.GetAll(searchTerm, sortTerm);
            var pagedList = shopList.ToPagedList(pageNumber, pageSize);

           
            if (HttpContext.Request.Cookies["sortCookie"] == null)
            {
                if (Session["userName"] != null)
                {
                    if (sortTerm != "")
                    {
                        var cookie = new HttpCookie("sortCookie", sortTerm);
                        Response.Cookies.Add(cookie);
                    }
                }
            }

            return View(pagedList);
        }
    }
}