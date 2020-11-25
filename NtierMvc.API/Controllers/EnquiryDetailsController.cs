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
    public class EnquiryDetailsController : ApiController
    {
        IEnquiryWorker _repository = new EnquiryWorker();


        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/EnquiryDetails/SaveEnquiryDetails")]
        public IHttpActionResult SaveEnquiryDetails(EnquiryEntity viewModel)
        {
            return Ok(_repository.SaveEnquiryDetails(viewModel));
        }

        [HttpGet]
        [Route("api/EnquiryDetails/GetUserDetails")]
        public IHttpActionResult GetUserDetails(string unitNo)
        {
            return Ok(_repository.GetUserCustDetails(unitNo));
        }

        [HttpPost]
        [Route("api/EnquiryDetails/DeleteEnquiryDetail")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteEnquiryDetail(int[] param)
        {
            return Ok(_repository.DeleteEnquiryDetail(param[0]));
        }

        [ResponseType(typeof(int))]
        [Route("api/EnquiryDetails/GetEnquiryDetails")]
        public IHttpActionResult GetEnquiryDetails(int pageIndex, int pageSize, string SearchEnqName = null, string SearchEnqVendorID = null, string SearchProductGroup = null, string SearchMonth = null, string SearchEOQ = null)
        {
            return Ok(_repository.GetEnquiryDetails(pageIndex, pageSize, SearchEnqName, SearchEnqVendorID, SearchProductGroup, SearchMonth, SearchEOQ));
        }

        [HttpGet]
        [Route("api/EnquiryDetails/GetCityName")]
        public IHttpActionResult GetCityName(string VendorName = "")
        {
            return Ok(_repository.GetCityName(VendorName));
        }
        

        //[HttpPost]
        //[ResponseType(typeof(EnquiryEntity))]
        //[Route("api/EnquiryDetails/AddEnquiryDetailsPopup")]
        //public IHttpActionResult AddEnquiryDetailsPopup(EnquiryEntity Model)
        //{
        //    return Ok(_repository.EnquiryDetailsPopup(Model));
        //}

        [HttpPost]
        [ResponseType(typeof(EnquiryEntity))]
        [Route("api/EnquiryDetails/AddEnquiryDetailsPopup")]
        [Route("api/EnquiryDetails/EnquiryDetailsPopup")]
        public IHttpActionResult EnquiryDetailsPopup(EnquiryEntity Model)
        {
            return Ok(_repository.EnquiryDetailsPopup(Model));
        }

        [HttpGet]
        [Route("api/EnquiryDetails/GetVendorDetailForEnquiry")]
        public IHttpActionResult GetVendorDetailForEnquiry(string CustomerId)
        {
            return Ok(_repository.GetVendorDetailForEnquiry(CustomerId));
        }

        [HttpGet]
        [Route("api/EnquiryDetails/GetDdlValueForEnquiry")]
        public IHttpActionResult GetDdlValueForEnquiry(string type, string EOQId = null, string ProductGroup = null, string VendorId = null)
        {
            return Ok(_repository.GetDdlValueForEnquiry(type, EOQId, ProductGroup, VendorId));
        }

    }
}
