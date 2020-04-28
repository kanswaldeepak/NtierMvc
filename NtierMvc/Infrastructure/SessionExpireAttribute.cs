using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Infrastructure;
using NtierMvc.Models;

namespace NtierMvc.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [ActionModel]    
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// comma seperated action names
        /// </summary>
        public string AvoidActions { get; set; }

        private DateTime _now = DateTime.Now;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestContext = HttpContext.Current.Request.RequestContext;

            HttpContext ctx = System.Web.HttpContext.Current;

            if (string.IsNullOrEmpty(AvoidActions))
            {
                AvoidActions = "";
            }

            if (!AvoidActions.Contains("*"))
            {
                var actions = AvoidActions.Split(',');
                var currentActionName =
                    (filterContext.ActionDescriptor).ActionName;
                if (!actions.Contains(currentActionName))
                    if (ctx.Request.ContentType == "json" || filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //if (System.Web.HttpContext.Current.Session["UserModel"] == null)

                        if (ERPContext.UserContext == null)
                        {
                            var response = ctx.Response;
                            response.StatusCode = 401;

                            response.StatusDescription = "json session out";
                            filterContext.HttpContext.Items["AjaxSessionTimeOut"] = true;
                        }
                        else {
                            var decryptworkflowInstanceId = String.Empty;
                            try
                            {
                                var workflowInstanceId = System.Web.HttpContext.Current.Request.QueryString["workflowinstanceid"];

                                if (!String.IsNullOrEmpty(workflowInstanceId))
                                {
                                    var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workflowInstanceId");
                                    var passwordPhrase = Helper.workflowInstanceEncryptString;

                                    try
                                    {
                                        decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(workflowInstanceId), passwordPhrase);
                                    }
                                    catch
                                    {
                                        decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.UrlEncode(workflowInstanceId), passwordPhrase);
                                    }
                                    filterContext.ActionParameters["workflowInstanceId"] = Convert.ToInt32(decryptworkflowInstanceId);
                                }
                              
                            }
                            catch (Exception ex)
                            {
                            }
                            try
                            {
                                var workItemId = System.Web.HttpContext.Current.Request.QueryString["workItemId"];

                                if (!String.IsNullOrEmpty(workItemId))
                                {
                                    var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workItemId");
                                    var passwordPhrase = Helper.workitemEncryptString;
                                    var decryptworkItemId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(workItemId), passwordPhrase);
                                    filterContext.ActionParameters["workItemId"] = Convert.ToInt32(decryptworkItemId);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            try
                            {
                                var registrationId = System.Web.HttpContext.Current.Request.QueryString["registrationId"];

                                if (!String.IsNullOrEmpty(registrationId))
                                {
                                    var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("registrationId");
                                    var passwordPhrase = Helper.registrationIdEncryptString;
                                    var decryptregistrationId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(registrationId), passwordPhrase);
                                    filterContext.ActionParameters["registrationId"] = Convert.ToInt32(decryptregistrationId);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        
                    }
                    else
                    {
                        DateTime currentdate = DateTime.Now;

                        //var sessionId = Convert.ToString(System.Web.HttpContext.Current.Session.SessionID);
                        var sessionId = System.Web.HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value;

                        if (System.Web.HttpContext.Current.Session["UserType"] == null)
                        {
                            filterContext.Result =
                                  new RedirectResult(new UrlHelper(requestContext).Action("SessionLogout", "Account", new { area = "" }));
                            return;
                        }
                        else
                        {
                            var userType = Convert.ToString(System.Web.HttpContext.Current.Session["UserType"]);
                            if (userType == "Institute" || userType == "Agency" || userType=="Committe")
                            {
                                if (userType == "Institute")
                                {
                                    BaseModel model = new BaseModel();
                                    var entity = model.GetApplicationSubmittedSatus(ERPContext.UserContext.RegistrationID);
                                    var isApplicationFilled = false;
                                    if (entity.AuthorizedRepSubmissionStatus && entity.BuildingSubmissionStatus
                && entity.ChairmanDetailsSubmissionStatus && entity.CommonFacilitySubmissionStatus
                && entity.DeclarationSubmissionStatus 
                && entity.FundsAvltyandSubmissionStatus && entity.InfrastructureSubmissionStatus
                //&& entity.LandDetailsSubmissionStatus 
                && entity.OtherInfrastructureSubmissionStatus
                   && entity.PowerDetailsSubmissionStatus && entity.PromotingOrganSubmissionStatus
                   && entity.ProposedInstSubmissionStatus && entity.ProposedTradeSubmissionStatus
                   && entity.TrusteeDetailsSubmissionStatus && entity.WorkshopSubmissionStatus && entity.PastPerformanceSubmissionStatus
                   )
                                    {
                                        isApplicationFilled = true;

                                    }
                                        System.Web.HttpContext.Current.Session["FeesPaid"] = entity.FeesSubmissionStatus;
                                    System.Web.HttpContext.Current.Session["ApplicationFilled"] = isApplicationFilled;
                                }
                                if (ERPContext.UserContext == null)
                                {
                                    filterContext.Result =
                                        new RedirectResult(new UrlHelper(requestContext).Action("SessionLogout", "Account", new { area = "" }));
                                    return;
                                }
                                else
                                {
                                    var user = ERPContext.UserContext;
                                    var decryptworkflowInstanceId = String.Empty;
                                    try {
                                        var workflowInstanceId = System.Web.HttpContext.Current.Request.QueryString["workflowinstanceid"];

                                        if (!String.IsNullOrEmpty(workflowInstanceId))
                                        {
                                            var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workflowInstanceId");
                                            var passwordPhrase = Helper.workflowInstanceEncryptString;
                                           
                                            try
                                            {
                                                decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(workflowInstanceId), passwordPhrase);
                                            }
                                            catch
                                            {
                                                decryptworkflowInstanceId = AesCipher.DecryptString(System.Web.HttpUtility.UrlEncode(workflowInstanceId), passwordPhrase);
                                            }
                                        }
                                            filterContext.ActionParameters["workflowInstanceId"] = Convert.ToInt32(decryptworkflowInstanceId);
                                        }
                                    
                                    catch(Exception ex)
                                    {

                                    }
                                    try
                                    {
                                        var workItemId = System.Web.HttpContext.Current.Request.QueryString["workItemId"];
                                        var decryptworkItemId = string.Empty;
                                        if (!String.IsNullOrEmpty(workItemId))
                                        {
                                            var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("workItemId");
                                            var passwordPhrase = Helper.workitemEncryptString;
                                            
                                            try
                                            {
                                                decryptworkItemId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(workItemId), passwordPhrase);
                                            }
                                            catch
                                            {
                                                decryptworkItemId = AesCipher.DecryptString(System.Web.HttpUtility.UrlEncode(workItemId), passwordPhrase);
                                            }
                                            filterContext.ActionParameters["workItemId"] = Convert.ToInt32(decryptworkItemId);
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    try
                                    {
                                        var registrationId = System.Web.HttpContext.Current.Request.QueryString["registrationId"];
                                        var decryptregistrationId = string.Empty;
                                        if (!String.IsNullOrEmpty(registrationId))
                                        {
                                            var valueProviderResult = filterContext.Controller.ValueProvider.GetValue("registrationId");
                                            var passwordPhrase = Helper.registrationIdEncryptString;
                                          //  var decryptregistrationId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(registrationId), passwordPhrase);
                                            try
                                            {
                                                decryptregistrationId = AesCipher.DecryptString(System.Web.HttpUtility.UrlDecode(registrationId), passwordPhrase);
                                            }
                                            catch
                                            {
                                                decryptregistrationId = AesCipher.DecryptString(System.Web.HttpUtility.UrlEncode(registrationId), passwordPhrase);
                                            }
                                            filterContext.ActionParameters["registrationId"] = Convert.ToInt32(decryptregistrationId);
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    //TODO: 1. check if flag is 1 then perform session abandon and redired to login page
                                    //TODO: 2. if flag is 0, do nothing

#if DEBUG
                                    return;

#else
                            if (user.ObjectType == "APPLICANT")
                            {
                                //PaymentGateWayResponse objPaymentGateWayResponse = new PaymentGateWayResponse();
                                //objPaymentGateWayResponse = new PaymentManager().GetPaymentGatewayResponseofRegistrationNumber(user.UserName);

                                //if (objPaymentGateWayResponse.STC == null)
                                //{
                                //    filterContext.Result = new RedirectResult(new UrlHelper(requestContext).Action("PaymentRegistration", "Payment", new { strUserName = user.UserName }));
                                //    return;
                                //    //return RedirectToAction("PaymentRegistration", "Payment", new { strUserName = model.Username });
                                //}
                                //else if (objPaymentGateWayResponse.STC == "101" || objPaymentGateWayResponse.STC == "111")
                                //{
                                //    filterContext.Result = new RedirectResult(new UrlHelper(requestContext).Action("PaymentRegistration", "Payment", new { strUserName = user.UserName }));
                                //    return;
                                //    //return RedirectToAction("PaymentRegistration", "Payment", new { strUserName = model.Username });
                                //}
                                //else
                                //{
                                //    return;
                                //}
                            }
                            else
                                return;
#endif
                                    //AccountManager accountManager = new AccountManager();
                                    //var dtble = accountManager.CheckLoginSession(sessionId, user.UserId);

                                    //if (dtble != null && dtble.Any() && dtble.FirstOrDefault() != null)
                                    //{
                                    //    //System.Web.HttpContext.Current.Session.Clear();
                                    //    if (dtble.FirstOrDefault().IsActive && String.Equals(
                                    //            $"{dtble.FirstOrDefault().SessionId}",
                                    //            sessionId, StringComparison.InvariantCultureIgnoreCase))
                                    //    {
                                    //        return;
                                    //    }
                                    //}

                                    //filterContext.Result =
                                    //    new RedirectResult(new UrlHelper(requestContext).Action("ForceLogout", "Account", new { area = "" }));

                                }
                            }
                            else if (userType == "Admin")
                            {
                                if (ERPContext.AdminContext == null)
                                {
                                    filterContext.Result =
                                        new RedirectResult(new UrlHelper(requestContext).Action("SessionLogout", "Account", new { area = "" }));
                                    return;
                                }
                                else
                                {
                                    var user = ERPContext.AdminContext;

                                    //TODO: 1. check if flag is 1 then perform session abandon and redired to login page
                                    //TODO: 2. if flag is 0, do nothing

                                    return;

                                }
                            }
                          

                        }
                    }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            //if(requestContext.HttpContext.Request.IsAjaxRequest())
            //{ 
            //    filterContext.RequestContext.HttpContext.Response.StatusCode = 500;
            //}
        }
    }
}