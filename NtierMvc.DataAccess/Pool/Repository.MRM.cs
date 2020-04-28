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
using NtierMvc.Model.MRM;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {

        public DataSet GetPRDetailsPopup(PRDetailEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetPRDetailsPopup"];
            var Params = new Dictionary<string, object>();
            Params.Add("@UserId", Model.UserId);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SavePRDetailsList(BulkUploadEntity entity)
        {
            string msgCode = "";
            entity.DestinationTable = "PurchaseRequest";
            msgCode = _dbAccess.BulkUpload(entity.DataRecordTable, entity.DestinationTable);


            return msgCode;
        }

    }
}
