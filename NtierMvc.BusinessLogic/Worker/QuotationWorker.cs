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
    public class QuotationWorker : IQuotationWorker
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        private Repository _repository = new Repository();

        public QuotationWorker()
        {
            //_loggingHandler = new LoggingHandler();
            //_repository = new Repository();
        }

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool bDisposing)
        //{
        //    // Check to see if Dispose has already been called.
        //    if (!_bDisposed)
        //    {
        //        if (bDisposing)
        //        {
        //            // Dispose managed resources.
        //            _loggingHandler = null;
        //        }
        //    }
        //    _bDisposed = true;
        //}
        #endregion

        //public string SaveQuotationDetails(QuotationEntity entity)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        result = _repository.SaveQuotationDetails(entity);
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //    }
        //    return result;
        //}

        public QuotationEntity GetUserQuoteDetails(string unitNo)
        {
            QuotationEntity result = new QuotationEntity();
            try
            {
                result = DT2Cust(_repository.GetUserQuoteDetails(unitNo));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public QuotationEntity GetVendorQuoteDetails(string vendorId)
        {
            QuotationEntity result = new QuotationEntity();
            try
            {
                result = DT2Vendor(_repository.GetVendorQuoteDetails(vendorId));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public QuotationEntity DT2Vendor(DataTable dtRecord)
        {
            QuotationEntity oVendor = new QuotationEntity();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    oVendor.UnitNo = Convert.ToString(dtRecord.Rows[0]["Id"]);
                    oVendor.CustomerId = Convert.ToString(dtRecord.Rows[0]["CustomerID"]);
                    oVendor.CustomerName = Convert.ToString(dtRecord.Rows[0]["CustomerName"]);
                    oVendor.Country = Convert.ToString(dtRecord.Rows[0]["CountryName"]);
                    oVendor.CountryId = Convert.ToString(dtRecord.Rows[0]["CountryId"]);
                    oVendor.GeoArea = Convert.ToString(dtRecord.Rows[0]["Geo_Area"]);
                    oVendor.GeoCode = Convert.ToString(dtRecord.Rows[0]["Geo_Code"]);
                    oVendor.QuoteType = Convert.ToString(dtRecord.Rows[0]["QuoteType"]);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oVendor;
        }

        public QuotationEntity DT2Cust(DataTable dtRecord)
        {
            QuotationEntity oCust = new QuotationEntity();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    oCust.UnitNo = Convert.ToString(dtRecord.Rows[0]["Id"]);
                    oCust.UserInitial = Convert.ToString(dtRecord.Rows[0]["UserName"]);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oCust;
        }

        public string DeleteQuotationDetail(int QuotationId)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteQuotationDetail(QuotationId);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }


        //public QuotationEntityDetails GetQuotationDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteVendorID = null, string SearchQuoteProductGroup = null, string SearchDeliveryTerms = null)
        //{
        //    try
        //    {
        //        QuotationEntityDetails qED = new QuotationEntityDetails();
        //        qED.lstQuoteEntity = new List<QuotationEntity>();
        //        DataSet ds = _repository.GetQuotationDetails(pageIndex, pageSize, SearchQuoteType, SearchQuoteVendorID, SearchQuoteProductGroup, SearchDeliveryTerms);
        //        //DataTable dt = _repository.GetQuotationDetails();

        //        if (ds.Tables.Count > 0)
        //        {
        //            DataTable dt1 = ds.Tables[0];
        //            DataTable dt2 = ds.Tables[1];

        //            if (dt1 != null && dt1.Rows.Count > 0)
        //            {
        //                if (dt1.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr1 in dt1.Rows)
        //                    {
        //                        QuotationEntity obj = new QuotationEntity();

        //                        obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
        //                        obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
        //                        obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
        //                        obj.CustomerId = dr1.IsNull("CustomerId") ? string.Empty : Convert.ToString(dr1["CustomerId"]);
        //                        obj.CustomerName = dr1.IsNull("CustomerName") ? string.Empty : Convert.ToString(dr1["CustomerName"]);
        //                        obj.QuoteType = dr1.IsNull("QuoteType") ? string.Empty : Convert.ToString(dr1["QuoteType"]);
        //                        obj.FileNo = dr1.IsNull("FileNo") ? string.Empty : Convert.ToString(dr1["FileNo"]);
        //                        obj.EnqRef = dr1.IsNull("EnqRef") ? string.Empty : Convert.ToString(dr1["EnqRef"]);
        //                        obj.EnqDt = dr1.IsNull("EnqDt") ? string.Empty : Convert.ToString(dr1["EnqDt"]);
        //                        obj.EnqNo = dr1.IsNull("EnqNo") ? string.Empty : Convert.ToString(dr1["EnqNo"]);
        //                        obj.QuoteNo = dr1.IsNull("QuoteNo") ? string.Empty : Convert.ToString(dr1["QuoteNo"]);
        //                        obj.QuoteDate = dr1.IsNull("QuoteDate") ? string.Empty : Convert.ToString(dr1["QuoteDate"]);
        //                        obj.QuoteValidity = dr1.IsNull("QuoteValidity") ? string.Empty : Convert.ToString(dr1["QuoteValidity"]);
        //                        //obj.BgReq = dr1.IsNull("BgReq") ? string.Empty : Convert.ToString(dr1["BgReq"]);
        //                        obj.Inspection = dr1.IsNull("Inspection") ? string.Empty : Convert.ToString(dr1["Inspection"]);
        //                        obj.Remarks = dr1.IsNull("Remarks") ? string.Empty : Convert.ToString(dr1["Remarks"]);
        //                        obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
        //                        obj.GeoArea = dr1.IsNull("GeoArea") ? string.Empty : Convert.ToString(dr1["GeoArea"]);
        //                        obj.EnqFor = dr1.IsNull("EnqFor") ? string.Empty : Convert.ToString(dr1["EnqFor"]);
        //                        obj.MainProdGrp = dr1.IsNull("ProdGrp") ? string.Empty : Convert.ToString(dr1["ProdGrp"]);
        //                        obj.Status = dr1.IsNull("Status") ? string.Empty : Convert.ToString(dr1["Status"]);
        //                        obj.Remarks = dr1.IsNull("Remarks") ? string.Empty : Convert.ToString(dr1["Remarks"]);
        //                        obj.Currency = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
        //                        obj.DeliveryTerms = dr1.IsNull("DeliveryTerms") ? string.Empty : Convert.ToString(dr1["DeliveryTerms"]);
        //                        obj.Subject = dr1.IsNull("Subject") ? string.Empty : Convert.ToString(dr1["Subject"]);

        //                        qED.lstQuoteEntity.Add(obj);
        //                    }
        //                }

        //                if (dt2.Rows.Count > 0)
        //                {
        //                    foreach (DataRow dr2 in dt2.Rows)
        //                    {
        //                        qED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
        //                    }
        //                }
        //            }
        //        }

        //        return qED;
        //    }
        //    catch (Exception Ex)
        //    {
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
        //        throw Ex;
        //    }
        //}


        public QuotationEntity QuotationDetailsPopup(QuotationEntity Model)
        {
            try
            {
                DataSet ds = _repository.QuotationDetailsPopup(Model);
                //Model = ExtractQuotationDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);
                    Model.CustomerId = dt1.Rows[0]["CustomerId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerId"]);
                    Model.CustomerName = dt1.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerName"]);
                    Model.Country = dt1.Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Country"]);
                    Model.CountryId = dt1.Rows[0]["CountryId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CountryId"]);
                    Model.QuoteType = dt1.Rows[0]["QuoteType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteType"]);
                    Model.FileNo = dt1.Rows[0]["FileNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FileNo"]);
                    Model.EnqRef = dt1.Rows[0]["EnqRef"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqRef"]);
                    Model.EnqFor = dt1.Rows[0]["EnqFor"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqFor"]);
                    Model.EnqDt = dt1.Rows[0]["EnqDt"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqDt"]);
                    Model.EnqNo = dt1.Rows[0]["EnqNo"] == DBNull.Value ? "" : Convert.ToString(dt1.Rows[0]["EnqNo"]);
                    Model.EnqNos = dt1.Rows[0]["EnqNo"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["EnqNo"]);

                    Model.QuoteNo = dt1.Rows[0]["QuoteNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteNo"]);
                    Model.QuoteNoView = dt1.Rows[0]["QuoteNoView"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteNoView"]);

                    Model.QuoteDate = dt1.Rows[0]["QuoteDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteDate"]);
                    Model.QuoteValidity = dt1.Rows[0]["QuoteValidity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteValidity"]);
                    Model.BgReq = dt1.Rows[0]["BgReq"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BgReq"]);
                    Model.Inspection = dt1.Rows[0]["Inspection"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Inspection"]);
                    Model.Remarks = dt1.Rows[0]["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Remarks"]);
                    Model.MainProdGrp = dt1.Rows[0]["ProdGrp"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ProdGrp"]);
                    Model.GeoArea = dt1.Rows[0]["GeoArea"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["GeoArea"]);
                    Model.GeoCode = dt1.Rows[0]["GeoCode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["GeoCode"]);
                    Model.Status = dt1.Rows[0]["Status"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Status"]);
                    Model.DeliveryTerms = dt1.Rows[0]["DeliveryTerms"] == DBNull.Value ? "" : Convert.ToString(dt1.Rows[0]["DeliveryTerms"]);
                    Model.ModeOfDespatch = dt1.Rows[0]["ModeOfDespatch"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ModeOfDespatch"]);
                    Model.PortOfDischarge = dt1.Rows[0]["PortOfDischarge"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PortOfDischarge"]);
                    Model.Currency = dt1.Rows[0]["Currency"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Currency"]);
                    Model.LeadTime = dt1.Rows[0]["LeadTime"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["LeadTime"]);
                    Model.LeadTimeDuration = dt1.Rows[0]["LeadTimeDuration"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["LeadTimeDuration"]);
                    Model.PaymentTerms = dt1.Rows[0]["PaymentTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PaymentTerms"]);
                    Model.SalesPerson = dt1.Rows[0]["SalesPerson"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SalesPerson"]);
                    Model.Subject = dt1.Rows[0]["Subject"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Subject"]);
                    Model.QuoteSentOn = dt1.Rows[0]["QuoteSentOn"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteSentOn"]);
                    Model.FinancialYear = dt1.Rows[0]["FinancialYear"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FinancialYear"]);
                    Model.SupplyTerms = dt1.Rows[0]["SupplyTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SupplyTerms"]);
                    Model.QuoteAddOnType= dt1.Rows[0]["QuoteAddOnType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteAddOnType"]);
                    Model.RevisedQuoteNo= dt1.Rows[0]["RevisedQuoteNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["RevisedQuoteNo"]);


                    //if (dt1.Rows[0]["DomesticQuoteNo"] != DBNull.Value)
                    //    Model.QuoteNo = Convert.ToString(dt1.Rows[0]["DomesticQuoteNo"]);
                    //else if (dt1.Rows[0]["ExportQuoteNo"] != DBNull.Value)
                    //    Model.QuoteNo = Convert.ToString(dt1.Rows[0]["ExportQuoteNo"]);
                    //else if (dt1.Rows[0]["ServiceQuoteNo"] != DBNull.Value)
                    //    Model.QuoteNo = Convert.ToString(dt1.Rows[0]["ServiceQuoteNo"]);
                    //else
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }


        QuotationEntity ExtractQuotationDetailsEntity(DataSet ds, QuotationEntity Model)
        {
            DataTable dt = ds.Tables[0];
            List<DropDownEntity> LstEOQ = new List<DropDownEntity>();
            BindDefault(LstEOQ);
            List<DropDownEntity> LstLeadTimeDuration = new List<DropDownEntity>();
            BindDefault(LstLeadTimeDuration);
            List<DropDownEntity> LstYesNo = new List<DropDownEntity>();
            BindDefault(LstYesNo);
            List<DropDownEntity> LstEnqThru = new List<DropDownEntity>();
            BindDefault(LstEnqThru);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Property"].ToString() == ConfigurationManager.AppSettings["EOQ"])
                    {
                        DropDownEntity ObjModel = BindDatas(dr);
                        LstEOQ.Add(ObjModel);
                    }
                    if (dr["Property"].ToString() == ConfigurationManager.AppSettings["LeadTimeDuration"])
                    {
                        DropDownEntity ObjModel = BindDatas(dr);
                        LstLeadTimeDuration.Add(ObjModel);
                    }
                    if (dr["Property"].ToString() == ConfigurationManager.AppSettings["YesNo"])
                    {
                        DropDownEntity ObjModel = BindDatas(dr);
                        LstYesNo.Add(ObjModel);
                    }
                    if (dr["Property"].ToString() == ConfigurationManager.AppSettings["EnqThru"])
                    {
                        DropDownEntity ObjModel = BindDatas(dr);
                        LstEnqThru.Add(ObjModel);
                    }
                }
            }


            //Model.LstEOQ = LstEOQ;
            //Model.LstLeadTimeDuration = LstLeadTimeDuration;
            //Model.LstYesNo = LstYesNo;
            //Model.LstEnqThru = LstEnqThru;

            return Model;
        }

        private DropDownEntity BindDatas(DataRow dr)
        {
            DropDownEntity Entity = new DropDownEntity();
            try
            {
                Entity.DataValueField = Convert.ToInt32(dr["DefaultID"]);
                Entity.DataTextField = dr["DefaultValue"].ToString();
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Entity;
        }

        private void BindDefault(List<DropDownEntity> List)
        {
            try
            {
                DropDownEntity Obj = new DropDownEntity();
                Obj.DataValueField = -1;
                Obj.DataTextField = "Select";
                List.Add(Obj);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
        }

        public List<DropDownEntity> GetCityName(string VendorName)
        {
            try
            {
                List<DropDownEntity> lstCityName = new List<DropDownEntity>();
                DataTable dt = _repository.GetCityNames();
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("Id"))
                            entity.DataValueField = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("City"))
                            entity.DataTextField = dr["City"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstCityName.Add(entity);
                    }
                }

                return lstCityName;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<DropDownEntity> GetDdlValueForQuote(string type, string VendorId = null, string QuoteType = null, string SubjectId = null)
        {
            try
            {
                List<DropDownEntity> lstDdl = new List<DropDownEntity>();
                DataTable dt = _repository.GetDdlValueForQuote(type, VendorId, QuoteType, SubjectId);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    //if (type == "ProductGroup")
                        foreach (DataRow dr in dt.Rows)
                        {
                            entity = new DropDownEntity();
                            if (dt.Columns.Contains("Id"))
                                entity.DataStringValueField = Convert.ToString(dr["Id"] ?? "0");

                            if (dt.Columns.Contains("Value"))
                                entity.DataTextField = dr["Value"]?.ToString() ?? "";

                            lstDdl.Add(entity);
                        }
                    //else if (type == "CustomerId")
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        entity = new DropDownEntity();
                    //        if (dt.Columns.Contains("Id"))
                    //            entity.DataStringValueField = Convert.ToString(dr["Id"] ?? "0");

                    //        if (dt.Columns.Contains("CustomerId"))
                    //            entity.DataTextField = dr["CustomerId"]?.ToString() ?? "";

                    //        lstDdl.Add(entity);
                    //    }
                }

                DropDownEntity entity1 = new DropDownEntity();
                entity1.DataStringValueField = "";
                entity1.DataTextField = "Select";

                lstDdl.Insert(0, entity1);
                return lstDdl;
            }

            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }

        }











    }
}
