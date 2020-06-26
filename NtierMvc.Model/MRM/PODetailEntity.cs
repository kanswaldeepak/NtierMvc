using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.MRM
{
    public class PODetailEntity
    {
        public int PRSetno { get; set; }
        public string POSetno { get; set; }
        public string PONo { get; set; }
        public string POdate { get; set; }
        public string PRcat { get; set; }
        public string DeptName { get; set; }
        public string WorkNo { get; set; }
        public int SN { get; set; }
        public string RMdescription { get; set; }
        public string Desc1 { get; set; }
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
        public string ApprovePerson1Sign { get; set; }
        public string ApprovePerson2Sign { get; set; }
        public string CompShortName { get; set; }
        public string SupplyTerms { get; set; }
        public string GeneralCondition { get; set; }
        public string CostCentre { get; set; }
        public string POQMSRequirement { get; set; }
        public string POQuality { get; set; }
        public string POPackForward { get; set; }
        public string ModeOfPayment { get; set; }
        public string PaymentTerms { get; set; }
        public string ModeOfTransport { get; set; }
        public string AnyOtherRequirements { get; set; }

        public string LotName { get; set; }
        public string LotDate { get; set; }
        public string LotQty { get; set; }
    }

    public class PODetailEntityBulkSave
    {
        public string POSetno { get; set; }
        public string PONo { get; set; }
        public int PRSetno { get; set; }
        public string POdate { get; set; }        
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
        public string WorkNo { get; set; }
        public string PORevNo { get; set; }
        public string ItemCategory { get; set; }
        public string DeliveryDate { get; set; }
        public string POValidity { get; set; }
        public string TotalPRSetPrice { get; set; }
        public string GeneralCondition { get; set; }
        public string POQMSRequirement { get; set; }
        public string POQuality { get; set; }
        public string POPackForward { get; set; }
        public string ModeOfPayment { get; set; }
        public string PaymentTerms { get; set; }
        public string ModeOfTransport { get; set; }
        public string AnyOtherRequirements { get; set; }
        public string LotName { get; set; }
        public string LotDate { get; set; }
        public string LotQty { get; set; }
        public string CostCentre { get; set; }
    }

    public class PODetailEntityDetails
    {
        public PODetailEntity pEntity { get; set; }
        public List<PODetailEntity> lstPOEntity { get; set; }
        public int totalcount { get; set; }
        public PODetailEntityDetails()
        {
            pEntity = new PODetailEntity();
            lstPOEntity = new List<PODetailEntity>();
        }
    }

}
