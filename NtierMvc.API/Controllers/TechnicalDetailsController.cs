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
    public class TechnicalDetailsController : ApiController
    {
        ITechnicalWorker _repository = new TechnicalWorker();


        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveQuotationDetails")]
        public IHttpActionResult SaveQuotationDetails(QuotationEntity viewModel)
        {
            return Ok(_repository.SaveQuotationDetails(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveQuotePreparation")]
        public IHttpActionResult SaveQuotePreparation(QuotationPreparationEntity entity)
        {
            return Ok(_repository.SaveQuotePreparation(entity));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetUserQuoteDetails")]
        public IHttpActionResult GetUserQuoteDetails(string unitNo)
        {
            return Ok(_repository.GetUserQuoteDetails(unitNo));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetVendorQuoteDetails")]
        public IHttpActionResult GetVendorQuoteDetails(string vendorId)
        {
            return Ok(_repository.GetVendorQuoteDetails(vendorId));
        }

        [HttpPost]
        [Route("api/TechnicalDetails/DeleteQuotationDetail")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteQuotationDetail(int[] param)
        {
            return Ok(_repository.DeleteQuotationDetail(param[0]));
        }

        [ResponseType(typeof(int))]
        [Route("api/TechnicalDetails/GetQuoteRegList")]
        public IHttpActionResult GetQuoteRegList(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteCustomerID = null, string SearchSubject = null, string SearchDeliveryTerms = null)
        {
            return Ok(_repository.GetQuoteRegList(pageIndex, pageSize, SearchQuoteType, SearchQuoteCustomerID, SearchSubject, SearchDeliveryTerms));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetCityName")]
        public IHttpActionResult GetCityName(string VendorName = "")
        {
            return Ok(_repository.GetCityName(VendorName));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetProductLineList")]
        public IHttpActionResult GetProductLineList(string productLine = "")
        {
            return Ok(_repository.GetProductLineList(productLine));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetProductNumber")]
        public IHttpActionResult GetProductNumber(string productNameId = "", int productType = 0)
        {
            return Ok(_repository.GetProductNumber(productNameId, productType));
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
        [Route("api/TechnicalDetails/AddQuotationDetailsPopup")]
        [Route("api/TechnicalDetails/QuotationDetailsPopup")]
        public IHttpActionResult QuotationDetailsPopup(QuotationEntity Model)
        {
            return Ok(_repository.QuotationDetailsPopup(Model));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetEnqNoList")]
        public IHttpActionResult GetEnqNoList(string vendorId = "")
        {
            return Ok(_repository.GetEnqNoList(vendorId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetQuoteNo")]
        public IHttpActionResult GetQuoteNo(string quotetypeId = null)
        {
            return Ok(_repository.GetQuoteNo(quotetypeId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetQuoteNoList")]
        public IHttpActionResult GetQuoteNoList(string quotetypeId = "", string SoNo = null)
        {
            return Ok(_repository.GetQuoteNoList(quotetypeId, SoNo));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetQuoteItemSlNoList")]
        public IHttpActionResult GetQuoteItemSlNoList(string quotetypeId = "", string SoNo = null)
        {
            return Ok(_repository.GetQuoteItemSlNoList(quotetypeId, SoNo));
        }

        [HttpPost]
        [Route("api/TechnicalDetails/GetQuoteEnqNoList")]
        public IHttpActionResult GetQuoteEnqNoList(QuotationEntity qE)
        {
            return Ok(_repository.GetQuoteEnqNoList(qE));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetPrepProductNames")]
        public IHttpActionResult GetPrepProductNames(string productId, string casingSize, string type = null)
        {
            return Ok(_repository.GetPrepProductNames(productId, casingSize, type));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveOrderDetail")]
        public IHttpActionResult SaveOrderDetail(OrderEntity oEntity)
        {
            return Ok(_repository.SaveOrderDetail(oEntity));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetUserOrderDetails")]
        public IHttpActionResult GetUserOrderDetails(string unitNo)
        {
            return Ok(_repository.GetUserOrderDetails(unitNo));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetVendorOrderDetails")]
        public IHttpActionResult GetVendorOrderDetails(string vendorId)
        {
            return Ok(_repository.GetVendorOrderDetails(vendorId));
        }

        [HttpPost]
        [Route("api/TechnicalDetails/DeleteOrderDetail")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteOrderDetail(int[] param)
        {
            return Ok(_repository.DeleteOrderDetail(param[0]));
        }

        [ResponseType(typeof(int))]
        [Route("api/TechnicalDetails/GetOrderDetails")]
        public IHttpActionResult GetOrderDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null)
        {
            return Ok(_repository.GetOrderDetails(pageIndex, pageSize, SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms));
        }


        [HttpPost]
        [ResponseType(typeof(OrderEntity))]
        [Route("api/TechnicalDetails/AddOrderDetailsPopup")]
        [Route("api/TechnicalDetails/OrderDetailsPopup")]
        public IHttpActionResult OrderDetailsPopup(OrderEntity Model)
        {
            return Ok(_repository.OrderDetailsPopup(Model));
        }


        [HttpGet]
        [Route("api/TechnicalDetails/GetOrderQuoteDetails")]
        public IHttpActionResult GetOrderQuoteDetails(string quoteType, string quoteNoId)
        {
            return Ok(_repository.GetOrderQuoteDetails(quoteType, quoteNoId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetQuoteOrderDetails")]
        public IHttpActionResult GetQuoteOrderDetails(string quoteType, string quoteNoId)
        {
            return Ok(_repository.GetQuoteOrderDetails(quoteType, quoteNoId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetOrderDetailsFromSO")]
        public IHttpActionResult GetOrderDetailsFromSO(int SoNo)
        {
            return Ok(_repository.GetOrderDetailsFromSO(SoNo));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveItemDetailList")]
        public IHttpActionResult SaveItemDetailList(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SaveItemDetailList(iEntity));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveItemDetail")]
        public IHttpActionResult SaveItemDetail(ItemEntity iEntity)
        {
            return Ok(_repository.SaveItemDetail(iEntity));
        }


        [HttpGet]
        [Route("api/TechnicalDetails/GetDataForDocument")]
        public IHttpActionResult GetDataForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            return Ok(_repository.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetListForDocument")]
        public IHttpActionResult GetListForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            return Ok(_repository.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveRevisedQuotationDetails")]
        public IHttpActionResult SaveRevisedQuotationDetails(QuotationEntity qEntity)
        {
            return Ok(_repository.SaveRevisedQuotationDetails(qEntity));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetVendorDetails")]
        public IHttpActionResult GetVendorDetails(string quotetypeId)
        {
            return Ok(_repository.GetVendorDetails(quotetypeId));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetQuotePrepDetails")]
        public IHttpActionResult GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo)
        {
            return Ok(_repository.GetQuotePrepDetails(itemNoId, quoteType, quoteNo));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveClarificationData")]
        public IHttpActionResult SaveClarificationData(ClarificationEntity cObj)
        {
            return Ok(_repository.SaveClarificationData(cObj));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveOrderClarificationData")]
        public IHttpActionResult SaveOrderClarificationData(ClarificationEntity cEntity)
        {
            return Ok(_repository.SaveOrderClarificationData(cEntity));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveQuoteNotes")]
        public IHttpActionResult SaveQuoteNotes(ClarificationEntity viewModel)
        {
            return Ok(_repository.SaveQuoteNotes(viewModel));
        }

        [HttpPost]
        [Route("api/TechnicalDetails/DeleteClarificationMails")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteClarificationMails(string[] param)
        {
            return Ok(_repository.DeleteClarificationMails(param));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetSoNoDetails")]
        public IHttpActionResult GetSoNoDetails(string soNo)
        {
            return Ok(_repository.GetSoNoDetails(soNo));
        }

        [HttpPost]
        [Route("api/TechnicalDetails/DeleteOrderClarifications")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteOrderClarifications(string[] param)
        {
            return Ok(_repository.DeleteOrderClarifications(param));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveNewDescDetail")]
        public IHttpActionResult SaveNewDescDetail(DescEntity viewModel)
        {
            return Ok(_repository.SaveNewDescDetail(viewModel));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveOrderNote")]
        public IHttpActionResult SaveOrderNote(OrderEntity viewModel)
        {
            return Ok(_repository.SaveOrderNote(viewModel));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/LoadDescDetail")]
        public IHttpActionResult LoadDescDetail(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            return Ok(_repository.LoadDescDetail(skip, pageSize, sortColumn, sortColumnDir, search));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveRevisedOrderDetails")]
        public IHttpActionResult SaveRevisedOrderDetails(OrderEntity oEntity)
        {
            return Ok(_repository.SaveRevisedOrderDetails(oEntity));
        }


        [HttpGet]
        [Route("api/TechnicalDetails/GetItemNosForEnqs")]
        public IHttpActionResult GetItemNosForEnqs(string EnqNo)
        {
            return Ok(_repository.GetItemNosForEnqs(EnqNo));
        }

        [HttpGet]
        [Route("api/TechnicalDetails/GetDataForContractReview")]
        public IHttpActionResult GetDataForContractReview(string EnqNo, string ItemNo, string type)
        {
            return Ok(_repository.GetDataForContractReview(EnqNo, ItemNo, type));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/TechnicalDetails/SaveContractReviewData")]
        public IHttpActionResult SaveContractReviewData(ContractReview viewModel)
        {
            return Ok(_repository.SaveContractReviewData(viewModel));
        }

        [HttpGet]
        public IHttpActionResult GetWorkAuthReport(string SoNo, string FromDate = null, string ToDate = null, string ReportType=null)
        {
            return Ok(_repository.GetWorkAuthReport(SoNo, FromDate, ToDate, ReportType));
        }

    }
}
