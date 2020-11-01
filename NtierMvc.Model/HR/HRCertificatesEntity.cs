using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.HR
{
    public class HRCertificatesEntity
    {
        public int Id { get; set; }        
        public string EmpId { get; set; }        
        public string EmpName { get; set; }        
        public string EmpCode { get; set; }        
        
        public string TechQual { get; set; }
        public string EduDeg { get; set; }
        public string EduPG { get; set; }
        public string ProfQual { get; set; }

        public string CertType { get; set; }
        public string CertValue { get; set; }


    }

    public class HRCertificatesEntityDetails
    {
        public HRCertificatesEntity empEnt { get; set; }
        public List<HRCertificatesEntity> ListEmployeeEnt { get; set; }
        public int totalcount { get; set; }
        public HRCertificatesEntityDetails()
        {
            empEnt = new HRCertificatesEntity();
            ListEmployeeEnt = new List<HRCertificatesEntity>();
        }
    }

}
