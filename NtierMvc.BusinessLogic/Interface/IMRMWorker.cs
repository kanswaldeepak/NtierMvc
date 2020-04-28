using NtierMvc.Model;
using NtierMvc.Model.MRM;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IMRMWorker
    {
        PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model);
        string SavePRDetailsList(BulkUploadEntity bEntity);
    }
}
