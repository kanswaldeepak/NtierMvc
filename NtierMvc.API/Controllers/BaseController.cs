using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NtierMvc.Common;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.BusinessLogic.Worker;
using System.Data;
using NtierMvc.Model.Account;
using NtierMvc.Model.Application;
using NtierMvc.Model;

namespace NtierMvc.API.Controllers
{
    public class BaseController : ApiController
    {
        IBase _repository = new BaseWorker();

        [Route("api/Base/GetTitle")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTitle()
        {
            return Ok(_repository.GetTitle());
        }

        [Route("api/Base/GetApplicationType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetApplicationType()
        {
            return Ok(_repository.GetApplicationType());
        }

        [Route("api/Base/GetApplicationTypesForVTIAdmin")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetApplicationTypesForVTIAdmin()
        {
            return Ok(_repository.GetApplicationTypesForVTIAdmin());
        }

        [Route("api/Base/GetApplicationTypesForITIAdmin")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetApplicationTypesForITIAdmin()
        {
            return Ok(_repository.GetApplicationTypesForITIAdmin());
        }

        [Route("api/Base/GetMSSDSPortal")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMSSDSPortal()
        {
            return Ok(_repository.GetMSSDSPortal());
        }

        // for MahaIT
        [HttpGet]
        [Route("api/Base/GetInstituteType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetInstituteType()
        {
            return Ok(_repository.GetInstituteType());
        }

        [HttpGet]
        [Route("api/Base/GetFacultyName")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetFacultyName()
        {
            return Ok(_repository.GetFacultyName());
        }


        [HttpGet]
        [Route("api/Base/GetAcademicYears")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetAcademicYears()
        {
            return Ok(_repository.GetAcademicYears());
        }
        
        [HttpGet]
        [Route("api/Base/GetDegreeName")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetDegreeName(int CourseId)
        {
            return Ok(_repository.GetDegreeName(CourseId));
        }

        [HttpGet]
        [Route("api/Base/GetGenderType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetGenderType()
        {
            return Ok(_repository.GetGenderType());
        }

        [HttpGet]
        [Route("api/Base/GetProposedInstituteType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetProposedInstituteType()
        {
            return Ok(_repository.GetProposedInstituteType());
        }

        [HttpGet]
        [Route("api/Base/GetMinorityCategoryType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMinorityCategoryType()
        {
            return Ok(_repository.GetMinorityCategoryType());
        }

        [HttpGet]
        [Route("api/Base/GetReligiousMinorityType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetReligiousMinorityType()
        {
            return Ok(_repository.GetReligiousMinorityType());
        }

        [HttpGet]
        [Route("api/Base/GetCourseLevelType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetCourseLevelType()
        {
            return Ok(_repository.GetCourseLevelType());
        }

        [HttpGet]
        [Route("api/Base/GetCourseType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetCourseType()
        {
            return Ok(_repository.GetCourseType());
        }

        [HttpGet]
        [Route("api/Base/GetSubjectName")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetSubjectName(int degreeId)
        {
            return Ok(_repository.GetSubjectName(degreeId));
        }

        [HttpGet]
        [ResponseType(typeof(List<DropDownEntity>))]
        [Route("api/Base/GetDocumentListByCollegeIDApi")]
        public IHttpActionResult GetDocumentListByCollegeIDApi(int RegistrationId, int UserId , int workCode)
        {
            return Ok(_repository.GetDocumentListByCollegeID(RegistrationId, UserId , workCode));
        }

        // for MahaIT
        [HttpGet]
        [Route("api/Base/GetOrganizationType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetOrganizationType()
        {
            return Ok(_repository.GetOrganizationType());
        }

        [HttpGet]
        [Route("api/Base/GerPinCodeSearch")]
        [ResponseType(typeof(DataSet))]
        public IHttpActionResult GerPinCodeSearch(string pinCode)
        {
            return Ok(_repository.GerPinCodeSearch(pinCode));
        }

        [HttpGet]
        [Route("api/Base/GerPinCodeSearchByTaluka")]
        [ResponseType(typeof(DataSet))]
        public IHttpActionResult GerPinCodeSearchByTaluka(string pinCode, string TalukaCode)
        {
            return Ok(_repository.GerPinCodeSearchByTaluka(pinCode, TalukaCode));
        }

        [Route("api/Base/GetOrganizationTypeByApplType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetOrganizationTypeByApplType(int applicationType)
        {
            return Ok(_repository.GetOrganizationTypeByApplType(applicationType));
        }

        [Route("api/Base/GetTaxonomyDropDownItems")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTaxonomyDropDownItems(string objectName, string property)
        {
            return Ok(_repository.GetTaxonomyDropDownItems(objectName, property));
        }

        [HttpGet]
        [Route("api/Base/GetBankDetails")]
        // [ResponseType(typeof(DataTable))]
        public IHttpActionResult GetBankDetails(string IfscCode)
        {
            return Ok(_repository.GetBankDetails(IfscCode));
        }

        [HttpGet]
        [Route("api/Base/GetTradeMachinesDropDownItems")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTradeMachinesDropDownItems(int tradeId)
        {
            return Ok(_repository.GetTradeMachinesDropDownItems(tradeId));
        }

        [HttpGet]
        [Route("api/Base/GenerateOtp")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GenerateOtp(string mobileNo, string msgBody)
        {
            return Ok(_repository.GenerateOtp(mobileNo, msgBody));
        }

        [HttpGet]
        [Route("api/Base/GenerateMailOtp")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GenerateMailOtp(string emailId, int mailOtp)
        {
            return Ok(_repository.GenerateMailOtp(emailId, mailOtp));
        }

        [HttpGet]
        [Route("api/Base/GetApplicationSubmissionStatus")]
        public IHttpActionResult GetApplicationSubmissionStatus(int registrationId)
        {
            return Ok(_repository.GetApplicationSubmissionStatus(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetApplicationSubmittedSatus")]
        public IHttpActionResult GetApplicationSubmittedSatus(int registrationId)
        {
            return Ok(_repository.GetApplicationSubmittedSatus(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetITIHeadDeclarationDetais")]
        public IHttpActionResult GetITIHeadDeclarationDetais(int registrationId)
        {
            return Ok(_repository.GetITIHeadDeclarationDetais(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetCourseApproverDDLItems")]
        public IHttpActionResult GetCourseApproverDDLItems(int applicationTypeId)
        {
            return Ok(_repository.GetCourseApproverDDLItems(applicationTypeId));
        }
        //[Route("api/Base/GetApplicationTypeForReg")]
        //[ResponseType(typeof(string))]
        //public IHttpActionResult GetApplicationTypeForReg(int applicationTypeId)
        //{
        //    return Ok(_repository.GetApplicationTypeForReg(applicationTypeId));
        //}

        [HttpGet]
        [Route("api/Base/GetTradeTypes")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTradeTypes()
        {
            return Ok(_repository.GetTradeTypes());
        }

        [HttpGet]
        [Route("api/Base/GetAllTrades")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTrades(int applicationTypeId, int tradeTypeId, int tradeSectorId)
        {
            return Ok(_repository.GetTrades(applicationTypeId,tradeTypeId,tradeSectorId));
        }

        [HttpGet]
        [Route("api/Base/GetRegionTypes")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetRegionTypes()
        {
            return Ok(_repository.GetRegionTypes());
        }

        [HttpGet]
        [Route("api/Base/GetApplicationStatusTypes")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetApplicationStatusTypes()
        {
            return Ok(_repository.GetApplicationStatusTypes());
        }

        [HttpGet]
        [Route("api/Base/GetInspectionFeesType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetInspectionFeesType()
        {
            return Ok(_repository.GetInspectionFeesType());
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        [Route("api/Base/GetTradeSectorsByAppType")]
        public IHttpActionResult GetTradeSectorsByAppType(string applicationTypeName)
        {
            return Ok(_repository.GetTradeSectorsByAppType(applicationTypeName));
        }

        
        [Route("api/Base/GetInspectionSections")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetInspectionSections()
        {
            return Ok(_repository.GetInspectionSections());
        }

        [Route("api/Base/GetInspectionStatus")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetInspectionStatus()
        {
            return Ok(_repository.GetInspectionStatus());
        }

        [Route("api/Base/GetInspectionAgencyCommitte")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetInspectionAgencyCommitte(int agencyId)
        {
            return Ok(_repository.GetInspectionAgencyCommitte(agencyId));
        }
        
        [HttpGet]
        [Route("api/Base/ResetTradeRelatedDetails")]
        [ResponseType(typeof(int))]
        public IHttpActionResult ResetTradeRelatedDetails(int registrationId)
        {
            return Ok(_repository.ResetTradeRelatedDetails(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetTradeSectors")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTradeSectors()
        {
            return Ok(_repository.GetTradeSectors());
        }
        [HttpGet]
        [Route("api/Base/GetUserDetails")]
        [ResponseType(typeof(UserEntity))]
        public IHttpActionResult GetUserDetails(string registrationNumber)
        {
            return Ok(_repository.GetUserDetails(registrationNumber));
        }

        [Route("api/Base/GetPasswordByUsername")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetPasswordByUsername(string Username)
        {
            return Ok(_repository.GetPasswordByUsername(Username));
        }

        [HttpGet]
        [Route("api/Base/GetTradesForAdminBySector")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetTradesForAdminBySector(int applicationTypeId, string tradeSectorId)
        {
            return Ok(_repository.GetTradesForAdminBySector(applicationTypeId, tradeSectorId));
        }

        [HttpGet]
        [Route("api/Base/GetAffiliationType")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetAffiliationType()
        {
            return Ok(_repository.GetAffiliationType());
        }

        //[HttpGet]
        //[Route("api/Base/UpdateInspectionFeeDetail")]
        //[ResponseType(typeof(FeeCalculationEntity))]
        //public IHttpActionResult UpdateInspectionFeeDetail(int inspectionFeeId)
        //{
        //    return Ok(_repository.UpdateInspectionFeeDetail(inspectionFeeId));
        //}

        [HttpGet]
        [Route("api/Base/DeleteInspectionFeeId")]
        [ResponseType(typeof(int))]
        public IHttpActionResult DeleteInspectionFeeId(int inspectionFeeId)
        {
            var result = _repository.DeleteInspectionFeeId(inspectionFeeId);
            if (result > 0)
            {
                return Json(new { Status = "ok" });
            }
            else
            {
                return Json(new { Status = "error" });
            }
        }
        [Route("api/Base/GetAppointmentTypes")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetAppointmentTypes()
        {
            return Ok(_repository.GetAppointmentTypes());
        }
        [HttpGet]
        [Route("api/Base/GetApplicationTypeByRegId")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetApplicationTypeByRegId(int regId)
        {
            return Ok(_repository.GetApplicationTypeByRegId(regId));
        }

        
        [HttpGet]
        [Route("api/Base/GetProgram")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetProgram(int facultyId)
        {
            return Ok(_repository.GetProgram(facultyId));
        } 

        //Added By:Akshay
        [HttpGet]
        [Route("api/Base/GetCourse")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetCourse(int facultyId)
        {
            return Ok(_repository.GetCourse(facultyId));
        }

        [Route("api/Base/GetCoursesList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetCourseList(int programId,int courseType,int registrationId)
        {
            return Ok(_repository.GetCourseList(programId, courseType, registrationId));
        }

        //Added By:Akshay End

        //Added By Ganesh Jha
        [HttpGet]
        [Route("api/Base/GetConcatenatedFacultyCourseMapping")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetConcatenatedFacultyCourseMapping(int registrationId)
        {
            return Ok(_repository.GetConcatenatedFacultyCourseMapping(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetAllBuilidingsByRegId")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetAllBuilidingsByRegId(int registrationId)
        {
            return Ok(_repository.GetAllBuilidingsByRegId(registrationId));
        }

        [HttpGet]
        [Route("api/Base/GetSubjectsList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetSubjectsList(int courseId)
        {
            return Ok(_repository.GetSubjectsList(courseId));
        }

        [Route("api/Base/GetMasterTableList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMasterTableList(string TableName, string DataValueField, string DataTextField)
        {
            return Ok(_repository.GetMasterTableList(TableName, DataValueField, DataTextField));
        }

        [Route("api/Base/GetMasterTableStringList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMasterTableStringList(string TableName, string DataValueField, string DataTextField)
        {
            return Ok(_repository.GetMasterTableStringList(TableName, DataValueField, DataTextField));
        }

        [Route("api/Base/GetMasterTableList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property, string columnName)
        {
            return Ok(_repository.GetMasterTableList(TableName, DataValueField, DataTextField, Property, columnName));
        }

        [Route("api/Base/GetMasterTableStringList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetMasterTableStringList(string TableName, string DataValueField, string DataTextField, string Property, string columnName, string ListType=null)
        {
            return Ok(_repository.GetMasterTableStringList(TableName, DataValueField, DataTextField, Property, columnName, ListType));
        }

        [Route("api/Base/GetDropDownList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            return Ok(_repository.GetDropDownList(TableName, ListType, DataValueField, DataTextField, Param, ColumnName, Others, orderBy, orderByColumn, Param1, ColumnName1, Param2, ColumnName2, Param3, ColumnName3, Param4, ColumnName4));
        }

        [Route("api/Base/GetExistingCourseList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetExistingCourseList(int RegistrationId)
        {
            return Ok(_repository.GetExistingCourseList(RegistrationId));
        }

        [HttpGet]
        [Route("api/Base/GetStateDetail")]
        public IHttpActionResult GetStateDetail(string countryId)
        {
            return Ok(_repository.GetStateDetail(countryId));
        }

        [HttpGet]
        [Route("api/Base/GetTableRecordsList")]
        public IHttpActionResult GetTableRecordsList(string ListType, List<string> stringColumnNames, List<string> stringColumnValues, List<int> intValueParams)
        {
            return Ok(_repository.GetTableRecordsList(ListType, stringColumnNames, stringColumnValues, intValueParams));
        }

        [Route("api/Base/GetTableDataList")]
        [ResponseType(typeof(IEnumerable<TableRecordsEntity>))]
        public IHttpActionResult GetTableDataList(string ListType, string TableName, string Column1 = null, string Param1 = null, string Column2 = null, string Param2 = null, string Column3 = null, string Param3 = null, string Column4 = null, string Param4 = null, string Column5 = null, string Param5 = null, string RequiredColumn1 = null, string RequiredColumn2 = null, string RequiredColumn3 = null, string RequiredColumn4 = null, string RequiredColumn5 = null, string RequiredColumn6 = null, string RequiredColumn7 = null, string RequiredColumn8 = null, string RequiredColumn9 = null, string RequiredColumn10 = null)
        {
            return Ok(_repository.GetTableDataList( ListType,  TableName,  Column1 ,  Param1 ,  Column2 ,  Param2 ,  Column3 ,  Param3 ,  Column4 ,  Param4 ,  Column5 ,  Param5 ,  RequiredColumn1 ,  RequiredColumn2 ,  RequiredColumn3 ,  RequiredColumn4 ,  RequiredColumn5 ,  RequiredColumn6 ,  RequiredColumn7 ,  RequiredColumn8 ,  RequiredColumn9 ,  RequiredColumn10 ));
        }

        //[HttpPost]
        //[ResponseType(typeof(List<TableRecordsEntity>))]
        //[Route("api/Base/GetTableDataList")]
        //public IHttpActionResult GetTableDataList(TableRecordsEntity TblObj)
        //{
        //    return Ok(_repository.GetTableDataList(TblObj));
        //}

        //[Route("api/Base/GetFileByteArray")]
        //[ResponseType(typeof(byte[]))]
        //public IHttpActionResult GetFileByteArray(string filePath)
        //{
        //    return Ok(_repository.GetFileByteArray(filePath));
        //}

        //[ResponseType(typeof(IEnumerable<DropDownEntity>))]
        //[Route("api/Base/CreateDownloadDocument")]
        //public IHttpActionResult CreateDownloadDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        //{
        //    return Ok(_repository.CreateDownloadDocument(downloadTypeId, quoteTypeId, quoteNumberId));
        //}

        [Route("api/Base/GetSingleColumnValues")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetSingleColumnValues(string TableName, string DataValueField1, string ColumnName1, string Param1, string DataValueField2 = null, string ColumnName2 = null, string Param2 = null, string DataValueField3 = null,  string ColumnName3 = null, string Param3 = null, string DataValueField4 = null, string ColumnName4 = null, string Param4 = null, string DataValueField5 = null, string DataValueField6 = null, string DataValueField7 = null, string DataValueField8 = null)
        {
            return Ok(_repository.GetSingleColumnValues(TableName, DataValueField1, ColumnName1, Param1, DataValueField2 ,    ColumnName2 ,  Param2 ,  DataValueField3 ,  ColumnName3 ,  Param3 ,  DataValueField4 , ColumnName4 ,  Param4, DataValueField5, DataValueField6 , DataValueField7 , DataValueField8 ));
        }

        //[Route("api/Base/GetTableRecordsList")]
        //[ResponseType(typeof(string))]
        //public IHttpActionResult GetTableRecordsList(string ListType, string TableName, string DataParamField1, string DataParamValueField1, string DataColumnTextField1, string DataColumnValueField1, string DataParamField2 = null, string DataParamValueField2 = null, string DataColumnTextField2 = null, string DataColumnValueField2 = null, string DataParamField3 = null, string DataParamValueField3 = null, string DataColumnTextField3 = null, string DataColumnValueField3 = null, string DataParamField4 = null, string DataParamValueField4 = null, string DataColumnTextField4 = null, string DataColumnValueField4 = null, string DataParamField5 = null, string DataParamValueField5 = null, string DataColumnTextField5 = null, string DataColumnValueField5 = null)
        //{
        //    return Ok(_repository.GetTableRecordsList( ListType,  TableName,  DataParamField1,  DataParamValueField1,  DataColumnTextField1,  DataColumnValueField1,  DataParamField2,  DataParamValueField2,  DataColumnTextField2,  DataColumnValueField2,  DataParamField3,  DataParamValueField3,  DataColumnTextField3,  DataColumnValueField3,  DataParamField4,  DataParamValueField4,  DataColumnTextField4,  DataColumnValueField4,  DataParamField5,  DataParamValueField5,  DataColumnTextField5,  DataColumnValueField5));
        //}

        [HttpGet]
        [ResponseType(typeof(string))]
        [Route("api/Base/DeleteFromTable")]
        public IHttpActionResult DeleteFromTable(string TableName, string ColumnName1, string Param1, string ColumnName2 = null, string Param2 = null, string ColumnName3 = null, string Param3 = null)
        {
            return Ok(_repository.DeleteFromTable(TableName, ColumnName1, Param1, ColumnName2, Param2, ColumnName3, Param3));
        }

        [HttpGet]
        [Route("api/Base/GetCommonSettings")]
        public IHttpActionResult GetCommonSettings()
        {
            return Ok(_repository.GetCommonSettings());
        }

        [Route("api/Base/GetSONoQuoteNoList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetSONoQuoteNoList(string EndUse, string quoteType)
        {
            return Ok(_repository.GetSONoQuoteNoList(EndUse, quoteType));
        }

        [HttpGet]
        [Route("api/Base/GetDataTableForDocument")]
        public IHttpActionResult GetDataTableForDocument([FromBody] string ListType, string TableName, string[] DataColumn, string[] DataParam, string[] RequiredColumn)
        {
            return Ok(_repository.GetDataTableForDocument(ListType, TableName, DataColumn, DataParam, RequiredColumn));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/Base/SaveBulkEntryDetails")]
        public IHttpActionResult SaveBulkEntryDetails(BulkUploadEntity iEntity)
        {
            return Ok(_repository.SaveBulkEntryDetails(iEntity));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/Base/SaveNewItemInDdl")]
        public IHttpActionResult SaveNewItemInDdl(AddDdlEntity dEntity)
        {
            return Ok(_repository.SaveNewItemInDdl(dEntity));
        }

        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("api/Base/SaveTableData")]
        public IHttpActionResult SaveTableData(InsertTableData iData)
        {
            return Ok(_repository.SaveTableData(iData));
        }

        [Route("api/Base/GetDateDropDownList")]
        [ResponseType(typeof(IEnumerable<DropDownEntity>))]
        public IHttpActionResult GetDateDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            return Ok(_repository.GetDateDropDownList(TableName, ListType, DataValueField, DataTextField, Param, ColumnName, Others, orderBy, orderByColumn, Param1, ColumnName1, Param2, ColumnName2, Param3, ColumnName3, Param4, ColumnName4));
        }

    }
}
