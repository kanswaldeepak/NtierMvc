using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Application
{
    public class ApplicationComplStatusEntity
    {
        public int PromotingOrganizationComplStatus { get; set; }
        public int PromotersDetailsComplStatus { get; set; }
        public int ProposedInstituteComplStatus { get; set; }
        public int ProposedTradeUnitComplStatus { get; set; }
        public int LandComplStatus { get; set; }
        public int FundsComplStatus { get; set; }
        public int WorkshopSpaceComplStatus { get; set; }
        public int CommonFacilityComplStatus { get; set; }
        public int BuildingComplStatus { get; set; }
        public int PowerComplStatus { get; set; }
        public int InfrastructureComplStatus { get; set; }
        public int OtherInfrastructureComplStatus { get; set; }
        public int FeesComplStatus { get; set; }
        public int DeclarationComplStatus { get; set; }

        public int PastPerformanceComplStatus { get; set; }
        
        public int InspectionComplStatus { get; set; }


        public int ExistingCourseDetailsStatus { get; set; }

        public int ProposedCourseAndProgramDetailStatus { get; set; }

         
    }
}
