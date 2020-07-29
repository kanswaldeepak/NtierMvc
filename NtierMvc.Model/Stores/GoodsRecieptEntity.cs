using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.Stores
{
    public class GoodsRecieptEntity
    {
        public int Id { get; set; }
        public int GRno { get; set; }
        public string GoodRecieptNo { get; set; }
        public string GRDate { get; set; }
        public string PRCat { get; set; }
        public string GateControlNo { get; set; }
        public string SupplierName { get; set; }
        public string SupplierInvNo { get; set; }
        public string PoNo { get; set; }
        public int POSetno { get; set; }
        public string SupplierLocation { get; set; } 
        public string SupplierDate { get; set; }
        public string PoDate { get; set; }
        public string BatchNo { get; set; }
        public string HeatNo { get; set; }
        public int QtyReqd { get; set; }
        public int RejTeqNo { get; set; }
        public string InspectionReportNo { get; set; }
        public string LocationDetail { get; set; }        
        public string PreparedBy { get; set; }
        public string StoresIncharge { get; set; }
        public string StoresInchargeSign { get; set; }
        public string TestCertificationNo { get; set; }
        public string SupplyType { get; set; }
        public string SupplyTerms { get; set; }
        public string SupplierLotNo { get; set; }
        public string StoresName { get; set; }
        public string BayNo { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public string Direction { get; set; }
        public string StoreArea { get; set; }

        public string SN { get; set; }
        public string RMdescription { get; set; }
        public string PRqty { get; set; }
        public string UOM { get; set; }
        public string UnitPrice { get; set; }
        public string Discount { get; set; }
        public string TotalPrice { get; set; }
        public string LotName { get; set; }
        public string LotDate { get; set; }
        public string LotQty { get; set; }
        public string PreparedBySign { get; set; }

    }

    public class GoodsRecieptBulkEntity
    {
        public int Id { get; set; }
        public string SupplyType { get; set; }
        public int GRno { get; set; }
        public string GoodRecieptNo { get; set; }
        public string GRDate { get; set; }
        public string PRCat { get; set; }
        public string PoNo { get; set; }
        public string POSetno { get; set; }
        public string SupplyTerms { get; set; }
        public string BatchNo { get; set; }
        public string HeatNo { get; set; }
        public string InspectionReportNo { get; set; }
        public string TestCertificationNo { get; set; }
        public string SupplierInvNo { get; set; }
        public string SupplierDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierLocation { get; set; }        
        public string SN { get; set; }        
        public string StoresName { get; set; }
        public string BayNo { get; set; }
        public string Location { get; set; }
        public string Position { get; set; }
        public string Direction { get; set; }
        public string StoreArea { get; set; }
        public string SupplierLotNo { get; set; }
        public int PreparedBy { get; set; }
        public int StoresIncharge { get; set; }

    }

    public class GoodsRecieptEntityDetails
    {
        public GoodsRecieptEntity grEntity { get; set; }
        public List<GoodsRecieptEntity> lstGREntity { get; set; }
        public int totalcount { get; set; }
        public GoodsRecieptEntityDetails()
        {
            grEntity = new GoodsRecieptEntity();
            lstGREntity = new List<GoodsRecieptEntity>();
        }
    }

}
