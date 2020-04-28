using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    [Serializable]
    public class DropDownEntity
    {
        public virtual int? DataValueField { get; set; }
        public virtual string DataStringValueField { get; set; }
        public virtual string DataTextField { get; set; }
        public virtual string DataCodeField { get; set; }
        public virtual string DataAltValueField { get; set; }
        public virtual string ValidationCode { get; set; }
        public virtual string ValidationMessage { get; set; }

    }




}
