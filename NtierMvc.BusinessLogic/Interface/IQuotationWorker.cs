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

        QuotationEntityDetails GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteVendorID = null, string SearchQuoteProductGroup = null, string SearchDeliveryTerms = null);

        QuotationEntity QuotationDetailsPopup(QuotationEntity Model);
        List<DropDownEntity> GetCityName(string VendorName);
        List<DropDownEntity> GetDdlValueForQuote(string type, string VendorId = null, string QuoteType = null);
    }
}
