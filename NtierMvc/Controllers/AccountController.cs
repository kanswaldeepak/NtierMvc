using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Model;
using System.Net;
using NtierMvc.Infrastructure;
using NtierMvc.Model.Account;
using System.Configuration;
using Newtonsoft.Json;
using BotDetect.Web.Mvc;
using NtierMvc.Models;
using Helper = NtierMvc.Infrastructure.Helper;
using NtierMvc.Model.Application;

namespace NtierMvc.Controllers
{
    public class AccountController : Controller
    {
        private LoggingHandler _loggingHandler;

        public AccountController()
        {
            _loggingHandler = new LoggingHandler();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_loggingHandler != null)
                {
                    _loggingHandler.Dispose();
                    _loggingHandler = null;
                }
            }

            base.Dispose(disposing);
        }

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (ERPContext.UserContext != null)
            {
                ForceLogout();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                BaseModel accountMgr = new BaseModel();
                CommonDetailsEntity objSessionLogin = new CommonDetailsEntity();
                var userCommonDetails = accountMgr.GetCommonSettings();
                Session["CommonDetails"] = userCommonDetails;
                return View();
            }

        }

        [HttpPost]
        //[CaptchaValidation("CaptchaCode", "LoginCaptcha", "Incorrect CAPTCHA Code!")]
        public ActionResult Login(LoginEntity model)
        {
            try
            {
                model.ErrorMessage = "";
                if (ModelState.IsValid)
                {
                    AccountManager accountMgr = new AccountManager();

                    UserEntity usermodel = accountMgr.CheckUserExists(model.UserName, "APPLICANT", model.Password);
                    // UserEntity usermodel = accountMgr.CheckUserExists(model.Username, "INSTITUTE", model.Password);
                    if (usermodel.AllowLogin)
                    {
                        var userHostAddress = Request.UserHostAddress; // .ServerVariables["REMOTE_ADDR"];
                        var hostName = Request.UserHostName;
                        var ipv4 = "";
                        var ipv6 = "";
                        IPAddress address;
                        if (IPAddress.TryParse(userHostAddress, out address))
                        {
                            switch (address.AddressFamily)
                            {
                                case System.Net.Sockets.AddressFamily.InterNetwork:
                                    // we have IPv4
                                    ipv4 = userHostAddress;
                                    break;
                                case System.Net.Sockets.AddressFamily.InterNetworkV6:
                                    // we have IPv6
                                    ipv6 = userHostAddress;
                                    break;
                                default:
                                    // umm... yeah... I'm going to need to take your red packet and...
                                    break;
                            }
                        }
                        usermodel.IpAddress = userHostAddress;
                        var userAgent = Request.UserAgent; // HTTP_USER_AGENT

                        SessionLoginEntity objSessionLogin = new SessionLoginEntity();
                        objSessionLogin.UserId = usermodel.UserId;
                        objSessionLogin.IPV4 = ipv4;
                        objSessionLogin.IPV6 = ipv6;
                        objSessionLogin.UserAgent = userAgent;
                        objSessionLogin.SessionId = Session.SessionID;
                        //accountMgr.SaveSessionlogin(objSessionLogin);
                        var now = DateTime.UtcNow.ToLocalTime();
                        usermodel.UserName = model.UserName;

                        //for global access
                        Session["UserId"] = objSessionLogin.UserId;
                        Session["UserName"] = usermodel.UserName;
                        Session["UserModel"] = usermodel;

                        //return RedirectToAction("Home", "Application", new { Area = "" });

                        var userRolesBusiness = accountMgr.GetUserRoles(model.UserName);
                                                
                        UserRoleEntity userrole = new UserRoleEntity();
                        List<UserRoleEntity> lstRole = userRolesBusiness;
                        //List<RolePermissionEntity> lstPermisssion = accountMgr.GetUserPermissions(usermodel.UserName);
                        usermodel.UserRoles = lstRole;
                        //usermodel.Permissions = lstPermisssion;
                        Session["UserType"] = lstRole.Count > 0 ? lstRole[0].RoleName : "";
                        ERPContext.UserContext = usermodel;

                        HttpCookie myCookie = new HttpCookie(ConfigurationManager.AppSettings["CookieURL"].ToString());
                        // Set the cookie value.
                        var encryptedCookie = AesCipher.Encrypt(JsonConvert.SerializeObject(usermodel),
                            ConfigurationManager.AppSettings["CookieEncryptionKey"]);
                        myCookie.Value = encryptedCookie;
                        // Set the cookie expiration date.
                        myCookie.Expires = now.AddDays(7);
                        // Add the cookie.
                        Response.Cookies.Add(myCookie);
                        var adminUsr = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.ERPADMINROLENAME).FirstOrDefault();
                        var inspectionAgency = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.INSPECTIONAGECY).FirstOrDefault();
                        var erpVTIAdminUser = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.ERPVTIADMINROLENAME).FirstOrDefault();
                        var erpITIAdminUser = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.ERPITIADMINROLENAME).FirstOrDefault();
                        var erpFinanceOfficerUser = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.ERPFINANCEOFFICERROLENAME).FirstOrDefault();
                        var inspectionCommitte = userRolesBusiness.Where(U => U.RoleName == NtierMvc.Model.Constants.INSPECTIONCOMMITTE).FirstOrDefault();
                        //erpPrivateITI.Data.ExceptionLogging.SendExcepMessageToDB(usermodel.UserName, usermodel.isPasswordChanged.ToString(), "","","","");

                        if (usermodel.isPasswordChanged == true)
                        {
                            //return RedirectToAction("ChangePassword", "Account", new { Area = "" });

                        }
                        else
                        {
                            //return RedirectToAction(usermodel.DashBoardAction, usermodel.DashBoardController, new { Area = usermodel.DashBoardArea, id = usermodel.DashBoardView });
                            if (adminUsr != null)
                            {
                                ERPContext.AdminContext = usermodel;
                                Session["UserType"] = "Admin";
                                return RedirectToAction("", "Home", new { Area = "Admin" });
                            }

                            else if (erpVTIAdminUser != null)
                            {
                                ERPContext.AdminContext = usermodel;
                                Session["UserType"] = "Admin";
                                Session["AdminUserId"] = ERPContext.AdminContext.UserId;

                                return RedirectToAction("", "Home", new { Area = "Admin" });

                            }
                            else if (erpITIAdminUser != null)
                            {
                                ERPContext.AdminContext = usermodel;
                                Session["UserType"] = "Admin";
                                return RedirectToAction("", "Home", new { Area = "Admin" });
                            }
                            else if (erpFinanceOfficerUser != null)
                            {
                                ERPContext.AdminContext = usermodel;
                                Session["UserType"] = "Admin";
                                return RedirectToAction("FinanceOfficerDashboardView", "FinanceOfficerDetails", new { Area = "Admin" });
                            }

                            else if (inspectionAgency != null)
                            {
                                Session["UserType"] = "Agency";
                                return RedirectToAction("", "InspectionRequest", new { Area = "InspectionAgency", UserID = 0 });
                            }
                            else if (inspectionCommitte != null)
                            {
                                Session["UserType"] = "Committe";
                                //return RedirectToAction("", "InspectionReport", new { Area = "InspectionAgency" });
                                return RedirectToAction("", "InspectionRequest", new { Area = "InspectionAgency", UserID = usermodel.UserId });
                            }
                            else
                            {
                                //#if DEBUG
                                 return RedirectToAction("CustomerMaster", "Customer");
                                //#else
                                //var objAgencyContext = new AgencyContext();
                                //objAgencyContext.AppCode = usermodel.AppCode;
                                //objAgencyContext.RegistrationId = usermodel.RegistrationID;
                                //objAgencyContext.WorkItemId = usermodel.WorkItemId.Value;
                                //objAgencyContext.WorkFlowInstanceId = usermodel.WorkflowInstanceId.Value;
                                //ERPContext.AgencyContext = objAgencyContext;
                                //PaymentGateWayResponse objPaymentGateWayResponse = new PaymentGateWayResponse();
                                //objPaymentGateWayResponse = new PaymentManager().GetPaymentGatewayResponseofRegistrationNumber(model.Username);

                                //if (objPaymentGateWayResponse.STC == null)
                                //{
                                //    return RedirectToAction("PaymentRegistration", "Payment", new { strUserName = model.Username });
                                //}
                                //else if (objPaymentGateWayResponse.STC == "101" || objPaymentGateWayResponse.STC == "111")
                                //{
                                //    return RedirectToAction("PaymentRegistration", "Payment", new { strUserName = model.Username });
                                //}
                                //else
                                //{

                                //    return RedirectToAction("Home", "Application");
                                //}
                                //#endif
                            }
                        }
                    }
                    else
                    {
                        model.ErrorMessage = "Invalid Username or Password.";
                        ModelState.AddModelError("CaptchaCode", "Invalid Username or Password.");

                    }
                }
                else
                {
                    model.ErrorMessage = "Invalid Username or Password or CAPTCHA.";

                }
            }
            catch (Exception ex)
            {
                Logger.Error("Login", ex);
            }
            MvcCaptcha.ResetCaptcha("GetLoginCaptcha");
            return View(model);
        }

        public ActionResult Registration()
        {
            if (TempData.ContainsKey("StatusMsg"))
            {
                ViewBag.StatusMsg = Convert.ToString(TempData["StatusMsg"]);
                ViewBag.UserName = Convert.ToString(TempData["UserName"]);
            }
            //BaseModel objBaseModel = new BaseModel();
            BaseModel model = new BaseModel();
            ViewBag.PermissionNames = model.GetTaxonomyDropDownItems("", "Permission");
            //ViewBag.FunctionalArea = model.GetTaxonomyDropDownItems("", "Functional Area");
            ViewBag.GenderType = model.GetTaxonomyDropDownItems("", "Gender");
            ViewBag.GenderTitle = model.GetTaxonomyDropDownItems("", "Title");
            return View();
        }

        [HttpGet]
        public ActionResult SessionLogout()
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[ConfigurationManager.AppSettings["CookieURL"].ToString()];
                if (authCookie != null)
                {
                    var decryptedObject = AesCipher.Decrypt(authCookie.Value,
                        ConfigurationManager.AppSettings["CookieEncryptionKey"]);
                    var user = JsonConvert.DeserializeObject<UserEntity>(decryptedObject);
                    SessionLoginEntity objSessionLogin = new SessionLoginEntity();
                    objSessionLogin.SessionId = user.SessionId;
                    objSessionLogin.UserId = user.UserId;
                    objSessionLogin.logout = LogoutOption.LogoutCode.Normal;
                    AccountManager accountMgr = new AccountManager();
                    //Logger.LogApplicationData("Login", "SessionLogout", decryptedObject, "Cookie decrypted Successfully");
                    accountMgr.SaveSessionLogout(objSessionLogin);
                    //Logger.LogApplicationData("Login", "SessionLogout", $"{user.UserId}", "SessionLogout Successfully");
                    Session.Clear();
                    Session.Abandon();
                    ViewBag.Message = "Session Expired. Please Login again.";
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("session Logout", ex);
            }
            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public ActionResult ForceLogout()
        {
            var user = new UserEntity();

            try
            {
                HttpCookie authCookie = Request.Cookies[ConfigurationManager.AppSettings["CookieURL"].ToString()];
                if (Session["UserModel"] == null)
                {
                    if (authCookie != null)
                    {
                        var decryptedObject = AesCipher.Decrypt(authCookie.Value,
                            ConfigurationManager.AppSettings["CookieEncryptionKey"]);
                        user = JsonConvert.DeserializeObject<UserEntity>(decryptedObject);
                        //Logger.LogApplicationData("Login", "SessionLogout", decryptedObject, "Cookie decrypted Successfully");
                    }
                }
                else
                {
                    user = (UserEntity)Session["UserModel"];
                }
                if (user.UserId != 0)
                {
                    SessionLoginEntity objSessionLogin = new SessionLoginEntity();
                    objSessionLogin.SessionId = user.SessionId;
                    objSessionLogin.UserId = user.UserId;
                    objSessionLogin.logout = LogoutOption.LogoutCode.Normal;
                    AccountManager accountMgr = new AccountManager();

                    accountMgr.SaveSessionLogout(objSessionLogin);
                    if (authCookie != null && !user.RememberMe)
                    {
                        authCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(authCookie);
                    }
                    //Logger.LogApplicationData("Login", "SessionLogout", $"{user.UserId}", "ForceLogout Successfully");
                }
                Session.Clear();
                Session.Abandon();
            }
            catch (Exception e)
            {
                Logger.Error("Force Logout", e);
            }

            if (Request.AcceptTypes.Contains("text/html"))
            {
                return RedirectToAction("Login", "Account");
            }
            else if (Request.AcceptTypes.Contains("application/json"))
            {
                return Json(new { uri = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }
            //else if (Request.AcceptTypes.Contains("application/xml") ||
            //         Request.AcceptTypes.Contains("text/xml"))
            //{
            //    //return Content("", "text/xml");
            //    // Create your own XDocument according to your requirements
            //    var xml = new XDocument(
            //        new XElement("root",
            //            new XAttribute("version", "2.0"),
            //            new XElement("uri", Url.Action("Login", "Account"))));

            //    return new XmlActionResult(xml);
            //}
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [SessionExpire]
        public ActionResult ChangePassword()
        {
            ChangePasswodEntity entity = new ChangePasswodEntity();
            AccountManager accountMgr = new AccountManager();
            entity.UserName = ERPContext.UserContext.UserName;
            entity = accountMgr.GetUserDetailsForChngePwd(entity);
            entity.UserName = ERPContext.UserContext.UserName;
            entity.RegNumber = ERPContext.UserContext.UserName;
            //TempData["OldPassword"] = AesCipher.Decrypt(entity.Password, entity.passPhrase);
            return View(entity);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswodEntity entityModel)
        {
            try
            {
                if (entityModel.Password == entityModel.ConfirmPassword)
                {
                    string CurrPassword = AesCipher.Decrypt(ERPContext.UserContext.Password, ERPContext.UserContext.PasswordPhrase);
                    if (CurrPassword == entityModel.OldPassword)
                    {
                        if (entityModel.OldPassword != entityModel.Password)
                        {
                            AccountManager accountMgr = new AccountManager();

                            var passwordPhrase = Infrastructure.Helper.RandomString(48);
                            var encryPassword = AesCipher.Encrypt(entityModel.Password, passwordPhrase);
                            entityModel.Password = encryPassword;
                            entityModel.passPhrase = passwordPhrase;

                            //UPDATING CONTEXT WITH NEW PASSWORD & PASSWORD PHRASE
                            ERPContext.UserContext.Password = encryPassword;
                            ERPContext.UserContext.PasswordPhrase = passwordPhrase;
                            //

                            accountMgr.ChangePwd(entityModel);

                            ViewBag.Message = "Success";
                            //return RedirectToAction("Home", "Application", new { area = "" });
                            return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);

                            //}                   
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(entityModel.OldPassword) && !string.IsNullOrEmpty(entityModel.Password))
                                ViewBag.Message = "SamePassword";

                        }

                        //return View();
                        return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(entityModel.OldPassword))
                            ViewBag.Message = "WrongPassword";
                        return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);

                        //return View(entityModel);
                    }
                }
                ViewBag.Message = "NotSame";
                return Json(ViewBag.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);

            }
            //return View(entityModel);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ChangePasswodEntity viewModel)
        {
            AccountManager accountMgr = new AccountManager();
            string RegConform = accountMgr.CheckRegNumber(viewModel);
            if (RegConform != "")
            {
                //accountMgr.ChangePwd(viewModel);
                ViewBag.Message = "Success";

                string firstsix = RegConform.ToString().Substring(0, 6); //gets the first 6 digits
                string Lastfour = RegConform.ToString().Substring(6, 4); //gets the next 4

                string combinedNumber = "******" + Lastfour;

                ViewBag.Number = combinedNumber;
            }
            else
            {
                ViewBag.Message = "Error";
            }

            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(ChangePasswodEntity viewEntity)
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(UserEntity uE, HttpPostedFileBase passportPhoto)
        {
            AccountManager objApplicationModel = new AccountManager();
            var passwordPhrase = Helper.RandomString(48);
            var encryPassword = AesCipher.Encrypt(uE.Password, passwordPhrase);
            uE.Password = encryPassword;
            uE.PasswordPhrase = passwordPhrase;

            if (passportPhoto != null)
            {
                FileUploadEntity objFileUpload = new FileUploadEntity();
                objFileUpload.ContentType = passportPhoto.ContentType;
                objFileUpload.FileName = passportPhoto.FileName;
                objFileUpload.FileExtension = System.IO.Path.GetExtension(objFileUpload.FileName);
                byte[] uploadedFile = new byte[passportPhoto.InputStream.Length];
                passportPhoto.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                objFileUpload.FileByteArray = uploadedFile;
                uE.PassportPhoto = objFileUpload;
            }

            /*, HttpPostedFileBase passportPhoto, HttpPostedFileBase educationPermissionFile, HttpPostedFileBase resolutionPertainingAuthorizedPersonFile, string rbjrnSnrCollege*/
            //viewModel.PromotingOrganizationModel.TypeofInstituteAttachedWithforCourses = rbjrnSnrCollege;

            //if (passportPhoto != null)
            //{
            //    FileUploadEntity objFileUpload = new FileUploadEntity();
            //    objFileUpload.ContentType = passportPhoto.ContentType;
            //    objFileUpload.FileName = passportPhoto.FileName;
            //    objFileUpload.FileExtension = System.IO.Path.GetExtension(objFileUpload.FileName);
            //    byte[] uploadedFile = new byte[passportPhoto.InputStream.Length];
            //    passportPhoto.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            //    objFileUpload.FileByteArray = uploadedFile;
            //    viewModel.AuthorizedRepresentativeModel.PassportPhoto = objFileUpload;
            //}
            //if (educationPermissionFile != null)
            //{
            //    FileUploadEntity objFileUpload = new FileUploadEntity();
            //    objFileUpload.ContentType = educationPermissionFile.ContentType;
            //    objFileUpload.FileName = educationPermissionFile.FileName;
            //    objFileUpload.FileExtension = System.IO.Path.GetExtension(objFileUpload.FileName);
            //    byte[] uploadedFile = new byte[educationPermissionFile.InputStream.Length];
            //    educationPermissionFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            //    objFileUpload.FileByteArray = uploadedFile;
            //    viewModel.PromotingOrganizationModel.EducationPermissionFile = objFileUpload;
            //}
            //if (resolutionPertainingAuthorizedPersonFile != null)
            //{
            //    FileUploadEntity objFileUpload = new FileUploadEntity();
            //    objFileUpload.ContentType = resolutionPertainingAuthorizedPersonFile.ContentType;
            //    objFileUpload.FileName = resolutionPertainingAuthorizedPersonFile.FileName;
            //    objFileUpload.FileExtension = System.IO.Path.GetExtension(objFileUpload.FileName);
            //    byte[] uploadedFile = new byte[resolutionPertainingAuthorizedPersonFile.InputStream.Length];
            //    resolutionPertainingAuthorizedPersonFile.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            //    objFileUpload.FileByteArray = uploadedFile;
            //    viewModel.AuthorizedRepresentativeModel.ResolutionPertainingAuthorizedPersonFile = objFileUpload;
            //}
            var registraionID = objApplicationModel.SaveApplicationRegistraionDetails(uE);
            BaseModel objBase = new BaseModel();
            //var ApplicationType = objBase.GetApplicationTypeForReg(viewModel.RegistrationModel.ApplicationTypeID);
            //var ApplicationType = objBase.GetApplicationTypeForReg(1);
            if (registraionID.Count > 0)
            {
                //Payment Gateway

                if (registraionID[0] == "UserName Already Present")
                {
                    TempData["StatusMsg"] = "Error";
                    TempData["ErrorMsg"] = registraionID[0];
                }
                else
                {
                    TempData["StatusMsg"] = "Success";
                    TempData["UserName"] = registraionID.FirstOrDefault().Value;
                    TempData["FirstName"] = uE.FirstName;
                    TempData["LastName"] = uE.LastName;
                    TempData["ErrorMsg"] = "";
                    //TempData["PromotingOrg"] = viewModel.PromotingOrganizationModel.SectorName;
                    //TempData["ApplicationType"] = ApplicationType;
                }
            }
            else
            {
                TempData["StatusMsg"] = "Error";
                TempData["UserName"] = "";
            }
            return RedirectToAction("Registration", "Account");
            //return RedirectToAction("MakePayment", "Payment");//redirecting from registration to payment page
        }
    }
}