using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.IO;
using NtierMvc.Model.Application;
using NtierMvc.Common;
using NtierMvc.DataAccess.Source;
using NtierMvc.DataAccess.Common;
using System.Data.Common;
using NtierMvc.Model;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {
        #region Class Declarations
        private readonly DatabaseAccess _dbAccess;
        private LoggingHandler _loggingHandler;
        private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        public Repository()
        {
            _dbAccess = new DatabaseAccess();
            //Repository Initializations
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


        public List<DropDownEntity> DataTableToDocumentList(DataTable table)
        {
            var list = new List<DropDownEntity>(table.Rows.Count);
            var ddl = new DropDownEntity()
            {
                DataCodeField = "-1",
                DataTextField = "Select"
            };
            list.Add(ddl);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                ddl = new DropDownEntity()
                {
                    DataCodeField = Convert.ToString(values[0]),
                    DataTextField = Convert.ToString(values[1])
                };
                list.Add(ddl);
            }
            return list;
        }
        public List<DropDownEntity> DataTableToList(DataTable table)
        {
            var list = new List<DropDownEntity>(table.Rows.Count);
            var ddl = new DropDownEntity()
            {
                DataValueField = -1,
                DataTextField = "Select"
            };
            list.Add(ddl);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                ddl = new DropDownEntity()
                {
                    DataValueField = Convert.ToInt32(values[0]),
                    DataTextField = values[1].ToString(),
                    DataCodeField = values.Count() > 2 ? values[2].ToString() : null

                };
                list.Add(ddl);
            }

            return list;
        }

        public List<DropDownEntity> DataTableToStringList(DataTable table)
        {
            var list = new List<DropDownEntity>(table.Rows.Count);
            var ddl = new DropDownEntity()
            {
                DataStringValueField = "",
                DataTextField = "Select"
            };
            list.Add(ddl);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                ddl = new DropDownEntity()
                {
                    DataStringValueField = Convert.ToString(values[0]),
                    DataTextField = values[1].ToString(),
                    DataCodeField = values.Count() > 2 ? values[2].ToString() : null

                };
                list.Add(ddl);
            }

            return list;
        }

        public List<DropDownEntity> GetApplicationType()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetSchemeDetails"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetApplicationTypesForVTIAdmin()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetApplicationTypesForVTIAdmin"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetApplicationTypesForITIAdmin()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetApplicationTypesForITIAdmin"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }


        public List<DropDownEntity> GetMSSDSPortal()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetMSSDSPortal"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetTitle()
        {
            var spName = ConfigurationManager.AppSettings["GetTitle"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetOrganizationType()
        {
            var spName = ConfigurationManager.AppSettings["GetOrganizationType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        // for MahaIT

        public List<DropDownEntity> GetInstituteType()
        {
            var spName = ConfigurationManager.AppSettings["GetInstituteType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetAcademicYears()
        {
            var spName = ConfigurationManager.AppSettings["GetAcademicYears"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }
        public List<DropDownEntity> GetFacultyName()
        {
            var spName = ConfigurationManager.AppSettings["GetFacultyName"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }
        public List<DropDownEntity> GetDegreeName(int CourseId)
        {
            var spName = ConfigurationManager.AppSettings["GetDegreeName"];
            var parms = new Dictionary<string, object>();
            parms.Add("@CourseId", CourseId);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }


        public List<DropDownEntity> GetGenderType()
        {
            var spName = ConfigurationManager.AppSettings["GetGenderType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetProposedInstituteType()
        {
            var spName = ConfigurationManager.AppSettings["GetProposedInstituteType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }
        public List<DropDownEntity> GetMinorityCategoryType()
        {
            var spName = ConfigurationManager.AppSettings["GetMinorityCategoryType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }
        public List<DropDownEntity> GetReligiousMinorityType()
        {
            var spName = ConfigurationManager.AppSettings["GetReligiousMinorityType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetCourseLevelType()
        {
            var spName = ConfigurationManager.AppSettings["GetCourseLevelType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetCourseType()
        {
            var spName = ConfigurationManager.AppSettings["GetCourseType"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetDocumentListByCollegeID(int RegistrationId, int UserId, int workCode)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", RegistrationId);
            parms.Add("@UserId", UserId);
            parms.Add("@WorkCode", workCode);
            var spName = ConfigurationManager.AppSettings["GetDocumentListByCollegeID"];
            return DataTableToDocumentList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetSubjectName(int degreeId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@DegreeId", degreeId);
            var spName = ConfigurationManager.AppSettings["GetSubjectName"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        // for MahaIT
        public List<DropDownEntity> GetOrganizationType(int applicationType)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@ApplicationTypeId", applicationType);
            var spName = ConfigurationManager.AppSettings["GetOrganizationSectorByApplType"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        //public async Task<string> UploadFilesToBlobAsync(FileUploadEntity objFileUpload, Dictionary<int, string> dicContainers)
        //{
        //    string url = "";
        //    string path = "erp";
        //    int index = 1;
        //    try
        //    {
        //        var items = from pair in dicContainers
        //                    orderby pair.Key ascending
        //                    select pair;

        //        string storageConnection = CloudConfigurationManager.GetSetting("StorageConnectionString");
        //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
        //        //create a block blob    
        //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        //        //create a container    
        //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("erp");

        //        // Display results.
        //        //create a container if it is not already exists    
        //        if (await cloudBlobContainer.CreateIfNotExistsAsync())
        //        {
        //            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
        //            {
        //                PublicAccess = BlobContainerPublicAccessType.Blob
        //            });
        //        }
        //        foreach (KeyValuePair<int, string> pair in items)
        //        {
        //            path = path + "/" + pair.Value;
        //            //cloudBlobContainer = cloudBlobClient.GetContainerReference(pair.Value);
        //            //if (await cloudBlobContainer.CreateIfNotExistsAsync())
        //            //{
        //            //    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
        //            //    {
        //            //        PublicAccess = BlobContainerPublicAccessType.Blob
        //            //    });
        //            //}
        //        }

        //        //string _imageName = Guid.NewGuid().ToString() + "-" + objFileUploadEntity.FileExtension;
        //        //   string imageName = path + "/" + objFileUpload.FileName;
        //        // string imageName = objFileUpload.FileName;
        //        //get Blob reference    
        //        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(path);
        //        using (var stream = new MemoryStream(objFileUpload.FileByteArray, writable: false))
        //        {
        //            await cloudBlockBlob.UploadFromStreamAsync(stream);
        //        }
        //        url = cloudBlockBlob.Uri.ToString();
        //        index++;

        //    }
        //    catch (Exception ex)
        //    {

        //    }


        //    return url;
        //}

        //public string UploadFilesToBlob(FileUploadEntity objFileUpload, Dictionary<int, string> dicContainers)
        //{
        //    string url = "";
        //    string path = "";
        //    int index = 1;
        //    try
        //    {
        //        var items = from pair in dicContainers
        //                    orderby pair.Key ascending
        //                    select pair;

        //        string storageConnection = CloudConfigurationManager.GetSetting("StorageConnectionString");
        //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
        //        //create a block blob    
        //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        //        //create a container    
        //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("erp");

        //        // Display results.
        //        //create a container if it is not already exists    
        //        cloudBlobContainer.CreateIfNotExists();
        //        cloudBlobContainer.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Blob
        //        });

        //        foreach (KeyValuePair<int, string> pair in items)
        //        {
        //            //if (path == "")
        //            //    path = pair.Value;
        //            //else
        //            path = path + pair.Value + "/";
        //        }

        //        String extension = "";

        //        int i = objFileUpload.FileName.LastIndexOf('.');
        //        if (i > 0)
        //        {
        //            extension = objFileUpload.FileName.Substring(i + 1);
        //        }

        //        var fileName = objFileUpload.FileName.Substring(0, i) + "_" + Guid.NewGuid() + "." + extension;
        //        path = path + fileName;
        //        //get Blob reference    
        //        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(path);
        //        using (var stream = new MemoryStream(objFileUpload.FileByteArray, writable: false))
        //        {
        //            cloudBlockBlob.UploadFromStream(stream);
        //        }
        //        url = cloudBlockBlob.Uri.ToString();
        //        index++;

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return url;
        //}

        //public byte[] GetFileByteArray(string filePath)
        //{
        //    byte[] byteData = { };
        //    try
        //    {
        //        string storageConnection = CloudConfigurationManager.GetSetting("StorageConnectionString");
        //        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(storageConnection);
        //        //create a block blob    
        //        CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
        //        //create a container    
        //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("erp");

        //        // Display results.
        //        //create a container if it is not already exists    
        //        cloudBlobContainer.SetPermissions(new BlobContainerPermissions
        //        {
        //            PublicAccess = BlobContainerPublicAccessType.Blob
        //        });


        //        // CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filePath);

        //        CloudBlob cloudBlob = cloudBlobContainer.GetBlobReference(filePath);                
        //        cloudBlob.DownloadToByteArray(byteData, 0);
        //        return byteData;

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return byteData;
        //}



        public DataSet GerPinCodeSearch(string pinCode)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["PinCodeSearch"];
            parms.Add("@PinCode", pinCode);
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GerPinCodeSearchByTaluka(string pinCode, string talukaCode)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["PinCodeSearchByTaluka"];
            parms.Add("@PinCode", pinCode);
            parms.Add("@TalukaCode", talukaCode);
            return _dbAccess.GetDataSet(spName, parms);
        }


        public List<DropDownEntity> TaxonomyDropDownItems(string objectName, string property)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTaxonomy"];
            if (!string.IsNullOrEmpty(objectName))
                parms.Add("@ObjectName", objectName);
            parms.Add("@Property", property);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetBankDetails(string IfscCode)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@IFSCCode", IfscCode);
            var spName = ConfigurationManager.AppSettings["erp_GetBankDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public List<DropDownEntity> GetTradeMachinesDropDownItems(int tradeId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTradeMachinesDropDownItems"];
            if (tradeId > 0)
                parms.Add("@TradeId", tradeId);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public bool GetApplicationSubmissionStatus(int registarionId)
        {
            var parms = new Dictionary<string, object>();
            bool statsu = true;
            var spName = ConfigurationManager.AppSettings["GetApplicationStatus"];

            parms.Add("@RegistrationId", registarionId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            if (dt.Rows.Count > 0)
                statsu = Convert.ToBoolean(dt.Rows[0]["ApplicationSubissionStatus"]);
            return statsu;
        }

        public DataTable GetApplicationSubmittedSatus(int registarionId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetApplicationSubmissionStatus"];
            parms.Add("@RegistrationId", registarionId);
            return _dbAccess.GetDataTable(spName, parms);
        }
        public DataTable GetITIHeadDeclarationDetais(int registarionId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetITIHeadDeclarationDetaisByRegistrationId"];
            parms.Add("@RegistrationId", registarionId);
            return _dbAccess.GetDataTable(spName, parms);
        }
        public DataTable GetCourseApproverByApplicationTypeId(int applicationTypeId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetCourseApprover"];
            parms.Add("@ApplicationTypeId", applicationTypeId);

            return _dbAccess.GetDataTable(spName, parms);
        }

        public List<DropDownEntity> GetCourseApproverDDLItems(int applicationTypeId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetCourseApproverDDLItems"];
            parms.Add("@ApplicationTypeId", applicationTypeId);

            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetTradeTypes()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTradeTypes"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetRegionTypes()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetRegionTypes"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetApplicationStatusTypes()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetApplicationStatusTypes"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetInspectionFeesType()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionFeesType"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetProposedInstituteStatusDetailsForAdminById(int applicationTypeId, int applicationStatusTypeId, int regionId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetProposedInstituteStatusDetailsForAdminById"];

            parms.Add("@ApplicationTypeID", applicationTypeId);
            parms.Add("@ApplicationStatusTypeID", applicationStatusTypeId);
            parms.Add("@RegionID", regionId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }
        public DataTable GetInspectionStatusDetailsForAdminById(int applicationTypeId, int regionId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionStatusforAdmin"];
            parms.Add("@ApplicationTypeId", applicationTypeId);
            parms.Add("@RegionId", regionId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }
        public DataTable GetProposedInstituteStatusDetailsForAdmin(int applicationTypeId, int applicationStatusTypeId, int regionId)
        {
            var parms = new Dictionary<string, object>();
            //var spName = ConfigurationManager.AppSettings["GetProposedInstituteStatusDetailsForAdmin"];
            var spName = ConfigurationManager.AppSettings["GetInstituteDetails_Admin"];

            parms.Add("@ApplicationTypeID", applicationTypeId);
            parms.Add("@ApplicationStatusTypeID", applicationStatusTypeId);
            parms.Add("@RegionID", regionId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }
        public List<DropDownEntity> GetTradeSectorsByAppType(string applicationTypeName)
        {
            var applicationTypeId = ConfigurationManager.AppSettings[applicationTypeName];
            var parms = new Dictionary<string, object>();
            parms.Add("@ApplicationTypeId", applicationTypeId);
            var spName = ConfigurationManager.AppSettings["GetTradeSectorsByAppType"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetTradeDetailsBySectorId(string applicationTypeName, int tradeSectorId)
        {
            var applicationTypeId = ConfigurationManager.AppSettings[applicationTypeName];
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTradeDetailsBySectorId"];

            parms.Add("@ApplicationTypeId", applicationTypeId);
            parms.Add("@TradeSectorId", tradeSectorId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public List<DropDownEntity> GetInspectionSections()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionSections"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetInspectionStatus()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionAction"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetInspectionAgencyCommitte(int agencyId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionAgencyCommitte"];
            parms.Add("@AdminId", agencyId);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public DataTable GetInspectionRegistrations(string adminType)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionRegistrationsForAdmin"];
            parms.Add("@AdminType", adminType);
            return (_dbAccess.GetDataTable(spName, parms));
        }
        public DataTable GetInspectionAgencies(string adminType)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetInspectionAgenciesForAdmin"];
            parms.Add("@AdminType", adminType);
            return (_dbAccess.GetDataTable(spName, parms));
        }
        public int ResetTradeRelatedDetails(int registrationId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", registrationId);
            var spName = ConfigurationManager.AppSettings["ResetInstituteTradeFlowDetails"];
            return _dbAccess.ExecuteNonQuery(spName, parms);
        }

        public DataTable GetRegistrationsReportList(string applicationTypeId)
        {

            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetAllRegistrationsReportByAppId"];

            parms.Add("@ApplicationTypeId", applicationTypeId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }
        public DataTable GetLOIReportList(string applicationTypeId)
        {

            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetAllLOIGeneratedReportByAppId"];

            parms.Add("@ApplicationTypeId", applicationTypeId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }



        public DataTable GetEmpanelledVTPByAppType(string applicationTypeName, int tradeSectorId)
        {
            var applicationTypeId = ConfigurationManager.AppSettings[applicationTypeName];
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GETALLVTPSUsers"];

            parms.Add("@ApplicationTypeId", applicationTypeId);
            parms.Add("@TradeSectorId", tradeSectorId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public List<DropDownEntity> GetTradeSectors()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTradeSectors"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public DataTable GetUserDetails(string registrationNumber)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["erp_CheckUserExists"];
            parms.Add("@UserName", registrationNumber);
            parms.Add("@ObjectType", '1');
            return _dbAccess.GetDataTable(spName, parms);
        }

        public List<DropDownEntity> GetTrades(int applicationTypeId, int tradeTypeId, int tradeSectorId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@ApplicationTypeId", applicationTypeId);
            parms.Add("@TradeTypeId", tradeTypeId);
            parms.Add("@TradeSectorId", tradeSectorId);
            var spName = ConfigurationManager.AppSettings["GetTrades"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetPasswordByUsername(string Username)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetPasswordByUsername"];
            parms.Add("@Username", Username);

            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetProposedInstituteStatusDetailsByIdForAdmin(int applicationTypeId, string applicationStatusType, int regionId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetProposedInstituteStatusDetailsByIdForAdmin"];

            //parms.Add("@ApplicationTypeID", applicationTypeId);
            //parms.Add("@ApplicationStatusTypeID", applicationStatusType);
            //parms.Add("@RegionID", regionId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }
        public DataTable GetInstitutStatusDetailsForAdmin(int applicationTypeId, string applicationStatusType, int regionId)
        {
            DataTable dt = null;
            var parms = new Dictionary<string, object>();
            //var spName = ConfigurationManager.AppSettings["GetProposedInstituteStatusDetailsForAdmin"];
            //var spName = ConfigurationManager.AppSettings["GetInstituteDetails_Admin"];

            parms.Add("@ApplicationTypeId", applicationTypeId);
            //parms.Add("@ApplicationStatusTypeID", applicationStatusType);
            parms.Add("@RegionId", regionId);
            if (applicationStatusType == "UA")
            {
                var spName = ConfigurationManager.AppSettings["GetUnAssignedAgencyInstituteDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }

            else if (applicationStatusType == "AA")
            {
                var spName = ConfigurationManager.AppSettings["GetAssignedAgencyInstituteDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }

            else if (applicationStatusType == "PWIFP")
            {
                var spName = ConfigurationManager.AppSettings["GetPendingNotInspectionFeesPaidDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }

            else if (applicationStatusType == "PALS")
            {
                var spName = ConfigurationManager.AppSettings["GetProvisionAdmissionLetterNotSubmittedDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }

            else if (applicationStatusType == "IFNS")
            {
                var spName = ConfigurationManager.AppSettings["GetInspectionFeesPaidNotSubmittedDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }
            else if (applicationStatusType == "SINSC")
            {
                var spName = ConfigurationManager.AppSettings["GetSubmittedInstitutesNotStartedCurrentDate"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }
            else if (applicationStatusType == "PALNGS")
            {
                var spName = ConfigurationManager.AppSettings["GetProvisionAdmissionLetterNotGeneratedSubmittedDetails"];
                dt = _dbAccess.GetDataTable(spName, parms);
            }

            return dt;
        }

        public DataTable GetApplicationTypeByRegId(int regId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetApplicationCodeByRegId"];
            parms.Add("@RegistrationId", regId);

            return _dbAccess.GetDataTable(spName, parms);
        }

        public List<DropDownEntity> GetTradesForAdminBySector(int applicationTypeId, string tradeSectorId)
        {
            // string[] result = tradeSectorId.Select(x => x.ToString()).ToArray();
            var parms = new Dictionary<string, object>();
            parms.Add("@ApplicationTypeId", applicationTypeId);
            parms.Add("@TradeSectorId", tradeSectorId);
            var spName = ConfigurationManager.AppSettings["GetTradesForAdmin"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetAffiliationType()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["Education_GetAffiliationTypes"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable UpdateInspectionFeeDetail(int ispectionFeeId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@InspectionFeeId", ispectionFeeId);
            var spName = ConfigurationManager.AppSettings["Education_GetInspectionFeeIdForUpdate"];
            return _dbAccess.GetDataTable(spName, parms);
        }

        public int DeleteInspectionFeeId(int inspectionFeeId)
        {
            string spName = ConfigurationManager.AppSettings["Education_DeleteInspectionFeeId"];
            var parms = new Dictionary<string, object>();
            parms.Add("@inspectionFeeId", inspectionFeeId);
            return _dbAccess.ExecuteNonQuery(spName, parms);
        }
        public List<DropDownEntity> GetAppointmentTypes()
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetAppointmentTypes"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetRegistrationPercentageDashboard()
        {
            var spName = ConfigurationManager.AppSettings["GetPrivateITISummaryReportInspcetionFeecountDashboard"];
            return _dbAccess.GetDataTable(spName);
        }
        public DataSet GetPrivateITISummaryReportByRegionDashboard()
        {
            var spName = ConfigurationManager.AppSettings["GetPrivateITISummaryReportByRegionDashboard"];
            var parms = new Dictionary<string, object>();
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataTable GetRegistrationFee(string strUserName)
        {
            string spName = ConfigurationManager.AppSettings["Education_GetRegistrationFee"];
            var parms = new Dictionary<string, object>();
            parms.Add("@strUserName", strUserName);
            return _dbAccess.GetDataTable(spName, parms);
        }
        public DataTable GetAdminStatistics(string adminType)
        {
            var spName = ConfigurationManager.AppSettings["AdminStatisticsDashboardITI"];
            if (adminType == "vti")
            {
                spName = ConfigurationManager.AppSettings["AdminStatisticsDashboardVTI"];
            }
            return _dbAccess.GetDataTable(spName);
        }


        public List<DropDownEntity> GetProgram(int facultyId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@FacultyId", facultyId);
            var spName = "Education_GetProgramsByFacultyID";
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        //Added By:Akshay

        //Modified by Ganesh Jha on 8 Jan 2019
        public List<DropDownEntity> GetCourse(int facultyId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@FacultyId", facultyId);
            var spName = ConfigurationManager.AppSettings["GetCourses"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetSubjectsList(int courseId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@i_CourseId", courseId);
            parms.Add("@i_CultureId", 1);
            var spName = "Eduction_GetSubjectsList";
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetCourseList(int programId, int courseType, int registationId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@i_ProgramId", programId);
            parms.Add("@i_CourseType", courseType);
            parms.Add("@i_RegistationId", registationId);
            parms.Add("@i_CultureId", 1);
            var spName = "Eduction_GetCoursesList";
            return _dbAccess.GetDataTable(spName, parms);
        }




        //Added By:Akshay End
        // BY Ganesh Jha
        public List<DropDownEntity> GetConcatenatedFacultyCourseMapping(int registrationid)
        {
            string spName = ConfigurationManager.AppSettings["Education_GetDeptCourseMappings"];
            var parms = new Dictionary<string, object>();
            parms.Add("@Registrationid", registrationid);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }
        public List<DropDownEntity> GetAllBuilidingsByRegId(int registrationid)
        {
            string spName = ConfigurationManager.AppSettings["Education_GetAllBuilidingsByRegId"];
            var parms = new Dictionary<string, object>();
            parms.Add("@Registrationid", registrationid);
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetCategory()
        {
            var spName = ConfigurationManager.AppSettings["GetCategory"];
            return DataTableToList(_dbAccess.GetDataTable(spName));
        }

        public List<DropDownEntity> GetCasteByCategory(int categoryId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@categoryId", categoryId);
            var spName = ConfigurationManager.AppSettings["GetCasteByCategory"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetStaffListByStaffTypeId(int staffTypeId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@StaffTypeId", staffTypeId);
            var spName = ConfigurationManager.AppSettings["GetStaffWiseEmployee"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetProfileDetailList(int registrationId)
        {
            string spName = ConfigurationManager.AppSettings["GetProfileDetailList"];
            spName = "[Education_Institute_GetStaffProfiles_ByRegId]";
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", registrationId);
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable SaveStaffStrengthByQualification(DataTable dt, int RegistrationID)
        {
            var Params = new Dictionary<string, object>();
            Params.Add("@Inputtable", dt);
            Params.Add("@RegID", RegistrationID);
            var SPName = "[dbo].[USP_AddStaffQualificationDetails]";
            return _dbAccess.GetDataTable(SPName, Params);
        }

        public DataTable StaffStrengthByQualification(int registrationId)
        {
            string spName = "[dbo].[Education_GetStaffQualificationDetails]";
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", registrationId);
            return _dbAccess.GetDataTable(spName, parms);
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetMasterTableList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);

            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetMasterTableList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);

            return DataTableToStringList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetMasterTableStringList(string TableName, string DataValueField, string DataTextField, string Property, string columnName, string ListType=null)
        {
            var parms = new Dictionary<string, object>();
            var spName = "";
            spName = ConfigurationManager.AppSettings["GetMasterTableList"];

            //if (ListType == GeneralConstants.ListTypeN)
            //    spName = ConfigurationManager.AppSettings["GetMasterTableList"];
            //else
            //    spName = ConfigurationManager.AppSettings["GetDistinctMasterTableList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);
            parms.Add("@Property", Property);
            parms.Add("@ColumnName", columnName);
            parms.Add("@ListType", ListType);

            return DataTableToStringList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetDropDownList(string TableName, string ListType, string DataValueField, string DataTextField, string Param, string ColumnName, string orderBy = null, string orderByColumn=null, string Param1 = null, string ColumnName1 = null, string Param2 = null, string ColumnName2 = null, string Param3 = null, string ColumnName3 = null, string Param4 = null, string ColumnName4 = null)
        {
            var parms = new Dictionary<string, object>();
            var spName = "";
            spName = ConfigurationManager.AppSettings["GetDropDownList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);
            parms.Add("@Param", Param);
            parms.Add("@ColumnName", ColumnName);
            parms.Add("@Param1", Param1);
            parms.Add("@ColumnName1", ColumnName1);
            parms.Add("@Param2", Param2);
            parms.Add("@ColumnName2", ColumnName2);
            parms.Add("@Param3", Param3);
            parms.Add("@ColumnName3", ColumnName3);
            parms.Add("@Param4", Param4);
            parms.Add("@ColumnName4", ColumnName4);
            parms.Add("@ListType", ListType);
            parms.Add("@orderBy", orderBy);
            parms.Add("@orderByColumn", orderByColumn);

            return DataTableToStringList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string property)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetMasterTableList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);
            parms.Add("@Property", property);

            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetMasterTableList(string TableName, string DataValueField, string DataTextField, string property, string columnName)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetMasterTableList"];

            parms.Add("@TableName", TableName);
            parms.Add("@DataValueField", DataValueField);
            parms.Add("@DataTextField", DataTextField);
            parms.Add("@Property", property);
            parms.Add("@ColumnName", columnName);

            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<DropDownEntity> GetExistingCourseList(int registrationId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", registrationId);
            var spName = ConfigurationManager.AppSettings["GetExistingCourseList"];
            return DataTableToList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetExistingCourseListForPermanantAffiliation(int registrationId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", registrationId);
            var spName = ConfigurationManager.AppSettings["GetExistingCourseList"];
            spName = "[Education_GetExistingCourseList_For_PermanentAffiliation]";
            return _dbAccess.GetDataTable(spName, parms);
        }
        public DataTable GetQualificationList(int profileId)
        {
            string spName = ConfigurationManager.AppSettings["GetProfileDetailList"];
            spName = "[Education_GetQualificationList_ByProfileId]";
            var parms = new Dictionary<string, object>();
            parms.Add("@profileId", profileId);
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetCourseForUnitExtension(int RegistrationId, int userId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@RegistrationId", RegistrationId);
            parms.Add("@userId", userId);

            var spName = "[Education_GetCourseList_ForUnitExtension]";
            return _dbAccess.GetDataTable(spName, parms);
        }

        public string DeleteStaffServiceDetail(int StaffProfileId, int userId)
        {
            string spName = "Eduction_DeleteStaffServiceDetails";
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@i_StaffProfileId", StaffProfileId);
            parms.Add("@i_UserId", userId);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public int DeleteStaffQualificationStrength(int RowId, string deletedBy)
        {
            string spName = "Eduction_DeleteStaffQualificationStrength";
            var parms = new Dictionary<string, object>();
            parms.Add("@RowId", RowId);
            parms.Add("@UserId", deletedBy);
            return _dbAccess.ExecuteNonQuery(spName, parms);
        }

        public int SavePermanantAffiliationDetails(PermanantAffiliationEntity entity)
        {
            var parms = new Dictionary<string, object>();

            parms.Add("@RegistrationId", entity.RegistrationId);
            parms.Add("@InstituteCourseId", entity.checkModel.InstituteCourseId);
            parms.Add("@CourseId", entity.checkModel.Id);
            parms.Add("@UserId", entity.CreatedBy);
            parms.Add("@IP", entity.IP);

            string spName = "Education_SavePermanentAffiliation";
            return Convert.ToInt32((_dbAccess.ExecuteScalar(spName, parms)));
        }

        public DataTable GetStateDetail(string countryId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@countryId", countryId);
            var spName = ConfigurationManager.AppSettings["GetStateDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public List<SingleColumnEntity> GetSingleColumnValues(string TableName, string DataValueField1, string ColumnName1, string Param1, string DataValueField2 = null, string ColumnName2 = null, string Param2 = null, string DataValueField3 = null, string ColumnName3 = null, string Param3 = null, string DataValueField4 = null, string ColumnName4 = null, string Param4 = null, string DataValueField5 = null, string DataValueField6 = null, string DataValueField7 = null, string DataValueField8 = null)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetSingleColumnList"];

            parms.Add("@TableName", TableName);

            parms.Add("@DataValueField1", DataValueField1);
            //parms.Add("@DataTextField1", DataTextField1);
            parms.Add("@ColumnName1", ColumnName1);
            parms.Add("@Param1", Param1);

            parms.Add("@DataValueField2", DataValueField2);
            //parms.Add("@DataTextField2", DataTextField2);
            parms.Add("@ColumnName2", ColumnName2);
            parms.Add("@Param2", Param2);

            parms.Add("@DataValueField3", DataValueField3);
            //parms.Add("@DataTextField3", DataTextField3);
            parms.Add("@ColumnName3", ColumnName3);
            parms.Add("@Param3", Param3);

            parms.Add("@DataValueField4", DataValueField4);
            //parms.Add("@DataTextField4", DataTextField4);
            parms.Add("@ColumnName4", ColumnName4);
            parms.Add("@Param4", Param4);

            parms.Add("@DataValueField5", DataValueField5);
            parms.Add("@DataValueField6", DataValueField6);
            parms.Add("@DataValueField7", DataValueField7);
            parms.Add("@DataValueField8", DataValueField8);

            return DataTableToSingleColumnList(_dbAccess.GetDataTable(spName, parms));
        }

        public List<SingleColumnEntity> DataTableToSingleColumnList(DataTable table)
        {
            var list = new List<SingleColumnEntity>(table.Rows.Count);
            var ddl = new SingleColumnEntity();
            //{
            //    DataValueField = -1,
            //    DataTextField = "Select"
            //};
            //list.Add(ddl);
            if (table.Rows.Count > 0)
            {
                var values = table.Rows[0].ItemArray;
                ddl = new SingleColumnEntity()
                {
                    DataValueField1 = values[0].ToString(),
                    //DataTextField1 = values[1].ToString(),
                    DataValueField2 = values.Count() > 1 ? values[1].ToString() : null,
                    DataValueField3 = values.Count() > 2 ? values[2].ToString() : null,
                    DataValueField4 = values.Count() > 3 ? values[3].ToString() : null,
                    DataValueField5 = values.Count() > 4 ? values[4].ToString() : null,
                    DataValueField6 = values.Count() > 5 ? values[5].ToString() : null,
                    DataValueField7 = values.Count() > 6 ? values[6].ToString() : null,
                    DataValueField8 = values.Count() > 7 ? values[7].ToString() : null,
                    //DataTextField2 = values.Count() > 3 ? values[3].ToString() : null,
                    //DataTextField3 = values.Count() > 5 ? values[5].ToString() : null,
                    //DataTextField4 = values.Count() > 7 ? values[7].ToString() : null,
                    //DataCodeField = values.Count() > 2 ? values[2].ToString() : null

                };
                list.Add(ddl);

            }

            return list;
        }

        public List<TableRecordsEntity> GetTableDataList(string ListType, string TableName, string Column1 = null, string Param1 = null, string Column2 = null, string Param2 = null, string Column3 = null, string Param3 = null, string Column4 = null, string Param4 = null, string Column5 = null, string Param5 = null, string RequiredColumn1 = null, string RequiredColumn2 = null, string RequiredColumn3 = null, string RequiredColumn4 = null, string RequiredColumn5 = null, string RequiredColumn6 = null, string RequiredColumn7 = null, string RequiredColumn8 = null, string RequiredColumn9 = null, string RequiredColumn10 = null)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetTableDataList"];

            parms.Add("@ListType", ListType);
            parms.Add("@TableName", TableName);

            parms.Add("@RequiredColumn1", RequiredColumn1);
            parms.Add("@RequiredColumn2", RequiredColumn2);
            parms.Add("@RequiredColumn3", RequiredColumn3);
            parms.Add("@RequiredColumn4", RequiredColumn4);
            parms.Add("@RequiredColumn5", RequiredColumn5);
            parms.Add("@RequiredColumn6", RequiredColumn6);
            parms.Add("@RequiredColumn7", RequiredColumn7);
            parms.Add("@RequiredColumn8", RequiredColumn8);
            parms.Add("@RequiredColumn9", RequiredColumn9);
            parms.Add("@RequiredColumn10", RequiredColumn10);

            parms.Add("@DataColumn1", Column1);
            parms.Add("@DataColumn2", Column2);
            parms.Add("@DataColumn3", Column3);
            parms.Add("@DataColumn4", Column4);
            parms.Add("@DataColumn5", Column5);

            parms.Add("@DataParam1", Param1);
            parms.Add("@DataParam2", Param2);
            parms.Add("@DataParam3", Param3);
            parms.Add("@DataParam4", Param4);
            parms.Add("@DataParam5", Param5);


            return DataTableToTableList(_dbAccess.GetDataTable(spName, parms));
        }

        //public List<TableRecordsEntity> GetTableDataList(TableRecordsEntity objTbl)
        //{
        //    var parms = new Dictionary<string, object>();
        //    var spName = ConfigurationManager.AppSettings["GetTableDataList"];

        //    parms.Add("@ListType", objTbl.ListType);
        //    parms.Add("@TableName", objTbl.TableName);

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        if (!string.IsNullOrEmpty(objTbl.DataParamField))
        //        {
        //            parms.Add("@DataParamField" + i + "", objTbl.DataParamField + i);
        //            parms.Add("@DataParamValueField" + i + "", objTbl.DataParamValueField + i);
        //        }

        //    }

        //    for (int i = 1; i <= 10; i++)
        //    {
        //        //if (!string.IsNullOrEmpty(objTbl.DataStringColumnField))
        //        //{
        //        //    parms.Add("@DataStringColumnField" + i + "", objTbl.DataStringColumnField + i);
        //        //}
        //    }

        //    return DataTableToTableList(_dbAccess.GetDataTable(spName, parms));
        //}

        public List<TableRecordsEntity> DataTableToTableList(DataTable table)
        {
            var list = new List<TableRecordsEntity>(table.Rows.Count);
            var ddl = new TableRecordsEntity()
            {
                //DataValueField = -1,
                //DataTextField = "Select"
            };
            //list.Add(ddl);
            foreach (DataRow dr in table.Rows)
            {
                var values = dr.ItemArray;
                ddl = new TableRecordsEntity();

                //if (dr["RequiredColumn1"] != null)
                //    ddl.RequiredColumn1 = dr["RequiredColumn1"].ToString();
                //if (dr.Table.Columns.Contains("DropDownValue"))
                //    ddl.DropDownValue = dr["DropDownValue"].ToString();
                //if (dr.Table.Columns.Contains("DropDownText"))
                //    ddl.DropDownText = dr["DropDownText"].ToString();

                if (dr.Table.Columns.Contains("RequiredColumn1"))
                    ddl.RequiredColumn1 = dr["RequiredColumn1"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn2"))
                    ddl.RequiredColumn2 = dr["RequiredColumn2"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn3"))
                    ddl.RequiredColumn3 = dr["RequiredColumn3"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn4"))
                    ddl.RequiredColumn4 = dr["RequiredColumn4"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn5"))
                    ddl.RequiredColumn5 = dr["RequiredColumn5"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn6"))
                    ddl.RequiredColumn6 = dr["RequiredColumn6"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn7"))
                    ddl.RequiredColumn7 = dr["RequiredColumn7"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn8"))
                    ddl.RequiredColumn8 = dr["RequiredColumn8"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn9"))
                    ddl.RequiredColumn9 = dr["RequiredColumn9"].ToString();
                if (dr.Table.Columns.Contains("RequiredColumn10"))
                    ddl.RequiredColumn10 = dr["RequiredColumn10"].ToString();


                list.Add(ddl);
            }

            return list;
        }

        //public List<TableRecordsEntity> GetTableRecordsList(string ListType, string TableName, string DataParamField1, string DataParamValueField1, string DataColumnTextField1, string DataColumnValueField1, string DataParamField2 = null, string DataParamValueField2 = null, string DataColumnTextField2 = null, string DataColumnValueField2 = null, string DataParamField3 = null, string DataParamValueField3 = null, string DataColumnTextField3 = null, string DataColumnValueField3 = null, string DataParamField4 = null, string DataParamValueField4 = null, string DataColumnTextField4 = null, string DataColumnValueField4 = null, string DataParamField5 = null, string DataParamValueField5 = null, string DataColumnTextField5 = null, string DataColumnValueField5 = null)
        //{
        //    var parms = new Dictionary<string, object>();
        //    var spName = ConfigurationManager.AppSettings["GetTableRecordsDataList"];

        //    parms.Add("@ListType", ListType);
        //    parms.Add("@TableName", TableName);

        //    parms.Add("@DataParamField1", DataParamField1);
        //    parms.Add("@DataParamValueField1", DataParamValueField1);
        //    parms.Add("@DataColumnTextField1", DataColumnTextField1);
        //    parms.Add("@DataColumnValueField1", DataColumnValueField1);
        //    parms.Add("@DataParamField2", DataParamField2);
        //    parms.Add("@DataParamValueField2", DataParamValueField2);
        //    parms.Add("@DataColumnTextField2", DataColumnTextField2);
        //    parms.Add("@DataColumnValueField2", DataColumnValueField2);
        //    parms.Add("@DataParamField3", DataParamField3);
        //    parms.Add("@DataParamValueField3", DataParamValueField3);
        //    parms.Add("@DataColumnTextField3", DataColumnTextField3);
        //    parms.Add("@DataColumnValueField3", DataColumnValueField3);
        //    parms.Add("@DataParamField4", DataParamField4);
        //    parms.Add("@DataParamValueField4", DataParamValueField4);
        //    parms.Add("@DataColumnTextField4", DataColumnTextField4);
        //    parms.Add("@DataColumnValueField4", DataColumnValueField4);
        //    parms.Add("@DataParamField5", DataParamField5);
        //    parms.Add("@DataParamValueField5", DataParamValueField5);
        //    parms.Add("@DataColumnTextField5", DataColumnTextField5);
        //    parms.Add("@DataColumnValueField5", DataColumnValueField5);


        //    return DataTableToTableList(_dbAccess.GetDataTable(spName, parms));
        //}

        public string DeleteFormTable(string TableName, string ColumnName1, string Param1, string ColumnName2 = null, string Param2 = null)
        {
            string msgCode = "";
            var ParamDict = new Dictionary<string, object>();
            ParamDict.Add("@TableName", TableName);
            //Params.Add("@Id", Model.Id);
            ParamDict.Add("@ColumnName1",ColumnName1);
            ParamDict.Add("@ColumnName2", ColumnName2);
            ParamDict.Add("@Param1",Param1);
            ParamDict.Add("@Param2",Param2);
            

            var SPName = ConfigurationManager.AppSettings["DeleteFromTable"];
            _dbAccess.ExecuteNonQuery(SPName, ParamDict, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public DataTable GetCommonSettings()
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            //parms.Add("@countryId", countryId);
            var spName = ConfigurationManager.AppSettings["GetCommonSettings"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public List<DropDownEntity> GetSONoQuoteNoList(string EndUse, string quoteType)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@NoType", EndUse);
            parms.Add("@quoteTypeId", quoteType);
            var spName = ConfigurationManager.AppSettings["GetSONoQuoteNoList"];
            return DataTableToStringList(_dbAccess.GetDataTable(spName, parms));
        }

        public DataTable GetDataTableForDocument(string ListType, string TableName, string[] DataColumn, string[] DataParam, string[] RequiredColumn)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetBaseDataForDocument"];

            parms.Add("@ListType", ListType);
            parms.Add("@TableName", TableName);

            if (DataColumn.ElementAtOrDefault(0) != null)
            {
                parms.Add("@DataColumn1", DataColumn[0]);
                parms.Add("@DataParam1", DataParam[0]);
            }
            if (DataColumn.ElementAtOrDefault(1) != null)
            {
                parms.Add("@DataColumn2", DataColumn[1]);
                parms.Add("@DataParam2", DataParam[1]);
            }
            if (DataColumn.ElementAtOrDefault(2) != null)
            {
                parms.Add("@DataColumn3", DataColumn[2]);
                parms.Add("@DataParam3", DataParam[2]);
            }
            if (DataColumn.ElementAtOrDefault(3) != null)
            {
                parms.Add("@DataColumn4", DataColumn[3]);
                parms.Add("@DataParam4", DataParam[3]);
            }
            if (DataColumn.ElementAtOrDefault(4) != null)
            {
                parms.Add("@DataColumn5", DataColumn[4]);
                parms.Add("@DataParam5", DataParam[4]);
            }
            
            parms.Add("@RequiredColumn1", RequiredColumn[0]);
            parms.Add("@RequiredColumn2", RequiredColumn[1]);
            if (RequiredColumn.ElementAtOrDefault(2) != null)
                parms.Add("@RequiredColumn3", RequiredColumn[2]);
            if (RequiredColumn.ElementAtOrDefault(3) != null)
                parms.Add("@RequiredColumn4", RequiredColumn[3]);
            if (RequiredColumn.ElementAtOrDefault(4) != null)
                parms.Add("@RequiredColumn5", RequiredColumn[4]);
            if (RequiredColumn.ElementAtOrDefault(5) != null)
                parms.Add("@RequiredColumn6", RequiredColumn[5]);
            if (RequiredColumn.ElementAtOrDefault(6) != null)
                parms.Add("@RequiredColumn7", RequiredColumn[6]);
            if (RequiredColumn.ElementAtOrDefault(7) != null)
                parms.Add("@RequiredColumn8", RequiredColumn[7]);
            if (RequiredColumn.ElementAtOrDefault(8) != null)
                parms.Add("@RequiredColumn9", RequiredColumn[8]);
            if (RequiredColumn.ElementAtOrDefault(9) != null)
                parms.Add("@RequiredColumn10", RequiredColumn[9]);
            if (RequiredColumn.ElementAtOrDefault(10) != null)
                parms.Add("@RequiredColumn11", RequiredColumn[10]);

            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string SaveBulkEntryDetails(BulkUploadEntity entity)
        {
            string msgCode = "";

            if (!string.IsNullOrEmpty(entity.IdentityNo.ToString()) && entity.IdentityNo != 0)
            {
                string spName = ConfigurationManager.AppSettings["DeleteFromTable"];
                var parms = new Dictionary<string, object>();
                parms.Add("@TableName", entity.DestinationTable);
                parms.Add("@ColumnName1", entity.IdentityNoColumnName);
                parms.Add("@Param1", entity.IdentityNo);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            }

            msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);

            return msgCode;

        }

    }
}
