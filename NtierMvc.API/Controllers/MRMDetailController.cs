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
        [ResponseType(typeof(PRDetailEntity))]
        [Route("api/MRMDetail/GetSavedPRDetailsPopup")]
        public IHttpActionResult GetSavedPRDetailsPopup(PRDetailEntity Model)
        {
            return Ok(_repository.GetSavedPRDetailsPopup(Model));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/MRMDetail/SavePRDetailsList")]
        public IHttpActionResult SavePRDetailsList(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SavePRDetailsList(iEntity));
        }

        [Route("api/MRMDetail/GetPRDetailsList")]
        public IHttpActionResult GetPRDetailsList(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            return Ok(_repository.GetPRDetailsList(pageIndex, pageSize, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup));
        }

    }
}
