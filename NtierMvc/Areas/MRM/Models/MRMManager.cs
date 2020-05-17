using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.MRM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;

namespace NtierMvc.Areas.MRM.Models
{
    public class MRMManager
    {
        public PRDetailEntity GetSavedPRDetailsPopup(PRDetailEntity Model)
        {
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetSavedPRDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PRDetailEntity>(data);
                }
            }
            return Model;
        }

        public List<PRDetailEntityBulkSave> GetPRTableDetails(string PRSetno)
        {
            var baseAddress = "MRMDetail";
            List<PRDetailEntityBulkSave> tableList = new List<PRDetailEntityBulkSave>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRTableDetails?PRSetno=" + PRSetno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<List<PRDetailEntityBulkSave>>(data);
                }
            }
            return tableList;
        }

        public PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model)
        {
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetPRDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PRDetailEntity>(data);
                }
            }
            return Model;
        }

        public string SavePRDetailsList(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SavePRDetailsList", objBU).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            var baseAddress = "MRMDetail";
            PRDetailEntityDetails prDetails = new PRDetailEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRDetailsList?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&DeptName=" + DeptName + "&SearchTypeId=" + SearchTypeId + "&SearchQuoteNo=" + SearchQuoteNo + "&SearchSONo=" + SearchSONo + "&SearchVendorId=" + SearchVendorId + "&SearchVendorName=" + SearchVendorName + "&SearchProductGroup=" + SearchProductGroup).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prDetails = JsonConvert.DeserializeObject<PRDetailEntityDetails>(data);
                }
            }
            return prDetails;
        }

        public string UpdateApproveReject(string PRSetno, string Status, string UserId, string PRFavouredOn, string PRStatus)
        {
            string result = "0";
            var baseAddress = "MRMDetail";
            string[] param = { PRSetno, Status, UserId, PRFavouredOn, PRStatus };
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/UpdateApproveReject", param).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SavePurchaseDetails(string PRSetno, string Communicate, string PONo, string ExpectedDeliveryDate, string UserId)
        {
            string result = "0";
            var baseAddress = "MRMDetail";
            string[] param = { PRSetno, Communicate, PONo, ExpectedDeliveryDate, UserId };
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SavePurchaseDetails", param).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public DataTable GetPRListForDocument(string PRSetNo)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRListForDocument?PRSetNo=" + PRSetNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

        public DataTable GetPRDataForDocument(string PRSetNo)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRDataForDocument?PRSetNo=" + PRSetNo).Result;
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