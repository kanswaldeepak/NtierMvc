using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    [Serializable]
    public class DeleteEntity
    {
        public virtual int? Id { get; set; }
        public virtual string TableName { get; set; }
        public virtual string ColumnName1 { get; set; }
        public virtual string ColumnName2 { get; set; }
        public virtual string ColumnName3 { get; set; }
        public virtual string ColumnName4 { get; set; }
        public virtual string ColumnName5 { get; set; }

        public virtual string Param1 { get; set; }
        public virtual string Param2 { get; set; }
        public virtual string Param3 { get; set; }
        public virtual string Param4 { get; set; }
        public virtual string Param5 { get; set; }


    }




}
