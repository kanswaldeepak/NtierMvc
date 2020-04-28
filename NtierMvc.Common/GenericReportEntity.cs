using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    public class GenericReportEntity
    {
        public string Heading { get; set; }
        public string Instructions { get; set; }
        public DataTable Table { get; set; }           
    }
}
