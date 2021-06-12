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
    public interface ITechnicalWorker
    {
        string SaveQuotationDetails(QuotationEntity entity);
        string SaveQuotePreparation(QuotationPreparationEntity entity);        
        QuotationEntity GetUserQuoteDetails(string unitNo);
        QuotationEntity GetVendorQuoteDetails(string vendorId);
        string DeleteQuotationDetail(int QuotationId);

        QuotationEntityDetails GetQuoteRegList(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteCustomerID = null, string SearchSubject = null, string SearchDeliveryTerms = null);

        QuotationEntity QuotationDetailsPopup(QuotationEntity Model);
        List<DropDownEntity> GetCityName(string VendorName);
        List<DropDownEntity> GetProductLineList(string productLine);
        string GetProductNumber(string productNameId, int productType);
        string GetQuoteNo(string quotetypeId=null, string finYear = null);
        List<DropDownEntity> GetQuoteNoList(string quotetypeId="", string SoNo = null);
        List<DropDownEntity> GetQuoteItemSlNoList(string quotetypeId="", string SoNo = null);
        List<DropDownEntity> GetEnqNoList(string vendorId = null);
        List<DropDownEntity> GetQuoteEnqNoList(QuotationEntity qE);
        List<ProductEntity> GetPrepProductNames(string productNameId, string productTypeId, string type = null);

        //Order Methods
        string SaveOrderDetail(OrderEntity orderEntity);
        OrderEntity GetUserOrderDetails(string unitNo);
        OrderEntity GetVendorOrderDetails(string vendorId);
        string DeleteOrderDetail(int Id);

        OrderEntityDetails GetOrderDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchCustomerID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null);

        OrderEntity OrderDetailsPopup(OrderEntity Model);
        List<DropDownEntity> GetOrderQuoteDetails(string quoteType, string quotetypeId);
        OrderEntity GetQuoteOrderDetails(string quoteType, string quotetypeId);
        ItemEntity GetOrderDetailsFromSO(string SoNoView);
        string SaveItemDetail(ItemEntity itemEntity);
        string SaveItemDetailList(BulkUploadEntity bEntity);
        
        DataTable GetDataForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId);
        DataTable GetListForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId);

        string SaveRevisedQuotationDetails(QuotationEntity entity);
        List<DropDownEntity> GetVendorDetails(string quotetypeId);
        QuotationPreparationEntity GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo, int QuotePrepId, int financialYear);
        string SaveClarificationData(ClarificationEntity cObj);
        string SaveOrderClarificationData(ClarificationEntity cEntity);
        string SaveQuoteNotes(ClarificationEntity cObj);

        string DeleteClarificationMails(string[] param);
        string DeleteOrderClarifications(string[] param);

        OrderEntity GetSoNoDetails(string soNo);

        string SaveNewDescDetail(DescEntity cObj);
        string SaveOrderNote(OrderEntity cEntity);

        List<DescEntity> LoadDescDetail(int skip, int pageSize, string sortColumn, string sortColumnDir, string search);
        List<PLEntity> LoadMasterPLlist(int skip, int pageSize, string sortColumn, string sortColumnDir, string search);
        List<QuotationPreparationEntity> LoadQuotePrepListDetails(int skip, int pageSize, string sortColumn, string sortColumnDir, string search, string quoteType = null, string quoteNo = null, string itemNo = null, string financialYear = null);
        string SaveRevisedOrderDetails(OrderEntity cEntity);
        string GetWorkAuthReport(string SoNo, string FromDate, string ToDate, string ReportType);
        List<DropDownEntity> GetItemNosForEnqs(string EnqNo);
        DataTable GetDataForContractReview(string EnqNo, string ItemNo, string type);
        string SaveContractReviewData(ContractReview entity);
        List<ItemEntity> LoadItemWiseOrders(int skip, int pageSize, string sortColumn, string sortColumnDir, string search);
        List<DropDownEntity> GetContractReviews(string customerId = null);
        List<DropDownEntity> GetQuoteItemSlNos(string quoteType, string quoteNo, string finYear);
        QuotationEntity GetQuoteNoDetailsforRevisedQuote(string quoteNoId, string quotetypeId, string financialYr);
        List<DropDownEntity> GetRevAndOriginalQuotes(string quotetypeId, string financialYr);

    }
}
