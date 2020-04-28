using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Data;
using System.Threading;
using System.Globalization;
using NtierMvc.Models.Infrastructure;

namespace NtierMvc.Models
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ActionModel : ActionFilterAttribute
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string LanguageSelected = string.Empty;
            if (HttpContext.Current.Session["LanguageID"] != null)
            {
                LanguageSelected = HttpContext.Current.Session["LanguageID"].ToString();
            }
            else
            {
                LanguageSelected = "En";
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(LanguageSelected);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            filterContext.Controller.ViewBag.IpAddress = HostUtility.GetIPAddress();
            filterContext.Controller.ViewBag.IndianStandardTime = indianTime;
        }
    }
}