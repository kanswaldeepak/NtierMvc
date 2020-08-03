using System;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.MRM;
using NtierMvc.Model;
using System.Collections.Generic;
using NtierMvc.Common;

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

        public PRDetailEntityDetails GetPRDetailsList(int pageIndex, int pageSize, string DeptName, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            try
            {
                PRDetailEntityDetails prDetailEntity = new PRDetailEntityDetails();
                prDetailEntity.lstPREntity = new List<PRDetailEntity>();

                DataSet ds = _repository.GetPRDetailsList(pageIndex, pageSize, SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDateFrom, SearchDeliveryDateTo);

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
                                
                                obj.DeptName = dr1.IsNull("DeptName") ? string.Empty : Convert.ToString(dr1["DeptName"]);
                                obj.LogInDeptName = DeptName;
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
                        Model.Desc1 = dr1.IsNull("Desc1") ? "" : Convert.ToString(dr1["Desc1"]);
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

        public PODetailEntityDetails GetPODetailsList(int pageIndex, int pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            try
            {
                PODetailEntityDetails poDetailEntity = new PODetailEntityDetails();
                poDetailEntity.lstPOEntity = new List<PODetailEntity>();

                DataSet ds = _repository.GetPODetailsList(pageIndex, pageSize, SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDateFrom, SearchDeliveryDateTo);

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
                                PODetailEntity obj = new PODetailEntity();
                                obj.POSetno = dr1.IsNull("POSetno") ? "" : Convert.ToString(dr1["POSetno"]);
                                
                                obj.PONo = dr1.IsNull("PONo") ? string.Empty : Convert.ToString(dr1["PONo"]);
                                obj.POdate = dr1.IsNull("POdate") ? string.Empty : Convert.ToString(dr1["POdate"]);
                                obj.WorkNo = dr1.IsNull("WorkNo") ? string.Empty : Convert.ToString(dr1["WorkNo"]);
                                obj.DeliveryDate = dr1.IsNull("DeliveryDate") ? string.Empty : Convert.ToString(dr1["DeliveryDate"]);
                                obj.POValidity = dr1.IsNull("POValidity") ? string.Empty : Convert.ToString(dr1["POValidity"]);
                                //obj.TotalPRSetPrice = dr1.IsNull("TotalPRSetPrice") ? "" : Convert.ToString(dr1["TotalPRSetPrice"]);
                               
                                poDetailEntity.lstPOEntity.Add(obj);
                            }
                        }
                    }


                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            poDetailEntity.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                        }
                    }
                }
                return poDetailEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public PODetailEntity GetPODetailsForPopup(PODetailEntity Model)
        {
            try
            {
                DataSet ds = _repository.GetPODetailsForPopup(Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];

                    Model.PONo = dr1.IsNull("PONo") ? "" : Convert.ToString(dr1["PONo"]);
                    Model.POSetno = dr1.IsNull("POSetno") ? "" : Convert.ToString(dr1["POSetno"]);
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public DataTable GetPODetailForDocument(string PRSetNo)
        {
            DataTable dt = _repository.GetPODetailForDocument(PRSetNo);
            return dt;
        }

        public DataTable GetPOListDataForDocument(string PRSetNo)
        {
            DataTable dt = _repository.GetPOListDataForDocument(PRSetNo);
            return dt;
        }

        public PODetailEntity GetSavedPODetails(string POSetNo)
        {
            PODetailEntity Model = new PODetailEntity();
            try
            {
                DataSet ds = _repository.GetSavedPODetails(POSetNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];

                    Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                    Model.PRcat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);
                    Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? "" : Convert.ToString(dr1["SupplyTerms"]);
                    Model.PONo = dr1.IsNull("POno") ? "" : Convert.ToString(dr1["POno"]);
                    Model.POdate = dr1.IsNull("POdate") ? "" : Convert.ToString(dr1["POdate"]);
                    Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                    Model.RMgrade = dr1.IsNull("RMgrade") ? "" : Convert.ToString(dr1["RMgrade"]);

                    Model.RMHardness = dr1.IsNull("RMHardness") ? "" : Convert.ToString(dr1["RMHardness"]);
                    Model.PSLlevel = dr1.IsNull("PSLlevel") ? "" : Convert.ToString(dr1["PSLlevel"]);
                    Model.QtyReqd = dr1.IsNull("QtyReqd") ? 0 : Convert.ToInt32(dr1["QtyReqd"]);
                    Model.QtyStock = dr1.IsNull("QtyStock") ? 0 : Convert.ToInt32(dr1["QtyStock"]);
                    Model.PRqty = dr1.IsNull("PRqty") ? 0 : Convert.ToInt32(dr1["PRqty"]);
                    Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                    Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                    Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);

                    Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);                    
                    Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                    Model.GeneralCondition = dr1.IsNull("GeneralCondition") ? "" : Convert.ToString(dr1["GeneralCondition"]);
                    Model.CostCentre = dr1.IsNull("CostCentre") ? "" : Convert.ToString(dr1["CostCentre"]);
                    Model.POQMSRequirement = dr1.IsNull("POQMSRequirement") ? "" : Convert.ToString(dr1["POQMSRequirement"]);
                    Model.POPackForward = dr1.IsNull("POPackForward") ? "" : Convert.ToString(dr1["POPackForward"]);
                    Model.POQuality = dr1.IsNull("POQuality") ? "" : Convert.ToString(dr1["POQuality"]);
                    Model.POValidity = dr1.IsNull("POValidity") ? "" : Convert.ToString(dr1["POValidity"]);
                    Model.ModeOfPayment = dr1.IsNull("ModeOfPayment") ? "" : Convert.ToString(dr1["ModeOfPayment"]);
                    Model.ItemCategory = dr1.IsNull("ItemCategory") ? "" : Convert.ToString(dr1["ItemCategory"]);
                    Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);
                    Model.ModeOfTransport = dr1.IsNull("ModeOfTransport") ? "" : Convert.ToString(dr1["ModeOfTransport"]);
                    Model.AnyOtherRequirements = dr1.IsNull("AnyOtherRequirements") ? "" : Convert.ToString(dr1["AnyOtherRequirements"]);
                    Model.WorkNo = dr1.IsNull("WorkNo") ? "" : Convert.ToString(dr1["WorkNo"]);
                    Model.LotName = dr1.IsNull("LotName") ? "" : Convert.ToString(dr1["LotName"]);
                    Model.LotDate = dr1.IsNull("LotDate") ? "" : Convert.ToString(dr1["LotDate"]);
                    Model.LotQty = dr1.IsNull("LotQty") ? "" : Convert.ToString(dr1["LotQty"]);                    
                    Model.SignStatus = dr1.IsNull("SignStatus") ? "" : Convert.ToString(dr1["SignStatus"]);
                    Model.PRStatus = dr1.IsNull("PRStatus") ? "" : Convert.ToString(dr1["PRStatus"]);
                    Model.ApprovePerson1 = dr1.IsNull("ApprovePerson1") ? "" : Convert.ToString(dr1["ApprovePerson1"]);
                    Model.ApprovePerson2 = dr1.IsNull("ApprovePerson2") ? "" : Convert.ToString(dr1["ApprovePerson2"]);
                    Model.ApprovePerson1Sign = dr1.IsNull("ApprovePerson1Sign") ? "" : Convert.ToString(dr1["ApprovePerson1Sign"]);
                    Model.ApprovePerson2Sign = dr1.IsNull("ApprovePerson2Sign") ? "" : Convert.ToString(dr1["ApprovePerson2Sign"]);                    

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public List<PODetailEntity> GetPOTableDetails(string POSetNo)
        {
            List<PODetailEntity> lstPO = new List<PODetailEntity>();
            
            try
            {
                DataSet ds = _repository.GetSavedPODetails(POSetNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        PODetailEntity Model = new PODetailEntity();
                        Model.PRSetno = dr1.IsNull("PRSetno") ? 0 : Convert.ToInt32(dr1["PRSetno"]);
                        Model.PRcat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);
                        Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? "" : Convert.ToString(dr1["SupplyTerms"]);
                        Model.PONo = dr1.IsNull("POno") ? "" : Convert.ToString(dr1["POno"]);
                        Model.POdate = dr1.IsNull("POdate") ? "" : Convert.ToString(dr1["POdate"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.RMgrade = dr1.IsNull("RMgrade") ? "" : Convert.ToString(dr1["RMgrade"]);

                        Model.RMHardness = dr1.IsNull("RMHardness") ? "" : Convert.ToString(dr1["RMHardness"]);
                        Model.PSLlevel = dr1.IsNull("PSLlevel") ? "" : Convert.ToString(dr1["PSLlevel"]);
                        Model.QtyReqd = dr1.IsNull("QtyReqd") ? 0 : Convert.ToInt32(dr1["QtyReqd"]);
                        Model.QtyStock = dr1.IsNull("QtyStock") ? 0 : Convert.ToInt32(dr1["QtyStock"]);
                        Model.PRqty = dr1.IsNull("PRqty") ? 0 : Convert.ToInt32(dr1["PRqty"]);
                        Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                        Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);

                        Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.Discount = dr1.IsNull("Discount") ? "" : Convert.ToString(dr1["Discount"]);
                        Model.GeneralCondition = dr1.IsNull("GeneralCondition") ? "" : Convert.ToString(dr1["GeneralCondition"]);
                        Model.CostCentre = dr1.IsNull("CostCentre") ? "" : Convert.ToString(dr1["CostCentre"]);
                        Model.POQMSRequirement = dr1.IsNull("POQMSRequirement") ? "" : Convert.ToString(dr1["POQMSRequirement"]);
                        Model.POPackForward = dr1.IsNull("POPackForward") ? "" : Convert.ToString(dr1["POPackForward"]);
                        Model.POQuality = dr1.IsNull("POQuality") ? "" : Convert.ToString(dr1["POQuality"]);
                        Model.POValidity = dr1.IsNull("POValidity") ? "" : Convert.ToString(dr1["POValidity"]);
                        Model.ModeOfPayment = dr1.IsNull("ModeOfPayment") ? "" : Convert.ToString(dr1["ModeOfPayment"]);
                        Model.ItemCategory = dr1.IsNull("ItemCategory") ? "" : Convert.ToString(dr1["ItemCategory"]);
                        Model.PaymentTerms = dr1.IsNull("PaymentTerms") ? "" : Convert.ToString(dr1["PaymentTerms"]);
                        Model.ModeOfTransport = dr1.IsNull("ModeOfTransport") ? "" : Convert.ToString(dr1["ModeOfTransport"]);
                        Model.AnyOtherRequirements = dr1.IsNull("AnyOtherRequirements") ? "" : Convert.ToString(dr1["AnyOtherRequirements"]);
                        Model.WorkNo = dr1.IsNull("WorkNo") ? "" : Convert.ToString(dr1["WorkNo"]);
                        Model.LotName = dr1.IsNull("LotName") ? "" : Convert.ToString(dr1["LotName"]);
                        Model.LotDate = dr1.IsNull("LotDate") ? "" : Convert.ToString(dr1["LotDate"]);
                        Model.LotQty = dr1.IsNull("LotQty") ? "" : Convert.ToString(dr1["LotQty"]);
                        Model.SignStatus = dr1.IsNull("SignStatus") ? "" : Convert.ToString(dr1["SignStatus"]);
                        Model.PRStatus = dr1.IsNull("PRStatus") ? "" : Convert.ToString(dr1["PRStatus"]);
                        Model.ApprovePerson1 = dr1.IsNull("ApprovePerson1") ? "" : Convert.ToString(dr1["ApprovePerson1"]);
                        Model.ApprovePerson2 = dr1.IsNull("ApprovePerson2") ? "" : Convert.ToString(dr1["ApprovePerson2"]);
                        Model.ApprovePerson1Sign = dr1.IsNull("ApprovePerson1Sign") ? "" : Convert.ToString(dr1["ApprovePerson1Sign"]);
                        Model.ApprovePerson2Sign = dr1.IsNull("ApprovePerson2Sign") ? "" : Convert.ToString(dr1["ApprovePerson2Sign"]);
                        Model.EntryPerson = dr1.IsNull("EntryPerson") ? "" : Convert.ToString(dr1["EntryPerson"]);
                        Model.EntryPersonSign = dr1.IsNull("EntryPersonSign") ? "" : Convert.ToString(dr1["EntryPersonSign"]);
                        Model.Desc1 = dr1.IsNull("Desc1") ? "" : Convert.ToString(dr1["Desc1"]);

                        lstPO.Add(Model);
                    }

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return lstPO;
        }

        public List<DropDownEntity> GetPRNoList(string DeptName)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = _repository.GetPRNoLists(DeptName);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetRMCategories(string SupplierId)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = _repository.GetRMCategories(SupplierId);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public List<DropDownEntity> GetDeliveryDates(string RMCategory)
        {
            List<DropDownEntity> objList = new List<DropDownEntity>();
            try
            {
                objList = _repository.GetDeliveryDates(RMCategory);
                return objList;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return objList;
        }

        public MRMBillMonitoringEntityDetails GetMRMDetailForGateControlNo(string GateControlNo)
        {
            MRMBillMonitoringEntityDetails objBM = new MRMBillMonitoringEntityDetails();
            objBM.lstMRMEntity = new List<MRMBillMonitoringEntity>();

            try
            {
                DataSet ds = _repository.GetMRMDetailForGateControlNo(GateControlNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        MRMBillMonitoringEntity Model = new MRMBillMonitoringEntity();

                        Model.VendorNatureId = dr1.IsNull("VendorNatureId") ? string.Empty : Convert.ToString(dr1["VendorNatureId"]);
                        Model.VendorId = dr1.IsNull("VendorId") ? string.Empty : Convert.ToString(dr1["VendorId"]);
                        Model.VendorName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                        Model.City = dr1.IsNull("City") ? string.Empty : Convert.ToString(dr1["City"]);
                        Model.EndUse = dr1.IsNull("EndUse") ? string.Empty : Convert.ToString(dr1["EndUse"]);
                        Model.EndUseNo = dr1.IsNull("EndUseNo") ? string.Empty : Convert.ToString(dr1["EndUseNo"]);
                        Model.FunctionalAreaId = dr1.IsNull("FunctionalAreaId") ? string.Empty : Convert.ToString(dr1["FunctionalAreaId"]);
                        Model.VendorPONO = dr1.IsNull("VendorPONO") ? string.Empty : Convert.ToString(dr1["VendorPONO"]);
                        Model.VendorPODate = dr1.IsNull("VendorPODate") ? string.Empty : Convert.ToString(dr1["VendorPODate"]);
                        Model.SupplierInvNo = dr1.IsNull("SupplierInvNo") ? string.Empty : Convert.ToString(dr1["SupplierInvNo"]);
                        Model.SupplierInvDate = dr1.IsNull("SupplierDate") ? string.Empty : Convert.ToString(dr1["SupplierDate"]);
                        Model.Currency = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
                        Model.SupplierInvAmount = dr1.IsNull("SupplierAmount") ? string.Empty : Convert.ToString(dr1["SupplierAmount"]);
                        Model.VehicleNo = dr1.IsNull("VehicleNo") ? string.Empty : Convert.ToString(dr1["VehicleNo"]);
                        Model.DriverName = dr1.IsNull("DriverName") ? string.Empty : Convert.ToString(dr1["DriverName"]);
                        Model.DriverContactNo = dr1.IsNull("DriverContactNo") ? string.Empty : Convert.ToString(dr1["DriverContactNo"]);
                        Model.TimeIn = dr1.IsNull("TimeIn") ? string.Empty : Convert.ToString(dr1["TimeIn"]);
                        Model.TimeOut = dr1.IsNull("TimeOut") ? string.Empty : Convert.ToString(dr1["TimeOut"]);
                        Model.VehicleReleased = dr1.IsNull("VehicleReleased") ? string.Empty : Convert.ToString(dr1["VehicleReleased"]);
                        Model.GRNo = dr1.IsNull("GRno") ? string.Empty : Convert.ToString(dr1["GRno"]);
                        Model.GRDate = dr1.IsNull("GRDate") ? string.Empty : Convert.ToString(dr1["GRDate"]);
                        Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? string.Empty : Convert.ToString(dr1["SupplyTerms"]);
                        Model.CostCentre = dr1.IsNull("CostCentre") ? string.Empty : Convert.ToString(dr1["CostCentre"]);

                        Model.SN = dr1.IsNull("SN") ? "" : Convert.ToString(dr1["SN"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.PRqty = dr1.IsNull("PRqty") ? "" : Convert.ToString(dr1["PRqty"]);
                        Model.UOM = dr1.IsNull("UOM") ? "" : Convert.ToString(dr1["UOM"]);
                        Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        Model.Discount = dr1.IsNull("Discount") ? "" : Convert.ToString(dr1["Discount"]);
                        Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                        Model.LotName = dr1.IsNull("LotName") ? "" : Convert.ToString(dr1["LotName"]);
                        Model.LotDate = dr1.IsNull("LotDate") ? "" : Convert.ToString(dr1["LotDate"]);
                        Model.LotQty = dr1.IsNull("LotQty") ? "" : Convert.ToString(dr1["LotQty"]);
                        

                        objBM.lstMRMEntity.Add(Model);
                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return objBM;
        }


    }

}
