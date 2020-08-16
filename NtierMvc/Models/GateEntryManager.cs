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

        public string SaveGateEntryDetails(GateEntryEntity objBU)
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

        public GateEntryEntityDetails FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchPONo = null)
        {
            var baseAddress = "GateEntryDetails";
            GateEntryEntityDetails vbmEnt = new GateEntryEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/FetchInboundList?pageIndex="
                    + pageIndex + "&pageSize=" + pageSize + "&SearchVendorNature=" + SearchVendorNature
                    + "&SearchVendorName=" + SearchVendorName + "&SearchPONo=" + SearchPONo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    vbmEnt = JsonConvert.DeserializeObject<GateEntryEntityDetails>(data);
                }
            }
            return vbmEnt;
        }


        public GateEntryEntityDetails GetPOTableDetailsForGateEntry(string PRSetno, string GateNo = null)
        {
            var baseAddress = "GateEntryDetails";
            GateEntryEntityDetails tableList = new GateEntryEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPOTableDetailsForGateEntry?POSetno=" + PRSetno + "&GateNo=" + GateNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<GateEntryEntityDetails>(data);
                }
            }
            return tableList;
        }

        public GateEntryEntity InboundDetailsPopup(string GateNo)
        {
            var baseAddress = "GateEntryDetails";
            GateEntryEntity tableList = new GateEntryEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/InboundDetailsPopup?GateNo=" + GateNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<GateEntryEntity>(data);
                }
            }
            return tableList;
        }

        public List<DropDownEntity> GetPoNoDetailsForGE()
        {
            var baseAddress = "GateEntryDetails";
            List<DropDownEntity> tblDdl = new List<DropDownEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPoNoDetailsForGE").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tblDdl = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return tblDdl;

        }



    }
}
