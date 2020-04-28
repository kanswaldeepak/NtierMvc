using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NtierMvc.Model.Application
{
    public class AgencyProfileEntity
    {
        public string AgencyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string AgencyType { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string Taluka { get; set; }

        public string PinCode { get; set; }

        public string EmailId { get; set; }
        public string CityName { get; set; }
        public int? DistrictCode { get; set; }

        public int? CityVillageCode { get; set; }
        public int? SubDistrictCode { get; set; }
        public string STDCode { get; set; }
        public int? WorkflowInstanceId { get; set; }



    }
    public class RequirementViewModel
    {

        public IList<BankInspectionModel> LstBank { get; set; }

        public RequirementViewModel()
        {

            LstBank = new List<BankInspectionModel>();

        }


    }
    public class BankInspectionModel
    {
        public int UniqueId { get; set; }
        public int TrnUniqueId { get; set; }
        public int FundsAvltyandReqtID { get; set; }
        public string TypeofAccount { get; set; }
        public string IFSC { get; set; }
        public string BankBranchName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? AmountInAccount { get; set; }
        public string BankStatementURL { get; set; }

        public string AccHolderName { get; set; }

        //public string BankStatementName { get; set; }

        public HttpPostedFileBase BankStatement { get; set; }
        public FileUploadEntity BankStatementFU { get; set; }

        public string BankStatementName { get; set; }


    }
    public class AgencyProfile_ViewModel
    {
        public int SubDistrictCode { get; set; }
        public int? CityVillageCode { get; set; }
        public int? StateCode { get; set; }
        public int? DistrictCode { get; set; }
        public int UserId { get; set; }
        public int AgencyProfileID { get; set; }
        public AgencyProfileEntity ProfileEntity { get; set; }
        public AgencyAuthorizedDts AuthorizedPersonDts { get; set; }

        public int? WorkflowStatusId { get; set; }
        public IList<BankInspectionModel> LstBank { get; set; }

        public AgencyProfile_ViewModel()
        {
            ProfileEntity = new AgencyProfileEntity();
            LstBank = new List<BankInspectionModel>();
            AuthorizedPersonDts = new AgencyAuthorizedDts();

        }

    }
    public class AgencyContext
    {
        public int RegistrationId { get; set; }
        public string AppCode { get; set; }
        public int AppType { get; set; }
        public int WorkFlowInstanceId { get; set; }
        public int WorkItemId { get; set; }
        public bool IsApplicationsubmitted { get; set; }
                      
    }
    public class AgencyAuthorizedDts
    {
        public int PromotersDetailsID { get; set; }
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string PassportPhotoURL { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string AadharNumber { get; set; }
        public int UserId { get; set; }
        public int TitleID { get; set; }
        public HttpPostedFileBase PassportPhoto { get; set; }
       public FileUploadEntity PassportPhotoFU { get; set; }
    }
}
