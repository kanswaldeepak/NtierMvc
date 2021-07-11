using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class ContractReview
    {
        public int Id { get; set; }
        //public string Listing1 { get; set; }
        //public string MainPLId { get; set; }
        //public string SubPLId { get; set; }
        
        public string Customer { get; set; }
        public string Country { get; set; }
        public string ENQNo { get; set; }
        public string ItemNo { get; set; }
        public string FileName { get; set; }

    }


}
