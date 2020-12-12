using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace NtierMvc.Model
{
    public class QuotationManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        QuotationEntity objEntity;


        public QuotationManager()
        {
            _loggingHandler = new LoggingHandler();
            objEntity = new QuotationEntity();
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
                    objEntity = null;
                }
            }
            _bDisposed = true;
        }
        #endregion

        //public string SaveQuotationDetails(QuotationEntity viewModel)
        //{
        //    string result = "0";
        //    var baseAddress = "QuotationDetails";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveQuotationDetails", viewModel).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            result = JsonConvert.DeserializeObject<string>(data);
        //        }
        //    }
        //    return result;
        //}
        
        public QuotationEntityDetails GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteCustomerID = null, string SearchSubject = null, string SearchDeliveryTerms = null)
        {
            var baseAddress = "TechnicalDetails";
            QuotationEntityDetails quote = new QuotationEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteRegList?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchQuoteType=" + SearchQuoteType + "&SearchQuoteCustomerID=" + SearchQuoteCustomerID + "&SearchSubject=" + SearchSubject + "&SearchDeliveryTerms=" + SearchDeliveryTerms).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    quote = JsonConvert.DeserializeObject<QuotationEntityDetails>(data);
                }
            }
            return quote;
        }

        public QuotationEntity GetUserQuoteDetails(string unitNo)
        {
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserQuoteDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return objEntity;
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
                    objEntity = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return objEntity;
        }

        public QuotationEntity QuotationDetailsPopup(QuotationEntity Model)
        {
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/QuotationDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return Model;
        }

        public QuotationEntity AddQuotationDetailsPopup(QuotationEntity Model)
        {
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/QuotationDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<QuotationEntity>(data);
                }
            }
            return Model;
        }

        public string DeleteQuotationDetail(int QuotationId)
        {
            string msgCode = "";
            int[] param = { QuotationId };
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteQuotationDetail", param).Result;
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

        public List<DropDownEntity> GetCityName(string VendorName = "")
        {
            List<DropDownEntity> LstCity = new List<DropDownEntity>();
            var baseAddress = "QuotationDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCityName?VendorName=" + VendorName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    LstCity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return LstCity;
        }

        public List<DropDownEntity> GetDdlValueForQuote(string type, string VendorId = null, string QuoteType = null, string SubjectId = null)
        {
            var baseAddress = "QuotationDetails";
            List<DropDownEntity> newDdl = new List<DropDownEntity>();

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDdlValueForQuote?Type=" + type + "&VendorId=" + VendorId + "&QuoteType=" + QuoteType + "&SubjectId=" + SubjectId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    newDdl = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return newDdl;
        }



    }
}
