using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMvc.Infrastructure
{
    public class ApplicationFormSessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = System.Web.HttpContext.Current;
            var requestContext = HttpContext.Current.Request.RequestContext;
            if (ctx.Request.QueryString["registrationId"] != null)
            {
                ApplicationFromEntity entity = new ApplicationFromEntity();
                entity.RegistrationId = Convert.ToInt32(ctx.Request.QueryString["registrationId"]);
                if (ctx.Request.QueryString["registrationId"] != null)
                    entity.InstituteId = Convert.ToInt32(ctx.Request.QueryString["InstituteId"]);
                if (ctx.Request.QueryString["workItemId"] != null)
                    entity.WorkitemId = Convert.ToInt32(ctx.Request.QueryString["workItemId"]);
                ERPContext.ApplicationFormContext = entity;
            }
            else
            {
                if (ERPContext.ApplicationFormContext == null)
                {
                    filterContext.Result =
                                new RedirectResult(new UrlHelper(requestContext).Action("", "Home", new { area = "Admin" }));
                }
            }
            return;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            base.OnActionExecuted(filterContext);
        }
    }
}