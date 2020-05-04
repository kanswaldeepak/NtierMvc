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

    }
}
