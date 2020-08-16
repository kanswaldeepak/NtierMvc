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

        public DataSet FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchPONo = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchType", SearchType);
            parms.Add("@SearchVendorNature", SearchVendorNature);
            parms.Add("@SearchVendorName", SearchVendorName);
            parms.Add("@SearchPONo", SearchPONo);

            string spName = ConfigurationManager.AppSettings["FetchInboundList"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet GetPOTableDetailsForGateEntry(string POSetno, string GateNo = null)
        {
            var SPName = ConfigurationManager.AppSettings["GetPOTableDetailsForGateEntry"];
            var Params = new Dictionary<string, object>();
            Params.Add("@POSetno", POSetno);
            Params.Add("@GateNo", GateNo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveGateEntryDetails(GateEntryEntity entity)
        {
            string msgCode = "";
            try
            {
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
            }
            catch (Exception ex)
            {
                msgCode = ex.Message;
            }

            return msgCode;
        }

        public DataSet InboundDetailsPopup(string GateNo)
        {
            var SPName = ConfigurationManager.AppSettings["GetPOTableDetailsForGateEntry"];
            var Params = new Dictionary<string, object>();
            Params.Add("@POSetno", null);
            Params.Add("@GateNo", GateNo);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public List<DropDownEntity> GetPoNoDetailsForGE()
        {
            var parms = new Dictionary<string, object>();
            //parms.Add("@NoType", EndUse);
            //parms.Add("@quoteTypeId", quoteType);
            var spName = ConfigurationManager.AppSettings["GetPoNoDetailsForGE"];
            return DataTableToStringList(_dbAccess.GetDataTable(spName, parms));
        }







        #endregion
    }
}
