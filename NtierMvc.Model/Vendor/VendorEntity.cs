using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Vendor
{
    public class VendorEntity
    {
        public string UserInitial  { get; set; }
    public bool? IsActive { get; set; }
        public string ipAddress { get; set; }
        public string DateOfAssociation { get; set; }  
        public string CountryId { get; set; }
        public string UnitNo { get; set; }
        public string VendorId { get; set; }
        public string VendorTypeId { get; set; }
        public string VendorType { get; set; }
        public string SupplierType { get; set; }
        public string VendorNatureId { get; set; }
        public string VendorNature { get; set; }
        public string FunctionAreaId { get; set; }
        public string FunctionArea { get; set; }
        public string VendorName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
     //   public string STATE { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string mob1 { get; set; }
        public string mob2 { get; set; }
        public string fax { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string ContactPerson { get; set; }
        public string Designation { get; set; }
        public string VendorInitial { get; set; }
        public int TotalCount { get; set; }
        public string SupplierName { get; set; }
        public string Status { get; set; }
        public String Documents { get; set; }
        public string BankName { get; set; }
      public string BankBranch { get; set; }
        public string BankAddress1 { get; set; }
        public string BankAddress2 { get; set; }
        public string BankCountry { get; set; }
        public string BankState { get; set; }
        public string BankCity { get; set; }
        public string BankZipCode { get; set; }
        public string BankIFSCCode { get; set; }
        public string BankRTGSCode { get; set; }
        public string IGSTCode { get; set; }
        public string PanNo { get; set; }
        //public string VendorStatus { get; set; }
        //public int Id { get; set; }

    }



    public class DocumentModel
    {
        public string VendorId { get; set; }
        public string Documents { get; set; }


    }
    public class SearchModel
    {
        public string SearchVendorType { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string SearchVendorName { get; set; }
        public string SearchVendorCountry { get; set; }
        public string SupplierType { get; set; }


    }
    public class VendorEntityDetails
    {
        public VendorEntity vendEnt { get; set; }
        public List<VendorEntity> LstVendEnt { get; set; }
        public int totalcount { get; set; }
        public VendorEntityDetails()
        {
            vendEnt = new VendorEntity();
            LstVendEnt = new List<VendorEntity>();
        }
    }

}
