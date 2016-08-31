using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapitalCoffee.Controllers
{
    public class BaseController : Controller
    {
       public bool VerifyPhoto(HttpPostedFileBase photo)
       {
         var maxPhotoSize = 5000000; //5mb
         if (photo.ContentLength <= maxPhotoSize && (photo.ContentType == "image/gif" || photo.ContentType == "image/jpeg" || photo.ContentType == "image/png"))
         {
             return true;
         }
         else
         {
             return false;
         }
       }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

	}
}