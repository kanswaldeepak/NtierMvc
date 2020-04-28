using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult LogIn(string userName, string password)
        //{
        //    try
        //    {
        //        using (var context = new Context())
        //        {
        //            var getUser = (from s in context.ObjRegisterUser where s.UserName == userName || s.EmailId == userName select s).FirstOrDefault();
        //            if (getUser != null)
        //            {
        //                var hashCode = getUser.VCode;
        //                //Password Hasing Process Call Helper Class Method    
        //                var encodingPasswordString = Helper.EncodePassword(password, hashCode);
        //                //Check Login Detail User Name Or Password    
        //                var query = (from s in context.ObjRegisterUser where (s.UserName == userName || s.EmailId == userName) && s.Password.Equals(encodingPasswordString) select s).FirstOrDefault();
        //                if (query != null)
        //                {
        //                    //RedirectToAction("Details/" + id.ToString(), "FullTimeEmployees");    
        //                    //return View("../Admin/Registration"); url not change in browser    
        //                    return RedirectToAction("Index", "Home");
        //                }
        //                ViewBag.ErrorMessage = "Invalid User Name or Password";
        //                return View();
        //            }
        //            ViewBag.ErrorMessage = "Invalid User Name or Password";
        //            return View();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMessage = " Error!!! contact ERP@ERP.in";
        //        return View();
        //    }
        //}
    }
}