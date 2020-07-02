using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Common;
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
        public IHttpActionResult GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            return Ok(_repository.GetPRDetailsList(pageIndex, pageSize, DeptName, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPRTableDetails")]
        public IHttpActionResult GetPRTableDetails(string PRSetno)
        {
            return Ok(_repository.GetPRTableDetails(PRSetno));
        }

        [HttpPost]
        [Route("api/MRMDetail/UpdateApproveReject")]
        [ResponseType(typeof(string))]
        public IHttpActionResult UpdateApproveReject(string[] param)
        {
            return Ok(_repository.UpdateApproveReject(param));
        }

        [HttpPost]
        [Route("api/MRMDetail/SavePurchaseDetails")]
        [ResponseType(typeof(string))]
        public IHttpActionResult SavePurchaseDetails(string[] param)
        {
            return Ok(_repository.SavePurchaseDetails(param));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPRListForDocument")]
        public IHttpActionResult GetPRListForDocument(string PRSetNo)
        {
            return Ok(_repository.GetPRListForDocument(PRSetNo));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPRDataForDocument")]
        public IHttpActionResult GetPRDataForDocument(string PRSetNo)
        {
            return Ok(_repository.GetPRDataForDocument(PRSetNo));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/MRMDetail/SavePODetailsList")]
        public IHttpActionResult SavePODetailsList(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SavePODetailsList(iEntity));
        }

        [Route("api/MRMDetail/GetPODetailsList")]
        public IHttpActionResult GetPODetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDate = null)
        {
            return Ok(_repository.GetPODetailsList(pageIndex, pageSize, SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDate));
        }

        [HttpPost]
        [ResponseType(typeof(PRDetailEntity))]
        [Route("api/MRMDetail/GetPODetailsForPopup")]
        public IHttpActionResult GetPODetailsForPopup(PODetailEntity Model)
        {
            return Ok(_repository.GetPODetailsForPopup(Model));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPODetailForDocument")]
        public IHttpActionResult GetPODetailForDocument(string PRSetNo)
        {
            return Ok(_repository.GetPODetailForDocument(PRSetNo));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPOListDataForDocument")]
        public IHttpActionResult GetPOListDataForDocument(string PRSetNo)
        {
            return Ok(_repository.GetPOListDataForDocument(PRSetNo));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetPOTableDetails")]
        public IHttpActionResult GetPOTableDetails(string POSetNo)
        {
            return Ok(_repository.GetPOTableDetails(POSetNo));
        }

        [HttpGet]
        [Route("api/MRMDetail/GetSavedPODetails")]
        public IHttpActionResult GetSavedPODetails(string POSetNo)
        {
            return Ok(_repository.GetSavedPODetails(POSetNo));
        }

        [Route("api/MRMDetail/GetPRNoList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetPRNoList(string DeptName)
        {
            return Ok(_repository.GetPRNoList(DeptName));
        }

        [Route("api/MRMDetail/GetRMCategories")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetRMCategories(string SupplierId)
        {
            return Ok(_repository.GetRMCategories(SupplierId));
        }

        [Route("api/MRMDetail/GetDeliveryDates")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetDeliveryDates(string RMCategory)
        {
            return Ok(_repository.GetDeliveryDates(RMCategory));
        }

    }
}
