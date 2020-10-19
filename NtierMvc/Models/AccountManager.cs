using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Account;
using NtierMvc.Model.Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace NtierMvc.Model
{
    public class AccountManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        public AccountManager()
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


        public UserEntity CheckUserExists(string username, string objectType, string password)
        {
            UserEntity objUserEntity = new UserEntity();
            var baseAddress = "Login";

            objUserEntity.UserName = username;
            objUserEntity.ObjectType = objectType;
            objUserEntity.Password = password;

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                //HttpResponseMessage response = client.GetAsync(baseAddress + "/CheckUserExists?username=" + username + "&objectType=" + objectType + "&password=" + password).Result;
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/CheckUserExists", objUserEntity).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objUserEntity = JsonConvert.DeserializeObject<UserEntity>(data);
                }
            }
            return objUserEntity;
        }

        public void SaveSessionlogin(SessionLoginEntity objSessionLoginEntity)
        {
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveSessionlogin", objSessionLoginEntity).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var data = response.Content.ReadAsStringAsync().Result;
                    //objUserEntity = JsonConvert.DeserializeObject<UserEntity>(data);
                }
            }
        }

        public List<UserRoleEntity> GetUserRoles(string username)
        {
            List<UserRoleEntity> lstUserRole = new List<UserRoleEntity>();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserRoles?username=" + username).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstUserRole = JsonConvert.DeserializeObject<List<UserRoleEntity>>(data);
                }
            }
            return lstUserRole;
        }

        public List<RolePermissionEntity> GetUserPermissions(string username)
        {
            List<RolePermissionEntity> lstRolePermission = new List<RolePermissionEntity>();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserPermissions?username=" + username).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstRolePermission = JsonConvert.DeserializeObject<List<RolePermissionEntity>>(data);
                }
            }
            return lstRolePermission;
        }

        public void SaveSessionLogout(SessionLoginEntity objSessionLoginEntity)
        {
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveSessionLogout", objSessionLoginEntity).Result;
                if (response.IsSuccessStatusCode)
                {
                    //var data = response.Content.ReadAsStringAsync().Result;
                    //objUserEntity = JsonConvert.DeserializeObject<UserEntity>(data);
                }
            }
        }

        public List<ActiveSession> CheckLoginSession(string sessionId, int userId)
        {
            List<ActiveSession> lstActiveSession = new List<ActiveSession>();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/CheckLoginSession?sessionId=" + sessionId + "&userId=" + userId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstActiveSession = JsonConvert.DeserializeObject<List<ActiveSession>>(data);
                }
            }
            return lstActiveSession;
        }
        public ChangePasswodEntity GetUserDetailsForChngePwd(ChangePasswodEntity entity)
        {
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserDetailsForChngePwd?userName=" + entity.UserName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    entity = JsonConvert.DeserializeObject<ChangePasswodEntity>(data);
                }
            }
            return entity;
        }
        public string CheckRegNumber(ChangePasswodEntity entity)
        {
            var baseAddress = "Login";
            string CheckReg = "";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/CheckRegNumber", entity).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    //bool CheckReg = JsonConvert.DeserializeObject<bool>(data);

                    CheckReg = JsonConvert.DeserializeObject<string>(data);
                    return CheckReg;
                }
            }
            return CheckReg;
        }
        public void ChangePwd(ChangePasswodEntity entity)
        {
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/ChangePwd", entity).Result;
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }
        public bool CheckUserDetails(ChangePasswodEntity entity)
        {
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/CheckUserDetails", entity).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    bool UserExistance = JsonConvert.DeserializeObject<bool>(data);
                    return UserExistance;
                }
            }
            return false;
        }

        public PagewiseAccessEntity GetPagewiseAccess(PagewiseAccessEntity entity)
        {
            PagewiseAccessEntity objEntity = new PagewiseAccessEntity();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                //HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPagewiseAccess).Result;
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPagewiseAccess?UserId=" + entity.UserId + "&RoleId=" + entity.RoleId + "&RegistrationId=" + entity.RegistrationId + "&RequestUrl=" + entity.requestURL + "&Action=" + entity.action).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<PagewiseAccessEntity>(data);
                }
            }
            return objEntity;
        }


        public AffiliationTypeEntity GetAffiliationType(int registrationId)
        {
            AffiliationTypeEntity objEntity = new AffiliationTypeEntity();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                //HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPagewiseAccess).Result;
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAffliationType?RegistrationId=" + registrationId.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<AffiliationTypeEntity>(data);
                }
            }
            return objEntity;
        }

        public Dictionary<int, string> SaveApplicationRegistraionDetails(UserEntity viewModel)
        {
            var baseAddress = "Registration";
            Dictionary<int, string> dic = new Dictionary<int, string>();
            ArrayList paramList = new ArrayList();
            paramList.Add(viewModel);

            //paramList.Add(viewModel.AuthorizedRepresentativeModel);
            //paramList.Add(viewModel.ProposedInstituteDetailsModel);
            //paramList.Add(viewModel.PromotingOrganizationModel);
            //paramList.Add(viewModel.UserModel);
            //paramList.Add(viewModel.RegistrationModel);

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress, paramList).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    dic = JsonConvert.DeserializeObject<Dictionary<int, string>>(data);
                }
            }
            return dic;
        }

        public UserEntity GetEmployeeDetail(string EmpId)
        {
            UserEntity objEntity = new UserEntity();
            var baseAddress = "Login";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetEmployeeDetail?EmpId=" + EmpId.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<UserEntity>(data);
                }
            }
            return objEntity;
        }


    }
}
