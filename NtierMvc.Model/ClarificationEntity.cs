using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class ClarificationEntity
    {
        public int Id { get; set; }        
        public int FinancialYear { get; set; }        
        public string QuoteType { get; set; }
        public string QuoteNo { get; set; }
        public string MailId { get; set; }
        public string Notes { get; set; }
        public string SoNo { get; set; }
        public string OrderDocName { get; set; }

    }


}
