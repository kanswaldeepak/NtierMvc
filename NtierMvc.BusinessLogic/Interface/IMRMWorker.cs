using NtierMvc.Common;
using NtierMvc.Model;
using NtierMvc.Model.MRM;
using NtierMvc.Model.Vendor;
using System.Collections.Generic;
using System.Data;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IMRMWorker
    {
        PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model);
        PRDetailEntity GetSavedPRDetailsPopup(PRDetailEntity Model);
        string SavePRDetailsList(BulkUploadEntity bEntity);
        //VendorEntityDetails GetVendorDetails(string SearchVendorType,int pageIndex, int pageSize, string SearchVendorName = null, string SearchVendorCountry = null);
        VendorEntityDetails GetVendorDetails(SearchModel model);
        string DeleteDocument(DocumentModel Documents);
        PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null);

        List<PRDetailEntity> GetPRTableDetails(string PRSetno);
        VendorEntity VendorDetailsPopup(VendorEntity Model);

        string SaveVendorDetails(VendorEntity entity);
        
        string UpdateApproveReject(string[] param);
        string SavePurchaseDetails(string[] param);
        DataTable GetPRListForDocument(string PRSetNo);
        DataTable GetPRDataForDocument(string PRSetNo);
        DataTable GetPODetailForDocument(string PRSetNo);
        DataTable GetPOListDataForDocument(string PRSetNo);
        string SavePODetailsList(BulkUploadEntity bEntity);
        PODetailEntityDetails GetPODetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string  SearchDeliveryDateTo = null);
        PODetailEntity GetPODetailsForPopup(PODetailEntity Model);
        PODetailEntity GetSavedPODetails(string POSetNo);
        List<PODetailEntity> GetPOTableDetails(string POSetNo);
        List<DropDownEntity> GetPRNoList(string DeptName);
        List<DropDownEntity> GetRMCategories(string SupplierId);
        List<DropDownEntity> GetDeliveryDates(string RMCategory);
        MRMBillMonitoringEntityDetails GetMRMDetailForGateControlNo(string GateControlNo, string BMno = null);
        MRMBillMonitoringEntity GetBillDetailsPopup(string BMno);
        int GetBillMonitoringNo();
        MRMBillMonitoringEntityDetails FetchBillMonitoringList(int pageIndex, int pageSize, string MRMSearchVendorTypeId = null, string MRMSearchSupplierId = null, string MRMSearchSupplierName = null, string MRMSearchApprovedDate = null, string MRMSearchTotalAmount = null);
    }
}
