using System;
using System.Collections.Generic;
using System.Data;
using System.Resources;

namespace NtierMvc.Model
{
    public class BulkUploadEntity
    {
        public DataTable DataRecordTable { get; set; }
        public string DestinationTable { get; set; }
        public string TableName { get; set; }
        public string EntryType { get; set; }
        public int IdentityNo { get; set; }
        public string IdentityNoColumnName { get; set; }
    }
}