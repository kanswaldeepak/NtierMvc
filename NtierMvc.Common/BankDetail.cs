using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    public class BankDetail
    {
        //, BankBranch as , isnull(AccNumberLength,0) , ,    
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccNumberLength { get; set; }
        public Int32 MinAccLength { get; set; }
        public Int32 MaxAccLength { get; set; }

    }
}
