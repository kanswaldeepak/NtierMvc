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

        public DataSet GetOrderDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchQuoteType", SearchQuoteType);
            parms.Add("@SearchCustomerID", SearchVendorID);
            parms.Add("@SearchProductGroup", SearchProductGroup);
            parms.Add("@SearchDeliveryTerms", SearchDeliveryTerms);
            string spName = ConfigurationManager.AppSettings["GetOrderDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet OrderDetailsPopup(OrderEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["FetchOrderDetailsById"];
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Model.Id);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveOrderDetails(OrderEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@OrderId", Model.Id == 0 ? 0 : Model.Id);
            Params.Add("@UserInitial", Model.UserInitial);
            Params.Add("@UnitNo", Model.UnitNo);
            //Params.Add("@VendorId", Model.VendorId);
            //Params.Add("@VendorName", Model.VendorName);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@QuoteQtyType", Model.QuoteQtyType);
            //Params.Add("@FileNo", Model.FileNo);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@QuoteDate", Model.QuoteDate);
            Params.Add("@SoNo", Model.SoNo);
            Params.Add("@PoEntity", Model.PoEntity);
            Params.Add("@PoLocation", Model.PoLocation);
            Params.Add("@PoDor", Model.PoDor);
            Params.Add("@PoNo", Model.PoNo);
            Params.Add("@PoDate", Model.PoDate);
            //Params.Add("@PoSLNo", Model.PoSLNo);
            //Params.Add("@PoItemDescription", Model.PoItemDescription);
            //Params.Add("@PoQty", Model.PoQty);
            Params.Add("@Curr", Model.Curr);
            //Params.Add("@UnitPrice", Model.UnitPrice);
            Params.Add("@PoDeliveryDate", Model.PoDeliveryDate);
            Params.Add("@PoTerms", Model.PoTerms);
            Params.Add("@SupplyTerms", Model.SupplyTerms);
            //Params.Add("@LotWiseQty", Model.LotWiseQty);
            //Params.Add("@LotWiseDate", Model.LotWiseDate);
            Params.Add("@ConsigneeName", Model.ConsigneeName);
            Params.Add("@ConsigneeLocation", Model.ConsigneeLocation);
            Params.Add("@InspectionAgency", Model.InspectionAgency);
            Params.Add("@ModeOfShipment", Model.ModeOfShipment);
            Params.Add("@PaymentTerms", Model.PaymentTerms);
            //Params.Add("@PoRequirements", Model.PoRequirements);

            Params.Add("@DeliveryTerms", Model.DeliveryTerms);
            Params.Add("@ExWorkValue", Model.ExWorkValue);
            Params.Add("@Inspection", Model.Inspection);
            Params.Add("@EndUser", Model.EndUser);
            Params.Add("@ProductGroup", Model.ProductGroup);
            Params.Add("@MultiQuoteNos", Model.MultiQuoteNos);
            //Params.Add("@UploadedFile", Model.UploadedFile);


            var SPName = ConfigurationManager.AppSettings["SaveOrderDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string DeleteOrderDetail(int Id)
        {
            string spName = ConfigurationManager.AppSettings["DeleteOrderDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@Id", Id);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataTable GetUserOrderDetails(string unitNo)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@unitNo", unitNo);
            var spName = ConfigurationManager.AppSettings["GetUserOrderDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetVendorOrderDetails(string vendorId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@vendorId", vendorId);
            var spName = ConfigurationManager.AppSettings["GetVendorOrderDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string SaveItemDetailList(BulkUploadEntity entity)
        {
            DataTable dt = new DataTable();
            string msgCode = ""; int i = 0;
            var Params = new Dictionary<string, object>();
            Params.Add("@SoNo", entity.DataRecordTable.Rows[0]["SoNo"].ToString());
            Params.Add("@PoQty", entity.DataRecordTable.Rows[0]["PoQty"].ToString());
            Params.Add("@UnitPrice", entity.DataRecordTable.Rows[0]["UnitPrice"].ToString());
            Params.Add("@QuotePrepId", entity.DataRecordTable.Rows[0]["QuotePrepId"].ToString());
            var spName = ConfigurationManager.AppSettings["GetExWorkValue"];
            i = _dbAccess.ExecuteNonQuery(spName, Params, "@o_MsgCode", out msgCode);


            if (msgCode == "Exceed")
                msgCode = "Exceeds Ex Works Value";
            else if (msgCode == "Exists")
                msgCode = "QuoteItemSlNo Already Exists In Records";
            else
            {
                entity.DestinationTable = "Items";
                msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);
            }

            return msgCode;
        }

        public string SaveItemDetails(ItemEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Model.Id == 0 ? 0 : Model.Id);
            Params.Add("@UnitNo", Model.UnitNo);
            Params.Add("@VendorId", Model.CustomerId);
            Params.Add("@VendorName", Model.CustomerName);

            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@SoNo", Model.SoNo);
            Params.Add("@QuotePrepId", Model.QuotePrepId);

            //Params.Add("@PoNo", Model.PoNo);
            //Params.Add("@PoDate", Model.PoDate);
            //Params.Add("@PoDeliveryDate", Model.PoDeliveryDate);
            Params.Add("@SupplyTerms", Model.SupplyTerms);

            Params.Add("@PoSLNo", Model.PoSLNo);
            Params.Add("@PoQty", Model.PoQty);
            Params.Add("@UnitPrice", Model.UnitPrice);

            Params.Add("@LotName", Model.LotName);
            Params.Add("@LotWiseQty", Model.LotWiseQty);
            Params.Add("@LotWiseDate", Model.LotWiseDate);
            Params.Add("@PoRequirements", Model.PoRequirements);
            Params.Add("@ipAddress", Model.ipAddress);

            var SPName = ConfigurationManager.AppSettings["SaveItemDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }
        #endregion
    }
}
