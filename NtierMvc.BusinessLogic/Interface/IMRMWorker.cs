using NtierMvc.Model;
using NtierMvc.Model.MRM;
using System.Collections.Generic;
using System.Data;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IMRMWorker
    {
        PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model);
        PRDetailEntity GetSavedPRDetailsPopup(PRDetailEntity Model);
        string SavePRDetailsList(BulkUploadEntity bEntity);

        PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null);

        List<PRDetailEntity> GetPRTableDetails(string PRSetno);

        string UpdateApproveReject(string[] param);
        string SavePurchaseDetails(string[] param);
        DataTable GetPRListForDocument(string PRSetNo);
        DataTable GetPRDataForDocument(string PRSetNo);
        string SavePODetailsList(BulkUploadEntity bEntity);

    }
}
