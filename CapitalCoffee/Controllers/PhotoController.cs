using CapitalCoffee.Data.Access;
using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapitalCoffee.Controllers
{
    public class PhotoController : Controller
    {
        private CapitalCoffeeContext db = new CapitalCoffeeContext();

        //
        // GET: /Photo/
        public ActionResult ShowReviewPicture(int id)
        {
            var shopDao = new ShopDao(db);
            var picture = shopDao.GetReviewPictures(id).First();

            return File(picture.Picture, picture.MimeType);
        }

        public ActionResult ShowDefaultPicture(int id)
        {
            var photoDao = new PhotoDao(db);
            var picture = photoDao.GetDefaultPicture(id);

            return File(picture.Picture, picture.MimeType);
        }

        public ActionResult ShowSelectedPicture(int id)
        {
            var shopDao = new ShopDao(db);
            var picture = shopDao.GetSelectedPicture(id);
            return File(picture.Picture, picture.MimeType);
        }

	}
}