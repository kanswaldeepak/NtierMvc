using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;

namespace NtierMvc.Model.Application
{
    public class PromoterEntity
    {
        public int PromotersDetailsID { get; set; }
        public int RegistrationID { get; set; }
        public int? TitleID { get; set; }
        public string Title { get; set; }
        public int? GenderId { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string AadharNumber { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string EmailID { get; set; }
        public bool IsChairmanAsAuthzdRep { get; set; }
        public bool IsAuthzdRep { get; set; }
        public bool IsChairman { get; set; }
        public bool IsTrustee { get; set; }
        public string PassportPhotoURL { get; set; }
        public string Comments { get; set; }
        public FileUploadEntity PassportPhoto { get; set; }
        public int WorkflowStatusId { get; set; }
        public int WorkitemId { get; set; }
        public Int32 UpdatedBy { get; set; }
        public string TrusteeGroupIds { get; set; }
        public int OrganizationSectorId { get; set; }

        ////NEW FIELDS FOR BI-FOCAL////
        public FileUploadEntity ResolutionPertainingAuthorizedPersonFile { get; set; }
        public string ResolutionPertainingAuthorizedPersonFileUrl { get; set; }
        public string Designation { get; set; }

        public string STDCode { get; set; }
        public string LandLineNumber1 { get; set; }

    public int StatusId { get; set; }
        public string Rejection_Remark { get; set; }

        public int TrusteeStatusId { get; set; }
        public string TrusteeRejection_Remark { get; set; }

        
        public string LandLineNo1 { get; set; }


    }

    public class promotersDetails_Inspection: PromoterEntity
    {
        public int TrnPromotersDetails_InspectionId { get; set; }
        public int TrnPromotersDetailsId { get; set; }
        public int workflowInstanceId { get; set; }
        
        public int Title_StatusId { get; set; }
        public string Title_Remarks { get; set; }
        public int FirstName_StatusId { get; set; }
        public string FirstName_Remarks { get; set; }
        public int MiddleName_StatusId { get; set; }
        public string MiddleName_Remarks { get; set; }
        public int LastName_StatusId { get; set; }
        public string LastName_Remarks { get; set; }
        public int DateofBirth_StatusId { get; set; }
        public string DateofBirth_Remarks { get; set; }
        public int MobileNumber_StatusId { get; set; }
        public string MobileNumber_Remarks { get; set; }
        public int AlternativeMobileNumber_StatusId { get; set; }
        public string AlternativeMobileNumber_Remarks { get; set; }
        public int EmailID_StatusId { get; set; }
        public string EmailID_Remarks { get; set; }
        public int AadharNumber_StatusId { get; set; }
        public string AadharNumber_Remarks { get; set; }
        public int PassportPhotoURL_StatusId { get; set; }
        public string PassportPhotoURL_Remarks { get; set; }       
        public int UserId { get; set; }
        public int ModifiedBy { get; set; }

        public int StatusId { get; set; }
        public string Rejection_Remark { get; set; }

        public int Designation_StatusId { get; set; }
        public string Designation_Remarks { get; set; }
        public int TrnAuthorizedRepresentativeId { get; set; }
        public int Gender_StatusId { get; set; }
        public string Gender_Remarks { get; set;}
        public string TypeCode { get; set; }

       





        //public int workflowInstanceId { get; set; }
    }


  public class  PromotersDetails_InspectionViewModel
    {
       public promotersDetails_Inspection ChairmanDtlsModel { get; set; }
        public IList<PromoterEntity> TrusteeDtls { get; set; }
        public string Code { get; set; }
        public PromotersDetails_InspectionViewModel()
        {
            ChairmanDtlsModel = new promotersDetails_Inspection();
            TrusteeDtls = new List<PromoterEntity>();

        }
    }
}
