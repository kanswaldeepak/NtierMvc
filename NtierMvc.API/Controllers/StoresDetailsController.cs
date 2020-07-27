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
using NtierMvc.Model.Stores;

namespace NtierMvc.API.Controllers.Application
{
    public class StoresDetailsController : ApiController
    {
        IStoresWorker _repository = new StoresWorker();

        [HttpGet]
        [Route("api/StoresDetails/FetchGoodsRecieptList")]
        public IHttpActionResult FetchGoodsRecieptList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            return Ok(_repository.FetchGoodsRecieptList(pageIndex, pageSize, SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDateFrom, SearchDeliveryDateTo));
        }

        [HttpGet]
        [Route("api/StoresDetails/GetDetailForGateControlNo")]
        public IHttpActionResult GetDetailForGateControlNo(string GateControlNo)
        {
            return Ok(_repository.GetDetailForGateControlNo(GateControlNo));
        }

        [HttpGet]
        [Route("api/StoresDetails/GetGRDetailsPopup")]
        public IHttpActionResult GetGRDetailsPopup(string GRno=null)
        {
            return Ok(_repository.GetGRDetailsPopup(GRno));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/StoresDetails/SaveGoodsRecieptEntryDetails")]
        public IHttpActionResult SaveGoodsRecieptEntryDetails(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SaveGoodsRecieptEntryDetails(iEntity));
        }

    }
}
