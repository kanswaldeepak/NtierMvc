using NtierMvc.Model.Stores;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IStoresWorker
    {

        GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize);

        GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo);
        string SaveGoodsRecieptEntryDetails(GoodsRecieptEntity bEntity);
        GoodsRecieptEntity GetGRDetailsPopup();
    }
}
