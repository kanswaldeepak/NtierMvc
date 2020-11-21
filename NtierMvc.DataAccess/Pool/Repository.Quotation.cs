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

        public DataSet GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteVendorID = null, string SearchQuoteProductGroup = null, string SearchDeliveryTerms = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchQuoteType", SearchQuoteType);
            parms.Add("@SearchQuoteVendorID", SearchQuoteVendorID);
            parms.Add("@SearchQuoteProductGroup", SearchQuoteProductGroup);
            parms.Add("@SearchDeliveryTerms", SearchDeliveryTerms);
            string spName = ConfigurationManager.AppSettings["GetQuotationDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet QuotationDetailsPopup(QuotationEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["FetchQuotationDetailsById"];
            var Params = new Dictionary<string, object>();
            Params.Add("@QuotationId", Model.Id);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveQuotationDetails(QuotationEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@QuotationId", Model.Id == 0 ? 0 : Model.Id);
            Params.Add("@UserInitial", Model.UserInitial);
            Params.Add("@UnitNo", Model.UnitNo);
            Params.Add("@VendorId", Model.CustomerId);
            Params.Add("@VendorName", Model.CustomerName);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@FileNo", Model.FileNo);
            Params.Add("@EnqRef", Model.EnqRef);
            Params.Add("@EnqNo", Model.EnqNo);
            Params.Add("@EnqDt", Model.EnqDt);
            Params.Add("@EnqFor", Model.EnqFor);
            Params.Add("@ProdGrp", Model.ProdGrp);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@GeoCode", Model.GeoCode);
            Params.Add("@QuoteDate", Model.QuoteDate);
            Params.Add("@QuoteValidity", Model.QuoteValidity);
            Params.Add("@BgReq", Model.BgReq);
            Params.Add("@Inspection", Model.Inspection);
            Params.Add("@Remarks", Model.Remarks);
            Params.Add("@Country", Model.CountryId);
            Params.Add("@Currency", Model.Currency);
            Params.Add("@Status", Model.Status);

            Params.Add("@ProductType", Model.ProductType);
            Params.Add("@DeliveryTerms", Model.DeliveryTerms);
            Params.Add("@ModeOfDespatch", Model.ModeOfDespatch);
            Params.Add("@PortOfDischarge", Model.PortOfDischarge);
            Params.Add("@DeliveryTime", Model.DeliveryTime);
            Params.Add("@PaymentTerms", Model.PaymentTerms);
            Params.Add("@SalesPerson", Model.SalesPerson);
            Params.Add("@Subject", Model.Subject);

            var SPName = ConfigurationManager.AppSettings["SaveQuotationDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string SaveQuotePreparation(QuotationPreparationEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@Id", Model.Id == 0 ? 0 : Model.Id);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@ItemNo", Model.ItemNo);
            Params.Add("@EnqSrNo", Model.EnqSrNo);
            Params.Add("@VendorName", Model.VendorName);
            Params.Add("@ProductLine", Model.ProductLine);
            Params.Add("@ProductName", Model.ProductName);
            Params.Add("@ProductNo", Model.ProductNo);
            Params.Add("@CasingSize", Model.CasingSize);
            Params.Add("@CasingPpf", Model.CasingPpf);
            Params.Add("@MaterialGrade", Model.MaterialGrade);
            Params.Add("@Connection", Model.Connection);
            Params.Add("@Qty", Model.Qty);
            Params.Add("@Uom", Model.Uom);
            Params.Add("@UnitPrice", Model.UnitPrice);
            Params.Add("@OpenHoleSize", Model.OpenHoleSize);
            Params.Add("@Currency", Model.Currency);
            Params.Add("@BallSize", Model.BallSize);
            Params.Add("@WallThickness", Model.WallThickness);

            //For Product Table
            Params.Add("@ViewProductId", Model.ViewProductId);
            Params.Add("@ViewProductName", Model.ViewProductName);
            Params.Add("@ViewProductCode", Model.ViewProductCode);
            Params.Add("@ViewPL", Model.ViewPL);
            Params.Add("@ViewProductNo", Model.ViewProductNo);
            Params.Add("@ViewPos1", Model.ViewPos1);
            Params.Add("@ViewPos2", Model.ViewPos2);
            Params.Add("@ViewPos3", Model.ViewPos3);
            Params.Add("@ViewPos4", Model.ViewPos4);
            Params.Add("@ViewPos5", Model.ViewPos5);
            Params.Add("@ViewPos6", Model.ViewPos6);
            Params.Add("@ViewPos7", Model.ViewPos7);
            Params.Add("@ViewPos8", Model.ViewPos8);
            Params.Add("@ViewPos9", Model.ViewPos9);
            Params.Add("@ViewPos10", Model.ViewPos10);
            Params.Add("@ViewDES", Model.ViewDES);
            Params.Add("@ViewProductDetails", Model.ViewProductDetails);

            var SPName = ConfigurationManager.AppSettings["SaveQuotePreparation"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string DeleteQuotationDetail(int QuotationId)
        {
            string spName = ConfigurationManager.AppSettings["DeleteQuotationDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@QuotationId", QuotationId);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataTable GetUserQuoteDetails(string unitNo)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@unitNo", unitNo);
            var spName = ConfigurationManager.AppSettings["GetUserQuoteDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetVendorQuoteDetails(string vendorId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@vendorId", vendorId);
            var spName = ConfigurationManager.AppSettings["GetVendorQuoteDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string SaveRevisedQuotationDetails(QuotationEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@UserInitial", Model.UserInitial);
            Params.Add("@UnitNo", Model.UnitNo);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@VendorId", Model.CustomerId);
            Params.Add("@VendorName", Model.CustomerName);
            Params.Add("@Country", Model.CountryId);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@QuoteDate", Model.QuoteDate);
            Params.Add("@QuoteValidity", Model.QuoteValidity);
            Params.Add("@Inspection", Model.Inspection);
            Params.Add("@RevisedQuoteNo", Model.RevisedQuoteNo);


            var SPName = ConfigurationManager.AppSettings["SaveRevisedQuotationDetails"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string DeleteClarificationMails(string[] param)
        {
            string spName = ConfigurationManager.AppSettings["DeleteClarificationMails"];
            string msgCode = "";

            List<int> numbers = new List<int>(Array.ConvertAll(param[0].Split(','), int.Parse));

            var parms = new Dictionary<string, object>();
            foreach (int num in numbers)
            {
                parms.Add("@Id", num);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
                parms.Clear();
            }

            return msgCode;
        }

        public string DeleteOrderClarifications(string[] param)
        {
            string spName = ConfigurationManager.AppSettings["DeleteOrderClarifications"];
            string msgCode = "";

            List<int> numbers = new List<int>(Array.ConvertAll(param[0].Split(','), int.Parse));

            var parms = new Dictionary<string, object>();
            foreach (int num in numbers)
            {
                parms.Add("@Id", num);
                _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
                parms.Clear();
            }

            return msgCode;
        }

        public DataTable GetDdlValueForQuote(string type, string VendorId = null, string QuoteType = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@Type", type);
            parms.Add("@VendorId", VendorId);
            parms.Add("@QuoteType", QuoteType);
            string spName = ConfigurationManager.AppSettings["GetDdlValueForQuote"];
            return _dbAccess.GetDataTable(spName, parms);
        }



        #endregion
    }
}
