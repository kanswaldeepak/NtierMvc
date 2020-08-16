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

        GateEntryEntityDetails FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchPONo = null);

        GateEntryEntityDetails GetPOTableDetailsForGateEntry(string POSetno, string GateNo = null);
        GateEntryEntity InboundDetailsPopup(string GateNo);
        string SaveGateEntryDetails(GateEntryEntity bEntity);
        List<DropDownEntity> GetPoNoDetailsForGE();
    }
}
