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

        public DataSet GetPRDetailsList(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchTypeId", SearchTypeId);
            parms.Add("@SearchQuoteNo", SearchQuoteNo);
            parms.Add("@SearchSONo", SearchSONo);
            parms.Add("@SearchVendorId", SearchVendorId);
            parms.Add("@SearchVendorName", SearchVendorName);
            parms.Add("@SearchProductGroup", SearchProductGroup);

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
                string spName = ConfigurationManager.AppSettings["DeleteRMPODetails"];
                var parms = new Dictionary<string, object>();
                parms.Add("@PRSetno", entity.IdentityNo);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            }

            msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);

            return msgCode;
        }


    }
}
