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
    public class QuotationDetailsController : ApiController
    {
        IQuotationWorker _repository = new QuotationWorker();


        //[HttpPost]
        //[ResponseType(typeof(string))]
        //[Route("api/QuotationDetails/SaveQuotationDetails")]
        //public IHttpActionResult SaveQuotationDetails(QuotationEntity viewModel)
        //{
        //    return Ok(_repository.SaveQuotationDetails(viewModel));
        //}

        [HttpGet]
        [Route("api/QuotationDetails/GetUserQuoteDetails")]
        public IHttpActionResult GetUserQuoteDetails(string unitNo)
        {
            return Ok(_repository.GetUserQuoteDetails(unitNo));
        }

        [HttpGet]
        [Route("api/QuotationDetails/GetVendorQuoteDetails")]
        public IHttpActionResult GetVendorQuoteDetails(string vendorId)
        {
            return Ok(_repository.GetVendorQuoteDetails(vendorId));
        }

        [HttpPost]
        [Route("api/QuotationDetails/DeleteQuotationDetail")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteQuotationDetail(int[] param)
        {
            return Ok(_repository.DeleteQuotationDetail(param[0]));
        }

        //[ResponseType(typeof(int))]
        //[Route("api/QuotationDetails/GetQuotationDetails")]
        //public IHttpActionResult GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteVendorID = null, string SearchQuoteProductGroup = null, string SearchDeliveryTerms = null)
        //{
        //    return Ok(_repository.GetQuotationDetails(pageIndex, pageSize, SearchQuoteType, SearchQuoteVendorID, SearchQuoteProductGroup, SearchDeliveryTerms));
        //}

        [HttpGet]
        [Route("api/QuotationDetails/GetCityName")]
        public IHttpActionResult GetCityName(string VendorName = "")
        {
            return Ok(_repository.GetCityName(VendorName));
        }
        

        //[HttpPost]
        //[ResponseType(typeof(QuotationEntity))]
        //[Route("api/QuotationDetails/AddQuotationDetailsPopup")]
        //public IHttpActionResult AddQuotationDetailsPopup(QuotationEntity Model)
        //{
        //    return Ok(_repository.QuotationDetailsPopup(Model));
        //}

        [HttpPost]
        [ResponseType(typeof(QuotationEntity))]
        [Route("api/QuotationDetails/AddQuotationDetailsPopup")]
        [Route("api/QuotationDetails/QuotationDetailsPopup")]
        public IHttpActionResult QuotationDetailsPopup(QuotationEntity Model)
        {
            return Ok(_repository.QuotationDetailsPopup(Model));
        }

        [HttpGet]
        [Route("api/QuotationDetails/GetDdlValueForQuote")]
        public IHttpActionResult GetDdlValueForQuote(string type, string VendorId = null, string QuoteType = null, string SubjectId = null)
        {
            return Ok(_repository.GetDdlValueForQuote(type, VendorId, QuoteType, SubjectId));
        }

    }
}
