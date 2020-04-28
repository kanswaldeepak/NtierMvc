using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public static class Constants
    {
        public const string ERPADMINROLENAME = "ERP";
        public const string INSPECTIONAGECY = "INSPECTION AGENCY";
        public const string ERPVTIADMINROLENAME = "VTIADMIN";
        public const string ERPITIADMINROLENAME = "ITIADMIN";
        public const string ERPFINANCEOFFICERROLENAME = "FINANCEOFFICER";
        public const string INSPECTIONCOMMITTE = "INSPECTION COMMITTE";
    }

    public static class GeneralConstants
    {
        public const string Inserted = "Inserted Successfully";
        public const string Updated = "Updated Successfully";
        public const string AlreadyExist = "Already Exist";

        public const string ERPADMINROLENAME = "ERP";
        public static string ProjectNameKey = "ProjectName";
        public static string ProjectNameValue = "ERP";
        public static string CommonDataErrorKey = "CommonDataError";
        public static string CommonDataErrorValue = "Contact Support with problem 'Common Data Load Error'";
        public static string NotSavedError = "Cannot Save! Contact Support!!";
        public static string NotDeletedError = "Cannot Delete Record! Contact Support!!";
        public static string NoTableNameFound = "Cannot Find Table To Save! Contact Support!!";
        public static string NullTableNameProvided = "Select Table To Save or Contact Support!!";
        public static string NoFileSelected = "No Record Selected! Please Select a Record and Proceed!!";
        public static string SavedSuccess = "Saved Successfully!";
        public static string DeleteSuccess = "Deleted Successfully!";
        public static string HomeLinkKey = "HomeLink";
        public static string FillAllMaterialDetails = "Kindly Fill All Material Details to Save!!";

        public static string FetchListMethod = "FetchListMethod";

        public static string HomeLinkValue = "/Home";

        public static string StatusInfo = "Status_Info";
        public static int StatusInfoOne = 1;
        public static int StatusInfoZero = 0;

        public static int MinusOne = -1;
        public static int One = 1;
        public static int Zero = 0;

        public static string AdminU = "AdminERP";
        public static string AdminP = ".Pass$123";

        public static int pageSize = 10;
        public static string FalseValue = "False";

        public static string Contact = "Contact";
        //public static string Connection = "server=184.168.194.62;database=gyanurDB_;User Id=gyanur; password=$gyanur123#.";

        public static string FileMessageSuccess = "Upload Successfully";
        public static string FileMessageFailure = "File upload failed! Please try again";
        public static string FileExtensionNotValid = "File Extension is not Valid. Only Upload with doc, docx, pdf, txt, ppt, pptx, xls, xlsx, zip, rar extension.";

        public static string ListTypeN = "Normal";
        public static string ListTypeD = "Distinct";

        public static string DocTypeXLS = "xlsx";
        public static string DocTypeDOC = "doc";
        public static string DocTypePDF = "pdf";

    }

    public static class ActivityName
    {
        public const string ISSUEACCEPTANCEOFEXPRESSIONOFINTEREST = "IssueAcceptanceofExpressionofInterest";
        public const string REQUESTFORINSPECTION = "RequestforInspection";
        public const string INSPECTIONREPORT = "InspectionReport";
        public const string SUBMISSIONOFINSPECTIONREPORT = "SubmissionOfInspectionReport";
    }

    public static class FundsRequirment
    {
        public const string NormsKey = "Funds Availability and Requirement Details";
        public static class NormsDetails
        {
            public const string FundsRequiredforInfrastructureFacility = "FundsRequiredforInfrastructureFacility";
            public const string FundsRequiredforTools = "FundsRequiredforTools";
            public const string FundsRequiredforCivilWork = "FundsRequiredforCivilWork";
            public const string BuildingCostperSqMeter = "BuildingCostperSqMeter";
        }
    }

    public static class FeesCalculation
    {
        public const string NormsKey = "Fees Calculation";
        public static class NormsDetails
        {
            public const string ApplicationFees = "ApplicationFees";
            public const string UnitWiseFees = "UnitWiseFees";
        }
    }

    public static class LandDetails
    {
        public const string NormsKey = "Land Details";
        public static class NormsDetails
        {
            public const string TotalLandRequiredasperNorms = "Total Land Required as per Norms";
        }
    }

    public static class AppType
    {
        public const string PITI = "PITI";
        public const string PSSC = "PSSC";
        public const string BIFO = "BIFO";
        public const string HSCV = "HSCV";
        public const string VTI = "VTI";
        public const string SAA = "SAA";
        public const string OTH = "OTH";
        public const string MSBV = "MSBVEE";        
    }

    public static class AffiliationType
    {
        public const string NEW = "NEW";
        public const string CONTINUATION = "CONTINUATION";
        public const string EXTENSION_SUBJECT = "EXTENSION_SUBJECT";
        public const string EXTENSION_FACULTY = "EXTENSION_FACULTY";
        public const string EXTENSION_UNIT = "EXTENSION_UNIT";
        public const string NATURAL_GROWTH = "NATURAL_GROWTH";
        public const string PERMANENT = "PERMANENT";
        public const string NEW_AUTONOMOUS = "NEW_AUTONOMOUS";
        public const string CONT_AUTONOMOUS = "CONT_AUTONOMOUS";
    }

    public static class ApplicationStatus
    {
        public const string ApplicationIncomplete = "Application Incomplete";
        public const string Applicationcomplete = "Application Completed";
        public const string SubmittedExpressPendingByAdmin = "Submitted Express of Interest and pending for Verification by Administrator";
        public const string SubmittedExpressPendingByAgency = "Submitted Express of Interest and pending for Verification by Inspection Agency";
        public const string VerifiedbyAdmin = "Verified by Administrator and open for filling Infrastructure details";
        public const string RejectedbyAdmin = "Rejected by Administrator for correction";
    }
}
