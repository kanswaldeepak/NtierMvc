using NtierMvc.Model;
using NtierMvc.Model.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {
        #region Class Methods

        public DataSet GetDetailForGateControlNo(string GateControlNo)
        {
            var SPName = ConfigurationManager.AppSettings["GetDetailForGateControlNo"];
            var Params = new Dictionary<string, object>();
            Params.Add("@GateControlNo", GateControlNo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveGoodsRecieptEntryDetails(BulkUploadEntity entity)
        {
            string msgCode = "";
            entity.DestinationTable = "GoodsReciept";

            if (!string.IsNullOrEmpty(entity.IdentityNo.ToString()) && entity.IdentityNo != 0)
            {
                string spName = ConfigurationManager.AppSettings["DeleteFormTable"];
                var parms = new Dictionary<string, object>();
                parms.Add("@TableName", entity.DestinationTable);
                parms.Add("@ColumnName1", "GRno");
                parms.Add("@Param1", entity.IdentityNo);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            }

            msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);

            //if (entity.EntryType == "Save")
            //    msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);
            //else if (entity.EntryType == "Edit") {

            //}

            return msgCode;

            //string msgCode = "";

            //string spName = ConfigurationManager.AppSettings["SaveGoodsRecieptEntryDetails"];
            //var parms = new Dictionary<string, object>();
            //parms.Add("@Id", string.IsNullOrEmpty(entity.Id.ToString()) ? 0 : entity.Id);
            //parms.Add("@SupplyType", entity.SupplyType);
            //parms.Add("@GRno", entity.GRno);
            //parms.Add("@GoodRecieptNo", entity.GoodRecieptNo);
            //parms.Add("@GRDate", entity.GRDate);
            //parms.Add("@PRCat", entity.PRCat);
            //parms.Add("@PoNo", entity.PoNo);
            //parms.Add("@SupplyTerms", entity.SupplyTerms);
            //parms.Add("@BatchNo", entity.BatchNo);
            //parms.Add("@HeatNo", entity.HeatNo);
            //parms.Add("@QtyReqd", entity.QtyReqd);
            //parms.Add("@RejTeqNo", entity.RejTeqNo);
            //parms.Add("@InspectionReportNo", entity.InspectionReportNo);
            //parms.Add("@LocationDetail", entity.LocationDetail);
            //parms.Add("@PreparedBy", entity.PreparedBy);
            //parms.Add("@StoresIncharge", entity.StoresIncharge);
            //parms.Add("@TestCertificationNo", entity.TestCertificationNo);
            //parms.Add("@SupplierName", entity.SupplierName);
            //parms.Add("@SupplierInvNo", entity.SupplierInvNo);
            //parms.Add("@SupplierDate", entity.SupplierDate);
            //parms.Add("@SupplierLocation", entity.SupplierLocation);
            
            //_dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);

            //return msgCode;
        }

        public DataSet FetchGoodsRecieptList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchVendorTypeId", SearchVendorTypeId);
            parms.Add("@SearchSupplierId", SearchSupplierId);
            parms.Add("@SearchRMCategory", SearchRMCategory);
            parms.Add("@SearchDeliveryDateFrom", SearchDeliveryDateFrom);
            parms.Add("@SearchDeliveryDateTo", SearchDeliveryDateTo);


            string spName = ConfigurationManager.AppSettings["FetchGoodsRecieptList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetGRDetailsPopup(string GRno=null)
        {
            var SPName = ConfigurationManager.AppSettings["GetGRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@GRno", GRno);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataTable GetGoodsListDataForDocument(string GRno)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetGoodsListDataForDocument"];

            parms.Add("@GRno", GRno);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetGoodsDetailForDocument(string GRno)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetGoodsDetailForDocument"];

            parms.Add("@GRno", GRno);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }


        #endregion
    }
}
