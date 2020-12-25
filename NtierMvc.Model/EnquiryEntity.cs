using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class EnquiryEntity
    {
        public int EnquiryId { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string EnqRef { get; set; }
        public string EnqDt { get; set; }
        public string EnqType { get; set; }
        public string DueDate { get; set; }
        public string MainProdGrp { get; set; }
        public string SubProdGrp { get; set; }
        public string ProdName { get; set; }
        public string EnqFor { get; set; }
        public string Eoq { get; set; }
        public int LeadTime { get; set; }
        public int LeadTimeDuration { get; set; }
        public string TimeDuration { get; set; }
        public string EndUser { get; set; }
        public string ApiMonogramRequirement { get; set; }
        public string ReasonForRegret { get; set; }
        public string DateOfReceipt { get; set; }
        public string Remarks { get; set; }
        public string EnqMode { get; set; }
        public string EnqThru { get; set; }
        public int EnqBGreq { get; set; }

        public string CityId { get; set; }
        public string StateId { get; set; }
        public string CountryId { get; set; }
        public string EnqTypeId { get; set; }
        public string AgentName { get; set; }

    }

    public class EnquiryEntityDetails
    {
        public EnquiryEntity enqEntity { get; set; }
        public List<EnquiryEntity> lstEnqEntity { get; set; }
        public int totalcount { get; set; }

        public EnquiryEntityDetails()
        {
            enqEntity = new EnquiryEntity();
            lstEnqEntity = new List<EnquiryEntity>();
        }
    }

}
