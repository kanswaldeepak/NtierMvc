using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string PL { get; set; }
        public string ProductNo { get; set; }
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
        public string NetWeight { get; set; }
        public string GrossWeight { get; set; }
        public string subProductDetails { get; set; }
    }

    public class ProductAccessories
    {
        public int Id { get; set; }
        public string ProductNamePA { get; set; }
        public string ProductCodePA { get; set; }
        public string MasterProductName { get; set; }
        public string Pos1PA { get; set; }
        public string Pos2PA { get; set; }
        public string Pos3PA { get; set; }
        public string Pos4PA { get; set; }
        public string Pos5PA { get; set; }        
    }

    public class ProductDetails
    {
        public ProductEntity objPE { get; set; }
        public ProductAccessories objPA { get; set; }
        List<ProductEntity> lstPE { get; set; }
        List<ProductAccessories> lstPA { get; set; }

        public ProductDetails(bool valid = false)
        {
            if (valid)
            {
                lstPE = new List<ProductEntity>();
                lstPA = new List<ProductAccessories>();
            }
            
        }

    }

}
