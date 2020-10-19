using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using System.Web.Http.Description;
using NtierMvc.Model;
using NtierMvc.Model.Account;

namespace NtierMvc.API.Controllers.Account
{
    public class LoginController : ApiController
    {
        IAccountWorker _repository = new AccountWorker();

        [HttpPost]
        [Route("api/Login/CheckUserExists")]
        [ResponseType(typeof(UserEntity))]
        //public IHttpActionResult CheckUserExists(string username, string objectType, string password)
        public IHttpActionResult CheckUserExists(UserEntity objUserEntity)
        {
            return Ok(_repository.CheckUserExists(objUserEntity.UserName, objUserEntity.ObjectType, objUserEntity.Password));
        }

        [HttpPost]
        [Route("api/Login/SaveSessionlogin")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveSessionlogin(SessionLoginEntity objSessionLoginEntity)
        {
            _repository.SaveSessionlogin(objSessionLoginEntity.UserId, objSessionLoginEntity.IPV4, objSessionLoginEntity.IPV6,
                objSessionLoginEntity.UserAgent, objSessionLoginEntity.SessionId);
            return Ok();
        }

        [HttpGet]
        [Route("api/Login/GetUserRoles")]
        [ResponseType(typeof(List<UserRoleEntity>))]
        public IHttpActionResult GetUserRoles(string username)
        {
            return Ok(_repository.GetUserRoles(username));
        }

        [HttpGet]
        [Route("api/Login/GetUserPermissions")]
        [ResponseType(typeof(List<RolePermissionEntity>))]
        public IHttpActionResult GetUserPermissions(string username)
        {
            return Ok(_repository.GetUserPermissions(username));
        }

        [HttpPost]
        [Route("api/Login/SaveSessionLogout")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveSessionLogout(SessionLoginEntity objSessionLogin)
        {
            _repository.SaveSessionLogout(objSessionLogin.SessionId, objSessionLogin.UserId, objSessionLogin.logout);
            return Ok();
        }

        [HttpGet]
        [Route("api/Login/CheckLoginSession")]
        [ResponseType(typeof(List<ActiveSession>))]
        public IHttpActionResult CheckLoginSession(string sessionId,int userId)
        {
            return Ok(_repository.CheckLoginSession(sessionId, userId));
        }
        [HttpPost]
        [Route("api/Login/CheckRegNumber")]
        [ResponseType(typeof(string))]
        public IHttpActionResult CheckRegNumber(ChangePasswodEntity entity)
        {
            return Ok(_repository.CheckRegNumber(entity));
        }
        //[HttpPost]
        //[Route("api/Login/ChangePwd")]
        //[ResponseType(typeof(void))]
        public IHttpActionResult ChangePwd(ChangePasswodEntity entity)
        {
            _repository.ChangePwd(entity);
            return Ok();
        }

        [HttpGet]
        [Route("api/Login/GetUserDetailsForChngePwd")]
        [ResponseType(typeof(ChangePasswodEntity))]
        public IHttpActionResult GetUserDetailsForChngePwd(string userName)
        {
            return Ok(_repository.GetUserDetailsForChngePwd(userName));
        }

        [HttpPost]
        [Route("api/Login/CheckUserDetails")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult CheckUserDetails(ChangePasswodEntity entity)
        {
            return Ok(_repository.CheckUserDetails(entity));
        }


        // Added By Ganesh Jha 21 Jan. To Check the page wise access for logged In user for that section
        //HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPagewiseAccess?=" + entity.UserId + "&=" + entity.RoleId + "&=" + entity.RegistrationId + "&=" + entity.requestURL).Result;


        [HttpGet]
        [Route("api/Login/GetPagewiseAccess")]
        [ResponseType(typeof(PagewiseAccessEntity))]
        public IHttpActionResult GetPagewiseAccess(int UserId, int RoleId, int RegistrationId, string RequestUrl , string Action )
        {
            PagewiseAccessEntity entity = new PagewiseAccessEntity();
            entity.requestURL = RequestUrl;
            entity.RoleId = RoleId;
            entity.UserId = UserId;
            entity.RegistrationId = RegistrationId;
            entity.action = Action;
            return Ok(_repository.GetPagewiseAccess(entity));
        }

        public IHttpActionResult GetAffliationType(int RegistrationId)
        {                 
            return Ok(_repository.GetAffiliationType(RegistrationId));
        }

        public IHttpActionResult GetEmployeeDetail(string EmpId)
        {
            return Ok(_repository.GetEmployeeDetail(EmpId));
        }

    }
}
