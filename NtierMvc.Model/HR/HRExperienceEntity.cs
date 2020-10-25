using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.HR
{
    public class HRExperienceEntity
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }        
        public int EmpId { get; set; }
        public string SN { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        
    }

    public class HRExperienceEntityBulkSave
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string SN { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }

    }

    public class HRExperienceEntityDetails
    {
        public HRExperienceEntity ExpEnt { get; set; }
        public List<HRExperienceEntity> ListExpEnt { get; set; }
        public int totalcount { get; set; }
        public HRExperienceEntityDetails()
        {
            ExpEnt = new HRExperienceEntity();
            ListExpEnt = new List<HRExperienceEntity>();
        }
    }

}
