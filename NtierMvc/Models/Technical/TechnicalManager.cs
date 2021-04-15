using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;

namespace NtierMvc.Model
{
    public class TechnicalManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        QuotationEntity oCusDetail;

        public TechnicalManager()
        {
            _loggingHandler = new LoggingHandler();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion

        public string SaveQuotationDetails(QuotationEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveQuotationDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveQuotePreparation(QuotationPreparationEntity quoteP)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveQuotePreparation", quoteP).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }


        public QuotationEntity GetUserDetails(string unitNo)
        {
            oCusDetail = new QuotationEntity();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserQuoteDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    oCusDetail = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return oCusDetail;
        }


        //public QuotationEntity TechnicalDetailsPopup(QuotationEntity Model)
        //{
        //    var baseAddress = "TechnicalDetails";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/TechnicalDetailsPopup", Model).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            Model = JsonConvert.DeserializeObject<QuotationEntity>(data);
        //        }
        //    }
        //    return Model;
        //}

        public QuotationEntityDetails GetQuoteRegList(int pageIndex, int pageSize, string SearchQuotRegVendorID = null, string SearchQuotRegVendorName = null, string SearchQuotRegQuoteNo = null, string SearchQuotRegProductGrp = null, string SearchQuotRegEnqFor = null, string SearchQuotRegQuoteType = null)
        {
            var baseAddress = "QuotationDetails";
            QuotationEntityDetails cusEnt = new QuotationEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuotationDetails?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchQuotRegVendorID=" + SearchQuotRegVendorID + "&SearchQuotRegVendorName=" + SearchQuotRegVendorName + "&SearchQuotRegQuoteNo=" + SearchQuotRegQuoteNo + "&SearchQuotRegProductGrp=" + SearchQuotRegProductGrp + "&SearchQuotRegEnqFor=" + SearchQuotRegEnqFor + "&SearchQuotRegQuoteType=" + SearchQuotRegQuoteType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    cusEnt = JsonConvert.DeserializeObject<QuotationEntityDetails>(data);
                }
            }
            return cusEnt;
        }

