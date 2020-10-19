using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using NtierMvc.Common;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.DataAccess.Common;
using System.Data.Common;
using NtierMvc.Model.HR;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository : IDisposable
    {
        public string SaveEmployeeDetails(EmployeeEntity objEmp)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            string msgCode = "";

            parms.Add("@EmployeeId", objEmp.Id == 0 ? 0 : objEmp.Id);
            parms.Add("@userinitial", objEmp.UserInitial);
            parms.Add("@unitno", objEmp.UnitNo);
            parms.Add("@EmpType", objEmp.EmpType);
            parms.Add("@Unit", objEmp.Unit);
            parms.Add("@Dept", objEmp.Dept);
            parms.Add("@IdCard", objEmp.IdCard);
            parms.Add("@EmpInitial", objEmp.EmpInitial);
            parms.Add("@GenderTitle", objEmp.GenderTitleID);
            parms.Add("@EmpName", objEmp.EmpName);
            parms.Add("@Gender", objEmp.Gender);
            parms.Add("@GUARDIANNAME", objEmp.GaurdianName);
            parms.Add("@BloodGroup", objEmp.BloodGroup);
            parms.Add("@BloodGroupType", objEmp.BloodGroupType);
            parms.Add("@Designation", objEmp.Designation);
            parms.Add("@DOB", objEmp.DateOfBirth);
            parms.Add("@DOJ", objEmp.DateOfJoining);
            parms.Add("@address1", objEmp.Address1);
            parms.Add("@address2", objEmp.Address2);
            parms.Add("@City", objEmp.City);
            parms.Add("@state", objEmp.State);
            parms.Add("@zipcode", objEmp.ZipCode);
            parms.Add("@mob1", objEmp.mob1);
            parms.Add("@mob2", objEmp.mob2);
            parms.Add("@email", objEmp.email);
            parms.Add("@EMERGCONTPERSON", objEmp.EmergContPerson);
            parms.Add("@EMERGCONTNO", objEmp.EmergContNo);
            parms.Add("@EduDeg", objEmp.EduDeg);
            parms.Add("@EduPG", objEmp.EduPG);
            parms.Add("@ProfQual", objEmp.ProfQual);
            parms.Add("@TechQual", objEmp.TechQual);
            parms.Add("@PassportNo", objEmp.PassportNo);
            parms.Add("@PassportDate", objEmp.PassportDate);
            parms.Add("@PassportExp", objEmp.PassportExp);
            parms.Add("@DlNo", objEmp.DlNo);
            parms.Add("@DlExp", objEmp.DlExp);
            parms.Add("@PanNo", objEmp.PanNo);
            parms.Add("@AadharNo", objEmp.AadharNo);
            parms.Add("@BANKACCTNO", objEmp.BankAcctNo);
            parms.Add("@BankBranch", objEmp.BankBranch);
            parms.Add("@BankLoc", objEmp.BankLoc);
            parms.Add("@BankName", objEmp.BankName);
            parms.Add("@EmpImage", objEmp.EmpImage);
            parms.Add("@IFSCode", objEmp.IFScode);
            parms.Add("@ipAddress", objEmp.ipAddress);
            spName = ConfigurationManager.AppSettings["SaveEmployeeDetails"];

            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public string SavePayrollDetails(PayrollEntity objPay)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            string msgCode = "";

            parms.Add("@PayrollId", objPay.Id == 0 ? 0 : objPay.Id);
            parms.Add("@userinitial", objPay.UserInitial);
            parms.Add("@UnitNo", objPay.UnitNo);
            parms.Add("@empid", objPay.EmpId);
            parms.Add("@BasicPay", objPay.BasicPay);
            parms.Add("@HRA", objPay.HRA);
            parms.Add("@Conv", objPay.Conv);
            parms.Add("@OtherAllow", objPay.OtherAllow);
            parms.Add("@CarAllow", objPay.CarAllow);
            parms.Add("@EPF", objPay.EPF);
            parms.Add("@PPF", objPay.PPF);
            parms.Add("@ESI", objPay.ESI);
            parms.Add("@TDSAMT", objPay.TDSAMT);
            parms.Add("@leaveAdj", objPay.leaveAdj);
            parms.Add("@Absent", objPay.Absent);
            parms.Add("@LoanAmt", objPay.LoanAmt);
            parms.Add("@Adv", objPay.Adv);
            parms.Add("@NetPay", objPay.NetPay);
            parms.Add("@ipAddress", objPay.ipAddress);
            parms.Add("@Month", objPay.Month);
            parms.Add("@Year", objPay.Year);

            if (objPay.Month == 0 && objPay.Year == 0)
                spName = ConfigurationManager.AppSettings["SavePayrollDetails"];
            else
                spName = ConfigurationManager.AppSettings["SaveMonthlyPayrollDetails"];

            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public DataTable GetUserEmpDetails(string unitNo)
        {
            DataTable dt = new DataTable();
            var parms = new Dictionary<string, object>();
            parms.Add("@unitNo", unitNo);
            var spName = ConfigurationManager.AppSettings["GetUserCustDetails"];
            dt = _dbAccess.GetDataTable(spName, parms);
            return dt;
        }

        public DataSet GetEmployeeDetails(int pageIndex, int pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null)
        {
            var parms = new Dictionary<string, object>();
            parms.Add("@pageIndex", pageIndex);
            parms.Add("@pageSize", pageSize);
            parms.Add("@SearchEmployeeNameId", SearchEmployeeNameId);
            parms.Add("@SearchDesignation", SearchDesignation);
            parms.Add("@SearchDepartment", SearchDepartment);
            string spName = ConfigurationManager.AppSettings["GetEmployeeDetails"];
            return _dbAccess.GetDataSet(spName, parms);
        }

        public DataSet EmployeeDetailsPopup(EmployeeEntity Model)
        {
            var SPName = ConfigurationManager.AppSettings["GetEmployeeDetailsById"];
            var Params = new Dictionary<string, object>();
            Params.Add("@EmployeeId", Model.Id);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string DeleteEmployeeDetail(int EmployeeId)
        {
            string spName = ConfigurationManager.AppSettings["DeleteEmployeeDetails"];
            string msgCode = "";
            var parms = new Dictionary<string, object>();
            parms.Add("@EmployeeId", EmployeeId);
            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;

        }

        public DataSet GetEmpPayrollData(int EmpId, int Yr = 0, int mnth = 0)
        {
            var SPName = ConfigurationManager.AppSettings["GetEmpPayrollData"];
            var Params = new Dictionary<string, object>();
            Params.Add("@EmployeeId", EmpId);
            Params.Add("@Year", Yr);
            Params.Add("@Month", mnth);
            return _dbAccess.GetDataSet(SPName, Params);
        }

        public string SaveEmpLeaveDetails(LeaveManagement objPay)
        {
            var parms = new Dictionary<string, object>();
            var spName = string.Empty;
            string msgCode = "";

            parms.Add("@LeaveId", objPay.Id == 0 ? 0 : objPay.Id);
            parms.Add("@userinitial", objPay.UserInitial);
            parms.Add("@UnitNo", objPay.UnitNo);
            parms.Add("@empid", objPay.EmpId);
            parms.Add("@LeaveType", objPay.LeaveType);
            parms.Add("@LeaveStartDate", objPay.LeaveStartDate);
            parms.Add("@LeaveEndDate", objPay.LeaveEndDate);
            parms.Add("@Dept", objPay.Dept);
            parms.Add("@ipAddress", objPay.ipAddress);

            spName = ConfigurationManager.AppSettings["SaveEmpLeaveDetails"];

            _dbAccess.ExecuteNonQuery(spName, parms, "@o_MsgCode", out msgCode);
            return msgCode;
        }

        public DataSet GetEmpLeaveList(int EmpId)
        {
            var SPName = ConfigurationManager.AppSettings["GetEmpLeaveList"];
            var Params = new Dictionary<string, object>();
            Params.Add("@EmpId", EmpId);
            return _dbAccess.GetDataSet(SPName, Params);
        }
    }
}
