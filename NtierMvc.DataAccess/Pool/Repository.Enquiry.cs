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

        public DataSet GetEnquiryDetails(int pageIndex, int pageSize, string SearchEQEnqType, string SearchCustomerName = null, string SearchEnqFor = null, string SearchEQDueDate = null, string SearchEOQ = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchEQEnqType", SearchEQEnqType);
            parms.Add("@SearchCustomerName", SearchCustomerName);
            parms.Add("@SearchEnqFor", SearchEnqFor);
            parms.Add("@SearchEQDueDate", SearchEQDueDate);
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
            //Params.Add("@QuoteType", "");
            Params.Add("@VendorId", Model.CustomerId);
            Params.Add("@City", Model.City);
            Params.Add("@State", Model.StateId);
            Params.Add("@Country", Model.CountryId);
            Params.Add("@EnqRef", Model.EnqRef);
            Params.Add("@EnqDt", Model.EnqDt);
            Params.Add("@EnqType", Model.EnqTypeId);
            Params.Add("@DueDate", Model.DueDate);
            Params.Add("@MainProdGrp", Model.MainProdGrp);
            Params.Add("@SubProdGrp", Model.SubProdGrp);
            Params.Add("@ProdName", Model.ProdName);
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
            Params.Add("@AgentName", Model.AgentName);

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

        public DataTable GetDdlValueForEnquiry(string type, string EnqType = null, string CustomerId = null, string EnqFor = null, string DueDate = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@Type", type);
            parms.Add("@EnqType", EnqType);
            parms.Add("@CustomerId", CustomerId);
            parms.Add("@EnqFor", EnqFor);
            parms.Add("@DueDate", DueDate);
            string spName = ConfigurationManager.AppSettings["GetProductGroups"];
            return _dbAccess.GetDataTable(spName, parms);
        }




        #endregion
    }
}
