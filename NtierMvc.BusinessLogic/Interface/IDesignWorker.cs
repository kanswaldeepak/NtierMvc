using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;
using NtierMvc.Model.DesignEng;
using System.Data;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IDesignWorker
    {
        //string DeleteEmployeeDetail(int EmpId);

        ProductRealisationDetails GetProductRealisationDetails(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null);

        List<BOMEntity> GetBOMList(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null);

        ProductRealisation PRPPopup(ProductRealisation Model);
        
        string SaveBOMDetails(BOMEntity bomE);
        string SaveProductRealisationDetails(ProductRealisation objPRP);
        GateEntryEntity BillDetailsPopup(GateEntryEntity objBill);
        string SaveBillMonitoringDetails(GateEntryEntity objBill);
        DataTable GetDataTablePRPData(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null);

        List<ProductRealisation> GetPoSLNoDetails(string POSlNo = null);
        List<DropDownEntity> GetVendorIdFromQuoteType(string ReportType=null);
        OrderEntity GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId);
    }
}
