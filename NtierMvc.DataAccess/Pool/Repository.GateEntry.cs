using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;
using NtierMvc.DataAccess.Common;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {
        #region Class Methods

        public string SaveGateEntry(GateEntryEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            //Params.Add("@GateEntryId", Model.Id == 0 ? 0 : Model.Id);
            //Params.Add("@UserInitial", Model.UserInitial);
            //Params.Add("@UnitNo", Model.UnitNo);

            //Params.Add("@VendorNatureId", Model.VendorNatureId);
            //Params.Add("@VendorId", Model.VendorId);
            //Params.Add("@VendorName", Model.VendorName);
            //Params.Add("@City", Model.City);
            //Params.Add("@Type", Model.Type);
            //Params.Add("@EndUse", Model.EndUse);
            //Params.Add("@EndUseNo", Model.EndUseNo);
            //Params.Add("@FunctionalAreaId", Model.FunctionalAreaId);
            //Params.Add("@VendorPONO", Model.VendorPONO);
            //Params.Add("@VendorPODate", Model.VendorPODate);
            //Params.Add("@SupplyType", Model.SupplyType);
            //Params.Add("@BillNo", Model.BillNo);
            //Params.Add("@BillDate", Model.BillDate);
            //Params.Add("@ItemDescription", Model.ItemDescription);
            //Params.Add("@Uom", Model.Uom);
            //Params.Add("@Qty", Model.Qty);
            //Params.Add("@Currency", Model.Currency);
            //Params.Add("@UnitRate", Model.UnitRate);
            //Params.Add("@BillAmount", Model.BillAmount);
            //Params.Add("@PaymentDueDate", Model.PaymentDueDate);
            //Params.Add("@SCCNO", Model.SCCNO);
            //Params.Add("@GSTPercent", Model.GSTPercent);
            //Params.Add("@GSTAmount", Model.GSTAmount);
            //Params.Add("@PassedBy", Model.PassedBy);
            //Params.Add("@ApprovedBy", Model.ApprovedBy);
            //Params.Add("@ApprovedStatus", Model.ApprovedStatus);
            //Params.Add("@ApprovalDate", Model.ApprovalDate);
            //Params.Add("@ControlNo", Model.ControlNo);
            //Params.Add("@ForwardedTo", Model.ForwardedTo);
            //Params.Add("@CostCentre", Model.CostCentre);
            //Params.Add("@VehicleNo", Model.VehicleNo);
            //Params.Add("@DriverName", Model.DriverName);
            //Params.Add("@DriverContactNo", Model.DriverContactNo);
            //Params.Add("@TimeIn", Model.TimeIn);
            //Params.Add("@TimeOut", Model.TimeOut);
            //Params.Add("@VehicleReleased", Model.VehicleReleased);
            //Params.Add("@PONumber", Model.PONumber);

            var SPName = ConfigurationManager.AppSettings["SaveGateEntryDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        //public DataSet GetGateEntryList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null)
        //{
        //    var parms = new Dictionary<string, object>();
        //    parms.Add("@pageIndex", pageIndex);
        //    parms.Add("@pageSize", pageSize);
        //    parms.Add("@SearchType", SearchType);
        //    parms.Add("@SearchVendorNature", SearchVendorNature);
        //    parms.Add("@SearchVendorName", SearchVendorName);
        //    parms.Add("@SearchBillNo", SearchBillNo);
        //    parms.Add("@SearchBillDate", SearchBillDate);
        //    parms.Add("@SearchCurrency", SearchCurrency);
        //    parms.Add("@SearchApprovalStatus", SearchApprovalStatus);

        //    string spName = ConfigurationManager.AppSettings["GetGateEntryList"];
        //    return _dbAccess.GetDataSet(spName, parms);
        //}

        public DataSet GetPOTableDetailsForGateEntry(string POSetno)
        {
            var SPName = ConfigurationManager.AppSettings["GetPOTableDetailsForGateEntry"];
            var Params = new Dictionary<string, object>();
            Params.Add("@POSetno", POSetno);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveGateEntryDetails(GateEntryEntity entity)
        {
            string msgCode = "";

            string spName = ConfigurationManager.AppSettings["SaveGateEntryDetails"];
            var parms = new Dictionary<string, object>();
            parms.Add("@Id", string.IsNullOrEmpty(entity.Id.ToString()) ? 0 : entity.Id);
            parms.Add("@VendorPONO", entity.VendorPONO);
            parms.Add("@GateNo", entity.GateNo);
            parms.Add("@GateControlNo", entity.GateControlNo);
            parms.Add("@VehicleNo", entity.VehicleNo);
            parms.Add("@ContainerNo", entity.ContainerNo);
            parms.Add("@DriverName", entity.DriverName);
            parms.Add("@DriverContactNo", entity.DriverContactNo);
            parms.Add("@TimeIn", entity.TimeIn);
            parms.Add("@TimeOut", entity.TimeOut);
            parms.Add("@VehicleReleased", entity.VehicleReleased);
            parms.Add("@SupplyType", entity.SupplyType);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        #endregion
    }
}
