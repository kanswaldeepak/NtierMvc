using System;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Model;
using NtierMvc.Model.Admin;
using NtierMvc.Model.DesignEng;

namespace NtierMvc.API.Controllers.Application
{
    public class AdminDetailsController : ApiController
    {
        IAdminWorker _repository = new AdminWorker();

        [Route("api/AdminDetails/GetRoleURLDetails")]
        public IHttpActionResult GetRoleURLDetails(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null)
        {
            return Ok(_repository.GetRoleURLDetails(skip, pageSize, sortColumn, sortColumnDir, search));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/AdminDetails/SaveRoleAssigns")]
        public IHttpActionResult SaveRoleAssigns(RoleAssignEntity viewModel)
        {
            return Ok(_repository.SaveRoleAssigns(viewModel));
        }


    }
}
