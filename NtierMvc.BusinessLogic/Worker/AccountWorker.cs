using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Utility;
using System.Configuration;
using System.IO;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.Account;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;
using Helper = NtierMvc.BusinessLogic.Utility.Helper;
using NtierMvc.Model.Application;

namespace NtierMvc.BusinessLogic.Worker
{
    public class AccountWorker : IAccountWorker, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        public AccountWorker()
        {
            _loggingHandler = new LoggingHandler();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
        
        //ShortMessageServiceClient _smsClient = new ShortMessageServiceClient();
        //EmailContent objEmailContent = new EmailContent();
        public List<ActiveSession> CheckLoginSession(string sessionId, int userId)
        {
            var result = new List<ActiveSession>();
            try
            {
                var dt = _repository.CheckLoginSession(sessionId, userId);
                foreach (DataRow row in dt.Rows)
                {
                    var active = new ActiveSession();

                    active.IsActive = Convert.ToBoolean(row["IsSessionActive"]);
                    active.SessionId = $"{row["SessionID"]}";
                    result.Add(active);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            //dtble.Rows[0]["IsActive"]
            //dtble.Rows[0]["SessionID"]
            return result;
        }

        public List<UserRoleEntity> GetUserRoles(string username)
        {
            var result = new List<UserRoleEntity>();
            try
            {
                var dt = _repository.GetUserRoles(username);
                foreach (DataRow row in dt.Rows)
                {
                    var role = new UserRoleEntity();
                    role.RoleName = $"{row["RoleName"]}";
                    role.RoleId = row.IsNull("RoleID") ? 0 : (int)row["RoleID"];
                    role.IsSysAdmin = row.IsNull("IsSysAdmin") ? false : (bool)row["IsSysAdmin"];
                    result.Add(role);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<RolePermissionEntity> GetUserPermissions(string username)
        {
            var result = new List<RolePermissionEntity>();
            try
            {
                var dt = _repository.GetUserPermissions(username);
                foreach (DataRow row in dt.Rows)
                {
                    var permission = new RolePermissionEntity();
                    permission.PermissionRoute = $"{row["UrlRoute"]}";
                    permission.PermissionRouteWrite = $"{row["UrlRouteReadWrite"]}";
                    permission.RoleId = row.IsNull("RoleID") ? 0 : (int)row["RoleID"];
                    permission.MainMenu = row.IsNull("MainMenu") ? "" : (string)row["MainMenu"];
                    permission.SubMenu = row.IsNull("SubMenu") ? "" : (string)row["SubMenu"];
                    //permission.PermissionId = row.IsNull("PermissionID") ? 0 : (int)row["PermissionID"];

                    result.Add(permission);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public void SaveSessionLogout(string sessionId, int userId, LogoutOption.LogoutCode logout)
        {
            try
            {
                var dt = _repository.SaveSessionLogout(sessionId, logout, userId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
        }

        public UserEntity CheckUserExists(string username, string objectType, string password)
        {
            var entity = new UserEntity();
            try
            {
                var dt = _repository.CheckUserExists(username, objectType);
                entity.AllowLogin = false;
                if (dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    entity.UserId = dr.IsNull("Id") ? 0 : Convert.ToInt32(dt.Rows[0]["Id"]);
                    entity.UserName = dr.IsNull("UserName") ? string.Empty : Convert.ToString(dt.Rows[0]["UserName"]);
                    entity.Password = dr.IsNull("Password") ? string.Empty : Convert.ToString(dt.Rows[0]["Password"]);
                    entity.PasswordPhrase = dr.IsNull("PasswordPhrase") ? string.Empty : Convert.ToString(dt.Rows[0]["PasswordPhrase"]);

                    entity.FirstName = dr.IsNull("FirstName") ? "" : Convert.ToString(dt.Rows[0]["FirstName"]);
                    entity.LastName = dr.IsNull("LastName") ? "" : Convert.ToString(dt.Rows[0]["LastName"]);

                    entity.RegistrationDate = dr.IsNull("RegistrationDate") ? "" : Convert.ToString(dt.Rows[0]["RegistrationDate"]);
                    entity.LoggedInEmailId = dr.IsNull("EmailId") ? "" : Convert.ToString(dt.Rows[0]["EmailId"]);
                    entity.LoggedInMobileNumber = dr.IsNull("MobileNumber") ? "" : Convert.ToString(dt.Rows[0]["MobileNumber"]);
                    entity.LoggedInEmpId = dr.IsNull("EmpId") ? "" : Convert.ToString(dt.Rows[0]["EmpId"]);
                    entity.DeptName = dr.IsNull("DeptName") ? "" : Convert.ToString(dt.Rows[0]["DeptName"]);
                    entity.Permission = dr.IsNull("Permission") ? "" : Convert.ToString(dt.Rows[0]["Permission"]);
                    entity.Designation = dr.IsNull("DesignationName") ? "" : Convert.ToString(dt.Rows[0]["DesignationName"]);
                    entity.SignImage = dr.IsNull("SignImage") ? "" : Convert.ToString(dt.Rows[0]["SignImage"]);
                    entity.EmailID = Utility.AesCipher.Decrypt(entity.Password, entity.PasswordPhrase);


                    if (Utility.AesCipher.Decrypt(entity.Password, entity.PasswordPhrase) == password)
                    {
                        entity.AllowLogin = true;
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return entity;
        }

        public void SaveSessionlogin(int userId, string ipv4, string ipv6, string userAgent, string sessionId)
        {
            try
            {
                var dt = _repository.SaveSessionlogin(userId, ipv4, ipv6, userAgent.Truncate(1024), sessionId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
        }

        public string CheckRegNumber(ChangePasswodEntity entity)
        {
            var dt = _repository.CheckRegNumber(entity);
            bool AllowLogin = false;
            string MobileNumber = "";
            try
            {
                if (dt.Rows.Count > 0)
                {
                    //EMAIL CODE 
                    //if (!string.IsNullOrEmpty(entity.EmailId))
                    //{
                    //    string path =
                    //        System.Web.HttpContext.Current.Server.MapPath(
                    //         ConfigurationManager.AppSettings["ResetPasswordEmailHtmlPath"]);
                    //    string mailcontent = string.Empty;

                    //    List<string> objMailToAddresses = new List<string>();
                    //    List<string> objCarbonCopyAddresses = new List<string>();
                    //    List<string> objBroadcastCarbonCopyAddresses = new List<string>();

                    //    objMailToAddresses.Add(entity.EmailId);
                    //    EmailContent objEmailContent = new EmailContent();

                    //    objEmailContent.MailToAddresses = objMailToAddresses;
                    //    objEmailContent.CarbonCopyAddresses = objCarbonCopyAddresses;
                    //    objEmailContent.BroadcastCarbonCopyAddresses = objBroadcastCarbonCopyAddresses;
                    //    objEmailContent.Subject = "Reset Password";
                    //    objEmailContent.MailPriority = System.Net.Mail.MailPriority.Normal;
                    //    mailcontent = File.ReadAllText(path);
                    //    //.Replace("@@Password@@", candPassword);
                    //    objEmailContent.Body = mailcontent;

                    //    objEmailContent.Attachments = null;

                    //    try
                    //    {
                    //        Helper.SendMailMessage(objEmailContent);
                    //        AllowLogin = true;
                    //    }
                    //    catch (Exception Ex)
                    //    { }
                    //}

                    //SMS CODE
                    string SMSContent = Convert.ToString(ConfigurationManager.AppSettings["ForgotPassSMS"]);
                    string ePassword = Convert.ToString(dt.Rows[0]["Password"]);
                    string ePassPhrase = Convert.ToString(dt.Rows[0]["PasswordPhrase"]);
                    string CurrPassword = AesCipher.Decrypt(ePassword, ePassPhrase);

                    SMSContent = SMSContent.Replace("@Username@", Convert.ToString(dt.Rows[0]["Username"]));
                    SMSContent = SMSContent.Replace("@Password@", CurrPassword);
                    MobileNumber = Convert.ToString(dt.Rows[0]["MobileNumber"]);

                    ShortMessageServiceClient oSMS = new ShortMessageServiceClient();
                    oSMS.SendMobileOTPMessage(Convert.ToString(dt.Rows[0]["MobileNumber"]), SMSContent);
                    AllowLogin = true;
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return MobileNumber;
        }
        public void ChangePwd(ChangePasswodEntity entity)
        {
            try
            {
                _repository.ChangePwd(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
        }
        public ChangePasswodEntity GetUserDetailsForChngePwd(string userName)
        {
            ChangePasswodEntity Entity = new ChangePasswodEntity();
            try
            {
                var dt = _repository.GetUserDetailsForChngePwd(userName);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Entity.MobileNumber = Convert.ToString(row["MobileNumber"]);
                    }
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return Entity;
        }
        public bool CheckUserDetails(ChangePasswodEntity entity)
        {
            bool IsUserValid = false;
            try
            {
                var dt = _repository.CheckUserDetails(entity);

                if (dt.Rows.Count > 0)
                {
                    IsUserValid = true;
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return IsUserValid;
        }
        public PagewiseAccessEntity GetPagewiseAccess(PagewiseAccessEntity entity)
        {
            PagewiseAccessEntity returEntity = new PagewiseAccessEntity();
            try
            {
                var dt = _repository.GetPagewiseAccess(entity);

                if (dt != null && dt.Rows.Count > 0)
                {
                    returEntity = entity;
                    returEntity.IsEditAllowed = Convert.ToBoolean(dt.Rows[0]["IsEditAllowed"]);
                    returEntity.IsDeleteAllowed = Convert.ToBoolean(dt.Rows[0]["IsDeleteAllowed"]);
                    returEntity.IsViewAllowed = Convert.ToBoolean(dt.Rows[0]["IsViewAllowed"]);
                    returEntity.IsInspectionRemarksAllowed = Convert.ToBoolean(dt.Rows[0]["IsInspectionRemarksAllowed"]);
                    returEntity.SectionId = Convert.ToInt32(dt.Rows[0]["SectionId"]);
                    returEntity.SectionName = dt.Rows[0]["SectionName"].ToString();
                }


            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return returEntity;
        }

        public AffiliationTypeEntity GetAffiliationType(int RegistrationId)
        {
            AffiliationTypeEntity entity = new AffiliationTypeEntity();
            try
            {
                var dt = _repository.GetAffiliationType(RegistrationId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    entity.AffiliationType = dt.Rows[0]["AffiliationType"].ToString();
                    entity.AffiliationTypeId = Convert.ToInt32(dt.Rows[0]["AffiliationTypeId"]);
                }


            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return entity;
        }

        readonly Repository _repository = new Repository();
        public Dictionary<int, string> SaveApplicationRegistraionDetails(UserEntity objUser)
        {
            Dictionary<int, string> dicReturnValues = new Dictionary<int, string>();
            try
            {
                //var AppTypedt = _repository.GetApplicantType(objRegistration.ApplicationTypeID);                
                //if (AppTypedt.Rows.Count > 0)
                //{
                //    objUser.ApplicationType = Convert.ToString(AppTypedt.Rows[0]["ApplicationType"]);
                //}

                var dt = _repository.SaveUserRegistraionDetails(objUser);
                //SEND MAIL
                if (dt != null && dt.Rows.Count > 0 && !string.IsNullOrEmpty(objUser.EmailID))
                {
                    try
                    {
                        string path =
                        HttpContext.Current.Server.MapPath(
                         ConfigurationManager.AppSettings["OrganizationRegistrationEmailHtmlPath"]);
                        string mailcontent = string.Empty;
                        var RepresentativeFullName = objUser.FirstName + " " + objUser.LastName;
                        int RepresentativeIDNo = Convert.ToInt32(dt.Rows[0]["ID"]);
                        var RepresentativeRegistno = Convert.ToString(dt.Rows[0]["UserName"]);

                        if (RepresentativeIDNo != 0)
                        {
                            List<string> objMailToAddresses = new List<string>();
                            List<string> objCarbonCopyAddresses = new List<string>();
                            List<string> objBroadcastCarbonCopyAddresses = new List<string>();

                            objMailToAddresses.Add(objUser.EmailID);
                            EmailContent objEmailContent = new EmailContent();

                            objEmailContent.MailToAddresses = objMailToAddresses;
                            objEmailContent.CarbonCopyAddresses = objCarbonCopyAddresses;
                            objEmailContent.BroadcastCarbonCopyAddresses = objBroadcastCarbonCopyAddresses;
                            objEmailContent.Subject = "Registration Confirmed";
                            objEmailContent.MailPriority = System.Net.Mail.MailPriority.Normal;
                            objUser.ApplicationType = "ERP";
                            mailcontent = File.ReadAllText(path);
                            mailcontent = mailcontent.Replace("@@RepresentativeName@@", RepresentativeFullName)
                                .Replace("@@RegistrationNumber@@", RepresentativeRegistno)
                            .Replace("@@AppType@@", objUser.ApplicationType);
                            //.Replace("@@Password@@", candPassword);
                            objEmailContent.Body = mailcontent;

                            objEmailContent.Attachments = null;


                            Helper.SendMailMessage(objEmailContent);
                            string UserName = objUser.FirstName + ' ' + objUser.LastName;
                            string MobileNumber = objUser.MobileNumber;
                            string RegNumber = Convert.ToString(dt.Rows[0]["UserName"]);
                            var strPassword = AesCipher.Decrypt(objUser.Password, objUser.PasswordPhrase);
                            //SendConfirmRegSMS(MobileNumber, UserName, strPassword, RegNumber, objUser.ApplicationType);
                        }

                    }
                    catch (Exception Ex)
                    { }

                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    Dictionary<int, string> dicContainers = new Dictionary<int, string>();
                    dicReturnValues.Add(Convert.ToInt32(dt.Rows[0]["ID"]), Convert.ToString(dt.Rows[0]["UserName"]));

                    //if (objUser.PassportPhoto != null && objUser.PassportPhoto.FileByteArray != null)
                    //{
                    //    //Dictionary<int, string> dicContainers = new Dictionary<int, string>();
                    //    dicContainers.Add(1, "ERP");
                    //    dicContainers.Add(2, Convert.ToString(dt.Rows[0]["UserName"]).Split('/')[0].ToLower());
                    //    dicContainers.Add(3, Convert.ToString(dt.Rows[0]["UserID"]));
                    //    dicContainers.Add(4, "authorizedrepresentative");
                    //    objUser.PassportPhotoURL = _repository.UploadFilesToBlob(objUser.PassportPhoto, dicContainers);
                    //}
                    ////////Bi-Focal/////////
                    //if (objauthrdReptve.ResolutionPertainingAuthorizedPersonFile != null && objauthrdReptve.ResolutionPertainingAuthorizedPersonFile.FileByteArray != null)
                    //{
                    //    objauthrdReptve.ResolutionPertainingAuthorizedPersonFileUrl = _repository.UploadFilesToBlob(objOrgzation.EducationPermissionFile, dicContainers);
                    //}
                    //////BOTH THE FILES ARE UDATING ONCE////
                    //_repository.UpdateAuthorizedReptvPhotoURL(objauthrdReptve.PassportPhotoURL, objauthrdReptve.ResolutionPertainingAuthorizedPersonFileUrl, Convert.ToInt32(dt.Rows[0]["PromotersDetailsID"]));

                    //if (objOrgzation.EducationPermissionFile != null && objOrgzation.EducationPermissionFile.FileByteArray != null)
                    //{
                    //    //Dictionary<int, string> dicContainers = new Dictionary<int, string>();
                    //    dicContainers.Add(1, "onlineprivateiti");
                    //    dicContainers.Add(2, Convert.ToString(dt.Rows[0]["UserName"]).Split('/')[0].ToLower());
                    //    dicContainers.Add(3, Convert.ToString(dt.Rows[0]["UserID"]));
                    //    dicContainers.Add(4, "promotingOrg");
                    //    objOrgzation.EducationPermissionFileUrl = _repository.UploadFilesToBlob(objOrgzation.EducationPermissionFile, dicContainers);
                    //    _repository.UpdatePromotingOrgEduFileURL(objOrgzation.EducationPermissionFileUrl, Convert.ToInt32(dt.Rows[0]["TrnPromotingOrganizationID"]));
                    //}
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return dicReturnValues;
        }

        public void SendConfirmRegSMS(string mobileNumber, string userName, string strPassword, string registrationNo, string appType)
        {
            try
            {

                string SMSContent = Convert.ToString(ConfigurationManager.AppSettings["ConfirmRegSMS"]);
                SMSContent = SMSContent.Replace("@Username@", userName);
                SMSContent = SMSContent.Replace("@ApplicationType@", appType);
                SMSContent = SMSContent.Replace("@@RegistrationNumber@@", registrationNo);
                SMSContent = SMSContent.Replace("@Password@", strPassword);
                ShortMessageServiceClient oSMS = new ShortMessageServiceClient();
                oSMS.SendMobileOTPMessage(mobileNumber, SMSContent);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
        }

        public UserEntity GetEmployeeDetail(string EmpId)
        {
            UserEntity entity = new UserEntity();
            try
            {
                var dt = _repository.GetEmployeeDetail(EmpId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    entity.FirstName= dt.Rows[0]["EmpName"].ToString();
                    entity.GenderTitleID = dt.Rows[0]["GenderTitle"].ToString();
                    entity.GenderID = Convert.ToInt32(dt.Rows[0]["Gender"]);
                    entity.DateofBirth = dt.Rows[0]["DOB"].ToString();
                    entity.STDCodeM = 91;
                    entity.MobileNumber = dt.Rows[0]["MobileNo"].ToString();
                    entity.AlternativeMobileNumber = dt.Rows[0]["AlternateNo"].ToString();
                    entity.EmailID = dt.Rows[0]["EMail"].ToString();
                    entity.Department= dt.Rows[0]["Dept"].ToString();
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return entity;
        }



    }
}
