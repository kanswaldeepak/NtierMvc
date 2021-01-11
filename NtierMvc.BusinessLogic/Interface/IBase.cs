using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;
using System.Data;
using NtierMvc.Model.Application;
using NtierMvc.Model.Account;
using NtierMvc.Model;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IBase
    {
        List<DropDownEntity> GetTitle();
        List<DropDownEntity> GetApplicationType();
        List<DropDownEntity> GetApplicationTypesForVTIAdmin();
        List<DropDownEntity> GetApplicationTypesForITIAdmin();
        
        List<DropDownEntity> GetMSSDSPortal();
        List<DropDownEntity> GetOrganizationType();

        DataSet GerPinCodeSearch(string pinCode);
        DataSet GerPinCodeSearchByTaluka(string pinCode,string talukaCode);

        
        List<DropDownEntity> GetOrganizationTypeByApplType(int applicationType);

        List<DropDownEntity> GetTaxonomyDropDownItems(string objectName, string property);
        BankDetail GetBankDetails(string IfscCode);

        List<DropDownEntity> GetTradeMachinesDropDownItems(int tradeId);

        string GenerateOtp(string mobileNo, string msgBody);
        string GenerateMailOtp(string emailId, int mailOtp);

        bool GetApplicationSubmissionStatus(int registarionId);

        ApplicationSubmissionStatusEntity GetApplicationSubmittedSatus(int registarionId);

        bool GetITIHeadDeclarationDetais(int registarionId);

        IList<CourseApproverEntity> GetCourseApproverByApplicationTypeId(int applicationTypeId);

        List<DropDownEntity> GetCourseApproverDDLItems(int applicationTypeId);
        //string GetApplicationTypeForReg(int applicationTypeId);

        List<DropDownEntity> GetTradeTypes();

        List<DropDownEntity> GetAcademicYears();

        List<DropDownEntity> GetTrades(int applicationTypeId, int tradeTypeId, int tradeSectorId);

        List<DropDownEntity> GetRegionTypes();

        List<DropDownEntity> GetApplicationStatusTypes();
        List<DropDownEntity> GetInspectionFeesType();
        List<DropDownEntity> GetTradeSectorsByAppType(string applicationTypeName);
        List<DropDownEntity> GetInspectionStatus();
        List<DropDownEntity> GetInspectionSections();
        List<DropDownEntity> GetInspectionAgencyCommitte(int agencyId);

        int ResetTradeRelatedDetails(int regsitrationId);
        List<DropDownEntity> GetTradeSectors();
        UserEntity GetUserDetails(string registrationNumber);

        ChangePasswodEntity GetPasswordByUsername(string Username);
        string GetApplicationTypeByRegId(int regId);
        List<DropDownEntity> GetTradesForAdminBySector(int applicationTypeId, string tradeSectorId);
        List<DropDownEntity> GetAffiliationType();
        int DeleteInspectionFeeId(int inspectionFeeId);
        List<DropDownEntity> GetAppointmentTypes();        
        List<DropDownEntity> GetInstituteType();
        // for Maha IT
        List<DropDownEntity> GetProposedInstituteType();
        List<DropDownEntity> GetFacultyName();
        List<DropDownEntity> GetDegreeName(int facultyId);
        List<DropDownEntity> GetGenderType();
        List<DropDownEntity> GetSubjectName(int degreeId);
        List<DropDownEntity> GetDocumentListByCollegeID(int RegistrationId, int UserId, int workCode);
        List<DropDownEntity> GetCourseType();
        List<DropDownEntity> GetCourseLevelType();
        List<DropDownEntity> GetMinorityCategoryType();
        List<DropDownEntity> GetReligiousMinorityType();
        // for Maha IT

        //Added By:Akshay
        List<DropDownEntity> GetCourse();

        List<DropDownEntity> GetCourse(int facultyId);
        List<DropDownEntity> GetProgram(int facultyId); 

        List<DropDownEntity> GetCourseList(int programId,int courseType,int registrationId);
                     
        List<DropDownEntity> GetSubjectsList(int courseId);

        //Added By:Akshay End

        //Add By Ganesh  Jha
        List<DropDownEntity> GetConcatenatedFacultyCourseMapping(int registrationId);
        List<DropDownEntity> GetAllBuilidingsByRegId(int registrationId);

        List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField);
        List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField);
        List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property);
        List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string Property, string ColumnName);
        List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField, string Property, string ColumnName,  string ListType=null);
        List<DropDownEntity> GetDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, bool Others = false, string orderBy = null, string orderByColumn = null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null);

        List<DropDownEntity> GetExistingCourseList(int RegistrationId);

        //byte[] GetFileByteArray(string filePath);
        List<GeographyEntity> GetStateDetail(string countryId);

        List<TableRecordsEntity> GetTableRecordsList(string ListType, List<string> stringColumnNames, List<string> stringColumnValues, List<int> intValueParams);
        List<TableRecordsEntity> GetTableDataList(string ListType, string TableName, string Column1 = null, string Param1 = null, string Column2 = null, string Param2 = null, string Column3 = null, string Param3 = null, string Column4 = null, string Param4 = null, string Column5 = null, string Param5 = null, string RequiredColumn1 = null, string RequiredColumn2 = null, string RequiredColumn3 = null, string RequiredColumn4 = null, string RequiredColumn5 = null, string RequiredColumn6 = null, string RequiredColumn7 = null, string RequiredColumn8 = null, string RequiredColumn9 = null, string RequiredColumn10 = null);

        List<TableRecordsEntity> GetTableDataList(TableRecordsEntity objTbl);
        SingleColumnEntity GetSingleColumnValues(string TableName, string DataValueField1, string ColumnName1, string Param1, string DataValueField2 = null, string ColumnName2 = null, string Param2 = null, string DataValueField3 = null, string ColumnName3 = null, string Param3 = null, string DataValueField4 = null, string ColumnName4 = null, string Param4 = null, string DataValueField5 = null, string DataValueField6 = null, string DataValueField7 = null, string DataValueField8 = null);

        //List<TableRecordsEntity> GetTableRecordsList(string ListType, string TableName, string DataParamField1, string DataParamValueField1, string DataColumnTextField1, string DataColumnValueField1, string DataParamField2 = null, string DataParamValueField2 = null, string DataColumnTextField2 = null, string DataColumnValueField2 = null, string DataParamField3 = null, string DataParamValueField3 = null, string DataColumnTextField3 = null, string DataColumnValueField3 = null, string DataParamField4 = null, string DataParamValueField4 = null, string DataColumnTextField4 = null, string DataColumnValueField4 = null, string DataParamField5 = null, string DataParamValueField5 = null, string DataColumnTextField5 = null, string DataColumnValueField5 = null);

        string DeleteFormTable(string TableName, string ColumnName1, string Param1, string ColumnName2 = null, string Param2 = null, string ColumnName3 = null, string Param3 = null);

        CommonDetailsEntity GetCommonSettings();

        List<DropDownEntity> GetSONoQuoteNoList(string EndUse, string quoteType);

        DataTable GetDataTableForDocument(string ListType, string TableName, string[] DataColumn, string[] DataParam, string[] RequiredColumn);

        string SaveBulkEntryDetails(BulkUploadEntity bEntity);
        List<DropDownEntity> SaveNewItemInDdl(AddDdlEntity bEntity);

    }
}
