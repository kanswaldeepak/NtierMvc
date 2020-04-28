using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Account
{
    public class ActiveSession
    {
        public string AlertMessage { get; set; }
        public string SessionId { get; set; }
        public bool IsActive { get; set; }
    }
}
