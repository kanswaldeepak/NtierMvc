using NtierMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NtierMvc.Infrastructure
{
    public class InspectionSessionExpireAttribute : ActionFilterAttribute 
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = System.Web.HttpContext.Current;
            var requestContext = HttpContext.Current.Request.RequestContext;
            if (ctx.Request.QueryString["registrationId"] != null)
            {
                ApplicationFromEntity entity = new ApplicationFromEntity();
                try
                {
                    entity.RegistrationId = Convert.ToInt32(ctx.Request.QueryString["registrationId"]);
                }
                catch (Exception ex) {
                    var registrationId = ctx.Request.QueryString["registrationId"];

                    if (!String.IsNullOrEmpty(registrationId))
                    {
                       // var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("registrationId");
                        var passwordPhrase = Helper.registrationIdEncryptString;
                        var decryptregistrationId = AesCipher.DecryptString(System.Web.HttpUtility.HtmlDecode(registrationId), passwordPhrase);
                        entity.RegistrationId = Convert.ToInt32(decryptregistrationId);
                    }
                }

                if (ctx.Request.QueryString["InstituteId"] != null)
                    entity.InstituteId = Convert.ToInt32(ctx.Request.QueryString["InstituteId"]);
                if (ctx.Request.QueryString["workItemId"] != null)
                    try {
                        entity.WorkitemId = Convert.ToInt32(ctx.Request.QueryString["workItemId"]);
                    }
                    catch(Exception ex)
                    {
                        var workItemId = System.Web.HttpContext.Current.Request.QueryString["workItemId"];

                        if (!String.IsNullOrEmpty(workItemId))
                        {
                           // var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workItemId");
                            var passwordPhrase = Helper.workitemEncryptString;
                            var decryptworkItemId = AesCipher.DecryptString(System.Web.HttpUtility.HtmlDecode(workItemId), passwordPhrase);
                            entity.WorkitemId = Convert.ToInt32(decryptworkItemId);
                        }
                    }
                if (ctx.Request.QueryString["workflowInstanceId"] != null)
                    try {
                        entity.WorkflowInstanceId = Convert.ToInt32(ctx.Request.QueryString["workflowInstanceId"]);
                    }
                    catch(Exception ex)
                    {
                        var workflowInstanceId = System.Web.HttpContext.Current.Request.QueryString["workflowinstanceid"];
                        var decryptworkflowInstanceId = String.Empty;
                        if (!String.IsNullOrEmpty(workflowInstanceId))
                        {
                           // var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workflowInstanceId");
                            var passwordPhrase = Helper.workflowInstanceEncryptString;

                            try
                            {
                                decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.HtmlDecode(workflowInstanceId), passwordPhrase);
                            }
                            catch
                            {
                                decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.UrlEncode(workflowInstanceId), passwordPhrase);
                            }
                        }
                       entity.WorkflowInstanceId = Convert.ToInt32(decryptworkflowInstanceId);
                    }
                ERPContext.ApplicationFormContext = entity;
            }
            else
            {
                if (ERPContext.ApplicationFormContext == null)
                {
                    filterContext.Result =
                                new RedirectResult(new UrlHelper(requestContext).Action("", "InspectionReport", new { area = "InspectionAgency" }));
                }
            }
            return;
        }
       
    }
}