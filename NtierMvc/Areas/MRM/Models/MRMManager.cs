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

        public PODetailEntity GetSavedPODetails(string POSetNo)
        {
            var baseAddress = "MRMDetail";
            PODetailEntity Model = new PODetailEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSavedPODetails?POSetNo="+ POSetNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PODetailEntity>(data);
                }
            }
            return Model;
        }

        public List<PODetailEntity> GetPOTableDetails(string POSetNo)
        {
            var baseAddress = "MRMDetail";
            List<PODetailEntity> Model = new List<PODetailEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPOTableDetails?POSetNo=" + POSetNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<List<PODetailEntity>>(data);
                }
            }
            return Model;
        }

        public List<PRDetailEntity> GetPRTableDetails(string PRSetno)
        {
            var baseAddress = "MRMDetail";
            List<PRDetailEntity> tableList = new List<PRDetailEntity>();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRTableDetails?PRSetno=" + PRSetno).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<List<PRDetailEntity>>(data);
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

        public PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var baseAddress = "MRMDetail";
            PRDetailEntityDetails prDetails = new PRDetailEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRDetailsList?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&DeptName=" + DeptName + "&SearchVendorTypeId=" + SearchVendorTypeId + "&SearchSupplierId=" + SearchSupplierId + "&SearchRMCategory=" + SearchRMCategory + "&SearchDeliveryDateFrom=" + SearchDeliveryDateFrom + "&SearchDeliveryDateTo=" + SearchDeliveryDateTo).Result;
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

        public string SavePODetailsList(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SavePODetailsList", objBU).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public PODetailEntityDetails GetPODetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var baseAddress = "MRMDetail";
            PODetailEntityDetails prDetails = new PODetailEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPODetailsList?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchVendorTypeId=" + SearchVendorTypeId + "&SearchSupplierId=" + SearchSupplierId + "&SearchRMCategory=" + SearchRMCategory + "&SearchDeliveryDateFrom=" + SearchDeliveryDateFrom + "&SearchDeliveryDateTo=" + SearchDeliveryDateTo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prDetails = JsonConvert.DeserializeObject<PODetailEntityDetails>(data);
                }
            }
            return prDetails;
        }

        public PODetailEntity GetPODetailsForPopup(PODetailEntity Model)
        {
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetPODetailsForPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PODetailEntity>(data);
                }
            }
            return Model;
        }

        public DataTable GetPODetailForDocument(string PRSetNo)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPODetailForDocument?PRSetNo=" + PRSetNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

        public DataTable GetPOListDataForDocument(string PRSetNo)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPOListDataForDocument?PRSetNo=" + PRSetNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

        public List<DropDownEntity> GetPRNoList(string DeptName)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPRNoList?DeptName="+DeptName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetRMCategories(string SupplierId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetRMCategories?SupplierId=" + SupplierId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetDeliveryDates(string RMCategory)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDeliveryDates?RMCategory=" + RMCategory).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public MRMBillMonitoringEntityDetails GetMRMDetailForGateControlNo(string GateControlNo)
        {
            var baseAddress = "MRMDetail";
            MRMBillMonitoringEntityDetails tableList = new MRMBillMonitoringEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMRMDetailForGateControlNo?GateControlNo=" + GateControlNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    tableList = JsonConvert.DeserializeObject<MRMBillMonitoringEntityDetails>(data);
                }
            }
            return tableList;
        }

    }
}