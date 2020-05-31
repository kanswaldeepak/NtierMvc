using System;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.MRM;
using NtierMvc.Model;
using System.Collections.Generic;

namespace NtierMvc.BusinessLogic.Worker
{
    public class MRMWorker : IMRMWorker
    {
        Repository _repository = new Repository();

        public PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model)
        {
            try
            {
                DataSet ds = _repository.GetPRDetailsPopup(Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];

                    Model.DeptId = dr1.IsNull("DeptId") ? 0 : Convert.ToInt32(dr1["DeptId"]);
                    Model.PRno = dr1.IsNull("PrNo") ? "" : Convert.ToString(dr1["PrNo"]);
                    Model.DeptName = dr1.IsNull("DeptName") ? "" : Convert.ToString(dr1["DeptName"]);
                    Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public PRDetailEntity GetSavedPRDetailsPopup(PRDetailEntity Model)
        {
            try
            {
                DataSet ds = _repository.GetSavedPRDetailsPopup(Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];

                    Model.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                    Model.PRno = dr1.IsNull("PrNo") ? "" : Convert.ToString(dr1["PrNo"]);
                    Model.PRdate = dr1.IsNull("PRdate") ? "" : Convert.ToString(dr1["PRdate"]);
                    Model.ReqFrom = dr1.IsNull("ReqFrom") ? "" : Convert.ToString(dr1["ReqFrom"]);
                    Model.QuoteType = dr1.IsNull("QuoteType") ? 0 : Convert.ToInt32(dr1["QuoteType"]);
                    Model.DeptName = dr1.IsNull("DeptName") ? "" : Convert.ToString(dr1["DeptName"]);
                    Model.PRcat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);

                    Model.Currency = dr1.IsNull("Currency") ? 0 : Convert.ToInt32(dr1["Currency"]);
                    Model.Priority = dr1.IsNull("Priority") ? 0 : Convert.ToInt32(dr1["Priority"]);
                    Model.EndUse = dr1.IsNull("EndUse") ? 0 : Convert.ToInt32(dr1["EndUse"]);
                    Model.EndUseNo = dr1.IsNull("EndUseNo") ? 0 : Convert.ToInt32(dr1["EndUseNo"]);
                    Model.CostCentre = dr1.IsNull("CostCentre") ? 0 : Convert.ToInt32(dr1["CostCentre"]);
                    //Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                    Model.RMcode = dr1.IsNull("RMcode") ? "" : Convert.ToString(dr1["RMcode"]);
                    Model.RMcat = dr1.IsNull("RMcat") ? 0 : Convert.ToInt32(dr1["RMcat"]);
                    Model.DeptName = dr1.IsNull("DeptName") ? "" : Convert.ToString(dr1["DeptName"]);
                    Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                    Model.Certificates = dr1.IsNull("Certificates") ? "" : Convert.ToString(dr1["Certificates"]);
                    Model.DeliveryTerms = dr1.IsNull("DeliveryTerms") ? "" : Convert.ToString(dr1["DeliveryTerms"]);
                    Model.ApprovedSupplier1 = dr1.IsNull("ApprovedSupplier1") ? "" : Convert.ToString(dr1["ApprovedSupplier1"]);
                    Model.ApprovedSupplier2 = dr1.IsNull("ApprovedSupplier2") ? "" : Convert.ToString(dr1["ApprovedSupplier2"]);
                    Model.ApprovedReject = dr1.IsNull("ApprovedReject") ? "" : Convert.ToString(dr1["ApprovedReject"]);
                    Model.Communicate = dr1.IsNull("Communicate") ? "" : Convert.ToString(dr1["Communicate"]);
                    Model.POno = dr1.IsNull("POno") ? "" : Convert.ToString(dr1["POno"]);
                    Model.DeliveryDate = dr1.IsNull("DeliveryDate") ? "" : Convert.ToString(dr1["DeliveryDate"]);
                    Model.ExpectedDeliveryDate = dr1.IsNull("ExpectedDeliveryDate") ? "" : Convert.ToString(dr1["ExpectedDeliveryDate"]);
                    Model.EntryDate = dr1.IsNull("EntryDate") ? "" : Convert.ToString(dr1["EntryDate"]);
                    Model.UOM = dr1.IsNull("UOM") ? 0 : Convert.ToInt32(dr1["UOM"]);
                    Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);
                    Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? 0 : Convert.ToInt32(dr1["SupplyTerms"]);
                    Model.SignStatus = dr1.IsNull("SignStatus") ? "" : Convert.ToString(dr1["SignStatus"]);
                    Model.PRStatus = dr1.IsNull("PRStatus") ? "" : Convert.ToString(dr1["PRStatus"]);
                    Model.EntryPerson = dr1.IsNull("EntryPerson") ? "" : Convert.ToString(dr1["EntryPerson"]);
                    Model.ApprovePerson1 = dr1.IsNull("ApprovePerson1") ? "" : Convert.ToString(dr1["ApprovePerson1"]);
                    Model.ApprovePerson2 = dr1.IsNull("ApprovePerson2") ? "" : Convert.ToString(dr1["ApprovePerson2"]);
                    Model.EntryPersonSign = dr1.IsNull("EntryPersonSign") ? "" : Convert.ToString(dr1["EntryPersonSign"]);
                    Model.ApprovePerson1Sign = dr1.IsNull("ApprovePerson1Sign") ? "" : Convert.ToString(dr1["ApprovePerson1Sign"]);
                    Model.ApprovePerson2Sign = dr1.IsNull("ApprovePerson2Sign") ? "" : Convert.ToString(dr1["ApprovePerson2Sign"]);
                    Model.PRFavouredOn = dr1.IsNull("PRFavouredOn") ? "" : Convert.ToString(dr1["PRFavouredOn"]);

                    //RMdescription
                    //RMgrade
                    //RMHardness
                    //PSLlevel
                    //OD
                    //WT
                    //Len
                    //QtyReqd
                    //QtyStock
                    //PRqty
                    //UnitPrice
                    //TotalPrice
                    //ReqDlyDate
                    //DlyTerms
                    //HSCode
                    //Supplier1
                    //Supplier2
                    //Vendor1
                    //Vendor2
                    //WIPno
                    //DRGno
                    //OPNcode
                    //ProcessName
                    //EDR
                    //OPNtime


                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public string SavePRDetailsList(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SavePRDetailsList(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SavePODetailsList(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SavePODetailsList(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            try
            {
                PRDetailEntityDetails prDetailEntity = new PRDetailEntityDetails();
                prDetailEntity.lstPREntity = new List<PRDetailEntity>();

                DataSet ds = _repository.GetPRDetailsList(pageIndex, pageSize, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup);

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
                                PRDetailEntity obj = new PRDetailEntity();
                                obj.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                                obj.PRno = dr1.IsNull("PRno") ? string.Empty : Convert.ToString(dr1["PRno"]);

                                obj.PRdate = dr1.IsNull("PRdate") ? string.Empty : Convert.ToString(dr1["PRdate"]);
                                obj.ReqFrom = dr1.IsNull("ReqFrom") ? string.Empty : Convert.ToString(dr1["ReqFrom"]);
                                obj.ApprovedSupplier1 = dr1.IsNull("ApprovedSupplier1") ? string.Empty : Convert.ToString(dr1["ApprovedSupplier1"]);
                                obj.TotalPRSetPrice = dr1.IsNull("TotalPRSetPrice") ? "" : Convert.ToString(dr1["TotalPRSetPrice"]);
                                obj.DeliveryDate = dr1.IsNull("DeliveryDate") ? string.Empty : Convert.ToString(dr1["DeliveryDate"]);
                                obj.SignStatus = dr1.IsNull("SignStatus") ? string.Empty : Convert.ToString(dr1["SignStatus"]);
                                obj.PRStatus = dr1.IsNull("PRStatus") ? string.Empty : Convert.ToString(dr1["PRStatus"]);
                                obj.LogInDeptName = DeptName;
                                obj.DeptName = dr1.IsNull("DeptName") ? string.Empty : Convert.ToString(dr1["DeptName"]);
                                obj.PurchasePerson = dr1.IsNull("PurchasePerson") ? string.Empty : Convert.ToString(dr1["PurchasePerson"]);

                                prDetailEntity.lstPREntity.Add(obj);
                            }
                        }
                    }


                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            prDetailEntity.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                        }
                    }
                }
                return prDetailEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<PRDetailEntity> GetPRTableDetails(string PRSetno)
        {
            List<PRDetailEntity> newList = new List<PRDetailEntity>();

            try
            {
                DataSet ds = _repository.GetPRTableDetails(PRSetno);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        PRDetailEntity Model = new PRDetailEntity();

                        Model.PRcat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);
                        Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                        Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.RMgrade = dr1.IsNull("RMgrade") ? "" : Convert.ToString(dr1["RMgrade"]);
                        Model.RMHardness = dr1.IsNull("RMHardness") ? "" : Convert.ToString(dr1["RMHardness"]);
                        Model.PSLlevel = dr1.IsNull("PSLlevel") ? "" : Convert.ToString(dr1["PSLlevel"]);
                        Model.OD = dr1.IsNull("OD") ? "" : Convert.ToString(dr1["OD"]);
                        Model.WT = dr1.IsNull("WT") ? "" : Convert.ToString(dr1["WT"]);
                        Model.Len = dr1.IsNull("Len") ? "" : Convert.ToString(dr1["Len"]);
                        Model.QtyReqd = dr1.IsNull("QtyReqd") ? 0 : Convert.ToInt32(dr1["QtyReqd"]);
                        Model.QtyStock = dr1.IsNull("QtyStock") ? 0 : Convert.ToInt32(dr1["QtyStock"]);
                        Model.PRqty = dr1.IsNull("PRqty") ? 0 : Convert.ToInt32(dr1["PRqty"]);
                        Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);

                        Model.EntryPersonSign = dr1.IsNull("EntryPersonSign") ? "" : Convert.ToString(dr1["EntryPersonSign"]);
                        Model.ApprovePerson1Sign = dr1.IsNull("ApprovePerson1Sign") ? "" : Convert.ToString(dr1["ApprovePerson1Sign"]);
                        Model.ApprovePerson2Sign = dr1.IsNull("ApprovePerson2Sign") ? "" : Convert.ToString(dr1["ApprovePerson2Sign"]);

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

        public string UpdateApproveReject(string[] param)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.UpdateApproveReject(param);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }

        public string SavePurchaseDetails(string[] param)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.SavePurchaseDetails(param);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }

        public DataTable GetPRListForDocument(string PRSetNo)
        {
            DataTable dt = _repository.GetPRListForDocument(PRSetNo);
            return dt;
        }

        public DataTable GetPRDataForDocument(string PRSetNo)
        {
            DataTable dt = _repository.GetPRDataForDocument(PRSetNo);
            return dt;
        }

    }

}
