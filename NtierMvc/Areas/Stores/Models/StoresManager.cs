using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Stores;
using System;
using System.Data;
using System.Net.Http;

namespace NtierMvc.Areas.Stores.Models
{
    public class StoresManager : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        public StoresManager()
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

        public GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo, string GRNo = null)
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntityDetails tableList = new GoodsRecieptEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDetailForGateControlNo?GateControlNo=" + GateControlNo + "&GRno="+GRNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<GoodsRecieptEntityDetails>(data);
                }
            }
            return tableList;
        }

        public GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntityDetails prDetails = new GoodsRecieptEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/FetchGoodsRecieptList?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchVendorTypeId=" + SearchVendorTypeId + "&SearchSupplierId=" + SearchSupplierId + "&SearchRMCategory=" + SearchRMCategory + "&SearchDeliveryDateFrom=" + SearchDeliveryDateFrom + "&SearchDeliveryDateTo=" + SearchDeliveryDateTo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prDetails = JsonConvert.DeserializeObject<GoodsRecieptEntityDetails>(data);
                }
            }
            return prDetails;
        }

        public GoodsRecieptEntity GetGRDetailsPopup(string GRno=null)
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntity Model = new GoodsRecieptEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGRDetailsPopup?GRno="+ GRno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<GoodsRecieptEntity>(data);
                }
            }
            return Model;
        }

        public string SaveGoodsRecieptEntryDetails(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "StoresDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveGoodsRecieptEntryDetails", objBU).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public DataTable GetGoodsListDataForDocument(string GRno)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "StoresDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGoodsListDataForDocument?GRno=" + GRno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

        public DataTable GetGoodsDetailForDocument(string GRno)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "StoresDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGoodsDetailForDocument?GRno=" + GRno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

    }
}
