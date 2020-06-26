using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;

namespace NtierMvc.Model
{
    public class GateEntryManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        QuotationEntity oCusDetail;

        public GateEntryManager()
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

        public string SaveGateEntryDetails(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "GateEntryDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveGateEntryDetails", objBU).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public GateEntryEntityDetails FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null)
        {
            var baseAddress = "GateEntryDetails";
            GateEntryEntityDetails vbmEnt = new GateEntryEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGateEntryList?pageIndex="
                    + pageIndex + "&pageSize=" + pageSize + "&SearchVendorNature=" + SearchVendorNature
                    + "&SearchVendorName=" + SearchVendorName + "&SearchBillNo=" + SearchBillNo + "&SearchBillDate="
                    + SearchBillDate + "&SearchItemDescription=" + SearchItemDescription + "&SearchCurrency="
                    + SearchCurrency + "&SearchApprovalStatus=" + SearchApprovalStatus).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    vbmEnt = JsonConvert.DeserializeObject<GateEntryEntityDetails>(data);
                }
            }
            return vbmEnt;
        }


        public GateEntryEntityDetails GetPOTableDetailsForGateEntry(string PRSetno)
        {
            var baseAddress = "GateEntryDetails";
            GateEntryEntityDetails tableList = new GateEntryEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPOTableDetailsForGateEntry?POSetno=" + PRSetno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<GateEntryEntityDetails>(data);
                }
            }
            return tableList;
        }

        


    }
}
