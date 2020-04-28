using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    public class ApplicationSubmissionStatusEntity
    {
        public bool PromotingOrganSubmissionStatus { get; set; }
        public bool ProposedTradeSubmissionStatus { get; set; }
        public bool ProposedInstSubmissionStatus { get; set; }
        public bool ChairmanDetailsSubmissionStatus { get; set; }
        public bool TrusteeDetailsSubmissionStatus { get; set; }
        public bool AuthorizedRepSubmissionStatus { get; set; }
        public bool DeclarationSubmissionStatus { get; set; }
        public bool FundsAvltyandSubmissionStatus { get; set; }
        public bool BuildingSubmissionStatus { get; set; }
        public bool WorkshopSubmissionStatus { get; set; }
        public bool CommonFacilitySubmissionStatus { get; set; }
        public bool PowerDetailsSubmissionStatus { get; set; }
        public bool InfrastructureSubmissionStatus { get; set; }
        public bool OtherInfrastructureSubmissionStatus { get; set; }
        public bool FeesSubmissionStatus { get; set; }
        public bool LandDetailsSubmissionStatus { get; set; }
        public bool PastPerformanceSubmissionStatus { get; set; }

    }
}
