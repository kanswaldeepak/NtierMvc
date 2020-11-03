using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Customer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace NtierMvc.Model
{
    public class CustomerManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        CustomerEntity oCusDetail;

        public CustomerManager()
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

        public string SaveCustomerDetails(CustomerEntity viewModel)
        {
            string result = "0";
            var baseAddress = "CustomerDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveCustomerDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }


        public CustomerEntity GetUserDetails(string unitNo)
        {
            oCusDetail = new CustomerEntity();
            var baseAddress = "CustomerDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    oCusDetail = JsonConvert.DeserializeObject<CustomerEntity>(data);
                }
            }
            return oCusDetail;
        }


        public CustomerEntity CustomerDetailsPopup(CustomerEntity Model)
        {
            var baseAddress = "CustomerDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/CustomerDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<CustomerEntity>(data);
                }
            }
            return Model;
        }

        public CustomerEntityDetails GetCustomerDetails(int pageIndex, int pageSize, string SearchCustomerName = null, string SearchCustomerID = null)
        {
            var baseAddress = "CustomerDetails";
            CustomerEntityDetails cusEnt = new CustomerEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCustomerDetails?pageIndex="+ pageIndex + "&pageSize=" + pageSize+ "&SearchCustomerName=" + SearchCustomerName + "&SearchCustomerID=" + SearchCustomerID).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    cusEnt = JsonConvert.DeserializeObject<CustomerEntityDetails>(data);
                }
            }
            return cusEnt;
        }

        public string DeleteCustomerDetail(int CustomerId)
        {
            string msgCode = "";
            int[] param = { CustomerId };
            var baseAddress = "CustomerDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteCustomerDetail", param).Result;
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

        public List<DropDownEntity> GetDdlValueForCustomer(string type, string CustomerId = null)
        {
            var baseAddress = "CustomerDetails";
            List<DropDownEntity> newDdl = new List<DropDownEntity>();

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDdlValueForCustomer?Type=" + type + "&CustomerId=" + CustomerId).Result;
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
