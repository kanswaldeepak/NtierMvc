using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NtierMvc.Model;
using NtierMvc.Infrastructure;
using NtierMvc.Model.Account;

namespace NtierMvc.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PagewiseAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = System.Web.HttpContext.Current;
            var requestContext = HttpContext.Current.Request.RequestContext;

            //if (ctx.Request.QueryString["registrationId"] != null)
            //{
                PagewiseAccessEntity pa, returnPA;
                AccountManager am;
            try
            {
                pa = new PagewiseAccessEntity();
                returnPA = new PagewiseAccessEntity();

                var url = filterContext.RouteData.Values["controller"].ToString() + "/" + filterContext.RouteData.Values["action"].ToString();

                //pa.RegistrationId = Convert.ToInt32(ctx.Request.QueryString["registrationId"]);
                pa.RegistrationId = ERPContext.UserContext.RegistrationID;
                pa.requestURL = url;
                pa.action = filterContext.HttpContext.Request.HttpMethod.ToString();
                pa.UserId = ERPContext.UserContext.UserId;
                pa.RoleId = 0;// MahaITContext.UserContext.UserRoles


                am = new AccountManager();
                returnPA = am.GetPagewiseAccess(pa);

                var viewBag = filterContext.Controller.ViewBag;
                // set the viewbag values
                viewBag.PagewiseAccessEntity = returnPA;

                //if (!returnPA.IsEditAllowed)
                //{
                //    new RedirectResult(new UrlHelper(requestContext).Action("Home", "Application", new { area = "" }));
                //}

            }
                catch (Exception ex)
                {

                }
           //}
           
            return;
        }


    }
}