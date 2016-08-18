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

        public ActionResult Index(string currentFilter, int page = 1, string searchTerm = " ")
        {
            //var vm = new IndexViewModel();
            var shopDao = new ShopDao(db);
            var shopList = shopDao.GetAll(searchTerm, page);
            foreach (var shop in shopList)
            {
                shop.AverageRating = shopDao.GetAverageRating(shop.ShopId);
                if(shop.AverageRating == null)
                {
                    shop.AverageRating = 0;
                }
            }

            //vm.Shops = shopList;
            return View(shopList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}