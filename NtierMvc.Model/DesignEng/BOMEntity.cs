using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.DesignEng
{
    public class BOMEntity
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public int ProductName { get; set; }
        public int ProductCode { get; set; }
        public string PL { get; set; }
        public int ProductNo { get; set; }
        public int CasingSize { get; set; }
        public int CasingPPF { get; set; }        
        public int Grade { get; set; }
        public int OpenHoleSize { get; set; }
        public int SN { get; set; }
        public string PartName { get; set; }
        public string CommodityNo { get; set; }
        public string COMMRevNo { get; set; }
        public int Qty { get; set; }
        public decimal Length { get; set; }
        public decimal OD { get; set; }
        public decimal WT { get; set; }
        public string UploadFile { get; set; }
        public string UOM { get; set; }
        public string RMTYPE { get; set; }
        public int Inch { get; set; }
    }
}
