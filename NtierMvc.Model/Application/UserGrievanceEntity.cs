using NtierMvc.Common;
using System.Collections.Generic;
using System.IO;

namespace NtierMvc.Model.Application
{
    public class UserGrievanceEntity
    {
        public int TrnApplicationId { get; set; }
        public int TrnApplication_InspectionId { get; set; }
        public string Application_Page_Type { get; set; }
        public string User_Field { get; set; }
        public string Particular { get; set; }
        public string User_Data { get; set; }
        public int Status_Id { get; set; }
        public string Remark { get; set; }
        public string User_Grievance { get; set; }
        public string Supporting_Docs_Url { get; set; }
        public FileUploadEntity Supporting_Docs { get; set; }
        public int HiddenFileID { get; set; }
        public string Admin_Remark { get; set; }
        public int Admin_Status_Id { get; set; }
        public string Status { get; set; }
        public int UsersGrievanceDataId { get; set; }

    }
    public class UserGrievanceCommonAppTypeWise
    {
        public List<UserGrievanceEntity> lstUserGrievanceEntity { get; set; }
        public string Application_Page_Type { get; set; }
        public int UserId { get; set; }
    }
    public class UserGrievanceDataDisplaySave
    {
        public List<UserGrievanceCommonAppTypeWise> lstUserGrievanceCommonAppTypeWise { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int RegistrationID { get; set; }
        public int? WorkItemId { get; set; }
        public int GrievDataModeId { get; set; }

        public UserGrievanceDataDisplaySave()
        {
            lstUserGrievanceCommonAppTypeWise = new List<UserGrievanceCommonAppTypeWise>();
        }
    }
    public class AdminGrievanceDataSave
    {
        public int UsersGrievanceDataId { get; set; }
        public string Admin_Remark { get; set; }
        public int Admin_Status_Id { get; set; }
    }
    public class UserGrievanceSuggestions
    {
        public int DataTypeID { get; set; }
        public string NameofInstitute { get; set; }
        public int RegistrationId { get; set; }
        public string State { get; set; }
        public string DistrictName { get; set; }
        public string SubDistrictName { get; set; }
        public string CityVillageName { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string Category { get; set; }
        public string Grievance_Suggestion_Type { get; set; }
        public string[] Grievance_Suggestion_Types { get; set; }
        public string Comments { get; set; }
        public string ScreenShotFile { get; set; }
        public string CommitteEstablishmentDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsEmailSent { get; set; }
        public FileUploadEntity UploadedDocs { get; set; }   
        public EmailContent EmailContentPage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
