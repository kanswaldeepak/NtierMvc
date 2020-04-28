using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using NtierMvc.Models.Infrastructure;

namespace NtierMvc.Infrastructure
{
    public class GlobalActionFilter : ActionFilterAttribute
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            filterContext.Controller.ViewBag.IpAddress = HostUtility.GetIPAddress();
            filterContext.Controller.ViewBag.IndianStandardTime = indianTime;
            filterContext.Controller.ViewBag.ReleaseVersion = ConfigurationManager.AppSettings["ReleaseVersion"];
        }
    }
}