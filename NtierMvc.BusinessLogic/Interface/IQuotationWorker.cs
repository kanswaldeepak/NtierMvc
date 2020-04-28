using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IQuotationWorker
    {
        //string SaveQuotationDetails(QuotationEntity entity);
        QuotationEntity GetUserQuoteDetails(string unitNo);
        QuotationEntity GetVendorQuoteDetails(string vendorId);
        string DeleteQuotationDetail(int QuotationId);

        QuotationEntityDetails GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteNo = null, string SearchQuoteVendorID = null, string SearchQuoteVendorName = null, string SearchQuoteProductGroup = null, string SearchQuoteEnqFor = null);

        QuotationEntity QuotationDetailsPopup(QuotationEntity Model);
        List<DropDownEntity> GetCityName(string VendorName);
        
    }
}
