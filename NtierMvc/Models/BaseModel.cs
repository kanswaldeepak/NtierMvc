using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NtierMvc.Common;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using NtierMvc.Model.Admin;
using NtierMvc.Model.Account;
using System.Resources;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Application;

namespace NtierMvc.Models
{
    public class BaseModel
    {
        public List<DropDownEntity> GetTitle()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTitle").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        // for Maha IT Affiliation
        public List<DropDownEntity> GetInstituteType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInstituteType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetOrganizationType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrganizationType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetFacultyName()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetFacultyName").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetDegreeName(int CourseId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDegreeName?CourseId=" + CourseId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetDegreeName()
        {
            int CourseId = 0;
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDegreeName?CourseId=" + CourseId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetGenderType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetGenderType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetProposedInstituteType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetProposedInstituteType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetMinorityCategoryType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMinorityCategoryType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetReligiousMinorityType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetReligiousMinorityType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetCourseLevelType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCourseLevelType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetAcademicYears()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAcademicYears").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }


        public List<DropDownEntity> GetCourseType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCourseType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetSubjectName(int degreeId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSubjectName?DegreeId=" + degreeId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetSubjectName()
        {
            int degreeId = 0;
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSubjectName?DegreeId=" + degreeId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetDocumentListByCollegeID(int RegistrationId, int UserId, string workCode)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDocumentListByCollegeIDApi?RegistrationId=" + RegistrationId + "&UserId=" + UserId + "&WorkCode=" + workCode).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        // for Maha IT Affiliation

        public List<DropDownEntity> GetApplicationType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }


