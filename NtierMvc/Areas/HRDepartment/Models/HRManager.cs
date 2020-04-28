using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model.HR;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NtierMvc.Areas.HRDepartment.Models
{
    public class HRManager
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        EmployeeEntity oCusDetail;

        public HRManager()
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

        public string SaveEmployeeDetails(EmployeeEntity viewModel)
        {
            string result = "0";
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveEmployeeDetails", viewModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SavePayrollDetails(PayrollEntity payModel)
        {
            string result = "0";
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SavePayrollDetails", payModel).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        public string SaveEmpLeaveDetails(LeaveManagement leaveM)
        {
            string result = "0";
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveEmpLeaveDetails", leaveM).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }


        public EmployeeEntity GetUserDetails(string unitNo)
        {
            oCusDetail = new EmployeeEntity();
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetUserDetails?unitNo=" + unitNo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    oCusDetail = JsonConvert.DeserializeObject<EmployeeEntity>(data);
                }
            }
            return oCusDetail;
        }


        public EmployeeEntity HREmpPopup(EmployeeEntity Model)
        {
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/EmployeeDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<EmployeeEntity>(data);
                }
            }
            return Model;
        }

        public EmployeeEntityDetails GetEmployeeDetails(int pageIndex, int pageSize, string SearchEmployeeNameId, string SearchDesignation, string SearchDepartment)
        {
            var baseAddress = "HRDetails";
            EmployeeEntityDetails cusEnt = new EmployeeEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetEmployeeDetails?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&SearchEmployeeNameId=" + SearchEmployeeNameId + "&SearchDesignation=" + SearchDesignation + "&SearchDepartment=" + SearchDepartment).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    cusEnt = JsonConvert.DeserializeObject<EmployeeEntityDetails>(data);
                }
            }
            return cusEnt;
        }

        public string DeleteEmployeeDetail(int EmpId)
        {
            string msgCode = "";
            int[] param = { EmpId };
            var baseAddress = "HRDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/DeleteEmpDetail", param).Result;
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

        public PayrollEntity GetEmpPayrollData(int EmpId, int Yr = 0, int mnth = 0)
        {
            int[] param = { EmpId, Yr, mnth };
            var baseAddress = "HRDetails";
            PayrollEntity Model = new PayrollEntity();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetEmpPayrollData", param).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PayrollEntity>(data);
                }
            }
            return Model;
        }

        public LeaveManagementEntityDetails GetEmpLeaveList(int EmpId)
        {
            var baseAddress = "HRDetails";
            LeaveManagementEntityDetails lstModel = new LeaveManagementEntityDetails();
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetEmpLeaveList?EmpId="+ EmpId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstModel = JsonConvert.DeserializeObject<LeaveManagementEntityDetails>(data);
                }
            }
            return lstModel;
        }
    }
}