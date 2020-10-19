using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;
using NtierMvc.Model.Application;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IAccountWorker
    {
        Dictionary<int, string> SaveApplicationRegistraionDetails(UserEntity objUser);
        List<ActiveSession> CheckLoginSession(string sessionId, int userId);
        List<UserRoleEntity> GetUserRoles(string modelUsername);
        List<RolePermissionEntity> GetUserPermissions(string userUsername);

        void SaveSessionlogin(int userId, string ipv4, string ipv6, string userAgent, string sessionSessionId);
        void SaveSessionLogout(string sessionId, int userId, LogoutOption.LogoutCode logout);
        UserEntity CheckUserExists(string username, string objectType, string password);
        string CheckRegNumber(ChangePasswodEntity entity);
        void ChangePwd(ChangePasswodEntity entity);
        ChangePasswodEntity GetUserDetailsForChngePwd(string userName);


        bool CheckUserDetails(ChangePasswodEntity entity);

        PagewiseAccessEntity GetPagewiseAccess(PagewiseAccessEntity entity);

        AffiliationTypeEntity GetAffiliationType(int RegistrationId);        
        UserEntity GetEmployeeDetail(string EmpId);        

    }
}
