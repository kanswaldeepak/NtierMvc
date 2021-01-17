using System;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.MRM;
using NtierMvc.Model;
using System.Collections.Generic;
using NtierMvc.Common;
using NtierMvc.Model.Vendor;

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
                    Model.CostCenter = dr1.IsNull("CostCenter") ? 0 : Convert.ToInt32(dr1["CostCenter"]);
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
        public string DeleteDocument(DocumentModel Documents)
        {
            string result = string.Empty;
            try
            {
                result = _repository.DeleteDocument(Documents);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }
       
        public VendorEntityDetails GetVendorDetails(SearchModel model)
        {
            //string SearchVendorType,int pageIndex, int pageSize, string SearchVendorName = null, string SearchVendorCountry = null
            try
            {
                VendorEntityDetails cED = new VendorEntityDetails();
                cED.LstVendEnt= new List<VendorEntity>();
                //     DataSet ds = _repository.GetVendorDetails(SearchVendorType,pageIndex, pageSize, SearchVendorName, SearchVendorCountry);
                     DataSet ds = _repository.GetVendorDetails(model);

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
                                VendorEntity obj = new VendorEntity();

                                //obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
                                obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
                                obj.VendorId = dr1.IsNull("VendorId") ? string.Empty : Convert.ToString(dr1["VendorId"]);
                                //obj.VendorTypeId = dr1.IsNull("VendorTypeId") ? string.Empty : Convert.ToString(dr1["VendorTypeId"]);
                                obj.VendorType = dr1.IsNull("VendorTypeId") ? string.Empty : Convert.ToString(dr1["VendorTypeId"]);
                                //obj.VendorNatureId = dr1.IsNull("VendorNatureId") ? string.Empty : Convert.ToString(dr1["VendorNatureId"]);
                                //obj.VendorNature = dr1.IsNull("VendorNature") ? string.Empty : Convert.ToString(dr1["VendorNature"]);
                                //obj.FunctionAreaId = dr1.IsNull("FunctionAreaId") ? string.Empty : Convert.ToString(dr1["FunctionAreaId"]);
                                //obj.FunctionArea = dr1.IsNull("FunctionArea") ? string.Empty : Convert.ToString(dr1["FunctionArea"]);
                                obj.VendorName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                                obj.City = dr1.IsNull("City") ? string.Empty : Convert.ToString(dr1["City"]);
                                obj.State = dr1.IsNull("STATE") ? string.Empty : Convert.ToString(dr1["STATE"]);

                                obj.Address1 = dr1.IsNull("Address1") ? string.Empty : Convert.ToString(dr1["Address1"]);
                                obj.Address2 = dr1.IsNull("Address2") ? string.Empty : Convert.ToString(dr1["Address2"]);
                                obj.Address3 = dr1.IsNull("Address3") ? string.Empty : Convert.ToString(dr1["Address3"]);
                                obj.ZipCode = dr1.IsNull("Address3") ? string.Empty : Convert.ToString(dr1["ZipCode"]);
                                obj.Country = dr1.IsNull("ZipCode") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.ContactPerson = dr1.IsNull("ContactPerson") ? string.Empty : Convert.ToString(dr1["ContactPerson"]);
                                obj.Designation = dr1.IsNull("Designation") ? string.Empty : Convert.ToString(dr1["Designation"]);
                                obj.tel1 = dr1.IsNull("tel1") ? string.Empty : Convert.ToString(dr1["tel1"]);
                                obj.tel2 = dr1.IsNull("tel2") ? string.Empty : Convert.ToString(dr1["tel2"]);
                                obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.CountryId = dr1.IsNull("Id") ? string.Empty : Convert.ToString(dr1["Id"]);

                                obj.fax = dr1.IsNull("fax") ? string.Empty : Convert.ToString(dr1["fax"]);
                             
                                obj.mob1 = dr1.IsNull("MOB1") ? string.Empty : Convert.ToString(dr1["MOB1"]);
                                obj.mob2 = dr1.IsNull("MOB2") ? string.Empty : Convert.ToString(dr1["MOB2"]);
                                obj.email1 = dr1.IsNull("EMAIL1") ? string.Empty : Convert.ToString(dr1["EMAIL1"]);
                                obj.email1 = dr1.IsNull("EMAIL2") ? string.Empty : Convert.ToString(dr1["EMAIL2"]);
                                obj.Status = dr1.IsNull("Status") ? string.Empty : Convert.ToString(dr1["Status"]);
                                obj.Status = dr1.IsNull("VendorStatus") ? string.Empty : Convert.ToString(dr1["VendorStatus"]);
                                obj.Documents = dr1.IsNull("Documents") ? string.Empty : Convert.ToString(dr1["Documents"]);
                                obj.BankName = dr1.IsNull("BankName") ? string.Empty : Convert.ToString(dr1["BankName"]);
                                obj.BankBranch = dr1.IsNull("BankBranch") ? string.Empty : Convert.ToString(dr1["BankBranch"]);
                                obj.BankAddress1 = dr1.IsNull("BankAddress1") ? string.Empty : Convert.ToString(dr1["BankAddress1"]);
                                obj.BankAddress2 = dr1.IsNull("BankAddress2") ? string.Empty : Convert.ToString(dr1["BankAddress2"]);
                                obj.BankCountry = dr1.IsNull("BankCountry") ? string.Empty : Convert.ToString(dr1["BankCountry"]);
                                obj.BankState = dr1.IsNull("BankState") ? string.Empty : Convert.ToString(dr1["BankState"]);
                                obj.BankCity= dr1.IsNull("BankCity") ? string.Empty : Convert.ToString(dr1["BankCity"]);
                                obj.BankRTGSCode = dr1.IsNull("BankRTGSCode") ? string.Empty : Convert.ToString(dr1["BankRTGSCode"]);
                                obj.BankZipCode = dr1.IsNull("BankZipCode") ? string.Empty : Convert.ToString(dr1["BankZipCode"]);
                                obj.SupplierType = dr1.IsNull("SupplierType") ? string.Empty : Convert.ToString(dr1["SupplierType"]);
                                obj.VendorNature = dr1.IsNull("VendorName1")? string.Empty : Convert.ToString(dr1["VendorName1"]);

                                obj.SupplierName = dr1.IsNull("SupplierName") ? string.Empty : Convert.ToString(dr1["SupplierName"]);
                                cED.LstVendEnt.Add(obj);
                               

                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                cED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }
                return cED;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }
        public VendorEntity VendorDetailsPopup(VendorEntity Model)
        {
            try
            {
                DataSet ds = _repository.VendorDetailsPopup(Model);
                //Model = ExtractCustomerDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    //Model.VendorId = dt1.Rows[0]["VendorId"] == DBNull.Value ? "0" : Convert.ToString(dt1.Rows[0]["VendorId"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);
                    Model.VendorId = dt1.Rows[0]["VendorID"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorID"]);
                   // Model.VendorTypeId = dt1.Rows[0]["VendorTypeId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorTypeID"]);
                    Model.VendorType = dt1.Rows[0]["VendorTypeID"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorTypeID"]);
                    //Model.VendorNatureId = dt1.Rows[0]["VendorNatureId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorNatureId"]);
                    //Model.VendorNature = dt1.Rows[0]["VendorNature"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorNature"]);
                    //Model.FunctionAreaId = dt1.Rows[0]["FunctionAreaId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FunctionAreaId"]);
                    //Model.FunctionArea = dt1.Rows[0]["FunctionArea"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FunctionArea"]);
                    Model.VendorName = dt1.Rows[0]["VendorName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorName"]);
                    Model.Address1 = dt1.Rows[0]["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address1"]);
                    Model.Address2 = dt1.Rows[0]["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address2"]);
                    Model.Address3 = dt1.Rows[0]["Address3"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address3"]);
                    Model.City = dt1.Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["City"]);
                    Model.State = dt1.Rows[0]["State"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["State"]);
                    Model.Country = dt1.Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Country"]);
                   Model.CountryId = dt1.Rows[0]["Id"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Id"]);
                   
                    Model.ZipCode = dt1.Rows[0]["ZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ZipCode"]);
                    Model.tel1 = dt1.Rows[0]["tel1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["tel1"]);
                    Model.tel2 = dt1.Rows[0]["tel2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["tel2"]);
                    Model.mob1 = dt1.Rows[0]["mob1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["mob1"]);
                    Model.mob2 = dt1.Rows[0]["mob2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["mob2"]);
                    Model.fax = dt1.Rows[0]["fax"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["fax"]);
                    Model.email1 = dt1.Rows[0]["email1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["email1"]);
                    Model.email2 = dt1.Rows[0]["email2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["email2"]);
                    Model.ContactPerson = dt1.Rows[0]["ContactPerson"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ContactPerson"]);
                    Model.Designation = dt1.Rows[0]["Designation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Designation"]);
                    Model.VendorInitial = dt1.Rows[0]["VendorInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorInitial"]);
                 
                    
                    Model.DateOfAssociation = dt1.Rows[0]["DateOfAssociation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DateOfAssociation"]);
                    Model.Status = dt1.Rows[0]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Status"]);
                    Model.Documents = dt1.Rows[0]["Documents"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Documents"]);
                    Model.SupplierType = dt1.Rows[0]["SupplierType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SupplierType"]);
                    Model.BankName = dt1.Rows[0]["BankName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankName"]);
                    Model.BankBranch = dt1.Rows[0]["BankBranch"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankBranch"]);
                    Model.BankAddress1 = dt1.Rows[0]["BankAddress1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankAddress1"]);
                    Model.BankAddress2 = dt1.Rows[0]["BankAddress2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankAddress2"]);
                    Model.BankCountry = dt1.Rows[0]["BankCountry"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankCountry"]);
                    Model.BankState = dt1.Rows[0]["BankState"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankState"]);
                    Model.BankCity = dt1.Rows[0]["BankCity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankCity"]);
                    Model.BankZipCode = dt1.Rows[0]["BankZipCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankZipCode"]);
                    Model.BankIFSCCode = dt1.Rows[0]["BankIFSCCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankIFSCCode"]);
                    Model.BankRTGSCode = dt1.Rows[0]["BankRTGSCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BankRTGSCode"]);
                    Model.IGSTCode = dt1.Rows[0]["IGSTCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["IGSTCode"]);
                    Model.PanNo = dt1.Rows[0]["PanNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PanNo"]);
                    
           
                    
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public string SaveVendorDetails(VendorEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveVendorDetails(entity);
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
                    Model.CostCenter = dr1.IsNull("CostCenter") ? "" : Convert.ToString(dr1["CostCenter"]);
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
                        Model.CostCenter = dr1.IsNull("CostCenter") ? "" : Convert.ToString(dr1["CostCenter"]);
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

        public MRMBillMonitoringEntityDetails GetMRMDetailForGateControlNo(string GateControlNo, string BMno = null)
        {
            MRMBillMonitoringEntityDetails objBM = new MRMBillMonitoringEntityDetails();
            objBM.lstMRMEntity = new List<MRMBillMonitoringEntity>();

            try
            {
                DataSet ds = _repository.GetMRMDetailForGateControlNo(GateControlNo, BMno);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        MRMBillMonitoringEntity Model = new MRMBillMonitoringEntity();

                        Model.VendorNatureId = dr1.IsNull("VendorNatureId") ? string.Empty : Convert.ToString(dr1["VendorNatureId"]);
                        Model.CustomerId = dr1.IsNull("VendorId") ? 0 : Convert.ToInt32(dr1["VendorId"]);
                        Model.CustomerName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
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
                        Model.GateNo = dr1.IsNull("GateNo") ? 0 : Convert.ToInt32(dr1["GateNo"]);
                        Model.GRDate = dr1.IsNull("GRDate") ? string.Empty : Convert.ToString(dr1["GRDate"]);
                        Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? string.Empty : Convert.ToString(dr1["SupplyTerms"]);
                        Model.CostCenter = dr1.IsNull("CostCenter") ? string.Empty : Convert.ToString(dr1["CostCenter"]);
                        Model.PRCat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);
                        
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

                        if (!string.IsNullOrEmpty(BMno) && Convert.ToInt32(BMno) > 0)
                        {
                            Model.SACNo = dr1.IsNull("SACNo") ? "" : Convert.ToString(dr1["SACNo"]);
                            Model.GSTPercent = dr1.IsNull("GSTPercent") ? "" : Convert.ToString(dr1["GSTPercent"]);
                            Model.GSTAmount = dr1.IsNull("GSTAmount") ? "" : Convert.ToString(dr1["GSTAmount"]);

                        }

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

        public MRMBillMonitoringEntity GetBillDetailsPopup(string BMno)
        {
            MRMBillMonitoringEntity Model = new MRMBillMonitoringEntity();
            try
            {
                DataSet ds = _repository.GetBillDetailsPopup(BMno);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        Model.GateNo = dr1.IsNull("GateNo") ? 0 : Convert.ToInt32(dr1["GateNo"]);
                        Model.GateControlNo = dr1.IsNull("GateControlNo") ? string.Empty : Convert.ToString(dr1["GateControlNo"]);
                        Model.SupplyType = dr1.IsNull("SupplyType") ? string.Empty : Convert.ToString(dr1["SupplyType"]);
                        Model.BMno = Convert.ToInt32(BMno);

                        Model.SupplierInvNo = dr1.IsNull("SupplierInvNo") ? string.Empty : Convert.ToString(dr1["SupplierInvNo"]);
                        Model.SupplierInvDate = dr1.IsNull("SupplierDate") ? string.Empty : Convert.ToString(dr1["SupplierDate"]);

                        //Model.VendorNatureId = dr1.IsNull("VendorNatureId") ? string.Empty : Convert.ToString(dr1["VendorNatureId"]);
                        Model.CustomerId = dr1.IsNull("CustomerID") ? 0 : Convert.ToInt32(dr1["CustomerID"]);
                        Model.CustomerName = dr1.IsNull("CustomerNAME") ? string.Empty : Convert.ToString(dr1["CustomerNAME"]);
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
                        Model.CostCenter = dr1.IsNull("CostCenter") ? string.Empty : Convert.ToString(dr1["CostCenter"]);
                        Model.PRCat = dr1.IsNull("PRcat") ? "" : Convert.ToString(dr1["PRcat"]);

                        Model.PaymentDueDate = dr1.IsNull("PaymentDueDate") ? "" : Convert.ToString(dr1["PaymentDueDate"]);
                        Model.VerifiedBy = dr1.IsNull("VerifiedBy") ? "" : Convert.ToString(dr1["VerifiedBy"]);
                        Model.ApprovedStatus = dr1.IsNull("ApprovedStatus") ? "" : Convert.ToString(dr1["ApprovedStatus"]);

                        //Model.SN = dr1.IsNull("SN") ? "" : Convert.ToString(dr1["SN"]);
                        //Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        //Model.PRqty = dr1.IsNull("PRqty") ? "" : Convert.ToString(dr1["PRqty"]);
                        //Model.UOM = dr1.IsNull("UOM") ? "" : Convert.ToString(dr1["UOM"]);
                        //Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        //Model.Discount = dr1.IsNull("Discount") ? "" : Convert.ToString(dr1["Discount"]);
                        //Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                        //Model.LotName = dr1.IsNull("LotName") ? "" : Convert.ToString(dr1["LotName"]);
                        //Model.LotDate = dr1.IsNull("LotDate") ? "" : Convert.ToString(dr1["LotDate"]);
                        //Model.LotQty = dr1.IsNull("LotQty") ? "" : Convert.ToString(dr1["LotQty"]);


                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }


        public int GetBillMonitoringNo()
        {
            int BMNo = 0;
            try
            {
                DataSet ds = _repository.GetBillMonitoringNo();
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        DataRow dr = dt1.Rows[0];
                        BMNo = dr.IsNull("BMNo") ? 0 : Convert.ToInt32(dr["BMNo"]);
                    }

                }

            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return BMNo;
        }

        public MRMBillMonitoringEntityDetails FetchBillMonitoringList(int pageIndex, int pageSize, string MRMSearchVendorTypeId = null, string MRMSearchSupplierId = null, string MRMSearchSupplierName = null, string MRMSearchApprovedDate = null, string MRMSearchTotalAmount = null)
        {
            try
            {
                MRMBillMonitoringEntityDetails mrmEntity = new MRMBillMonitoringEntityDetails();
                mrmEntity.lstMRMEntity = new List<MRMBillMonitoringEntity>();

                DataSet ds = _repository.FetchBillMonitoringList(pageIndex, pageSize, MRMSearchVendorTypeId, MRMSearchSupplierId, MRMSearchSupplierName, MRMSearchApprovedDate, MRMSearchTotalAmount);

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
                                MRMBillMonitoringEntity obj = new MRMBillMonitoringEntity();
                                obj.BMno = dr1.IsNull("BMno") ? 0 : Convert.ToInt32(dr1["BMno"]);
                                obj.SupplyType = dr1.IsNull("SupplyType") ? string.Empty : Convert.ToString(dr1["SupplyType"]);
                                obj.GateControlNo = dr1.IsNull("GateControlNo") ? string.Empty : Convert.ToString(dr1["GateControlNo"]);

                                obj.CustomerId = dr1.IsNull("CustomerId") ? 0 : Convert.ToInt32(dr1["CustomerId"]);
                                obj.CustomerName = dr1.IsNull("CustomerName") ? string.Empty : Convert.ToString(dr1["CustomerName"]);
                                obj.SupplierInvNo = dr1.IsNull("SupplierInvNo") ? string.Empty : Convert.ToString(dr1["SupplierInvNo"]);
                                obj.SupplierInvDate = dr1.IsNull("SupplierInvDate") ? "" : Convert.ToString(dr1["SupplierInvDate"]);
                                obj.GRNo = dr1.IsNull("GRNo") ? string.Empty : Convert.ToString(dr1["GRNo"]);
                                obj.GRDate = dr1.IsNull("GRDate") ? string.Empty : Convert.ToString(dr1["GRDate"]);

                                mrmEntity.lstMRMEntity.Add(obj);
                            }
                        }
                    }


                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            mrmEntity.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                        }
                    }
                }
                return mrmEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }






    }

}
