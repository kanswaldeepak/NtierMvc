using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.DataAccess.Pool;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Net;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Common;
using NtierMvc.Model;
using NtierMvc.Model.Account;
using NtierMvc.BusinessLogic.Utility;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NtierMvc.Model.Application;

namespace NtierMvc.BusinessLogic.Worker
{
    public class BaseWorker : IBase
    {
        Repository objData = new Repository();

        public List<DropDownEntity> GetTitle()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTitle();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetApplicationType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetApplicationType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetApplicationTypesForVTIAdmin()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetApplicationTypesForVTIAdmin();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetApplicationTypesForITIAdmin()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetApplicationTypesForITIAdmin();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }


        public List<DropDownEntity> GetMSSDSPortal()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMSSDSPortal();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetOrganizationType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetOrganizationType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        // for Maha IT
        public List<DropDownEntity> GetInstituteType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetInstituteType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetAcademicYears()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetAcademicYears();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetFacultyName()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetFacultyName();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }


        public List<DropDownEntity> GetDegreeName(int CourseId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetDegreeName(CourseId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetGenderType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetGenderType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetProposedInstituteType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetProposedInstituteType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMinorityCategoryType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMinorityCategoryType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetReligiousMinorityType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetReligiousMinorityType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetCourseLevelType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetCourseLevelType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetCourseType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetCourseType();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetDocumentListByCollegeID(int RegistrationId, int UserId, int workCode)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetDocumentListByCollegeID(RegistrationId, UserId, workCode);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetSubjectName(int degreeId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetSubjectName(degreeId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        // for Maha IT
        //public string UploadFilesToBlob(FileUploadEntity objFileUpload, Dictionary<int, string> dicContainers)
        //{
        //    string result = "";
        //    try
        //    {
        //        result = objData.UploadFilesToBlob(objFileUpload, dicContainers);
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return result;

        //}

        public DataSet GerPinCodeSearch(string pinCode)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = objData.GerPinCodeSearch(pinCode);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return ds;
        }

        public DataSet GerPinCodeSearchByTaluka(string pinCode, string talukaCode)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = objData.GerPinCodeSearchByTaluka(pinCode, talukaCode);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return ds;
        }
        public List<DropDownEntity> GetOrganizationTypeByApplType(int applicationType)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetOrganizationType(applicationType);
                //return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetTaxonomyDropDownItems(string objectName, string property)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.TaxonomyDropDownItems(objectName, property);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public BankDetail GetBankDetails(string IfscCode)
        {
            return DT2EBank(objData.GetBankDetails(IfscCode));
        }

        public BankDetail DT2EBank(DataTable dtRecord)
        {
            BankDetail oBank = new BankDetail();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    oBank.BankName = Convert.ToString(dtRecord.Rows[0]["BankName"]);
                    oBank.BranchName = Convert.ToString(dtRecord.Rows[0]["BranchName"]);
                    oBank.AccNumberLength = Convert.ToString(dtRecord.Rows[0]["AccNumberLength"]);
                    oBank.MinAccLength = Convert.ToInt32(dtRecord.Rows[0]["MinAccLength"]);
                    oBank.MaxAccLength = Convert.ToInt32(dtRecord.Rows[0]["MaxAccLength"]);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oBank;
        }

        public List<DropDownEntity> GetTradeMachinesDropDownItems(int tradeId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTradeMachinesDropDownItems(tradeId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        public string GenerateOtp(string mobileNo, string msgBody)
        {

            string result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(msgBody))
                {
                    result = new ShortMessageServiceClient().SendMobileOTPMessage(mobileNo, msgBody);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }
        public string GenerateMailOtp(string emailId, int mailOtp)
        {

            string result = string.Empty;
            var otpformail = Convert.ToString(mailOtp);
            //SEND MAIL
            if (!string.IsNullOrEmpty(emailId))
            {
                try
                {
                    string path =
                    HttpContext.Current.Server.MapPath(
                     ConfigurationManager.AppSettings["MailOtpTempl"]);
                    string mailcontent = string.Empty;
                    //var RepresentativeFullName = "user name";
                    var RepresentativeRegistno = otpformail;

                    List<string> objMailToAddresses = new List<string>();
                    List<string> objCarbonCopyAddresses = new List<string>();
                    List<string> objBroadcastCarbonCopyAddresses = new List<string>();

                    objMailToAddresses.Add(emailId);
                    EmailContent objEmailContent = new EmailContent();

                    objEmailContent.MailToAddresses = objMailToAddresses;
                    objEmailContent.CarbonCopyAddresses = objCarbonCopyAddresses;
                    objEmailContent.BroadcastCarbonCopyAddresses = objBroadcastCarbonCopyAddresses;
                    objEmailContent.Subject = "Email verification";
                    objEmailContent.MailPriority = System.Net.Mail.MailPriority.Normal;
                    mailcontent = File.ReadAllText(path);
                    mailcontent = mailcontent.Replace("@@otp@@", otpformail);
                    //.Replace("@@RegistrationNumber@@", RepresentativeRegistno);
                    //.Replace("@@Password@@", candPassword);
                    objEmailContent.Body = mailcontent;


                    objEmailContent.Attachments = null;


                    Utility.Helper.SendMailMessage(objEmailContent);
                    //string UserName = objauthrdReptve.FirstName + ' ' + objauthrdReptve.LastName;
                    //string MobileNumber = objauthrdReptve.MobileNumber;
                    //string RegNumber = Convert.ToString(dt.Rows[0]["UserName"]);
                    //var strPassword = AesCipher.Decrypt(objUser.Password, objUser.PasswordPhrase);
                    //SendConfirmRegSMS(MobileNumber, UserName, strPassword, RegNumber);
                }
                catch (Exception Ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                }

            }
            return result;
        }

        public bool GetApplicationSubmissionStatus(int registarionId)
        {
            return objData.GetApplicationSubmissionStatus(registarionId);
        }

        public List<DropDownEntity> GetCourseApproverDDLItems(int applicationTypeId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetCourseApproverDDLItems(applicationTypeId);
                //return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public ApplicationSubmissionStatusEntity GetApplicationSubmittedSatus(int registarionId)
        {
            ApplicationSubmissionStatusEntity entity = new ApplicationSubmissionStatusEntity();
            var dt = objData.GetApplicationSubmittedSatus(registarionId);
            foreach (DataRow row in dt.Rows)
            {
                entity.AuthorizedRepSubmissionStatus = Convert.ToBoolean(row["AuthorizedRepSubmissionStatus"]);
                entity.BuildingSubmissionStatus = Convert.ToBoolean(row["BuildingSubmissionStatus"]);
                entity.ChairmanDetailsSubmissionStatus = Convert.ToBoolean(row["ChairmanDetailsSubmissionStatus"]);
                entity.CommonFacilitySubmissionStatus = Convert.ToBoolean(row["CommonFacilitySubmissionStatus"]);
                entity.DeclarationSubmissionStatus = Convert.ToBoolean(row["DeclarationSubmissionStatus"]);
                entity.FeesSubmissionStatus = Convert.ToBoolean(row["FeesSubmissionStatus"]);
                entity.FundsAvltyandSubmissionStatus = Convert.ToBoolean(row["FundsAvltyandSubmissionStatus"]);
                entity.InfrastructureSubmissionStatus = Convert.ToBoolean(row["InfrastructureSubmissionStatus"]);
                entity.OtherInfrastructureSubmissionStatus = Convert.ToBoolean(row["OtherInfrastructureSubmissionStatus"]);
                entity.PowerDetailsSubmissionStatus = Convert.ToBoolean(row["PowerDetailsSubmissionStatus"]);
                entity.PromotingOrganSubmissionStatus = Convert.ToBoolean(row["PromotingOrganSubmissionStatus"]);
                entity.ProposedInstSubmissionStatus = Convert.ToBoolean(row["ProposedInstSubmissionStatus"]);
                entity.ProposedTradeSubmissionStatus = Convert.ToBoolean(row["ProposedTradeSubmissionStatus"]);
                entity.TrusteeDetailsSubmissionStatus = Convert.ToBoolean(row["TrusteeDetailsSubmissionStatus"]);
                entity.WorkshopSubmissionStatus = Convert.ToBoolean(row["WorkshopSubmissionStatus"]);
                entity.LandDetailsSubmissionStatus = Convert.ToBoolean(row["LandDetailsSubmissionStatus"]);
                entity.PastPerformanceSubmissionStatus = Convert.ToBoolean(row["PastPerformanceSubmissionStatus"]);
            }

            return entity;
        }
        public bool GetITIHeadDeclarationDetais(int registrationId)
        {
            var dt = objData.GetITIHeadDeclarationDetais(registrationId);
            if (dt.Rows.Count == 0)
                return true;
            else
            {
                var admissionDate = Convert.ToString(dt.Rows[0]["UndertakingAdmissionPrintDate"]);
                if (!String.IsNullOrEmpty(admissionDate))
                    //As per dicussion with Shuanak, Previous logic was "Return False", Due to that user was not able to submit, but Now I hv make tht as following
                    //return true;
                    //As per dicussion with Shuanak, reverting back to original logic.
                    return false;
            }

            return true;
        }

        public IList<CourseApproverEntity> GetCourseApproverByApplicationTypeId(int applicationTypeId)
        {
            List<CourseApproverEntity> lstEntity = new List<CourseApproverEntity>();
            DataTable dt = objData.GetCourseApproverByApplicationTypeId(applicationTypeId);
            CourseApproverEntity entity;
            foreach (DataRow row in dt.Rows)
            {
                entity = new CourseApproverEntity();
                entity.Id = Convert.ToInt32(row["Id"]);
                entity.CourseApprover = Convert.ToString(row["CourseApprover"]);
                entity.ShortName = Convert.ToString(row["ShortName"]);
                lstEntity.Add(entity);
            }

            return lstEntity;
        }
        //public string GetApplicationTypeForReg(int applicationTypeId)
        //{
        //    Repository _repository = new Repository();
        //    string ApplicationType = "";
        //    try
        //    {
        //        var AppTypedt = _repository.GetApplicantType(applicationTypeId);
        //        if (AppTypedt.Rows.Count > 0)
        //        {
        //            ApplicationType = Convert.ToString(AppTypedt.Rows[0]["ApplicationType"]);
        //        }
        //        //return objList;
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return ApplicationType;
        //}

        public List<DropDownEntity> GetTradeTypes()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTradeTypes();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetTrades(int applicationTypeId, int tradeTypeId, int tradeSectorId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTrades(applicationTypeId, tradeTypeId, tradeSectorId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetRegionTypes()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetRegionTypes();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetApplicationStatusTypes()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetApplicationStatusTypes();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetInspectionFeesType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetInspectionFeesType();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetTradeSectorsByAppType(string applicationTypeName)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTradeSectorsByAppType(applicationTypeName);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        public List<DropDownEntity> GetInspectionSections()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetInspectionSections();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetInspectionStatus()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetInspectionStatus();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        public List<DropDownEntity> GetInspectionAgencyCommitte(int agencyId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetInspectionAgencyCommitte(agencyId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        //public List<InspectionRegistrationDetails> GetInspectionRegistrations(string adminType)
        //{
        //    List<InspectionRegistrationDetails> objList = new List<InspectionRegistrationDetails>();
        //    try
        //    {
        //        DataTable dt = objData.GetInspectionRegistrations(adminType);
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                InspectionRegistrationDetails objRegistration = new InspectionRegistrationDetails();
        //                objRegistration.ApplicationType = Convert.ToString(row["Applicationtype"]);
        //                //RegistrationNumber         StateName     SubDistrictName           
        //                objRegistration.RegistrationNumber = Convert.ToString(row["Applicationtype"]);
        //                objRegistration.NameofInstitute = Convert.ToString(row["InstituteName"]);
        //                objRegistration.Address = Convert.ToString(row["Address"]);
        //                objRegistration.PinCode = Convert.ToString(row["Pincode"]);
        //                objRegistration.PhoneNumber = Convert.ToString(row["MobileNumber"]);
        //                objRegistration.District = Convert.ToString(row["DistrictName"]);
        //                if (row["FromDate"] != DBNull.Value)
        //                    objRegistration.FromDate = Convert.ToDateTime(row["FromDate"]);
        //                if (row["ToDate"] != DBNull.Value)
        //                    objRegistration.ToDate = Convert.ToDateTime(row["ToDate"]);
        //                objRegistration.City = Convert.ToString(row["CityName"]);
        //                objRegistration.EmailId = Convert.ToString(row["EmailID"]);
        //                objRegistration.Region = Convert.ToString(row["RegionName"]);
        //                objRegistration.RegistrationId = Convert.ToInt32(row["RegistrationId"]);
        //                objList.Add(objRegistration);
        //            }

        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return objList;
        //}
        //public List<InspectionAgencyDetails> GetInspectionAgencies(string adminType)
        //{
        //    List<InspectionAgencyDetails> objList = new List<InspectionAgencyDetails>();
        //    try
        //    {
        //        DataTable dt = objData.GetInspectionAgencies(adminType);
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                InspectionAgencyDetails objAgency = new InspectionAgencyDetails();
        //                objAgency.UserName = Convert.ToString(row["AgencyUserName"]);
        //                objAgency.Name = Convert.ToString(row["AgencyName"]);
        //                //RegistrationNumber         StateName     SubDistrictName           
        //                objAgency.Address = Convert.ToString(row["Address"]);
        //                objAgency.PinCode = Convert.ToString(row["Pincode"]);
        //                objAgency.PhoneNumber = Convert.ToString(row["MobileNumber"]);
        //                objAgency.District = Convert.ToString(row["DistrictName"]);
        //                objAgency.City = Convert.ToString(row["CityName"]);
        //                objAgency.EmailId = Convert.ToString(row["EmailID"]);
        //                objAgency.Region = Convert.ToString(row["RegionName"]);
        //                objAgency.UserId = Convert.ToInt32(row["UserId"]);
        //                objList.Add(objAgency);
        //            }
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return objList;
        //}
        public int ResetTradeRelatedDetails(int regsitrationId)
        {
            int result = 0;
            try
            {
                result = objData.ResetTradeRelatedDetails(regsitrationId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DropDownEntity> GetTradeSectors()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTradeSectors();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public UserEntity GetUserDetails(string registrationNumber)
        {
            var entity = new UserEntity();
            try
            {
                DataTable dt = objData.GetUserDetails(registrationNumber);
                if (dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    entity.UserId = dr.IsNull("UserID") ? 0 : Convert.ToInt32(dt.Rows[0]["UserID"]);
                    entity.Password = dr.IsNull("Password") ? string.Empty : Convert.ToString(dt.Rows[0]["Password"]);
                    entity.PasswordPhrase = dr.IsNull("PasswordPhrase") ? string.Empty : Convert.ToString(dt.Rows[0]["PasswordPhrase"]);
                    entity.ObjectType = dr.IsNull("ObjectType") ? String.Empty : Convert.ToString(dt.Rows[0]["ObjectType"]);
                    entity.ObjectId = dr.IsNull("ObjectID") ? 0 : Convert.ToInt32(dt.Rows[0]["ObjectID"]);
                    entity.RegistrationID = dr.IsNull("RegistrationID") ? 0 : Convert.ToInt32(dt.Rows[0]["RegistrationID"]);
                    entity.ProposedInstituteId = dr.IsNull("ProposedInstituteId") ? 0 : Convert.ToInt32(dt.Rows[0]["ProposedInstituteId"]);
                    entity.ApplicationStatus = Convert.ToString(dt.Rows[0]["ApplicationStatus"]);

                    entity.FirstName = dr.IsNull("FirstName") ? "" : Convert.ToString(dt.Rows[0]["FirstName"]);
                    entity.LastName = dr.IsNull("LastName") ? "" : Convert.ToString(dt.Rows[0]["LastName"]);

                    entity.ApplicationType = dr.IsNull("ApplicationType") ? "" : Convert.ToString(dt.Rows[0]["ApplicationType"]);
                    entity.ApplicationTypeId = dr.IsNull("ApplicationTypeId") ? 0 : Convert.ToInt32(dt.Rows[0]["ApplicationTypeId"]);
                    entity.AppCode = dr.IsNull("AppCode") ? "" : Convert.ToString(dt.Rows[0]["AppCode"]);
                    entity.MobileNumber = Convert.ToString(dt.Rows[0]["MobileNumber"]);
                    entity.IsApplicationEditable = Convert.ToBoolean(dt.Rows[0]["IsApplicationEditable"]);
                    try
                    {
                        entity.isPasswordChanged = dr.IsNull("IsPasswordChanged") ? true : Convert.ToBoolean(dt.Rows[0]["IsPasswordChanged"]);
                    }
                    catch
                    {
                        entity.isPasswordChanged = true;
                    }
                    try
                    {
                        entity.ReferenceVtpNumber = dr.IsNull("ReferenceVTPNumber") ? "" : Convert.ToString(dt.Rows[0]["ReferenceVTPNumber"]);
                    }
                    catch
                    {
                        entity.ReferenceVtpNumber = String.Empty;
                    }
                    try
                    {
                        entity.WorkItemId = dr.IsNull("WorkItemId") ? 0 : Convert.ToInt32(dt.Rows[0]["WorkItemId"]);

                    }
                    catch
                    {
                        entity.WorkItemId = 0;
                    }
                    try
                    {
                        entity.IsAgencyRejected = Convert.ToBoolean(dt.Rows[0]["isAgencyRejected"]);

                    }
                    catch
                    {
                        entity.IsAgencyRejected = false;
                    }
                    try
                    {
                        entity.WorkflowInstanceId = Convert.ToInt32(dt.Rows[0]["WorkflowInstanceId"]);

                    }
                    catch
                    {
                        entity.WorkflowInstanceId = 0;
                    }
                    try
                    {
                        entity.IsGrievanceRaised = Convert.ToBoolean(dt.Rows[0]["IsGrievanceRaised"]);
                    }
                    catch
                    {
                        entity.IsGrievanceRaised = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return entity;
        }
        //public string GenerateMailOtp(string emailId, int mailOtp)
        //{
        //    try
        //    {
        //        var fromAddress = new MailAddress("narendrav529@gmail.com", "narendra");
        //        var toAddress = new MailAddress(emailId, "user x");
        //        const string fromPassword = "PASSWORD";
        //        const string subject = "Subject";
        //        const string body = "Body";

        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
        //            Timeout = 20000
        //        };
        //        using (var message = new MailMessage(fromAddress, toAddress)
        //        {
        //            Subject = subject,
        //            Body = body
        //        })
        //        {
        //            smtp.Send(message);
        //        }
        //        return "success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Failure";
        //    }

        //}
        //public List<DropDownEntity> GetTitle()
        //{
        //    List<DropDownEntity> objList = new List<DropDownEntity>();
        //    try
        //    {
        //        objList = objData.GetTitle();
        //        return objList;
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return objList;
        //}

        public ChangePasswodEntity GetPasswordByUsername(string Username)
        {
            ChangePasswodEntity objpassword = new ChangePasswodEntity();
            //string Password = "";
            //string PasswordPhrase = "";
            try
            {
                var PsawTypedt = objData.GetPasswordByUsername(Username);
                if (PsawTypedt.Rows.Count > 0)
                {
                    objpassword.Password = Convert.ToString(PsawTypedt.Rows[0]["Password"]);
                    objpassword.passPhrase = Convert.ToString(PsawTypedt.Rows[0]["PasswordPhrase"]);

                }
                //return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objpassword;
        }
        public string GetApplicationTypeByRegId(int regId)
        {
            string ApplicationType = null;
            //try
            //{
            DataTable dt = objData.GetApplicationTypeByRegId(regId);
            foreach (DataRow row in dt.Rows)
            {
                ApplicationType = Convert.ToString(row["Code"]);
            }
            //catch (Exception Ex)
            //{
            //    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            //}
            return ApplicationType;
        }

        public List<DropDownEntity> GetTradesForAdminBySector(int applicationTypeId, string tradeSectorId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetTradesForAdminBySector(applicationTypeId, tradeSectorId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetAffiliationType()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetAffiliationType();
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        //public FeeCalculationEntity UpdateInspectionFeeDetail(int inspectionFeeId)
        //{
        //    var entity = new FeeCalculationEntity();
        //    var dt = new DataTable();
        //    try
        //    {
        //        dt = objData.UpdateInspectionFeeDetail(inspectionFeeId);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                //var entity = new FeeCalculationEntity();
        //                var dr = dt.Rows[i];
        //                entity.StrApplicationType = Convert.ToString(dt.Rows[i]["ApplicationType"]);
        //                entity.StrAffiliationType = Convert.ToString(dt.Rows[i]["AffiliationType"]);
        //                entity.TradesTypeStr = Convert.ToString(dt.Rows[i]["TradeName"]);
        //                entity.SectorTypeStr = Convert.ToString(dt.Rows[i]["SectorName"]);
        //                entity.StrInspectionFeesType = Convert.ToString(dt.Rows[i]["InspectionFeesType"]);
        //                entity.InspectionFeesAmount = Convert.ToDecimal(dt.Rows[i]["InspectionFeesAmount"]);
        //                entity.ApplicationTypeID = Convert.ToInt32(dt.Rows[i]["ApplicationTypeId"]);
        //                entity.InspectionFeeTypeId = Convert.ToInt32(dt.Rows[i]["InspectionFeesTypeId"]);
        //                entity.AffiliationTypeID = Convert.ToInt32(dt.Rows[i]["AffilliationTypeId"]);
        //                entity.SectorTypeId = Convert.ToInt32(dt.Rows[i]["TradeSectorId"]);
        //                entity.TradesTypeId = Convert.ToInt32(dt.Rows[i]["TradeId"]);

        //                if (dt.Rows[i]["CreatedOn"] != DBNull.Value)
        //                    entity.DateofInspectionAmount = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
        //                if (dt.Rows[i]["EndDate"] != DBNull.Value)
        //                    entity.EndDate = Convert.ToDateTime(dt.Rows[i]["EndDate"]);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return entity;
        //}

        public int DeleteInspectionFeeId(int inspectionFeeId)
        {
            int returnValue = 0;
            try
            {
                returnValue = objData.DeleteInspectionFeeId(inspectionFeeId);
            }
            catch (Exception ex)
            {
                DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return returnValue;
        }
        public List<DropDownEntity> GetAppointmentTypes()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetAppointmentTypes();
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetCourse()
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetCourse(0);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }


        public List<DropDownEntity> GetProgram(int facultyId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetProgram(facultyId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        //Added By:Akshay
        public List<DropDownEntity> GetCourse(int facultyId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetCourse(facultyId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetSubjectsList(int courseId)
        {

            List<DropDownEntity> list = new List<DropDownEntity>();
            DropDownEntity entity;

            try
            {
                DataTable dt = objData.GetSubjectsList(courseId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("SubjectId"))
                            entity.DataValueField = Convert.ToInt32(dr["SubjectId"] ?? 0);

                        if (dt.Columns.Contains("Subject"))
                            entity.DataTextField = dr["Subject"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        list.Add(entity);
                    }

                }
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return list;

        }

        public List<DropDownEntity> GetCourseList(int programId, int courseType, int registationId)
        {

            List<DropDownEntity> list = new List<DropDownEntity>();
            DropDownEntity entity;

            try
            {
                DataTable dt = objData.GetCourseList(programId, courseType, registationId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("CourseId"))
                            entity.DataValueField = Convert.ToInt32(dr["CourseId"] ?? 0);

                        if (dt.Columns.Contains("Course"))
                            entity.DataTextField = dr["Course"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        list.Add(entity);
                    }

                }
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return list;

        }



        //Added By:Akshay End

        //Add By Ganesh  Jha
        public List<DropDownEntity> GetConcatenatedFacultyCourseMapping(int registrationId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetConcatenatedFacultyCourseMapping(registrationId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }
        public List<DropDownEntity> GetAllBuilidingsByRegId(int registrationId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetAllBuilidingsByRegId(registrationId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMasterTableList(TableName, DataValueField, DataTextField);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMasterTableStringList(TableName, DataValueField, DataTextField);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMasterTableList(TableName, DataValueField, DataTextField, Property);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property, string columnName)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMasterTableList(TableName, DataValueField, DataTextField, Property, columnName);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField, string Property, string columnName, string ListType = null)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetMasterTableStringList(TableName, DataValueField, DataTextField, Property, columnName, ListType);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetDropDownList(TableName, ListType, DataValueField, DataTextField, Param, ColumnName, Others, orderBy, orderByColumn, Param1, ColumnName1, Param2, ColumnName2, Param3, ColumnName3, Param4, ColumnName4);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }


        public List<DropDownEntity> GetExistingCourseList(int RegistrationId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetExistingCourseList(RegistrationId);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<GeographyEntity> GetStateDetail(string countryId)
        {
            List<GeographyEntity> result = new List<GeographyEntity>();
            try
            {
                result = DT2State(objData.GetStateDetail(countryId));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<GeographyEntity> DT2State(DataTable dtRecord)
        {
            List<GeographyEntity> oState = new List<GeographyEntity>();
            GeographyEntity gEntity;
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRecord.Rows)
                    {
                        gEntity = new GeographyEntity();
                        gEntity.Id = dr.IsNull("Id") ? -1 : Convert.ToInt32(dr["Id"]);
                        gEntity.State = dr.IsNull("State") ? "Select" : Convert.ToString(dr["State"]);

                        oState.Add(gEntity);
                    }
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oState;
        }

        //public byte[] GetFileByteArray(string filePath)
        //{
        //    byte[] by = { };
        //    try
        //    {
        //        by = objData.GetFileByteArray(filePath);
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return by;
        //}

        public List<TableRecordsEntity> GetTableRecordsList(string ListType, List<string> stringColumnNames, List<string> stringColumnValues, List<int> intValueParams)
        {
            List<TableRecordsEntity> recordsList = new List<TableRecordsEntity>();


            return recordsList;
        }

        public List<TableRecordsEntity> GetTableDataList(string ListType, string TableName, string Column1 = null, string Param1 = null, string Column2 = null, string Param2 = null, string Column3 = null, string Param3 = null, string Column4 = null, string Param4 = null, string Column5 = null, string Param5 = null, string RequiredColumn1 = null, string RequiredColumn2 = null, string RequiredColumn3 = null, string RequiredColumn4 = null, string RequiredColumn5 = null, string RequiredColumn6 = null, string RequiredColumn7 = null, string RequiredColumn8 = null, string RequiredColumn9 = null, string RequiredColumn10 = null)
        {
            List<TableRecordsEntity> dataList = new List<TableRecordsEntity>();
            try
            {
                dataList = objData.GetTableDataList(ListType, TableName, Column1, Param1, Column2, Param2, Column3, Param3, Column4, Param4, Column5, Param5, RequiredColumn1, RequiredColumn2, RequiredColumn3, RequiredColumn4, RequiredColumn5, RequiredColumn6, RequiredColumn7, RequiredColumn8, RequiredColumn9, RequiredColumn10);


                return dataList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return dataList;
        }

        public List<TableRecordsEntity> GetTableDataList(TableRecordsEntity objTbl)
        {
            List<TableRecordsEntity> dataList = new List<TableRecordsEntity>();
            try
            {
                //dataList = objData.GetTableDataList(objTbl);
                return dataList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return dataList;
        }

        public SingleColumnEntity GetSingleColumnValues(string TableName, string DataValueField1, string ColumnName1, string Param1, string DataValueField2 = null, string ColumnName2 = null, string Param2 = null, string DataValueField3 = null, string ColumnName3 = null, string Param3 = null, string DataValueField4 = null, string ColumnName4 = null, string Param4 = null, string DataValueField5 = null, string DataValueField6 = null, string DataValueField7 = null, string DataValueField8 = null)
        {
            List<SingleColumnEntity> dataList = new List<SingleColumnEntity>();
            SingleColumnEntity objSingle = new SingleColumnEntity();
            try
            {
                dataList = objData.GetSingleColumnValues(TableName, DataValueField1, ColumnName1, Param1, DataValueField2, ColumnName2, Param2, DataValueField3, ColumnName3, Param3, DataValueField4, ColumnName4, Param4, DataValueField5, DataValueField6, DataValueField7, DataValueField8);

                if (dataList.Count > 0)
                    objSingle = dataList[0];

                return objSingle;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return objSingle;
        }

        //public List<TableRecordsEntity> GetTableRecordsList(string ListType, string TableName, string DataParamField1, string DataParamValueField1, string DataColumnTextField1, string DataColumnValueField1, string DataParamField2 = null, string DataParamValueField2 = null, string DataColumnTextField2 = null, string DataColumnValueField2 = null, string DataParamField3 = null, string DataParamValueField3 = null, string DataColumnTextField3 = null, string DataColumnValueField3 = null, string DataParamField4 = null, string DataParamValueField4 = null, string DataColumnTextField4 = null, string DataColumnValueField4 = null, string DataParamField5 = null, string DataParamValueField5 = null, string DataColumnTextField5 = null, string DataColumnValueField5 = null)
        //{
        //    List<TableRecordsEntity> dataList = new List<TableRecordsEntity>();
        //    try
        //    {
        //        dataList = objData.GetTableRecordsList(ListType, TableName, DataParamField1, DataParamValueField1, DataColumnTextField1, DataColumnValueField1, DataParamField2, DataParamValueField2, DataColumnTextField2, DataColumnValueField2, DataParamField3, DataParamValueField3, DataColumnTextField3, DataColumnValueField3, DataParamField4, DataParamValueField4, DataColumnTextField4, DataColumnValueField4, DataParamField5, DataParamValueField5, DataColumnTextField5, DataColumnValueField5);


        //        return dataList;
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }

        //    return dataList;
        //}

        public string DeleteFromTable(string TableName, string ColumnName1, string Param1, string ColumnName2 = null, string Param2 = null, string ColumnName3 = null, string Param3 = null)
        {
            string result = string.Empty;
            try
            {
                result = objData.DeleteFromTable(TableName, ColumnName1, Param1, ColumnName2, Param2, ColumnName3, Param3);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public CommonDetailsEntity GetCommonSettings()
        {
            CommonDetailsEntity entity = new CommonDetailsEntity();
            try
            {
                var dt = objData.GetCommonSettings();

                List<CommonDetailsEntity> listObj = new List<CommonDetailsEntity>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["Property"].ToString() == "CompanyName") && dr["Details"].ToString() != "")
                            entity.CompanyName = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Logo") && dr["Details"].ToString() != "")
                            entity.Logo = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Address1") && dr["Details"].ToString() != "")
                            entity.Address1 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Address2") && dr["Details"].ToString() != "")
                            entity.Address2 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Address3") && dr["Details"].ToString() != "")
                            entity.Address3 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Phone") && dr["Details"].ToString() != "")
                            entity.Phone = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Mobile") && dr["Details"].ToString() != "")
                            entity.Mobile = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Website") && dr["Details"].ToString() != "")
                            entity.Website = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Email1") && dr["Details"].ToString() != "")
                            entity.Email1 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Email2") && dr["Details"].ToString() != "")
                            entity.Email2 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Email3") && dr["Details"].ToString() != "")
                            entity.Email3 = dr["Details"].ToString();
                        else if ((dr["Property"].ToString() == "Notation") && dr["Details"].ToString() != "")
                            entity.Notation = dr["Details"].ToString();

                    }
                }

            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return entity;
        }

        public List<DropDownEntity> GetSONoQuoteNoList(string EndUse, string quoteType)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetSONoQuoteNoList(EndUse, quoteType);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public DataTable GetDataTableForDocument(string ListType, string TableName, string[] DataColumn, string[] DataParam, string[] RequiredColumn)
        {
            DataTable dt = objData.GetDataTableForDocument(ListType, TableName, DataColumn, DataParam, RequiredColumn);
            return dt;
        }

        public string SaveBulkEntryDetails(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = objData.SaveBulkEntryDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DropDownEntity> SaveNewItemInDdl(AddDdlEntity entity)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();
            try
            {
                ddl = objData.SaveNewItemInDdl(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return ddl;
        }

        public string SaveTableData(InsertTableData iData)
        {
            string result = "";
            try
            {
                result = objData.SaveTableData(iData);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DropDownEntity> GetDateDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = objData.GetDateDropDownList(TableName, ListType, DataValueField, DataTextField, Param, ColumnName, Others, orderBy, orderByColumn, Param1, ColumnName1, Param2, ColumnName2, Param3, ColumnName3, Param4, ColumnName4);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }


    }
}
