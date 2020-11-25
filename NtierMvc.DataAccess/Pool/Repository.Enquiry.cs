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

        public DataSet GetEnquiryDetails(int pageIndex, int pageSize, string SearchEnqName = null, string SearchEnqVendorID = null, string SearchProductGroup = null, string SearchMonth = null, string SearchEOQ = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchEnqName", SearchEnqName);
            parms.Add("@SearchEnqVendorID", SearchEnqVendorID);
            parms.Add("@SearchProductGroup", SearchProductGroup);
            parms.Add("@SearchMonth", SearchMonth);
            parms.Add("@SearchEOQ", SearchEOQ);
            string spName = ConfigurationManager.AppSettings["GetEnquiryDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataTable GetCityNames()
        {
            var parms = new Dictionary<string, object>();
            string spName = ConfigurationManager.AppSettings["GetCityNames"];
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataSet EnquiryDetailsPopup(EnquiryEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["FetchEnquiryDetailsById"];
            var Params = new Dictionary<string, object>();
            Params.Add("@EnquiryId", Model.EnquiryId);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveEnquiryDetails(EnquiryEntity Model)
        {
            var Params = new Dictionary<string, object>();
            string msgCode = "";

            Params.Add("@EnquiryId", Model.EnquiryId == 0 ? 0 : Model.EnquiryId);
            Params.Add("@UserInitial", Model.UserInitial);
            Params.Add("@UnitNo", Model.UnitNo);
            Params.Add("@QuoteType", Model.QuoteType);
            Params.Add("@VendorId", Model.CustomerId);
            Params.Add("@City", Model.City);
            Params.Add("@State", Model.StateId);
            Params.Add("@Country", Model.CountryId);
            Params.Add("@EnqRef", Model.EnqRef);
            Params.Add("@EnqDt", Model.EnqDt);
            Params.Add("@EnqType", Model.EnqTypeId);
            Params.Add("@DueDate", Model.DueDate);
            Params.Add("@ProdGrp", Model.ProdGrp);
            Params.Add("@EnqFor", Model.EnqFor);
            Params.Add("@EOQ", Model.Eoq);
            Params.Add("@LeadTime", Model.LeadTime);
            Params.Add("@LeadTimeDuration", Model.LeadTimeDuration);
            Params.Add("@EndUser", Model.EndUser);
            Params.Add("@ApiMonogramRequirement", Model.ApiMonogramRequirement);
            Params.Add("@ReasonForRegret", Model.ReasonForRegret);
            Params.Add("@Remarks", Model.Remarks);
            Params.Add("@EnqMode", Model.EnqMode);
            Params.Add("@EnqThru", Model.EnqThru);
            Params.Add("@EnqBGreq", Model.EnqBGreq);

            var spName = ConfigurationManager.AppSettings["SaveEnquiryDetails"];
            _dbAccess.ExecuteNonQuery(spName, Params, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public string DeleteEnquiryDetail(int EnquiryId)
        {
            string spName = ConfigurationManager.AppSettings["DeleteEnquiryDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@EnquiryId", EnquiryId);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataTable GetVendorDetailForEnquiry(string CustomerId)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@CustomerId", CustomerId);
            var spName = ConfigurationManager.AppSettings["GetVendorDetailForEnquiry"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataTable GetVendorDetails(string quotetypeId)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@quotetypeId", quotetypeId);
            string spName = ConfigurationManager.AppSettings["GetVendorIdForQuote"];
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@itemNo", itemNoId);
            parms.Add("@quoteType", quoteType);
            parms.Add("@quoteNo", quoteNo);
            string spName = ConfigurationManager.AppSettings["GetQuotePrepDetails"];
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetSoNoDetails(string soNo)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@soNo", soNo);
            string spName = ConfigurationManager.AppSettings["GetSoNoDetails"];
            return _dbAccess.GetDataTable(spName, parms);
        }

        public DataTable GetDdlValueForEnquiry(string type, string EOQId, string ProductGroup = null, string VendorId = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@Type", type);
            parms.Add("@EOQId", EOQId);
            parms.Add("@ProductGroup", ProductGroup);
            parms.Add("@VendorId", VendorId);
            string spName = ConfigurationManager.AppSettings["GetProductGroups"];
            return _dbAccess.GetDataTable(spName, parms);
        }




        #endregion
    }
}
