
using NtierMvc.Common;
using System.Collections.Generic;

namespace NtierMvc.Model
{
    public class DescEntity
    {
        public int SNo { get; set; }
        public int TotalRecords { get; set; }
        public int Id { get; set; }
        public string MainPL { get; set; }
        public string SubPL { get; set; }
        public string ProductName { get; set; }
        public string ProductNo { get; set; }
        public string FieldName { get; set; }
        public string Pos1 { get; set; }
        public string Pos2 { get; set; }
        public string Pos3 { get; set; }
        public string Pos4 { get; set; }
        public string Pos5 { get; set; }
        public string Pos6 { get; set; }
        public string Pos7 { get; set; }
        public string Pos8 { get; set; }
        public string Pos9 { get; set; }
        public string Pos10 { get; set; }
        public string DES { get; set; }
        public string DESQuery { get; set; }

        public string FieldName1 { get; set; }
        public string FieldName2 { get; set; }
        public string FieldName3 { get; set; }
        public string FieldName4 { get; set; }
        public string FieldName5 { get; set; }
        public string FieldName6 { get; set; }
        public string FieldName7 { get; set; }
        public string FieldName8 { get; set; }
        public string FieldName9 { get; set; }
        public string FieldName10 { get; set; }
    }

    public class DescDetailLists
    {
        public List<DropDownEntity> DescMailPL = new List<DropDownEntity>();
        public List<DropDownEntity> DescSubPL = new List<DropDownEntity>();
            
        public List<DropDownEntity> DescPosList = new List<DropDownEntity>();
        public List<DropDownEntity> FieldNameList = new List<DropDownEntity>();

    }

}
