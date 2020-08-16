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

        [HttpGet]
        [Route("api/GateEntryDetails/FetchInboundList")]
        public IHttpActionResult FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchPONo = null)
        {
            return Ok(_repository.FetchInboundList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchType, SearchVendorNature, SearchVendorName, SearchPONo));
        }


        [HttpGet]
        [Route("api/GateEntryDetails/GetPOTableDetailsForGateEntry")]
        public IHttpActionResult GetPOTableDetailsForGateEntry(string POSetno, string GateNo = null)
        {
            return Ok(_repository.GetPOTableDetailsForGateEntry(POSetno, GateNo));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/GateEntryDetails/SaveGateEntryDetails")]
        public IHttpActionResult SaveGateEntryDetails(GateEntryEntity iEntity)
        {
            return Ok(_repository.SaveGateEntryDetails(iEntity));
        }

        [HttpGet]
        [Route("api/GateEntryDetails/InboundDetailsPopup")]
        public IHttpActionResult InboundDetailsPopup(string GateNo)
        {
            return Ok(_repository.InboundDetailsPopup(GateNo));
        }

        [HttpGet]
        [Route("api/GateEntryDetails/GetPoNoDetailsForGE")]
        public IHttpActionResult GetPoNoDetailsForGE()
        {
            return Ok(_repository.GetPoNoDetailsForGE());
        }



    }
}
