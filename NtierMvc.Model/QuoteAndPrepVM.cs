using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class QuoteAndPrepVM
    {
        public string VendorName { get; set; }
        public string EnqNo { get; set; }
        public string EnqDt { get; set; }
        public string ItemNo { get; set; }
        public string EnqSrNo { get; set; }
        public string Qty { get; set; }
        public string ViewProductDetails { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        //public QuotationEntity qEntity { get; set; }
        //public QuotationPreparationEntity qpEntity { get; set; }

    }
    

}
