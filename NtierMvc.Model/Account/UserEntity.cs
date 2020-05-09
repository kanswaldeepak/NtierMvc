using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Account
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Password { get; set; }
        public string PasswordPhrase { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? Inactive { get; set; }
        public bool AllowLogin { get; set; }
        public bool RememberMe { get; set; }
        public string IpAddress { get; set; }
        public string SessionId { get; set; }
        public int RegistrationID { get; set; }
        public string RegistrationDate { get; set; }
        public int ProposedInstituteId { get; set; }
        public int ApplicationTypeId { get; set; }
        public string ApplicationType { get; set; }
        public string AppCode { get; set; }
        public string ApplicationStatus { get; set; }
        public bool IsApplicationEditable { get; set; }
        public List<UserRoleEntity> UserRoles { get; set; }
        public List<RolePermissionEntity> Permissions { get; set; }
        public string MobileNumber { get; set; }

        public bool isPasswordChanged { get; set; }

        public string ReferenceVtpNumber { get; set; }

        public int PermissionId { get; set; }
        public int? WorkItemId { get; set; }
        public bool IsAgencyRejected { get; set; }

        public bool IsReAssigned { get; set; }
        public bool IsGrievanceRaised { get; set; }

        public int? WorkflowInstanceId { get; set; }

        public string LoggedInEmailId { get; set; }
        public string LoggedInEmpId { get; set; }
        public string LoggedInMobileNumber { get; set; }

        public int FunctionalAreaID { get; set; }
        public int GenderID { get; set; }

        public string DateofBirth { get; set; }
        public int STDCodeM { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Designation { get; set; }
        public int STDCodeL { get; set; }
        public string LandLineNumber1 { get; set; }
        public FileUploadEntity PassportPhoto { get; set; }
        public string GenderTitleID { get; set; }
        public string DeptName { get; set; }
        public string Department { get; set; }
        public string Permission { get; set; }
        public string SignImage { get; set; }
    }

    public class UserRoleEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSysAdmin { get; set; }

        public List<RolePermissionEntity> Permissions = new List<RolePermissionEntity>();
    }

    public class RolePermissionEntity
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionRoute { get; set; }
    }
    public class ApplicationTypeDetails
    {

        public string ApplicationType { get; set; }
        public string Prefix { get; set; }
        public string Code { get; set; }
    }

    public class PagewiseAccessEntity
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int RegistrationId { get; set; }
        public bool IsEditAllowed { get; set; }
        public bool IsDeleteAllowed { get; set; }
        public bool IsViewAllowed { get; set; }
        public bool IsInspectionRemarksAllowed { get; set; }
        public string requestURL { get; set; }
        public string action { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
    }



    public class AffiliationTypeEntity
    {
        public string AffiliationType { get; set; }
        public int AffiliationTypeId { get; set; }

    }
}
