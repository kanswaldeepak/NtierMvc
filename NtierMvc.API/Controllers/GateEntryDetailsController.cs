using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Collections;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.Account;
using NtierMvc.BusinessLogic.Worker;
using NtierMvc.Model;

namespace NtierMvc.API.Controllers.Application
{
    public class GateEntryDetailsController : ApiController
    {
        IGateEntryWorker _repository = new GateEntryWorker();

        //[HttpGet]
        //[Route("api/GateEntryDetails/GetGateEntryList")]
        //public IHttpActionResult GetGateEntryList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null)
        //{
        //    return Ok(_repository.GetGateEntryList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchType, SearchVendorNature, SearchVendorName, SearchBillNo, SearchBillDate, SearchItemDescription, SearchCurrency, SearchApprovalStatus));
        //}


        [HttpGet]
        [Route("api/GateEntryDetails/GetPOTableDetailsForGateEntry")]
        public IHttpActionResult GetPOTableDetailsForGateEntry(string POSetno)
        {
            return Ok(_repository.GetPOTableDetailsForGateEntry(POSetno));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/GateEntryDetails/SaveGateEntryDetails")]
        public IHttpActionResult SaveGateEntryDetails(GateEntryEntity iEntity)
        {
            return Ok(_repository.SaveGateEntryDetails(iEntity));
        }


    }
}
