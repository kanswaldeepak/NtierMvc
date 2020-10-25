using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;
using NtierMvc.Model.HR;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IHrWorker
    {
        string SaveEmployeeDetails(EmployeeEntity entity);
        string SavePayrollDetails(PayrollEntity payE);
        string SaveEmpLeaveDetails(LeaveManagement leaveM);
        EmployeeEntity GetUserEmployeeDetails(string unitNo);
        string DeleteEmployeeDetail(int EmpId);

        EmployeeEntityDetails GetEmployeeDetails(int pageIndex, int pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null);

        EmployeeEntity EmployeeDetailsPopup(EmployeeEntity Model);
        PayrollEntity GetEmpPayrollData(int EmpId, int Yr = 0, int mnth = 0);
        LeaveManagementEntityDetails GetEmpLeaveList(int EmpId);
        string SaveExperienceDetailsList(BulkUploadEntity bEntity);

    }
}
