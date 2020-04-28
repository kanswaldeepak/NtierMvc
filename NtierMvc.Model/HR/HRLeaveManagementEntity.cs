using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.HR
{
    public class LeaveManagement
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string EmpId { get; set; }
        public string EmpCode { get; set; }
        public string LeaveType { get; set; }
        public string LeaveStartDate { get; set; }
        public string LeaveEndDate { get; set; }
        public int Dept { get; set; }

    }

    public class LeaveManagementEntityDetails
    {
        public LeaveManagement leaveMgmt { get; set; }
        public List<LeaveManagement> ListLeaveMgmt { get; set; }
        public int totalcount { get; set; }
        public LeaveManagementEntityDetails()
        {
            leaveMgmt = new LeaveManagement();
            ListLeaveMgmt = new List<LeaveManagement>();
        }
    }

}
