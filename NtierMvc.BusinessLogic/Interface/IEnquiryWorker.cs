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
    public interface IEnquiryWorker
    {
        string SaveEnquiryDetails(EnquiryEntity entity);
        EnquiryEntity GetUserCustDetails(string unitNo);
        string DeleteEnquiryDetail(int EnquiryId);

        EnquiryEntityDetails GetEnquiryDetails(int pageIndex, int pageSize, string SearchEQEnqType, string SearchCustomerName = null, string SearchEnqFor = null, string SearchEQDueDate = null, string SearchEOQ = null);

        EnquiryEntity EnquiryDetailsPopup(EnquiryEntity Model);
        List<DropDownEntity> GetCityName(string VendorName);
        EnquiryEntity GetVendorDetailForEnquiry(string CustomerId);
        List<DropDownEntity> GetDdlValueForEnquiry(string type, string Value=null);

    }
}