        public string DeleteTechnicalDetail(int TechnicalId)
        {
            string msgCode = "";
            int[] param = { TechnicalId };
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteTechnicalDetail", param).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    msgCode = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(response.ReasonPhrase));
                    throw new Exception("WebAPI error");
                }
            }
            return msgCode;
        }

        public QuotationEntity GetVendorQuoteDetails(string vendorId)
        {
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetVendorQuoteDetails?vendorId=" + vendorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    oCusDetail = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return oCusDetail;
        }

        public List<DropDownEntity> GetProductLineList(string productLine)
        {
            List<DropDownEntity> prodList = new List<DropDownEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetProductLineList?productLine=" + productLine).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prodList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return prodList;
        }

        public string GetProductNumber(string productNameId, int productType)
        {
            var baseAddress = "TechnicalDetails";
            ProductEntity pEntity = new ProductEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetProductNumber?productNameId=" + productNameId + "&productType=" + productType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    pEntity.ProductNo = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return pEntity.ProductNo;
        }

        public List<DropDownEntity> GetEnqNoList(string vendorId)
        {
            var baseAddress = "TechnicalDetails";
            List<DropDownEntity> ListEnquiryNo = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetEnqNoList?vendorId=" + vendorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ListEnquiryNo = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return ListEnquiryNo;
        }
        public string GetQuoteNo(string quotetypeId = null, string finYear = null)
        {
            var baseAddress = "TechnicalDetails";
            string Number = "0";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteNo?quotetypeId=" + quotetypeId+ "&finYear=" + finYear).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Number = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return Number;
        }

        public List<DropDownEntity> GetQuoteNoList(string quotetypeId = "", string SoNoView = null)
        {
            var baseAddress = "TechnicalDetails";
            List<DropDownEntity> QuoteNoList = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteNoList?quotetypeId=" + quotetypeId + "&SoNo=" + SoNoView).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    QuoteNoList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }

            return QuoteNoList;
        }

        public List<DropDownEntity> GetQuoteItemSlNoList(string quotetypeId = "", string SoNoId = null)
        {
            var baseAddress = "TechnicalDetails";
            List<DropDownEntity> QuoteNoList = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteItemSlNoList?quotetypeId=" + quotetypeId + "&SoNo=" + SoNoId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    QuoteNoList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }

            return QuoteNoList;
        }

        public List<DropDownEntity> GetQuoteEnqNoList(QuotationEntity qE)
        {
            List<DropDownEntity> enqNoList = new List<DropDownEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetQuoteEnqNoList", qE).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    enqNoList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return enqNoList;
        }

        public List<ProductEntity> GetPrepProductNames(string productId, string casingSize, string type = null)
        {
            var baseAddress = "TechnicalDetails";
            List<ProductEntity> QuoteNoList = new List<ProductEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPrepProductNames?productId=" + productId + "&casingSize=" + casingSize + "&type=" + type).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    QuoteNoList = JsonConvert.DeserializeObject<List<ProductEntity>>(data);
                }
            }
            return QuoteNoList;
        }

        public string SaveOrderDetail(OrderEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveOrderDetail", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public OrderEntityDetails GetOrderDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchCustomerID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            var baseAddress = "TechnicalDetails";
            OrderEntityDetails Order = new OrderEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrderDetails?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchQuoteType=" + SearchQuoteType + "&SearchCustomerID=" + SearchCustomerID + "&SearchProductGroup=" + SearchProductGroup + "&SearchDeliveryTerms=" + SearchDeliveryTerms + "&SearchPODeliveryDate=" + SearchPODeliveryDate).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Order = JsonConvert.DeserializeObject<OrderEntityDetails>(data);
                }
            }
            return Order;
        }

        public OrderEntity GetUserOrderDetails(string unitNo)
        {
            var baseAddress = "TechnicalDetails";
            OrderEntity objEntity = new OrderEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserOrderDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }
            return objEntity;
        }

        public OrderEntity GetVendorOrderDetails(string vendorId)
        {
            var baseAddress = "TechnicalDetails";
            OrderEntity objEntity = new OrderEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetVendorOrderDetails?vendorId=" + vendorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }
            return objEntity;
        }


        public OrderEntity OrderDetailsPopup(OrderEntity Model)
        {
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/OrderDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }
            return Model;
        }

        public string DeleteOrderDetail(int Id)
        {
            string msgCode = "";
            int[] param = { Id };
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteOrderDetail", param).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    msgCode = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(response.ReasonPhrase));
                    throw new Exception("WebAPI error");
                }
            }
            return msgCode;
        }

        public List<DropDownEntity> GetOrderQuoteDetails(string quoteType, string quoteNoId)
        {
            var baseAddress = "TechnicalDetails";
            List<DropDownEntity> QuoteList = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrderQuoteDetails?quoteType=" + quoteType + "&quoteNoId=" + quoteNoId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    QuoteList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }

            return QuoteList;
        }

        public OrderEntity GetQuoteOrderDetails(string quoteType, string quoteNoId)
        {
            var baseAddress = "TechnicalDetails";
            OrderEntity orderEntity = new OrderEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteOrderDetails?quoteType=" + quoteType + "&quoteNoId=" + quoteNoId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    orderEntity = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }

            return orderEntity;
        }

        public ItemEntity GetOrderDetailsFromSO(string SoNoView)
        {
            var baseAddress = "TechnicalDetails";
            ItemEntity itemEntity = new ItemEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrderDetailsFromSO?SoNoView=" + SoNoView).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    itemEntity = JsonConvert.DeserializeObject<ItemEntity>(data);
                }
            }

            return itemEntity;
        }

        public string SaveItemDetail(ItemEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveItemDetail", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveItemDetailList(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveItemDetailList", objBU).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<QuoteAndPrepVM> CreateDownloadDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            List<QuoteAndPrepVM> lstQuotePrep = new List<QuoteAndPrepVM>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/CreateDownloadDocument?downloadTypeId=" + downloadTypeId + "&quoteTypeId=" + quoteTypeId + "&quoteNumberId=" + quoteNumberId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstQuotePrep = JsonConvert.DeserializeObject<List<QuoteAndPrepVM>>(data);
                }
            }
            return lstQuotePrep;
        }

        public DataTable GetDataForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            DataTable lstQuotePrep = new DataTable();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDataForDocument?downloadTypeId=" + downloadTypeId + "&quoteTypeId=" + quoteTypeId + "&quoteNumberId=" + quoteNumberId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstQuotePrep = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstQuotePrep;
        }

        public DataTable GetListForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            DataTable lstQuotePrep = new DataTable();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetListForDocument?downloadTypeId=" + downloadTypeId + "&quoteTypeId=" + quoteTypeId + "&quoteNumberId=" + quoteNumberId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstQuotePrep = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstQuotePrep;
        }

        public string SaveRevisedTechnicalQuoteDetails(QuotationEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveRevisedQuotationDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<DropDownEntity> GetVendorDetails(string quotetypeId)
        {
            var baseAddress = "TechnicalDetails";
            List<DropDownEntity> VendorList = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetVendorDetails?quotetypeId=" + quotetypeId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    VendorList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }

            return VendorList;
        }

        //public QuotationPreparationEntity GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo)
        //{
        //    var baseAddress = "TechnicalDetails";
        //    QuotationPreparationEntity itemEntity = new QuotationPreparationEntity();
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuotePrepDetails?itemNoId=" + itemNoId + "&quoteType=" + quoteType + "&quoteNo=" + quoteNo).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            itemEntity = JsonConvert.DeserializeObject<QuotationPreparationEntity>(data);
        //        }
        //    }

        //    return itemEntity;
        //}

        public string SaveClarificationData(ClarificationEntity cObj)
        {
            string result = "";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveClarificationData", cObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveOrderClarificationData(ClarificationEntity ClObj)
        {
            string result = "";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveOrderClarificationData", ClObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveQuoteNotes(ClarificationEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveQuoteNotes", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string DeleteClarificationMails(string Multiparam)
        {
            string msgCode = "";
            var baseAddress = "TechnicalDetails";
            string[] param = { Multiparam };
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteClarificationMails", param).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    msgCode = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(response.ReasonPhrase));
                    throw new Exception("WebAPI error");
                }
            }
            return msgCode;
        }

        public OrderEntity GetSoNoDetails(string soNo)
        {
            var baseAddress = "TechnicalDetails";
            OrderEntity itemEntity = new OrderEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSoNoDetails?soNo=" + soNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    itemEntity = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }

            return itemEntity;
        }

        public string DeleteOrderClarifications(string Multiparam)
        {
            string msgCode = "";
            var baseAddress = "TechnicalDetails";
            string[] param = { Multiparam };
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteOrderClarifications", param).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    msgCode = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(response.ReasonPhrase));
                    throw new Exception("WebAPI error");
                }
            }
            return msgCode;
        }

        public string SaveNewDescDetail(DescEntity dObj)
        {
            string result = "";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveNewDescDetail", dObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveOrderNote(OrderEntity oEn)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveOrderNote", oEn).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<DescEntity> LoadDescDetail(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            List<DescEntity> objList = new List<DescEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/LoadDescDetail?skip=" + skip + "&pageSize=" + pageSize + "&sortColumn=" + sortColumn + "&sortColumnDir=" + sortColumnDir + "&search=" + search).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objList = JsonConvert.DeserializeObject<List<DescEntity>>(data);
                }
            }

            return objList;
        }

        public string SaveRevisedOrderDetails(OrderEntity viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveRevisedOrderDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<DropDownEntity> GetItemNosForEnqs(string EnqNo)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetItemNosForEnqs?EnqNo=" + EnqNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }

            return objList;
        }

        public DataTable GetDataForContractReview(string EnqNo, string ItemNo, string type)
        {
            DataTable lstQuotePrep = new DataTable();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDataForContractReview?EnqNo=" + EnqNo + "&ItemNo=" + ItemNo + "&type=" + type).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstQuotePrep = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstQuotePrep;
        }

        public string SaveContractReviewData(ContractReview viewModel)
        {
            string result = "0";
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveContractReviewData", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<PLEntity> LoadMasterPLlist(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            List<PLEntity> objList = new List<PLEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/LoadMasterPLlist?skip=" + skip + "&pageSize=" + pageSize + "&sortColumn=" + sortColumn + "&sortColumnDir=" + sortColumnDir + "&search=" + search).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objList = JsonConvert.DeserializeObject<List<PLEntity>>(data);
                }
            }

            return objList;
        }


        public List<QuotationPreparationEntity> LoadQuotePrepListDetails(int skip, int pageSize, string sortColumn, string sortColumnDir, string search, string quoteType = null, string quoteNo = null, string itemNo = null)
        {
            List<QuotationPreparationEntity> objList = new List<QuotationPreparationEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/LoadQuotePrepListDetails?skip=" + skip + "&pageSize=" + pageSize + "&sortColumn=" + sortColumn + "&sortColumnDir=" + sortColumnDir + "&search=" + search + "&quoteType=" + quoteType + "&quoteNo=" + quoteNo + "&itemNo=" + itemNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objList = JsonConvert.DeserializeObject<List<QuotationPreparationEntity>>(data);
                }
            }

            return objList;
        }


        public QuotationPreparationEntity GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo, int QuotePrepId)
        {
            var baseAddress = "TechnicalDetails";
            QuotationPreparationEntity newDdl = new QuotationPreparationEntity();

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuotePrepDetails?itemNoId=" + itemNoId + "&quoteType=" + quoteType + "&quoteNo=" + quoteNo + "&QuotePrepId=" + QuotePrepId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    newDdl = JsonConvert.DeserializeObject<QuotationPreparationEntity>(data);
                }
            }
            return newDdl;
        }

        public List<ItemEntity> LoadItemWiseOrders(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            List<ItemEntity> objList = new List<ItemEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/LoadItemWiseOrders?skip=" + skip + "&pageSize=" + pageSize + "&sortColumn=" + sortColumn + "&sortColumnDir=" + sortColumnDir + "&search=" + search).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objList = JsonConvert.DeserializeObject<List<ItemEntity>>(data);
                }
            }

            return objList;
        }

        public List<DropDownEntity> GetContractReviews(string customerId = null)
        {
            List<DropDownEntity> prodList = new List<DropDownEntity>();
            var baseAddress = "TechnicalDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetContractReviews?customerId=" + customerId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prodList = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return prodList;
        }


    }
}
