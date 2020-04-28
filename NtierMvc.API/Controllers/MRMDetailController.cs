using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Model;
using NtierMvc.Model.MRM;

namespace NtierMvc.API.Controllers.Application
{
    public class MRMDetailController : ApiController
    {
        IMRMWorker _repository = new MRMWorker();

        
        [HttpPost]
        [ResponseType(typeof(PRDetailEntity))]
        [Route("api/MRMDetail/GetPRDetailsPopup")]
        public IHttpActionResult GetPRDetailsPopup(PRDetailEntity Model)
        {
            return Ok(_repository.GetPRDetailsPopup(Model));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/MRMDetail/SavePRDetailsList")]
        public IHttpActionResult SavePRDetailsList(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SavePRDetailsList(iEntity));
        }

    }
}
