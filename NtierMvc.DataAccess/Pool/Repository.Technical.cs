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

        public DataTable GetProductList(string productLine)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@productLine", productLine);
            var spName = ConfigurationManager.AppSettings["GetProductListDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetProductNumber(string productNameId, int productType)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@productNameId", productNameId);
            parms.Add("@productType", productType);
            var spName = ConfigurationManager.AppSettings["GetProductNumber"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataSet GetQuoteRegList(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteCustomerID = null, string SearchSubject = null, string SearchDeliveryTerms = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchDeliveryTerms", SearchDeliveryTerms);
            parms.Add("@SearchSubject", SearchSubject);
            parms.Add("@SearchQuoteCustomerID", SearchQuoteCustomerID);
            parms.Add("@SearchQuoteType", SearchQuoteType);
            string spName = ConfigurationManager.AppSettings["GetQuoteReg"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataTable GetEnqNoList(string vendorId = "")
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@CustomerId", vendorId);
            var spName = ConfigurationManager.AppSettings["GetEnqNoList"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string GetQuoteNo(string quotetypeId = null)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@quotetypeId", quotetypeId);
            var spName = ConfigurationManager.AppSettings["GetQuoteNo"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return Convert.ToString(dt.Rows[0]["QuoteNo"]);
        }

        public DataTable GetQuoteItemSlNoList(string quotetypeId = "", string SoNo = null)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(quotetypeId))
                quotetypeId = "";
            parms.Add("@quotetypeId", quotetypeId);
            parms.Add("@SoNo", SoNo);
            var spName = ConfigurationManager.AppSettings["GetQuoteItemSlNoList"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetQuoteNoList(string quotetypeId = "", string SoNo=null)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(quotetypeId))
                quotetypeId = "";
            parms.Add("@quotetypeId", quotetypeId);
            parms.Add("@SoNo", SoNo);
            var spName = ConfigurationManager.AppSettings["GetQuoteNoList"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetQuoteEnqNoList(QuotationEntity qE)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@QuoteId", qE.Id);
            var spName = ConfigurationManager.AppSettings["FetchEnquiryNumber"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetPrepProductNames(string productId, string casingSize, string type = null)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@productId", productId);
            parms.Add("@casingSize", casingSize);
            parms.Add("@type", type);
            var spName = ConfigurationManager.AppSettings["FetchProductDetailsById"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetOrderQuoteDetails(string quoteType, string quoteNoId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(quoteType))
                quoteType = "";
            parms.Add("@quotetype", quoteType);
            parms.Add("@quoteNoId", quoteNoId);
            var spName = ConfigurationManager.AppSettings["GetOrderQuoteDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetOrderDetailsForQuotes(string quoteType, string quoteNoId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            if (string.IsNullOrEmpty(quoteType))
                quoteType = "";
            parms.Add("@quotetype", quoteType);
            parms.Add("@quoteNoId", quoteNoId);
            var spName = ConfigurationManager.AppSettings["GetOrderDetailsForQuotes"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetOrderDetailsFromSO(int SoNo)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();            
            parms.Add("@SoNo", SoNo);
            var spName = ConfigurationManager.AppSettings["GetOrderDetailsFromSO"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }


        public DataTable GetDataForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetDataForDocument"];
            parms.Add("@QuoteTypeId", quoteTypeId);
            parms.Add("@QuoteNumberId", quoteNumberId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetListForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            var parms = new Dictionary<string, object>();
            var spName = ConfigurationManager.AppSettings["GetListForDocument"];
            parms.Add("@QuoteTypeId", quoteTypeId);
            parms.Add("@QuoteNumberId", quoteNumberId);
            var dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public string SaveClarificationData(ClarificationEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@MailId", Model.MailId);
            
            var SPName = ConfigurationManager.AppSettings["SaveClarificationData"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string SaveOrderClarificationData(ClarificationEntity cEn)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@SoNo", cEn.SoNo);
            Params.Add("@OrderDocName", cEn.OrderDocName);

            var SPName = ConfigurationManager.AppSettings["SaveOrderClarificationData"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public string SaveQuoteNotes(ClarificationEntity Model)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@QuoteNo", Model.QuoteNo);
            Params.Add("@NoteMsg", Model.Notes);

            var SPName = ConfigurationManager.AppSettings["SaveQuoteNotes"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }
        #endregion
    }
}