        public List<DropDownEntity> GetApplicationTypesForITIAdmin()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationTypesForITIAdmin").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetApplicationTypesForVTIAdmin()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationTypesForVTIAdmin").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }


        public List<DropDownEntity> GetMSSDSPortal()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMSSDSPortal").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        //public List<DropDownEntity> GetOrganizationType()
        //{
        //    List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
        //    var baseAddress = "Base";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrganizationType").Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
        //        }
        //    }
        //    return lstDropDownEntity;
        //}
        public List<DropDownEntity> GetOrganizationTypeByApplType(int applicationType)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetOrganizationTypeByApplType?applicationType=" + applicationType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public DataSet GerPinCodeSearch(string pinCode)
        {
            DataSet ds = new DataSet();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GerPinCodeSearch?pinCode=" + pinCode).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ds = JsonConvert.DeserializeObject<DataSet>(data);
                }
            }
            return ds;
        }
        public bool IsProdPayment()
        {
            string IsProdPaymentVal = ConfigurationManager.AppSettings["isProdPayment"];
            if (IsProdPaymentVal == "false")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataSet GerPinCodeSearchByTaluka(string pinCode, string subDistrictCode)
        {
            DataSet ds = new DataSet();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GerPinCodeSearchByTaluka?pinCode=" + pinCode + "&TalukaCode=" + subDistrictCode).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ds = JsonConvert.DeserializeObject<DataSet>(data);
                }
            }
            return ds;
        }




        public List<DropDownEntity> GetTaxonomyDropDownItems(string objectName, string property)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTaxonomyDropDownItems?objectName=" + objectName + "&property=" + property).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public BankDetail GetBankDetails(string IfscCode)
        {
            BankDetail oBankDetail = new BankDetail();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetBankDetails?IfscCode=" + IfscCode).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    oBankDetail = JsonConvert.DeserializeObject<BankDetail>(data);
                }
            }
            return oBankDetail;
        }

        public List<DropDownEntity> GetTradeMachinesDropDownItems(int tradeId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTradeMachinesDropDownItems?tradeId=" + tradeId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public string GenerateOtp(string mobileNo, string msgBody)
        {
            string result = "failure";
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GenerateOtp?mobileNo=" + mobileNo + "&msgBody=" + msgBody).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                }
            }
            return result;
        }
        public string GenerateMailOtp(string emailId, int mailOtp)
        {
            string result = "failure";
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GenerateMailOtp?emailId=" + emailId + "&mailOtp=" + mailOtp).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(result);
                }
            }
            return result;
        }

        public bool GetApplicationSubmissionStatus(int registrationId)
        {
            bool _accept = false;
            try
            {
                var baseAddress = "Base";
                using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
                {
                    HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationSubmissionStatus?registrationId=" + registrationId).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        _accept = JsonConvert.DeserializeObject<bool>(data);
                    }
                }
            }
            catch (Exception ex)
            { }

            return _accept;
        }

        public ApplicationSubmissionStatusEntity GetApplicationSubmittedSatus(int registrationId)
        {
            ApplicationSubmissionStatusEntity entity = new ApplicationSubmissionStatusEntity();
            try
            {
                var baseAddress = "Base";
                using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
                {
                    HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationSubmittedSatus?registrationId=" + registrationId).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        entity = JsonConvert.DeserializeObject<ApplicationSubmissionStatusEntity>(data);
                    }
                }
            }
            catch (Exception ex)
            { }

            return entity;
        }
        public bool GetITIHeadDeclarationDetais(int registrationId)
        {
            bool _accept = true;
            try
            {
                var baseAddress = "Base";
                using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
                {
                    HttpResponseMessage response = client.GetAsync(baseAddress + "/GetITIHeadDeclarationDetais?registrationId=" + registrationId).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        _accept = JsonConvert.DeserializeObject<bool>(data);
                    }
                }
            }
            catch (Exception ex)
            { }

            return _accept;
        }



        public List<DropDownEntity> GetCourseApproverDDLItems(int applicationTypeId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCourseApproverDDLItems?applicationTypeId=" + applicationTypeId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public string GetApplicationTypeForReg(int applicationTypeId)
        {
            string ApplicationType = "";
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationTypeForReg?applicationTypeId=" + applicationTypeId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ApplicationType = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return ApplicationType;
        }

        public List<DropDownEntity> GetTradeTypes()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTradeTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetTrades(int applicationTypeId, int tradeTypeId, int tradeSectorId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAllTrades?applicationTypeId=" + applicationTypeId + "&tradeTypeId=" + tradeTypeId + "&tradeSectorId=" + tradeSectorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetRegionTypes()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetRegionTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public int ResetTradeRelatedDetails(int registrationId)
        {
            int result = 0;
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/ResetTradeRelatedDetails?registrationId=" + registrationId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<int>(data);
                }
            }
            return result;
        }
        public List<DropDownEntity> GetApplicationStatusTypes()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationStatusTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetInspectionFeesType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInspectionFeesType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetTradeSectorsByAppType(string name)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTradeSectorsByAppType?applicationTypeName=" + name).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetInspectionSections()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInspectionSections").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetInspectionStatus()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInspectionStatus").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetTradeSectors()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTradeSectors").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetInspectionAgencyCommitte(int agencyId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInspectionAgencyCommitte?agencyId=" + agencyId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        //public List<InspectionRegistrationDetails> GetInspectionRegistrations(string adminType)
        //{
        //    List<InspectionRegistrationDetails> lstInspectionRegistration = new List<InspectionRegistrationDetails>();
        //    var baseAddress = "Base";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.GetAsync(baseAddress + "/GetInspectionRegistrations?adminType=" + adminType).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            lstInspectionRegistration = JsonConvert.DeserializeObject<List<InspectionRegistrationDetails>>(data);
        //        }
        //    }
        //    return lstInspectionRegistration;
        //}
        public List<GetInfraNormsEntity> MyList()
        {
            List<GetInfraNormsEntity> lstNormsData = new List<GetInfraNormsEntity>();
            var baseAddress = "Admin";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/MyList").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstNormsData = JsonConvert.DeserializeObject<List<GetInfraNormsEntity>>(data);
                }
            }
            return lstNormsData;
        }

        public string GetApplicationTypeByRegId(int regId)
        {
            string ApplicationType = null;
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetApplicationTypeByRegId?regId=" + regId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ApplicationType = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    return ApplicationType;
                }
                return ApplicationType;
            }
        }

        public ChangePasswodEntity GetPasswordByUsername(string Username)
        {
            ChangePasswodEntity ObjPassword = new ChangePasswodEntity();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPasswordByUsername?Username=" + Username).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ObjPassword = JsonConvert.DeserializeObject<ChangePasswodEntity>(data);
                }
            }
            return ObjPassword;
        }

        public List<DropDownEntity> GetTradesForAdminBySector(int applicationTypeId, string tradeSectorId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTradesForAdminBySector?applicationTypeId=" + applicationTypeId + "&tradeSectorId=" + tradeSectorId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetAffiliationType()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAffiliationType").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public class AadhaarResponse
        {
            public string DeviceId { get; set; }
            public string SubAUAtransId { get; set; }
            public string Ret { get; set; }
            public string ResponseTs { get; set; }
            public string ResponseCode { get; set; }
            public string msg { get; set; }
        }


        // By Ganesh Jha
        public List<DropDownEntity> GetConcatenatedFacultyCourseMapping(int registrationId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetConcatenatedFacultyCourseMapping?registrationId=" + registrationId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }
        public List<DropDownEntity> GetAllBuilidingsByRegId(int registrationId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAllBuilidingsByRegId?registrationId=" + registrationId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }




        public class APIAuthData
        {
            public string username { get; set; }
            public string password { get; set; }
            public string secret_key { get; set; }
            public string salt_key { get; set; }
        }


        public class AadhaarRequest
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string is_fathers_name { get; set; }
            public string fathers_name { get; set; }
            public string aadhar_no { get; set; }
            public long mobile_no { get; set; }
            public string dobday { get; set; }
            public string dobmonth { get; set; }
            public string dobyear { get; set; }
            public string gender { get; set; }
            public string is_only_year { get; set; }
            public APIAuthData apiAuthData { get; set; }

        }
        public List<DropDownEntity> GetAppointmentTypes()
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetAppointmentTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetProgram(int facultyId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetProgram?facultyId=" + facultyId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);

                }
                return lstDropDownEntity;
            }
        }

        public List<DropDownEntity> GetCourse(int facultyId)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCourse?facultyId=" + facultyId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);

                }
                return lstDropDownEntity;
            }
        }

        public List<DropDownEntity> GetCourse()
        {
            int facultyId = 0;
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCourse?facultyId=" + facultyId.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);

                }
                return lstDropDownEntity;
            }
        }


        public List<DropDownEntity> GetBlankDropdown()
        {
            List<DropDownEntity> list = new List<DropDownEntity>();
            list.Insert(0, new DropDownEntity() { DataTextField = "Select", DataValueField = 0, DataCodeField = "" });
            return list;
        }

        public List<DropDownEntity> GetCourseList(int ProgramId, int CourseType = 0, int registrationId = 0)
        {

            List<DropDownEntity> entity = new List<DropDownEntity>();
            var baseAddress = "Base";

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCoursesList?programId=" + ProgramId + "&courseType=" + CourseType + "&registrationId=" + registrationId).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    entity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(data));
                    throw new Exception("WebAPI error");
                }
            }
            entity.Insert(0, new DropDownEntity() { DataTextField = "Select", DataValueField = 0, DataCodeField = "" });
            return entity;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMasterTableList?TableName=" + TableName + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMasterTableStringList?TableName=" + TableName + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMasterTableList?TableName=" + TableName + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField + "&Property=" + Property).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField, string Property, string ColumnName, string ListType = null)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMasterTableStringList?TableName=" + TableName + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField + "&Property=" + Property + "&ColumnName=" + ColumnName + "&ListType=" + ListType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property, string ColumnName)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetMasterTableList?TableName=" + TableName + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField + "&Property=" + Property + "&ColumnName=" + ColumnName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        //public static string GetResourceValue(string key)
        //{
        //    ResourceManager rm = new ResourceManager(typeof(NtierMvc.PrivateITIResources.Common.Common));
        //    return rm.GetString(key);
        //}

        public List<GeographyEntity> GetStateDetail(string countryId)
        {
            List<GeographyEntity> geoDetail = new List<GeographyEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetStateDetail?countryId=" + countryId).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    geoDetail = JsonConvert.DeserializeObject<List<GeographyEntity>>(data);
                }
            }
            return geoDetail;
        }

        //public List<TableRecordsEntity> GetTableRecordsList(string ListType, List<string> stringColumnNames, List<string> stringColumnValues, List<int> intValueParams)
        //{
        //    List<TableRecordsEntity> lstTableRecordsEntity = new List<TableRecordsEntity>();
        //    var baseAddress = "Base";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTableRecordsList?ListType=" + ListType + "&stringColumnNames=" + stringColumnNames + "&stringColumnValues=" + stringColumnValues + "&intValueParams=" + intValueParams).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            lstTableRecordsEntity = JsonConvert.DeserializeObject<List<TableRecordsEntity>>(data);
        //        }
        //    }
        //    return lstTableRecordsEntity;
        //}

        public List<TableRecordsEntity> GetTableDataList(string ListType, string TableName, string Column1 = null, string Param1 = null, string Column2 = null, string Param2 = null, string Column3 = null, string Param3 = null, string Column4 = null, string Param4 = null, string Column5 = null, string Param5 = null, string RequiredColumn1 = null, string RequiredColumn2 = null, string RequiredColumn3 = null, string RequiredColumn4 = null, string RequiredColumn5 = null, string RequiredColumn6 = null, string RequiredColumn7 = null, string RequiredColumn8 = null, string RequiredColumn9 = null, string RequiredColumn10 = null)
        {
            List<TableRecordsEntity> lstTableRecordsEntity = new List<TableRecordsEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTableDataList?ListType=" + ListType + "&TableName=" + TableName + "&Param1=" + Param1 + "&Param2=" + Param2 + "&Param3=" + Param3 + "&Param4=" + Param4 + "&Param5=" + Param5 + "&Column1=" + Column1 + "&Column2=" + Column2 + "&Column3=" + Column3 + "&Column4=" + Column4 + "&Column5=" + Column5 + "&RequiredColumn1=" + RequiredColumn1 + "&RequiredColumn2=" + RequiredColumn2 + "&RequiredColumn3=" + RequiredColumn3 + "&RequiredColumn4=" + RequiredColumn4 + "&RequiredColumn5=" + RequiredColumn5 + "&RequiredColumn6=" + RequiredColumn6 + "&RequiredColumn7=" + RequiredColumn7 + "&RequiredColumn8=" + RequiredColumn8 + "&RequiredColumn9=" + RequiredColumn9 + "&RequiredColumn10=" + RequiredColumn10).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTableRecordsEntity = JsonConvert.DeserializeObject<List<TableRecordsEntity>>(data);
                }
            }
            return lstTableRecordsEntity;
        }

        public List<TableRecordsEntity> GetTableDataList(TableRecordsEntity TblObj)
        {
            List<TableRecordsEntity> lstTableRecordsEntity = new List<TableRecordsEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetTableDataList", TblObj).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTableRecordsEntity = JsonConvert.DeserializeObject<List<TableRecordsEntity>>(data);
                }
            }
            return lstTableRecordsEntity;
        }

        public SingleColumnEntity GetSingleColumnValues(string TableName, string DataValueField1, string DataTextField1, string ColumnName1, string Param1, string DataValueField2 = null, string ColumnName2 = null, string Param2 = null, string DataValueField3 = null, string ColumnName3 = null, string Param3 = null, string DataValueField4 = null, string ColumnName4 = null, string Param4 = null, string DataValueField5 = null, string DataValueField6 = null, string DataValueField7 = null, string DataValueField8 = null)
        {
            SingleColumnEntity ObjSingleEntity = new SingleColumnEntity();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSingleColumnValues?TableName=" + TableName + "&DataValueField1=" + DataValueField1 + "&DataTextField1=" + DataTextField1 + "&Param1=" + Param1 + "&ColumnName1=" + ColumnName1 + "&DataValueField2=" + DataValueField2 + "&Param2=" + Param2 + "&ColumnName2=" + ColumnName2 + "&DataValueField3=" + DataValueField3 + "&Param3=" + Param3 + "&ColumnName3=" + ColumnName3 + "&DataValueField4=" + DataValueField4 + "&Param4=" + Param4 + "&ColumnName4=" + ColumnName4 + "&DataValueField5=" + DataValueField5 + "&DataValueField6=" + DataValueField6 + "&DataValueField7=" + DataValueField7 + "&DataValueField8="
                    + DataValueField8).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    ObjSingleEntity = JsonConvert.DeserializeObject<SingleColumnEntity>(data);
                }
            }
            return ObjSingleEntity;
        }

        //public List<TableRecordsEntity> GetTableRecordsList(string ListType, string TableName, string DataParamField1, string DataParamValueField1, string DataColumnTextField1, string DataColumnValueField1, string DataParamField2 = null, string DataParamValueField2 = null, string DataColumnTextField2 = null, string DataColumnValueField2 = null, string DataParamField3 = null, string DataParamValueField3 = null, string DataColumnTextField3 = null, string DataColumnValueField3 = null, string DataParamField4 = null, string DataParamValueField4 = null, string DataColumnTextField4 = null, string DataColumnValueField4 = null, string DataParamField5 = null, string DataParamValueField5 = null, string DataColumnTextField5 = null, string DataColumnValueField5 = null)
        //{
        //    List<TableRecordsEntity> tblRecordList = new List<TableRecordsEntity>();
        //    var baseAddress = "Base";
        //    using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
        //    {
        //        HttpResponseMessage response = client.GetAsync(baseAddress + "/GetTableRecordsList?ListType=" + ListType + "&TableName=" + TableName + "&DataParamField1=" + DataParamField1 + "&DataParamValueField1=" + DataParamValueField1 + "&DataColumnTextField1=" + DataColumnTextField1 + "&DataColumnValueField1=" + DataColumnValueField1 + "&DataParamField2=" + DataParamField2 + "&DataParamValueField2=" + DataParamValueField2 + "&DataColumnTextField2=" + DataColumnTextField2 + "&DataColumnValueField2=" + DataColumnValueField2 + "&DataParamField3=" + DataParamField3 + "&DataParamValueField3=" + DataParamValueField3 + "&DataColumnTextField3=" + DataColumnTextField3 + "&DataColumnValueField3=" + DataColumnValueField3 + "&DataParamField4=" + DataParamField4 + "&DataParamValueField4=" + DataParamValueField4 + "&DataColumnTextField4=" + DataColumnTextField4 + "&DataColumnValueField4=" + DataColumnValueField4 + "&DataParamField5=" + DataParamField5 + "&DataParamValueField5=" + DataParamValueField5 + "&DataColumnTextField5=" + DataColumnTextField5 + "&DataColumnValueField5=" + DataColumnValueField5).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var data = response.Content.ReadAsStringAsync().Result;
        //            tblRecordList = JsonConvert.DeserializeObject<List<TableRecordsEntity>>(data);
        //        }
        //    }
        //    return tblRecordList;
        //}

        public string DeleteFormTable(string TableName, string ColumnName1, string Param1, string ColumnName2 = null, string Param2 = null)
        {
            string result = "0";
            var baseAddress = "Base";

            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/DeleteFormTable?TableName=" + TableName + "&ColumnName1=" + ColumnName1 + "&Param1=" + Param1 + "&ColumnName2=" + ColumnName2 + "&Param2=" + Param2).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
                else
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(new Exception(response.ReasonPhrase));
                    throw new Exception("WebAPI error");
                }
            }
            return result;
        }

        public List<DropDownEntity> GetDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDropDownList?TableName=" + TableName + "&ListType=" + ListType + "&DataValueField=" + DataValueField + "&DataTextField=" + DataTextField + "&Param=" + Param + "&ColumnName=" + ColumnName + "&Others=" + Others + "&orderBy=" + orderBy + "&orderByColumn=" + orderByColumn + "&Param1=" + Param1 + "&ColumnName1=" + ColumnName1 + "&Param2=" + Param2 + "&ColumnName2=" + ColumnName2 + "&Param3=" + Param3 + "&ColumnName3=" + ColumnName3 + "&Param4=" + Param4 + "&ColumnName4=" + ColumnName4).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }


        public CommonDetailsEntity GetCommonSettings()
        {
            CommonDetailsEntity objEntity = new CommonDetailsEntity();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetCommonSettings").Result;
                //HttpResponseMessage response = client.GetAsync(baseAddress + "/GetPagewiseAccess?UserId=" + entity.UserId + "&RoleId=" + entity.RoleId + "&RegistrationId=" + entity.RegistrationId + "&RequestUrl=" + entity.requestURL + "&Action=" + entity.action).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    objEntity = JsonConvert.DeserializeObject<CommonDetailsEntity>(data);
                }
            }
            return objEntity;
        }

        public List<DropDownEntity> GetSONoQuoteNoList(string EndUse, string quoteType)
        {
            List<DropDownEntity> lstDropDownEntity = new List<DropDownEntity>();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetSONoQuoteNoList?EndUse=" + EndUse + "&quoteType=" + quoteType).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstDropDownEntity = JsonConvert.DeserializeObject<List<DropDownEntity>>(data);
                }
            }
            return lstDropDownEntity;
        }

        public DataTable GetDataTableForDocument(string ListType, string TableName, string[] DataColumn, string[] DataParam, string[] RequiredColumn)
        {
            DataTable lstTable = new DataTable();
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetDataTableForDocument?ListType=" + ListType + "&TableName=" + TableName + "&DataColumn=" + DataColumn + "&DataParam=" + DataParam + "&RequiredColumn=" + RequiredColumn).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    lstTable = JsonConvert.DeserializeObject<DataTable>(data);
                }
            }
            return lstTable;
        }

        public string NumberToWords(string rawnumber)
        {
            int inputNum = 0;
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;
            string dollars, cents;
            try
            {
                string[] Splits = new string[2];
                Splits = rawnumber.Split('.');   //notice that it is ' and not "
                inputNum = Convert.ToInt32(Splits[0]);
                dollars = "";
                cents = Splits[1];
                if (cents.Length == 1)
                {
                    cents += "0";   // 12.5 is twelve and 50/100, not twelve and 5/100
                }
            }
            catch
            {
                cents = "00";
                inputNum = Convert.ToInt32(rawnumber);
                dollars = "";
            }

            string x = "";

            //they had zero for ones and tens but that gave ninety zero for 90
            string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] thou = { "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion" };

            bool isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }
            if (inputNum == 0)
            {
                return "Zero and Cents " + cents;
            }

            string cent = cents.ToString();
            while (cent.Length > 0)
            {
                threeDigits = int.Parse(cent);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                cents = tens[dig2] + " " + ones[dig1];
                cent = cent.Substring(0, cent.Length - 2);
            }

            string s = inputNum.ToString();

            while (s.Length > 0)
            {
                //Get the three rightmost characters
                x = (s.Length < 3) ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = (threeDigits % 10);

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    dollars = thou[level] + " " + dollars;
                    dollars = dollars.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20)
                    {
                        // if less than 20, use "ones" only
                        dollars = ones[lasttwo] + " " + dollars;
                    }
                    else
                    {
                        // otherwise, use both "tens" and "ones" array
                        dollars = tens[dig2] + " " + ones[dig3] + " " + dollars;
                    }
                    if (s.Length < 3)
                    {
                        if (isNegative) { dollars = "Negative " + dollars; }
                        //return dollars + " and " + cents + "/100";
                        return dollars + " and Cents " + cents;
                    }
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                {
                    dollars = ones[dig1] + " Hundred " + dollars;
                    s = (s.Length - 3) > 0 ? s.Substring(0, s.Length - 3) : "";
                    level++;
                }
                else
                {
                    if (s.Length > 3)
                    {
                        s = s.Substring(0, s.Length - 3);
                        level++;
                    }
                }
            }

            if (isNegative) { dollars = "Negative " + dollars; }

            if (!string.IsNullOrEmpty(cents.Trim()))
                return dollars + " and Cents " + cents;
            else
                return dollars;
        }

        public string SaveBulkEntryDetails(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "Base";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveBulkEntryDetails", objBU).Result;
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