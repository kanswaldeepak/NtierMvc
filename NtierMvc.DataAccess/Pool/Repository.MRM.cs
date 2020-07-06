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
                string spName = ConfigurationManager.AppSettings["DeleteFormTable"];
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

    }
}
