using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.HR
{
    public class EmployeeEntity
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string GenderTitleID { get; set; }
        public string EmpName { get; set; }
        public string EmpCode { get; set; }
        public string Dept { get; set; }
        public string Designation { get; set; }
        public string DateOfBirth { get; set; }
        public string Unit { get; set; }
        public string DateOfJoining { get; set; }        
        public string PresAddress1 { get; set; }
        public string PresAddress2 { get; set; }
        public string PresAddress3 { get; set; }
        public string Location { get; set; }
        public string PresDistrict { get; set; }        
        public string PresCity { get; set; }
        public string PresState { get; set; }
        public string PresCountry { get; set; }
        public string PresZipCode { get; set; }
        public string PerAddress1 { get; set; }
        public string PerAddress2 { get; set; }
        public string PerAddress3 { get; set; }        
        public string PerDistrict { get; set; }
        public string PerCity { get; set; }
        public string PerState { get; set; }
        public string PerCountry { get; set; }
        public string PerZipCode { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string mob1 { get; set; }
        public string mob2 { get; set; }
        public string email { get; set; }
        public string email2 { get; set; }
        public string EmergContPerson { get; set; }
        public string EmergContNo { get; set; }
        public string DateModified { get; set; }
        public string EmpImage { get; set; }
        public string EmpType { get; set; }
        public string IdCard { get; set; }
        public string EmpInitial { get; set; }
        public string Gender { get; set; }
        public string GaurdianName { get; set; }
        public string BloodGroup { get; set; }
        public string BloodGroupType { get; set; }
        public string EduDeg { get; set; }
        public string EduPG { get; set; }
        public string ProfQual { get; set; }
        public string TechQual { get; set; }
        public string PassportNo { get; set; }
        public string PassportDate { get; set; }
        public string PassportExp { get; set; }
        public string DlNo { get; set; }
        public string DlExp { get; set; }
        public string PanNo { get; set; }
        public string AadharNo { get; set; }
        public string BankAcctNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankLoc { get; set; }
        public string IFScode { get; set; }



        public int TotalCount { get; set; }

    }

    public class EmployeeEntityDetails
    {
        public EmployeeEntity empEnt { get; set; }
        public List<EmployeeEntity> ListEmployeeEnt { get; set; }
        public int totalcount { get; set; }
        public EmployeeEntityDetails()
        {
            empEnt = new EmployeeEntity();
            ListEmployeeEnt = new List<EmployeeEntity>();
        }
    }

}
