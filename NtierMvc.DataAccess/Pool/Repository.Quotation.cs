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

        //public DataSet GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteVendorID = null, string SearchQuoteProductGroup = null, string SearchDeliveryTerms = null)
        //{
        //    var parms = new Dictionary<string, object>();
        //    parms.Add("@pageIndex", pageIndex);
        //    parms.Add("@pageSize", pageSize);
        //    parms.Add("@SearchQuoteType", SearchQuoteType);
        //    parms.Add("@SearchQuoteVendorID", SearchQuoteVendorID);
        //    parms.Add("@SearchQuoteProductGroup", SearchQuoteProductGroup);
        //    parms.Add("@SearchDeliveryTerms", SearchDeliveryTerms);
        //    string spName = ConfigurationManager.AppSettings["GetQuotationDetails"];
        //    return _dbAccess.GetDataSet(spName, parms);
        //}

        
        #endregion
    }
}
