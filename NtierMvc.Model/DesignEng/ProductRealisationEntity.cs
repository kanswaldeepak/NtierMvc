using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.DesignEng
{
    public class ProductRealisation
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public string QuoteType { get; set; }
        public string SONo { get; set; }
        public string QuoteNo { get; set; }
        public string QuoteDate { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string SupplyTerms { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public string ProductName { get; set; }
        public string OpnCode { get; set; }
        public int ProductCode { get; set; }
        public string CasingSize { get; set; }
        public string CasingPPF { get; set; }
        public string Grade { get; set; }
        public string OpenHoleSize { get; set; }
        public string ProductNo { get; set; }
        public string ProductNoView { get; set; }
        public string ProductGroup { get; set; }
        public string CommName { get; set; }
        public string POSlNo { get; set; }
        public string PODeliveryDate { get; set; }
        public string PL { get; set; }
        public int SN { get; set; }
        
        public string PartName { get; set; }
        public string CommNo { get; set; }
        public string POItemDesc { get; set; }
        public int POQty { get; set; }
        public string COMMRevNo { get; set; }
        public int Qty { get; set; }
        public string Length { get; set; }
        public string OD { get; set; }
        public string WT { get; set; }
        public string UOM { get; set; }
        public string RMTYPE { get; set; }
        public string UploadFile { get; set; }
    }

    public class ProductRealisationMaster
    {
        public int Id { get; set; }
        public string ipAddress { get; set; }
        public string Type { get; set; }
        public string QuoteNo { get; set; }
        public string QuoteDate { get; set; }
        public int SONo { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
    }

    public class ProductRealisationDetails
    {
        //public EmployeeEntity empEnt { get; set; }
        public List<ProductRealisation> ListPR { get; set; }
        public int totalcount { get; set; }
        public ProductRealisationDetails()
        {
            //empEnt = new EmployeeEntity();
            ListPR = new List<ProductRealisation>();
        }
    }

}
