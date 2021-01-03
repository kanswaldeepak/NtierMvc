using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class AddDdlEntity
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string Value { get; set; }
        public string Nametbl { get; set; }
        public string Property { get; set; }
        public string ColumnName { get; set; }
        public string ColumnName1 { get; set; }
        public string Value1 { get; set; }

    }

    public class AddDdlEntityDetails
    {
        public EnquiryEntity enqEntity { get; set; }
        public List<AddDdlEntity> lstEnqEntity { get; set; }
        public int totalcount { get; set; }

        public AddDdlEntityDetails()
        {
            enqEntity = new EnquiryEntity();
            lstEnqEntity = new List<AddDdlEntity>();
        }
    }

}
