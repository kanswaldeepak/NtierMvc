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
    public class EnquiryManager
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        EnquiryEntity objEntity;


        public EnquiryManager()
        {
            _loggingHandler = new LoggingHandler();
            objEntity = new EnquiryEntity();
        }

        #endregion

        public string SaveEnquiryDetails(EnquiryEntity viewModel)
        {
            string result = "0";
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveEnquiryDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public EnquiryEntityDetails GetEnquiryDetails(int pageIndex, int pageSize, string SearchEnqName = null, string SearchEnqVendorID = null, string SearchProductGroup = null, string SearchMonth = null, string SearchEOQ = null)
        {
            var baseAddress = "EnquiryDetails";
            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetEnquiryDetails?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchEnqName=" + SearchEnqName + "&SearchEnqVendorID=" + SearchEnqVendorID + "&SearchProductGroup=" + SearchProductGroup + "&SearchMonth=" + SearchMonth + "&SearchEOQ=" + SearchEOQ).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    enq = JsonConvert.DeserializeObject<EnquiryEntityDetails>(data);
                }
            }
            return enq;
        }

        public EnquiryEntity GetUserDetailsForEnquiry(string unitNo)
        {
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<EnquiryEntity>(data);
                }
            }
            return objEntity;
        }

        public EnquiryEntity EnquiryDetailsPopup(EnquiryEntity Model)
        {
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/EnquiryDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<EnquiryEntity>(data);
                }
            }
            return Model;
        }

        public EnquiryEntity AddEnquiryDetailsPopup(EnquiryEntity Model)
        {
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/EnquiryDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<EnquiryEntity>(data);
                }
            }
            return Model;
        }

        public string DeleteEnquiryDetail(int enquiryId)
        {
            string msgCode = "";
            int[] param = { enquiryId };
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteEnquiryDetail", param).Result;
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
            var baseAddress = "EnquiryDetails";
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

        public EnquiryEntity GetVendorDetailForEnquiry(string vendorId)
        {
            var baseAddress = "EnquiryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetVendorDetailForEnquiry?vendorId=" + vendorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<EnquiryEntity>(data);
                }
            }
            return objEntity;
        }

        public List<DropDownEntity> GetDdlValueForEnquiry(string type, string EOQId = null, string ProductGroup = null, string VendorId = null)
        {
            var baseAddress = "EnquiryDetails";
            List<DropDownEntity> newDdl = new List<DropDownEntity>();

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDdlValueForEnquiry?Type=" + type + "&EOQId=" + EOQId + "&ProductGroup=" + ProductGroup + "&VendorId=" + VendorId).Result;
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
