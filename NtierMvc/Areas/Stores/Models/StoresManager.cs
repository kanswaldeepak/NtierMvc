using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model.Stores;
using System;
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

        public GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo)
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntityDetails tableList = new GoodsRecieptEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDetailForGateControlNo?GateControlNo=" + GateControlNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<GoodsRecieptEntityDetails>(data);
                }
            }
            return tableList;
        }

        public GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize)
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntityDetails prDetails = new GoodsRecieptEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/FetchGoodsRecieptList?pageIndex=" + pageIndex + "&pageSize=" + pageSize).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prDetails = JsonConvert.DeserializeObject<GoodsRecieptEntityDetails>(data);
                }
            }
            return prDetails;
        }

        public GoodsRecieptEntity GetGRDetailsPopup()
        {
            var baseAddress = "StoresDetails";
            GoodsRecieptEntity Model = new GoodsRecieptEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGRDetailsPopup").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<GoodsRecieptEntity>(data);
                }
            }
            return Model;
        }

        public string SaveGoodsRecieptEntryDetails(GoodsRecieptEntity viewModel)
        {
            string result = "0";
            var baseAddress = "StoresDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveGoodsRecieptEntryDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

    }
}
