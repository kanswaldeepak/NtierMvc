using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class PLEntity
    {
        public int Id { get; set; }
        public int SNo { get; set; }
        public string MainPL { get; set; }
        public string MainPLName { get; set; }
        public string SubPL { get; set; }
        public int TotalRecords { get; set; }

    }


}
