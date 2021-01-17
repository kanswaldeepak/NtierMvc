using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using NtierMvc.Common;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.DataAccess.Common;
using System.Data.Common;
using NtierMvc.Model.MRM;
using NtierMvc.Model.Vendor;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {

        public DataSet GetPRDetailsPopup(PRDetailEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetPRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@UserId", Model.UserId);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet GetSavedPRDetailsPopup(PRDetailEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetSavedPRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@PRSetno", Model.PRSetno);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet GetPRTableDetails(string PRSetno)
        {
            var SPName = ConfigurationManager.AppSettings["GetSavedPRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@PRSetno", PRSetno);
            return _dbAccess.GetDataSet(SPName, Params);
        }
        public DataSet VendorDetailsPopup(VendorEntity model)
        {
            var SPName = ConfigurationManager.AppSettings["GetVendorDetails"];
            var Params = new Dictionary<string, object>();
            Params.Add("@VendorId", model.VendorId);
            Params.Add("@pageIndex", "");
            Params.Add("@pageSize", "");
            Params.Add("@SearchVendorName", "");
            Params.Add("@SearchVendorID","");
            Params.Add("@SupplierType ", "");

            return _dbAccess.GetDataSet(SPName, Params);
        }
        public string SaveVendorDetails(VendorEntity objUser)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            string msgCode = "";

            parms.Add("@userinitial", objUser.UserInitial);
            parms.Add("@unitno", objUser.UnitNo);
            //parms.Add("@VendorTypeId", objUser.VendorTypeId);
            //parms.Add("@vendornatureid", objUser.VendorNatureId);
            parms.Add("@VendorTypeId", objUser.VendorType);
            parms.Add("@VendorName", objUser.VendorName);
            parms.Add("@address1", objUser.Address1);
            parms.Add("@address2", objUser.Address2);
            parms.Add("@address3", objUser.Address3);
            parms.Add("@city", objUser.City);
            parms.Add("@state", objUser.State);
            parms.Add("@country", objUser.CountryId);
            parms.Add("@zipcode", objUser.ZipCode);
            parms.Add("@tel1", objUser.tel1);
            parms.Add("@tel2", objUser.tel2);
            parms.Add("@mob1", objUser.mob1);
            parms.Add("@mob2", objUser.mob2);
            parms.Add("@fax", objUser.fax);
            parms.Add("@email1", objUser.email1);
            parms.Add("@email2", objUser.email2);
            parms.Add("@contactperson", objUser.ContactPerson);
            parms.Add("@designation", objUser.Designation);
            parms.Add("@Id", objUser.VendorId);
            parms.Add("@VendorInitial", objUser.VendorInitial);
            parms.Add("@DateOfAssociation", objUser.DateOfAssociation);
            parms.Add("@ipAddress", objUser.ipAddress);
            parms.Add("@Status", objUser.Status);
            parms.Add("@Documents", objUser.Documents);
            parms.Add("@SupplierType", objUser.SupplierType);
            parms.Add("@BankName", objUser.BankName);
            parms.Add("@BankBranch", objUser.BankBranch);
            parms.Add("@BankAddress1", objUser.BankAddress1);
            parms.Add("@BankAddress2", objUser.BankAddress2);
            parms.Add("@BankCountry", objUser.BankCountry);
            parms.Add("@BankState", objUser.BankState);
            parms.Add("@BankCity", objUser.BankCity);
            parms.Add("@BankZipCode", objUser.BankZipCode);
            parms.Add("@BankIFSCCode", objUser.BankIFSCCode);
            parms.Add("@BankRTGSCode", objUser.BankRTGSCode);
            parms.Add("@IGSTCode", objUser.IGSTCode);
            parms.Add("@PanNo", objUser.PanNo);
            spName = ConfigurationManager.AppSettings["SaveVendorDetails"];

            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public DataSet GetVendorDetails(SearchModel model)
        {
           // string SearchVendorType,int pageIndex, int pageSize, string SearchVendorName = null, string SearchVendorCountry = null
            var parms = new Dictionary<string, object>();

            parms.Add("@VendorId", model.SearchVendorType);
            parms.Add("@pageIndex", model.pageIndex);
            parms.Add("@pageSize", model.pageSize);
            parms.Add("@SearchVendorName", model.SearchVendorName);
            parms.Add("@SearchVendorID", model.SearchVendorCountry);
            parms.Add("@SupplierType", model.SupplierType);
            string spName = ConfigurationManager.AppSettings["GetVendorDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }


        public string DeleteDocument(DocumentModel Documents)
        {
            string msgCode = "";
            // entity.DestinationTable = "PurchaseRequest";

            if (!string.IsNullOrEmpty(Documents.VendorId) && !string.IsNullOrEmpty(Documents.Documents))
            {
                string spName = ConfigurationManager.AppSettings["DeleteDocuments"];
                var parms = new Dictionary<string, object>();
                parms.Add("@VendorId", Documents.VendorId);
                parms.Add("@Documents", Documents.Documents);
                int count = _dbAccess.ExecuteNonQuery(spName, parms);
                if (count > 0)
                    msgCode = "Deleted";
                else
                    msgCode = "error";
            }
            return msgCode;
        }

        public string SavePRDetailsList(BulkUploadEntity entity)
        {
            string msgCode = "";
            entity.DestinationTable = "PurchaseRequest";

            if (!string.IsNullOrEmpty(entity.IdentityNo.ToString()) && entity.IdentityNo != 0)
            {
                string spName = ConfigurationManager.AppSettings["DeletePurchaseRequestDetails"];
                var parms = new Dictionary<string, object>();
                parms.Add("@PRSetno", entity.IdentityNo);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            }

            msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);

            //if (entity.EntryType == "Save")
            //    msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);
            //else if (entity.EntryType == "Edit") {

            //}

            return msgCode;
        }

        public DataSet GetPRDetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchVendorTypeId", SearchVendorTypeId);
            parms.Add("@SearchSupplierId", SearchSupplierId);
            parms.Add("@SearchRMCategory", SearchRMCategory);
            parms.Add("@SearchDeliveryDateFrom", SearchDeliveryDateFrom);
            parms.Add("@SearchDeliveryDateTo", SearchDeliveryDateTo);

            string spName = ConfigurationManager.AppSettings["GetPRDetailsList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public string UpdateApproveReject(string[] param)
        {
            string spName = ConfigurationManager.AppSettings["UpdateApproveReject"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@PRSetNo", param[0]);
            parms.Add("@SignStatus", param[1]);
            parms.Add("@UserId", param[2]);
            parms.Add("@PRFavouredOn", param[3]);
            parms.Add("@PRStatus", param[4]);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public string SavePurchaseDetails(string[] param)
        {
            string spName = ConfigurationManager.AppSettings["SavePurchaseDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@PRSetNo", param[0]);
            parms.Add("@Communicate", param[1]);
            parms.Add("@PONo", param[2]);
            parms.Add("@ExpectedDeliveryDate", param[3]);
            parms.Add("@UserId", param[4]);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataTable GetPRListForDocument(string PRSetNo)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetListForPRDocument"];

            parms.Add("@PRSetNo", PRSetNo);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetPRDataForDocument(string PRSetNo)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetPRDataForDocument"];

            parms.Add("@PRSetNo", PRSetNo);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string SavePODetailsList(BulkUploadEntity entity)
        {
            string msgCode = "";
            entity.DestinationTable = "RMPO";

            if (!string.IsNullOrEmpty(entity.IdentityNo.ToString()) && entity.IdentityNo != 0)
            {
                string spName = ConfigurationManager.AppSettings["DeleteFromTable"];
                var parms = new Dictionary<string, object>();
                parms.Add("@TableName", "RMPO");
                parms.Add("@ColumnName1", "PRSetno");
                parms.Add("@Param1", entity.IdentityNo);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            }

            if (msgCode == "Deleted Successfully!")
            {
                msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);
            }
            return msgCode;

            //if (msgCode == "Duplicate")
            //{
            //    msgCode = "Duplicate Record Found";
            //    return msgCode;
            //}
            //else
            //{

            //}
        }

        public DataSet GetPODetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchVendorTypeId", SearchVendorTypeId);
            parms.Add("@SearchSupplierId", SearchSupplierId);
            parms.Add("@SearchRMCategory", SearchRMCategory);
            parms.Add("@SearchDeliveryDateFrom", SearchDeliveryDateFrom);
            parms.Add("@SearchDeliveryDateTo", SearchDeliveryDateTo);

            string spName = ConfigurationManager.AppSettings["GetPODetailsList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetPODetailsForPopup(PODetailEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetPODetailsForPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@CompShortName", Model.CompShortName);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataTable GetPODetailForDocument(string PRSetNo)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetPODetailForDocument"];

            parms.Add("@PRSetNo", PRSetNo);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetPOListDataForDocument(string PRSetNo)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetPOListDataForDocument"];

            parms.Add("@PRSetNo", PRSetNo);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataSet GetSavedPODetails(string POSetNo)
        {
            var SPName = ConfigurationManager.AppSettings["GetSavedPODetails"];
            var Params = new Dictionary<string, object>();
            Params.Add("@POSetno", POSetNo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public List<DropDownEntity> GetPRNoLists(string DeptName)
        {
            var spName = ConfigurationManager.AppSettings["GetPRNoLists"];
            var Params = new Dictionary<string, object>();
            Params.Add("@DeptName", DeptName);
            return DataTableToStringList(_dbAccess.GetDataTable(spName, Params));
        }

        public List<DropDownEntity> GetRMCategories(string SupplierId)
        {
            var spName = ConfigurationManager.AppSettings["GetRMCategories"];
            var Params = new Dictionary<string, object>();
            Params.Add("@SupplierId", SupplierId);
            return DataTableToStringList(_dbAccess.GetDataTable(spName, Params));
        }

        public List<DropDownEntity> GetDeliveryDates(string RMCategory)
        {
            var spName = ConfigurationManager.AppSettings["GetDeliveryDates"];
            var Params = new Dictionary<string, object>();
            Params.Add("@RMCategory", RMCategory);
            return DataTableToStringList(_dbAccess.GetDataTable(spName, Params));
        }

        public DataSet GetMRMDetailForGateControlNo(string GateControlNo, string BMno = null)
        {
            var SPName = ConfigurationManager.AppSettings["GetMRMDetailForGateControlNo"];
            var Params = new Dictionary<string, object>();
            Params.Add("@GateControlNo", GateControlNo);
            Params.Add("@BMno", BMno);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet GetBillDetailsPopup(string BMno)
        {
            var SPName = ConfigurationManager.AppSettings["GetBillDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@BMno", BMno);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet GetBillMonitoringNo()
        {
            var SPName = ConfigurationManager.AppSettings["GetBillMonitoringNo"];
            var Params = new Dictionary<string, object>();
            //Params.Add("@GateControlNo", GateControlNo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet FetchBillMonitoringList(int pageIndex, int pageSize, string MRMSearchVendorTypeId = null, string MRMSearchSupplierId = null, string MRMSearchSupplierName = null, string MRMSearchApprovedDate = null, string MRMSearchTotalAmount = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchVendorTypeId", MRMSearchVendorTypeId);
            parms.Add("@SearchSupplierId", MRMSearchSupplierId);
            parms.Add("@SearchSupplierName", MRMSearchSupplierName);
            parms.Add("@SearchApprovedDate", MRMSearchApprovedDate);
            parms.Add("@SearchTotalAmount", MRMSearchTotalAmount);

            string spName = ConfigurationManager.AppSettings["FetchBillMonitoringList"];
            return _dbAccess.GetDataSet(spName, parms);
        }






    }
}
