using NtierMvc.Common;
using NtierMvc.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class QuotationPreparationEntity
    {
        public int SNo { get; set; }
        public int Id { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string QuoteType { get; set; }
        public string QuoteNo { get; set; }
        public string ItemNo { get; set; }
        public string EnqSrNo { get; set; }
        public string MainProdGrp { get; set; }
        public string SubProdGrp { get; set; }
        public string ProductName { get; set; }
        public string ProductNo { get; set; }
        public string CasingSize { get; set; }
        public string CasingPpf { get; set; }
        public string MaterialGrade { get; set; }
        public string Connection { get; set; }
        public string Qty { get; set; }
        public string Uom { get; set; }
        public string CustomerName { get; set; }
        public int OpenHoleSize { get; set; }        
        public int BallSize { get; set; }
        public decimal UnitPrice { get; set; }
        public int Currency { get; set; }
        public int CasingWeight { get; set; }
        public int RecordCount { get; set; }
        public string ipAddress { get; set; }
        public string WallThickness { get; set; }

        //For Product Table Details
        public int ViewProductId { get; set; }
        public string ViewProductName { get; set; }
        public string ViewProductCode { get; set; }
        public string ViewPL { get; set; }
        public string ViewProductNo { get; set; }
        public string ViewPos1 { get; set; }
        public string ViewPos2 { get; set; }
        public string ViewPos3 { get; set; }
        public string ViewPos4 { get; set; }
        public string ViewPos5 { get; set; }
        public string ViewPos6 { get; set; }
        public string ViewPos7 { get; set; }
        public string ViewPos8 { get; set; }
        public string ViewPos9 { get; set; }
        public string ViewPos10 { get; set; }
        public string ViewDES { get; set; }
        public string ViewProductDetails { get; set; }
        public string TotalPrice { get; set; }
        public string ODSize { get; set; }
        public string TotalBows { get; set; }
        public string PDCFeatures { get; set; }
        public string SupplyTerms { get; set; }
        public string LeadTime { get; set; }
        public string LeadTimeDuration { get; set; }
        public string PDCDrillable { get; set; }
        public int TotalRecords { get; set; }
        public int FinancialYear { get; set; }
        public string QuoteNoView { get; set; }


    }

    public class QuotationPreparationEntityDetails
    {
        public QuotationPreparationEntity quotPr { get; set; }
        public List<QuotationPreparationEntity> lstQuotePrep { get; set; }
        public int totalcount { get; set; }
        public QuotationPreparationEntityDetails()
        {
            quotPr = new QuotationPreparationEntity();
            lstQuotePrep = new List<QuotationPreparationEntity>();
        }
    }

}
