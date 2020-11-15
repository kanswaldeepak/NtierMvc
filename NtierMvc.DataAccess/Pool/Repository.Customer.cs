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
            //parms.Add("@CustomerTypeId", objUser.CustomerTypeId);
            //parms.Add("@vendornatureid", objUser.VendorNatureId);
            parms.Add("@functionareaid", objUser.FunctionAreaId);
            parms.Add("@CustomerName", objUser.CustomerName);
            parms.Add("@address1", objUser.Address1);
            parms.Add("@address2", objUser.Address2);
            parms.Add("@address3", objUser.Address3);
            parms.Add("@city", objUser.City);
            parms.Add("@state", objUser.State);
            parms.Add("@country", objUser.CountryId);
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
            parms.Add("@Id", objUser.Id);
            parms.Add("@CustomerInitial", objUser.CustomerInitial);
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

        public DataSet GetCustomerDetails(int pageIndex, int pageSize, string SearchCustomerName = null, string SearchCustomerID = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchCustomerName", SearchCustomerName);
            parms.Add("@SearchCustomerID", SearchCustomerID);
            string spName = ConfigurationManager.AppSettings["GetCustomerDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet CustomerDetailsPopup(CustomerEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetCustomerDetails"];
            var Params = new Dictionary<string, object>();
            Params.Add("@CustomerId", Model.Id);
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

        public DataTable GetDdlValueForCustomer(string type, string CountryId, string CustomerId = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@Type", type);
            parms.Add("@CountryId", CountryId);
            parms.Add("@CustomerId", CustomerId);
            
            string spName = ConfigurationManager.AppSettings["GetDdlValueForCustomer"];
            return _dbAccess.GetDataTable(spName, parms);
        }





    }
}
