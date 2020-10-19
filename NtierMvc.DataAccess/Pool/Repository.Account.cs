using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using NtierMvc.Common;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.DataAccess.Common;
using System.Data.Common;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository
    {
        public DataTable SaveNewPassword(string password, int userId)
        {
            int reset = 1;
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@UserId", userId);
            parms.Add("@pNewPassword", password);
            parms.Add("@ResetOrChange", reset);

            string spName = ConfigurationManager.AppSettings["ForgotPasswordReset"];
            return _dbAccess.GetDataTable(spName, parms);

        }
        public DataTable InsertOrCheckLoginLog(int? centreId, int? contactId, string email, string sessionid, int? userId,
            string logout = null, int pagetype = 1)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@CentreId", centreId);
            prams.Add("@ContactId", contactId);
            prams.Add("@SessionId", sessionid);
            prams.Add("@LogoutType", logout);
            prams.Add("@PageType", pagetype);
            prams.Add("@UserId", userId);
            string spName = ConfigurationManager.AppSettings["ERP_InsertLoginRecord"];
            return _dbAccess.GetDataTable(spName, prams);
        }
        public DataTable CheckLoginSession(string sessionid, int userId)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@SessionId", sessionid);
            prams.Add("@UserId", userId);
            string spName = ConfigurationManager.AppSettings["ERP_CheckLoginSession"];
            return _dbAccess.GetDataTable(spName, prams);

        }

        public DataTable CheckUserExists(string username, string objectType)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserName", username);
            prams.Add("@ObjectType", objectType);
            var spName = ConfigurationManager.AppSettings["ERP_CheckUserExists"];
            return _dbAccess.GetDataTable(spName, prams);

        }

        public DataTable AuthorizeUser(string username, string password)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserName", username);
            prams.Add("@Pwd", password);

            var spName = ConfigurationManager.AppSettings["ERP_Login"];
            return _dbAccess.GetDataTable(spName, prams);
        }

        public DataTable CheckLoginDate(int? contactId, int? userId)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserId", userId);
            prams.Add("@ContactId", contactId);

            string spName = ConfigurationManager.AppSettings["ERP_FirstLogin"];
            return _dbAccess.GetDataTable(spName, prams);

        }

        public DataTable GetUserRoles(string username)
        {
            DataTable dt = new DataTable();

            string spName = ConfigurationManager.AppSettings["ERP_GetUserRoles"];
            var parms = new Dictionary<string, object>();

            parms.Add("@Username", username);

            return _dbAccess.GetDataTable(spName, parms);

        }

        public DataTable GetUserPermissions(string username)
        {
            string spName = ConfigurationManager.AppSettings["GetUserPermissions"];
            var parms = new Dictionary<string, object>();

            parms.Add("@Username", username);

            return _dbAccess.GetDataTable(spName, parms);
        }

        public object SaveSessionLogout(string sessionId, LogoutOption.LogoutCode logout, int userId)
        {
            DataTable dt = new DataTable();
            var prams = new Dictionary<string, object>();
            prams.Add("@SessionId", sessionId);
            prams.Add("@LogoutType", (int)logout);
            prams.Add("@UserId", userId);

            string spName = ConfigurationManager.AppSettings["ERP_SaveLogOutSession"];
            return _dbAccess.GetDataTable(spName, prams);
        }

        public int SaveSessionlogin(int userId, string ipv4, string ipv6, string userAgent, string sessionId)
        {
            var prams = new Dictionary<string, object>();

            prams.Add("@UserID", userId);
            prams.Add("@IPV4", ipv4);
            prams.Add("@IPV6", ipv6);
            prams.Add("@UserAgent", userAgent);
            prams.Add("@SessionID", sessionId);

            string spName = ConfigurationManager.AppSettings["ERP_SaveLoginSession"];
            return _dbAccess.ExecuteNonQuery(spName, prams);
        }

        //checking Registration Number and Email Id
        public DataTable CheckRegNumber(ChangePasswodEntity entity)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@RegNumber", entity.RegNumber);
            prams.Add("@EmailId", entity.EmailId); //NOT IN USE
            var spName = ConfigurationManager.AppSettings["ERP_CheckRegistrationNumber"];
            return _dbAccess.GetDataTable(spName, prams);

        }
        public void ChangePwd(ChangePasswodEntity entity)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@RegNumber", entity.RegNumber);
            prams.Add("@Password", entity.Password);
            prams.Add("@PasswordPhrase", entity.passPhrase);
            var spName = ConfigurationManager.AppSettings["ERP_SaveForgotPassword"];
             _dbAccess.GetDataTable(spName, prams);

        }
        public DataTable GetUserDetailsForChngePwd(string userName)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserName", userName);
            var spName = ConfigurationManager.AppSettings["ERP_GetUserDetialsForChangePwd"];
            return _dbAccess.GetDataTable(spName, prams);

        }
        public DataTable CheckUserDetails(ChangePasswodEntity entity)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserName", entity.UserName);
            prams.Add("@EmailId", entity.EmailId);
            prams.Add("@Password", entity.Password);
            var spName = ConfigurationManager.AppSettings["ERP_GetUserDetials"];
            return _dbAccess.GetDataTable(spName, prams);

        }
        
        public DataTable GetPagewiseAccess(PagewiseAccessEntity entity)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@UserId", entity.UserId);
            prams.Add("@RoleId", entity.RoleId);
            prams.Add("@RequestUrl", entity.requestURL);
            prams.Add("@RegistrationId", entity.RegistrationId);
            prams.Add("@Action", entity.action);

            var spName = ConfigurationManager.AppSettings["GetSectionwiseAccessForUser"];
            return _dbAccess.GetDataTable(spName, prams);

        }

        public DataTable GetAffiliationType(int RegistrationId)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@RegistrationId", RegistrationId);
            var spName = "Education_GetAffiliationType"; //ConfigurationManager.AppSettings["GetSectionwiseAccessForUser"];
            return _dbAccess.GetDataTable(spName, prams);

        }

        public DataTable SaveUserRegistraionDetails(UserEntity objUser)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            parms.Add("@UserName", objUser.UserName);
            parms.Add("@Password", objUser.Password);
            parms.Add("@PasswordPhrase", objUser.PasswordPhrase);
            parms.Add("@EmpId", objUser.EmpId);
            //parms.Add("@FirstName", objUser.FirstName);
            //parms.Add("@LastName", objUser.LastName);
            //parms.Add("@GenderTitleID", objUser.GenderTitleID);
            //parms.Add("@GenderID", objUser.GenderID);
            //parms.Add("@PermissionId", objUser.PermissionId);
            //parms.Add("@FunctionalAreaID", objUser.FunctionalAreaID);
            //parms.Add("@DateofBirth", objUser.DateofBirth);
            parms.Add("@STDCodeM", objUser.STDCodeM);
            //parms.Add("@MobileNumber", objUser.MobileNumber);
            //parms.Add("@AlternativeMobileNumber", objUser.AlternativeMobileNumber);
            //parms.Add("@EmailID", objUser.EmailID);
            //parms.Add("@Department", objUser.Department);
            parms.Add("@STDCodeL", objUser.STDCodeL);
            parms.Add("@LandLineNumber1", objUser.LandLineNumber1);
            ////end////
            spName = "erp_SaveUserDetails"; // ConfigurationManager.AppSettings["CreateResearcherAccount"];

            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetEmployeeDetail(string EmpId)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@EmpId", EmpId);
            var spName = ConfigurationManager.AppSettings["GetEmpDetailsForRegister"];
            return _dbAccess.GetDataTable(spName, prams);
        }


    }
}
