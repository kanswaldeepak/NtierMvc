using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;

namespace NtierMvc.Model.Application
{
    public class TrusteeEntity
    {
        public int TitleID { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string MobileNumber { get; set; }
        public string AlternativeMobileNumber { get; set; }
        public string EmailID { get; set; }
        public string AadharNumber { get; set; }
        public FileUploadEntity PassportPhoto { get; set; }

       
    }
}
