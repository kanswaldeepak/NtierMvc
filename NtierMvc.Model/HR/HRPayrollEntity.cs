using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.HR
{
    public class PayrollEntity
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string EmpId { get; set; }
        public string EmpCode { get; set; }
        public string BasicPay { get; set; }
        public string HRA { get; set; }
        public string Conv { get; set; }
        public string OtherAllow { get; set; }
        public string CarAllow { get; set; }
        public string Total { get; set; }
        public string EPF { get; set; }
        public string PPF { get; set; }
        public string ESI { get; set; }
        public string TDSAMT { get; set; }
        public string leaveAdj { get; set; }
        public string Absent { get; set; }
        public string LoanAmt { get; set; }
        public string Adv { get; set; }
        public string NetPay { get; set; }
        public string IsActive { get; set; }        
        public int TotalCount { get; set; }
        public int Dept { get; set; }
        public string EmpName { get; set; }

        public Int16 Month { get; set; }
        public int Year { get; set; }
    }

    public class PayrollEntityDetails
    {
        public PayrollEntity salEnt { get; set; }
        public List<PayrollEntity> ListSalaryEnt { get; set; }
        public int totalcount { get; set; }
        public PayrollEntityDetails()
        {
            salEnt = new PayrollEntity();
            ListSalaryEnt = new List<PayrollEntity>();
        }
    }

}
