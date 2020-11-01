using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Model;
using NtierMvc.Model.HR;

namespace NtierMvc.API.Controllers.Application
{
    public class HrDetailsController : ApiController
    {
        IHrWorker _repository = new HrWorker();


        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/HrDetails/SaveEmployeeDetails")]
        public IHttpActionResult SaveEmployeeDetails(EmployeeEntity viewModel)
        {
            return Ok(_repository.SaveEmployeeDetails(viewModel));
        }


        [HttpGet]
        [Route("api/HrDetails/GetUserDetails")]
        public IHttpActionResult GetUserDetails(string unitNo)
        {
            return Ok(_repository.GetUserEmployeeDetails(unitNo));
        }

        [HttpPost]
        [ResponseType(typeof(EmployeeEntity))]
        [Route("api/HrDetails/EmployeeDetailsPopup")]
        public IHttpActionResult EmployeeDetailsPopup(EmployeeEntity Model)
        {
            return Ok(_repository.EmployeeDetailsPopup(Model));
        }

        [Route("api/HrDetails/GetEmployeeDetails")]
        public IHttpActionResult GetEmployeeDetails(int pageIndex, int pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null)
        {
            return Ok(_repository.GetEmployeeDetails(pageIndex, pageSize, SearchEmployeeNameId, SearchDesignation, SearchDepartment));
        }

        [HttpPost]
        [Route("api/HrDetails/DeleteEmpDetail")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteEmpDetail(int[] param)
        {
            return Ok(_repository.DeleteEmployeeDetail(param[0]));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/HrDetails/SavePayrollDetails")]
        public IHttpActionResult SavePayrollDetails(PayrollEntity payModel)
        {
            return Ok(_repository.SavePayrollDetails(payModel));
        }

        [HttpPost]
        [ResponseType(typeof(PayrollEntity))]
        [Route("api/HrDetails/GetEmpPayrollData")]
        public IHttpActionResult GetEmpPayrollData(int[] param)
        {
            return Ok(_repository.GetEmpPayrollData(param[0],param[1],param[2]));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/HrDetails/SaveEmpLeaveDetails")]
        public IHttpActionResult SaveEmpLeaveDetails(LeaveManagement lvModel)
        {
            return Ok(_repository.SaveEmpLeaveDetails(lvModel));
        }


        [Route("api/HrDetails/GetEmpLeaveList")]
        public IHttpActionResult GetEmpLeaveList(int EmpId)
        {
            return Ok(_repository.GetEmpLeaveList(EmpId));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/HrDetails/SaveExperienceDetailsList")]
        public IHttpActionResult SaveExperienceDetailsList(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SaveExperienceDetailsList(iEntity));
        }

        [HttpGet]
        [Route("api/HrDetails/HRCertificates")]
        public IHttpActionResult HRCertificates(int EmpId)
        {
            return Ok(_repository.HRCertificates(EmpId));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/HrDetails/SaveEmpCertificates")]
        public IHttpActionResult SaveEmpCertificates(HRCertificatesEntity viewModel)
        {
            return Ok(_repository.SaveEmpCertificates(viewModel));
        }

    }
}
