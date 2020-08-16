using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class GateEntryEntity
    {
        public int Id { get; set; }
        public string GateNo { get; set; }
        public string GateControlNo { get; set; }
        public string PONo { get; set; }
        public string POdate { get; set; }
        public string VendorPONO { get; set; }
        public string PRCat { get; set; }
        public string WorkNo { get; set; }
        public int SN { get; set; }
        public string RMdescription { get; set; }
        public int PRqty { get; set; }
        public string UOM { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string FinalPrice { get; set; }
        public string LotName { get; set; }
        public string LotDate { get; set; }
        public string LotQty { get; set; }
        public string POValidity { get; set; }
        public string PORevNo { get; set; }
        public string ItemCategory { get; set; }
        public string DeliveryDate { get; set; }
        public string ModeOfTransport { get; set; }
        public string UserInitial { get; set; }
        public string UnitNo { get; set; }        
        public string VehicleNo { get; set; }
        public string GateDate { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public bool TimeIn { get; set; }
        public bool TimeOut { get; set; }
        public string TimeInDate { get; set; }
        public string TimeOutDate { get; set; }
        public string VehicleReleased { get; set; }
        public string SupplyTerms { get; set; }
        public string ContainerNo { get; set; }
        public string SupplyType { get; set; }
        public string ViewType { get; set; }
        
    }

    public class GateEntryEntityBulkSave
    {
        public int GateNo { get; set; }
        public string GateControlNo { get; set; }
        public string VendorPONO { get; set; }
        public int SN { get; set; }
        public string RMdescription { get; set; }
        public int PRqty { get; set; }
        public string UOM { get; set; }
        public string UnitPrice { get; set; }
        public string Discount { get; set; }
        public string FinalPrice { get; set; }
        public string LotName { get; set; }
        public string LotDate { get; set; }
        public string LotQty { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string VehicleReleased { get; set; }
    }

    public class GateEntryEntityDetails
    {
        public GateEntryEntity vmbEn { get; set; }
        public List<GateEntryEntity> lstVBM { get; set; }
        public int totalcount { get; set; }
        public GateEntryEntityDetails()
        {
            vmbEn = new GateEntryEntity();
            lstVBM = new List<GateEntryEntity>();
        }
    }

}
