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
using NtierMvc.Model.DesignEng;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {
        public DataSet GetProductRealisationDetails(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
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
            string spName = ConfigurationManager.AppSettings["GetProductRealisationDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet PRPPopup(ProductRealisation Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetProductRealisationById"];
            var Params = new Dictionary<string, object>();
            Params.Add("@SONo", Model.SONo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public DataSet GetBOMList(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@ProductName", ProductName);
            parms.Add("@ProductCode", ProductCode);
            parms.Add("@PL", PL);
            parms.Add("@ProductNo", ProductNo);
            parms.Add("@CasingSize", CasingSize);
            parms.Add("@CasingPPF", CasingPPF);
            parms.Add("@Grade", Grade);
            parms.Add("@OpenHoleSize", OpenHoleSize);
            string spName = ConfigurationManager.AppSettings["GetBOMList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetPoSLNoDetails(string POSlNo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@POSlNo", POSlNo);
            string spName = ConfigurationManager.AppSettings["GetPOSLNoDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public string SaveBOMDetails(BOMEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@ProductName", Model.ProductName);
            Params.Add("@ProductCode", Model.ProductCode);
            Params.Add("@PL", Model.PL);
            Params.Add("@ProductNo", Model.ProductNo);
            Params.Add("@CasingSize", Model.CasingSize);
            Params.Add("@CasingPPF", Model.CasingPPF);
            Params.Add("@Grade", Model.Grade);
            Params.Add("@OpenHoleSize", Model.OpenHoleSize);
            Params.Add("@SN", Model.SN);
            Params.Add("@PartName", Model.PartName);
            Params.Add("@CommodityNo", Model.CommodityNo);
            Params.Add("@COMMRevNo", Model.COMMRevNo);
            Params.Add("@Qty", Model.Qty);
            Params.Add("@Length", Model.Length);
            Params.Add("@OD", Model.OD);
            Params.Add("@WT", Model.WT);
            Params.Add("@UOM", Model.UOM);
            Params.Add("@RMTYPE", Model.RMTYPE);
            Params.Add("@UploadFile", Model.UploadFile);

            var SPName = ConfigurationManager.AppSettings["SaveBOMDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string SaveProductRealisationDetails(ProductRealisation Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@QuoteDate", Model.QuoteDate);
            Params.Add("@SONo", Model.SONo);
            Params.Add("@VendorID", Model.VendorID);
            Params.Add("@VendorName", Model.VendorName);
            Params.Add("@PONo", Model.PONo);
            Params.Add("@PODate", Model.PODate);
            Params.Add("@ProductCode", Model.ProductCode);
            Params.Add("@ProductName", Model.ProductName);
            Params.Add("@ProductNo", Model.ProductNo);
            Params.Add("@POSlNo", Model.POSlNo);

            var SPName = ConfigurationManager.AppSettings["SaveProductRealisationDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public DataSet BillDetailsPopup(BillMonitoringEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetVendorsMasterBillDetails"];
            var Params = new Dictionary<string, object>();
            Params.Add("@BillId", Model.Id);
            return _dbAccess.GetDataSet(SPName, Params);
        }


        public string SaveBillMonitoringDetails(BillMonitoringEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@BillMonitorId", Model.Id == 0 ? 0 : Model.Id);
            Params.Add("@UserInitial", Model.UserInitial);
            Params.Add("@UnitNo", Model.UnitNo);
            Params.Add("@VendorNatureId", Model.VendorNatureId);
            Params.Add("@VendorId", Model.VendorId);
            Params.Add("@VendorName", Model.VendorName);
            Params.Add("@City", Model.City);
            Params.Add("@Type", Model.Type);
            Params.Add("@EndUse", Model.EndUse);
            Params.Add("@EndUseNo", Model.EndUseNo);
            Params.Add("@FunctionalAreaId", Model.FunctionalAreaId);
            Params.Add("@VendorPONO", Model.VendorPONO);
            Params.Add("@VendorPODate", Model.VendorPODate);
            Params.Add("@SupplyType", Model.SupplyType);
            Params.Add("@BillNo", Model.BillNo);
            Params.Add("@BillDate", Model.BillDate);
            Params.Add("@ItemDescription", Model.ItemDescription);
            Params.Add("@Uom", Model.Uom);
            Params.Add("@Qty", Model.Qty);
            Params.Add("@Currency", Model.Currency);
            Params.Add("@UnitRate", Model.UnitRate);
            Params.Add("@BillAmount", Model.BillAmount);
            Params.Add("@PaymentDueDate", Model.PaymentDueDate);
            Params.Add("@SCCNO", Model.SCCNO);
            Params.Add("@GSTPercent", Model.GSTPercent);
            Params.Add("@GSTAmount", Model.GSTAmount);
            Params.Add("@PassedBy", Model.PassedBy);
            Params.Add("@ApprovedBy", Model.ApprovedBy);
            Params.Add("@ApprovedStatus", Model.ApprovedStatus);
            Params.Add("@ApprovalDate", Model.ApprovalDate);
            Params.Add("@ControlNo", Model.ControlNo);
            Params.Add("@ForwardedTo", Model.ForwardedTo);
            Params.Add("@CostCenter", Model.CostCenter);
            Params.Add("@VehicleNo", Model.VehicleNo);
            Params.Add("@DriverName", Model.DriverName);
            Params.Add("@DriverContactNo", Model.DriverContactNo);
            Params.Add("@TimeIn", Model.TimeIn);
            Params.Add("@TimeOut", Model.TimeOut);
            Params.Add("@VehicleReleased", Model.VehicleReleased);
            Params.Add("@PONumber", Model.PONumber);

            var SPName = ConfigurationManager.AppSettings["SaveBillMonitoringDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public DataSet GetDataTablePRPData(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@DateFrom", DateFrom);
            parms.Add("@DateTo", DateTo);
            parms.Add("@ReportType", ReportType);
            parms.Add("@VendorId", VendorId);
            parms.Add("@SoNo", SoNo);
            string spName = ConfigurationManager.AppSettings["GetDataTablePRPData"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetVendorIdFromQuoteType(string ReportType=null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@ReportType", ReportType);
            string spName = ConfigurationManager.AppSettings["GetVendorIdFromQuoteType"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@QuoteType", quoteType);
            parms.Add("@QuoteNo", quoteNoId);
            string spName = ConfigurationManager.AppSettings["GetQuoteOrderDetailsForPRP"];
            return _dbAccess.GetDataSet(spName, parms);
        }

    }
}
