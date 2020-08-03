using NtierMvc.Model;
using NtierMvc.Model.Stores;
using System.Data;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IStoresWorker
    {

        GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null);

        GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo, string GRNo = null);
        string SaveGoodsRecieptEntryDetails(BulkUploadEntity bEntity);
        GoodsRecieptEntity GetGRDetailsPopup(string GRno=null);
        DataTable GetGoodsListDataForDocument(string GRno);
        DataTable GetGoodsDetailForDocument(string GRno);
    }
}
