using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtierMvc.Model
{
    public class GateEntryEntity
    {
        public int Id { get; set; }


        public int PRSetno { get; set; }
        public string POSetno { get; set; }
        public string PONo { get; set; }
        public string POdate { get; set; }
        public string PRCat { get; set; }
        public string WorkNo { get; set; }
        public int SN { get; set; }
        public string RMdescription { get; set; }
        public string RMgrade { get; set; }
        public string RMHardness { get; set; }
        public string PSLlevel { get; set; }
        public int QtyReqd { get; set; }
        public int QtyStock { get; set; }
        public int PRqty { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string FinalPrice { get; set; }
        public string TotalPRSetPrice { get; set; }
        public string POValidity { get; set; }
        public string PORevNo { get; set; }
        public string ItemCategory { get; set; }
        public string DeliveryDate { get; set; }
        public int UserId { get; set; }
        public string SignStatus { get; set; }
        public string PRStatus { get; set; }
        public string ApprovePerson1 { get; set; }
        public string ApprovePerson2 { get; set; }
        
        public string GeneralCondition { get; set; }
        public string QMSRequirement { get; set; }
        public string Quality { get; set; }
        public string PackForward { get; set; }
        public string ModeOfPayment { get; set; }
        public string PaymentTerms { get; set; }
        public string ModeOfTransport { get; set; }
        public string AnyOtherRequirements { get; set; }



        public string UserInitial { get; set; }
        public string UnitNo { get; set; }
        public string VendorNatureId { get; set; }
        public string VendorNature { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string City { get; set; }
        public string Type { get; set; }
        public string EndUse { get; set; }
        public string EndUseNo { get; set; }
        public int FunctionalAreaId { get; set; }
        public string VendorPONO { get; set; }
        public string VendorPODate { get; set; }
        public string SupplyType { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public int Qty { get; set; }
        public string Currency { get; set; }
        public int UnitRate { get; set; }
        public int BillAmount { get; set; }
        public string PaymentDueDate { get; set; }
        public string SCCNO { get; set; }
        public string GSTPercent { get; set; }
        public string GSTAmount { get; set; }
        public string PassedBy { get; set; }
        public string ApprovedStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectReason { get; set; }
        public string PendingReason { get; set; }
        public string ControlNo { get; set; }
        public string ApprovalDate { get; set; }
        public string ForwardedTo { get; set; }
        public string CostCentre { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string VehicleReleased { get; set; }
        public string PONumber { get; set; }
        public string MRNNo { get; set; }
        public string MRNDate { get; set; }

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
