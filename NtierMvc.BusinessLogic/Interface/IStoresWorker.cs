using NtierMvc.Model;
using NtierMvc.Model.Stores;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IStoresWorker
    {

        GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null);

        GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo);
        string SaveGoodsRecieptEntryDetails(BulkUploadEntity bEntity);
        GoodsRecieptEntity GetGRDetailsPopup(string GRno=null);
    }
}
