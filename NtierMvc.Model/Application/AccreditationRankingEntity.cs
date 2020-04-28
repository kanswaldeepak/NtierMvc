using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Application
{
    public class AccreditationRankingEntity
    {
        public int AccreditationRankingBodyID { get; set; }
        public string AccreditationRankingBodyName { get; set; }

        public int RegistrationId { get; set; }
        public int AccreditationRankingId { get; set; }
        public int CourseID { get; set; }

        public string Course { get; set; }
        public bool StatusOfAccreditation { get; set; }
        public int ValidityYears { get; set; }
        public string ValidityDate { get; set; }
        public int AccreditationCycleID { get; set; }

        public string AccreditationCycle { get; set; }

        public string CurrentAccreReaccreditationYear { get; set; }
        public int GradeId { get; set; }

        public string Grade { get; set; }

        public decimal RankingCGPA { get; set; }
        public int AccreditationStatusID { get; set; }

        public string AccreditationStatus { get; set; }
        public string AccreditationCertificationDate { get; set; }
        public string CertificateNumber { get; set; }
        public string AccreditationRemarks { get; set; }
        public bool GrantedCollegeOfPotentialExcellenceStatusbyUGC { get; set; }
        public string IP { get; set; }
        public int UserID { get; set; }
    }

    public class AccreditationRankingViewModel
    {
        public List<AccreditationRankingEntity> lstAccreditationRankingEntity { get; set; }
        public AccreditationRankingEntity accreditationRankingEntity { get; set; }

        public AccreditationRankingViewModel()
        {
            lstAccreditationRankingEntity = new List<AccreditationRankingEntity>();
        }
    }
}
