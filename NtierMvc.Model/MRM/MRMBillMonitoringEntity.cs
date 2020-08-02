﻿using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Model.MRM
{
    public class MRMBillMonitoringEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string GateControlNo { get; set; }
        public string VendorNatureId { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string City { get; set; }
        public string EndUse { get; set; }
        public string EndUseNo { get; set; }
        public string FunctionalAreaId { get; set; }
        public string VendorPONO { get; set; }
        public string VendorPODate { get; set; }
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string Currency { get; set; }
        public int BillAmount { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverContactNo { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string VehicleReleased { get; set; }
        public string SupplyType { get; set; }


        public string GRNo { get; set; }
        public string GRDate { get; set; }
        public string PaymentDueDate { get; set; }
        public string PassedBy { get; set; }
        public string CostCentre { get; set; }
        public string ApprovedStatus { get; set; }
        public int RejectReason { get; set; }
        public int PendingReason { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovalDate { get; set; }
        public string ForwardedTo { get; set; }

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


    public class MRMBillMonitoringEntityDetails
    {
        public MRMBillMonitoringEntity mrmEntity { get; set; }
        public List<MRMBillMonitoringEntity> lstMRMEntity { get; set; }
        public int totalcount { get; set; }
        public MRMBillMonitoringEntityDetails()
        {
            mrmEntity = new MRMBillMonitoringEntity();
            lstMRMEntity = new List<MRMBillMonitoringEntity>();
        }
    }

}
