using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.DesignEng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Web.Mvc;

namespace NtierMvc.Areas.DesignEng.Models
{
    public class DesignManager
    {
        
        public ProductRealisationDetails FetchProductRealisationList(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            var baseAddress = "DesignDetails";
            ProductRealisationDetails cusEnt = new ProductRealisationDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetProductRealisationDetails?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchTypeId=" + SearchTypeId + "&SearchQuoteNo=" + SearchQuoteNo + "&SearchSONo=" + SearchSONo + "&SearchVendorId=" + SearchVendorId + "&SearchVendorName=" + SearchVendorName + "&SearchProductGroup=" + SearchProductGroup).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    cusEnt = JsonConvert.DeserializeObject<ProductRealisationDetails>(data);
                }
            }
            return cusEnt;
        }

        public ProductRealisation PRPPopup(ProductRealisation Model)
        {
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/PRPPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<ProductRealisation>(data);
                }
            }
            return Model;
        }
        
        public List<BOMEntity> GetBOMList(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null)
        {
            var baseAddress = "DesignDetails";
            List<BOMEntity> bomEnt = new List<BOMEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetBOMList?ProductName=" + ProductName + "&ProductCode=" + ProductCode + "&PL=" + PL + "&ProductNo=" + ProductNo + "&CasingSize=" + CasingSize + "&CasingPPF=" + CasingPPF + "&Grade=" + Grade + "&OpenHoleSize=" + OpenHoleSize).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    bomEnt = JsonConvert.DeserializeObject<List<BOMEntity>>(data);
                }
            }
            return bomEnt;
        }

        public string SaveBOMDetails(BOMEntity quoteP)
        {
            string result = "0";
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveBOMDetails", quoteP).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveProductRealisationDetails(ProductRealisation objPRP)
        {
            string result = "0";
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveProductRealisationDetails", objPRP).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public GateEntryEntity BillDetailsPopup(GateEntryEntity Model)
        {
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/BillDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<GateEntryEntity>(data);
                }
            }
            return Model;
        }

        public string SaveBillMonitoringDetails(GateEntryEntity vmbE)
        {
            string result = "0";
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveBillMonitoringDetails", vmbE).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public List<ProductRealisation> GetPoSLNoDetails(string POSlNo)
        {
            List<ProductRealisation> prpList = new List<ProductRealisation>();
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPoSLNoDetails?POSlNo=" + POSlNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prpList = JsonConvert.DeserializeObject<List<ProductRealisation>>(data);
                }
            }
            return prpList;
        }

        public DataTable GetDataTablePRPData(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null)
        {
            DataTable prpList = new DataTable();
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDataTablePRPData?DateFrom=" + DateFrom + "&DateTo=" + DateTo + "&ReportType=" + ReportType + "&VendorId=" + VendorId + "&SoNo=" + SoNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prpList = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return prpList;
        }

        public List<DropDownEntity> GetVendorIdFromQuoteType(string ReportType=null)
        {
            List<DropDownEntity> lst = new List<DropDownEntity>();
            var baseAddress = "DesignDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetVendorIdFromQuoteType?ReportType=" + ReportType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lst = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lst;
        }

        public OrderEntity GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId)
        {
            var baseAddress = "DesignDetails";
            OrderEntity orderEntity = new OrderEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetQuoteOrderDetailsForPRP?quoteType=" + quoteType + "&quoteNoId=" + quoteNoId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    orderEntity = JsonConvert.DeserializeObject<OrderEntity>(data);
                }
            }

            return orderEntity;
        }

    }
}