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
using NtierMvc.Model.Customer;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {        
        public string SaveCustomerDetails(CustomerEntity objUser)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            string msgCode = "";

            parms.Add("@userinitial", objUser.UserInitial);
            parms.Add("@unitno", objUser.UnitNo);
            parms.Add("@vendortypeid", objUser.VendorTypeId);
            parms.Add("@vendornatureid", objUser.VendorNatureId);
            parms.Add("@functionareaid", objUser.FunctionAreaId);
            parms.Add("@vendorname", objUser.VendorName);
            parms.Add("@address1", objUser.Address1);
            parms.Add("@address2", objUser.Address2);
            parms.Add("@address3", objUser.Address3);
            parms.Add("@city", objUser.City);
            parms.Add("@state", objUser.State);
            parms.Add("@country", objUser.Country);
            parms.Add("@zipcode", objUser.ZipCode);
            parms.Add("@tel1", objUser.tel1);
            parms.Add("@tel2", objUser.tel2);
            parms.Add("@mob1", objUser.mob1);
            parms.Add("@mob2", objUser.mob2);
            parms.Add("@fax", objUser.fax);
            parms.Add("@email1", objUser.email1);
            parms.Add("@email2", objUser.email2);
            parms.Add("@contactperson", objUser.ContactPerson);
            parms.Add("@designation", objUser.Designation);
            parms.Add("@CustomerId", objUser.CustomerId);
            parms.Add("@VendorInitial", objUser.VendorInitial);
            parms.Add("@ipAddress", objUser.ipAddress);
            spName = ConfigurationManager.AppSettings["SaveCustomerDetails"];

            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public DataTable GetUserCustomerDetails(string unitNo)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@unitNo", unitNo);
            var spName = ConfigurationManager.AppSettings["GetUserCustDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataSet GetCustomerDetails(int pageIndex, int pageSize, string SearchCustName = null, string SearchCustVendorID = null, string SearchCustVendorType = null, string SearchCustVendorNature = null, string SearchCustFunctionalArea = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchCustName", SearchCustName);
            parms.Add("@SearchCustVendorID", SearchCustVendorID);
            parms.Add("@SearchCustVendorType", SearchCustVendorType);
            parms.Add("@SearchCustVendorNature", SearchCustVendorNature);
            parms.Add("@SearchCustFunctionalArea", SearchCustFunctionalArea);
            string spName = ConfigurationManager.AppSettings["GetCustomerDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet CustomerDetailsPopup(CustomerEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetCustomerDetails"];
            var Params = new Dictionary<string, object>();
            Params.Add("@CustomerId", Model.CustomerId);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string DeleteCustomerDetail(int CustomerId)
        {
            string spName = ConfigurationManager.AppSettings["DeleteCustomerDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@CustomerId", CustomerId);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataTable GetDdlValueForCustomer(string type, string VendorType = null, string VendorNatureId = null, string VendorName = null, string FunctionalArea = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@Type", type);
            parms.Add("@VendorType", VendorType);
            parms.Add("@VendorNatureId", VendorNatureId);
            parms.Add("@VendorNameId", VendorName);
            parms.Add("@FunctionalArea", FunctionalArea);
            
            string spName = ConfigurationManager.AppSettings["GetDdlValueForCustomer"];
            return _dbAccess.GetDataTable(spName, parms);
        }





    }
}
