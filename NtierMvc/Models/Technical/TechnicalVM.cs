using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NtierMvc.Model;

namespace NtierMvc.Models.Technical
{
    public class TechnicalVM
    {
        public QuotationEntity qEobj { get; set; }
        public QuotationPreparationEntity qPEobj { get; set; }
    }
}