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

        public string SaveGoodsRecieptEntryDetails(GoodsRecieptEntity entity)
        {
            string msgCode = "";

            string spName = ConfigurationManager.AppSettings["SaveGoodsRecieptEntryDetails"];
            var parms = new Dictionary<string, object>();
            parms.Add("@Id", string.IsNullOrEmpty(entity.Id.ToString()) ? 0 : entity.Id);
            parms.Add("@SupplyType", entity.SupplyType);
            parms.Add("@GRno", entity.GRno);
            parms.Add("@GoodRecieptNo", entity.GoodRecieptNo);
            parms.Add("@GRDate", entity.GRDate);
            parms.Add("@PRCat", entity.PRCat);
            parms.Add("@PoNo", entity.PoNo);
            parms.Add("@SupplyTerms", entity.SupplyTerms);
            parms.Add("@BatchNo", entity.BatchNo);
            parms.Add("@HeatNo", entity.HeatNo);
            parms.Add("@QtyReqd", entity.QtyReqd);
            parms.Add("@RejTeqNo", entity.RejTeqNo);
            parms.Add("@InspectionReportNo", entity.InspectionReportNo);
            parms.Add("@LocationDetail", entity.LocationDetail);
            parms.Add("@PreparedBy", entity.PreparedBy);
            parms.Add("@StoresIncharge", entity.StoresIncharge);
            parms.Add("@TestCertificationNo", entity.TestCertificationNo);
            parms.Add("@SupplierName", entity.SupplierName);
            parms.Add("@SupplierInvNo", entity.SupplierInvNo);
            parms.Add("@SupplierDate", entity.SupplierDate);
            parms.Add("@SupplierLocation", entity.SupplierLocation);
            
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public DataSet FetchGoodsRecieptList(int pageIndex, int pageSize)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);

            string spName = ConfigurationManager.AppSettings["FetchGoodsRecieptList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetGRDetailsPopup()
        {
            var SPName = ConfigurationManager.AppSettings["GetGRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            return _dbAccess.GetDataSet(SPName, Params);
        }

        #endregion
    }
}
