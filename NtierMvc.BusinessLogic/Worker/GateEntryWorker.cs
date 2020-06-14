using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Utility;
using System.Configuration;
using System.IO;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;

namespace NtierMvc.BusinessLogic.Worker
{
    public class GateEntryWorker : IGateEntryWorker
    {
        #region Class Declarations

        private Repository _repository = new Repository();
        
        #endregion

        public string SaveGateEntry(GateEntryEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveGateEntry(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public GateEntryEntityDetails GetGateEntryList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null)
        {
            try
            {
                GateEntryEntityDetails vmbEnDetails = new GateEntryEntityDetails();
                vmbEnDetails.lstVBM = new List<GateEntryEntity>();

                DataSet ds = _repository.GetGateEntryList(pageIndex, pageSize, SearchType, SearchVendorNature, SearchVendorName, SearchBillNo, SearchBillDate, SearchItemDescription, SearchCurrency, SearchApprovalStatus);
                //DataTable dt = _repository.GetQuotationDetails();

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                GateEntryEntity obj = new GateEntryEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.Type = dr1.IsNull("Type") ? string.Empty : Convert.ToString(dr1["Type"]);
                                obj.VendorNature = dr1.IsNull("VendorNature") ? string.Empty : Convert.ToString(dr1["VendorNature"]);
                                obj.VendorName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                                obj.BillNo = dr1.IsNull("BillNo") ? string.Empty : Convert.ToString(dr1["BillNo"]);
                                obj.BillDate = dr1.IsNull("BillDate") ? string.Empty : Convert.ToString(dr1["BillDate"]);
                                obj.BillAmount = dr1.IsNull("BillAmount") ? 0 : Convert.ToInt32(dr1["BillAmount"]);
                                obj.ItemDescription = dr1.IsNull("ItemDescription") ? string.Empty : Convert.ToString(dr1["ItemDescription"]);
                                obj.Currency = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
                                obj.PaymentDueDate = dr1.IsNull("PaymentDueDate") ? string.Empty : Convert.ToString(dr1["PaymentDueDate"]);
                                obj.ControlNo = dr1.IsNull("ControlNo") ? string.Empty : Convert.ToString(dr1["ControlNo"]);
                                obj.ApprovalDate = dr1.IsNull("ApprovalDate") ? string.Empty : Convert.ToString(dr1["ApprovalDate"]);
                                obj.ApprovedStatus = dr1.IsNull("ApprovedStatus") ? string.Empty : Convert.ToString(dr1["ApprovedStatus"]);

                                vmbEnDetails.lstVBM.Add(obj);
                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                vmbEnDetails.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }

                return vmbEnDetails;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public List<GateEntryEntity> GetPOTableDetailsForGateEntry(string POSetno)
        {
            List<GateEntryEntity> newList = new List<GateEntryEntity>();

            try
            {
                DataSet ds = _repository.GetPOTableDetailsForGateEntry(POSetno);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        GateEntryEntity Model = new GateEntryEntity();

                        Model.PONo = dr1.IsNull("PONo") ? "" : Convert.ToString(dr1["PONo"]);
                        Model.POdate = dr1.IsNull("POdate") ? "" : Convert.ToString(dr1["POdate"]);
                        Model.PRCat = dr1.IsNull("PRCat") ? "" : Convert.ToString(dr1["PRCat"]);
                        Model.POValidity = dr1.IsNull("POValidity") ? "" : Convert.ToString(dr1["POValidity"]);
                        Model.WorkNo = dr1.IsNull("WorkNo") ? "" : Convert.ToString(dr1["WorkNo"]);
                        Model.GeneralCondition = dr1.IsNull("GeneralCondition") ? "" : Convert.ToString(dr1["GeneralCondition"]);
                        Model.DeliveryDate = dr1.IsNull("DeliveryDate") ? "" : Convert.ToString(dr1["DeliveryDate"]);
                        Model.QMSRequirement = dr1.IsNull("POQMSRequirement") ? "" : Convert.ToString(dr1["POQMSRequirement"]);
                        Model.Quality = dr1.IsNull("POQuality") ? "" : Convert.ToString(dr1["POQuality"]);
                        Model.PackForward = dr1.IsNull("POPackForward") ? "" : Convert.ToString(dr1["POPackForward"]);
                        Model.PORevNo = dr1.IsNull("PORevNo") ? "" : Convert.ToString(dr1["PORevNo"]);
                        Model.ModeOfPayment = dr1.IsNull("ModeOfPayment") ? "" : Convert.ToString(dr1["ModeOfPayment"]);
                        Model.ItemCategory = dr1.IsNull("ItemCategory") ? "" : Convert.ToString(dr1["ItemCategory"]);
                        Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);
                        Model.ModeOfTransport = dr1.IsNull("ModeOfTransport") ? "" : Convert.ToString(dr1["ModeOfTransport"]);
                        Model.AnyOtherRequirements = dr1.IsNull("AnyOtherRequirements") ? "" : Convert.ToString(dr1["AnyOtherRequirements"]);
                        Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.RMgrade = dr1.IsNull("RMgrade") ? "" : Convert.ToString(dr1["RMgrade"]);
                        Model.RMHardness = dr1.IsNull("RMHardness") ? "" : Convert.ToString(dr1["RMHardness"]);
                        Model.PSLlevel = dr1.IsNull("PSLlevel") ? "" : Convert.ToString(dr1["PSLlevel"]);
                        Model.QtyReqd = dr1.IsNull("QtyReqd") ? 0 : Convert.ToInt32(dr1["QtyReqd"]);
                        Model.QtyStock = dr1.IsNull("QtyStock") ? 0 : Convert.ToInt32(dr1["QtyStock"]);
                        Model.PRqty = dr1.IsNull("PRqty") ? 0 : Convert.ToInt32(dr1["PRqty"]);
                        Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                        Model.Discount = dr1.IsNull("Discount") ? "" : Convert.ToString(dr1["Discount"]);
                        Model.FinalPrice = dr1.IsNull("FinalPrice") ? "" : Convert.ToString(dr1["FinalPrice"]);


                        newList.Add(Model);
                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return newList;
        }


    }
}
