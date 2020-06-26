using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;
using System.Data;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IGateEntryWorker
    {

        //GateEntryEntityDetails GetGateEntryList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null);

        GateEntryEntityDetails GetPOTableDetailsForGateEntry(string POSetno);
        string SaveGateEntryDetails(BulkUploadEntity bEntity);
    }
}
