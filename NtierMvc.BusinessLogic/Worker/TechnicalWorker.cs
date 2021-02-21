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
using Newtonsoft.Json;

namespace NtierMvc.BusinessLogic.Worker
{
    public class TechnicalWorker : ITechnicalWorker
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        private Repository _repository = new Repository();

        public TechnicalWorker()
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

        public string SaveQuotationDetails(QuotationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveQuotationDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveQuotePreparation(QuotationPreparationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveQuotePreparation(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

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
                    oVendor.CustomerId = Convert.ToString(dtRecord.Rows[0]["CustomerId"]);
                    oVendor.CustomerName = Convert.ToString(dtRecord.Rows[0]["CustomerName"]);
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
                    //oCust.QuoteNo = Convert.ToString(dtRecord.Rows[0]["QuoteNo"]);
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


        public QuotationEntityDetails GetQuoteRegList(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchQuoteCustomerID = null, string SearchSubject = null, string SearchDeliveryTerms = null)
        {
            try
            {
                QuotationEntityDetails qED = new QuotationEntityDetails();
                qED.lstQuoteEntity = new List<QuotationEntity>();
                DataSet ds = _repository.GetQuoteRegList(pageIndex, pageSize, SearchQuoteType, SearchQuoteCustomerID, SearchSubject, SearchDeliveryTerms);
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
                                QuotationEntity obj = new QuotationEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
                                obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
                                obj.CustomerId = dr1.IsNull("CustomerId") ? string.Empty : Convert.ToString(dr1["CustomerId"]);
                                obj.CustomerName = dr1.IsNull("CustomerName") ? string.Empty : Convert.ToString(dr1["CustomerName"]);
                                obj.QuoteType = dr1.IsNull("QuoteType") ? string.Empty : Convert.ToString(dr1["QuoteType"]);
                                obj.FileNo = dr1.IsNull("FileNo") ? string.Empty : Convert.ToString(dr1["FileNo"]);
                                obj.EnqRef = dr1.IsNull("EnqRef") ? string.Empty : Convert.ToString(dr1["EnqRef"]);
                                obj.EnqDt = dr1.IsNull("EnqDt") ? string.Empty : Convert.ToString(dr1["EnqDt"]);
                                obj.QuoteNo = dr1.IsNull("QuoteNo") ? string.Empty : Convert.ToString(dr1["QuoteNo"]);
                                obj.QuoteDate = dr1.IsNull("QuoteDate") ? string.Empty : Convert.ToString(dr1["QuoteDate"]);
                                obj.QuoteValidity = dr1.IsNull("QuoteValidity") ? string.Empty : Convert.ToString(dr1["QuoteValidity"]);
                                obj.BgReq = dr1.IsNull("BgReq") ? string.Empty : Convert.ToString(dr1["BgReq"]);
                                obj.Inspection = dr1.IsNull("Inspection") ? string.Empty : Convert.ToString(dr1["Inspection"]);
                                obj.Remarks = dr1.IsNull("Remarks") ? string.Empty : Convert.ToString(dr1["Remarks"]);
                                obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.EnqFor = dr1.IsNull("EnqFor") ? string.Empty : Convert.ToString(dr1["EnqFor"]);
                                obj.MainProdGrp = dr1.IsNull("ProdGrp") ? string.Empty : Convert.ToString(dr1["ProdGrp"]);
                                obj.GeoArea = dr1.IsNull("GeoArea") ? string.Empty : Convert.ToString(dr1["GeoArea"]);
                                obj.Status = dr1.IsNull("Status") ? string.Empty : Convert.ToString(dr1["Status"]);
                                obj.Currency = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
                                obj.EnqDt = dr1.IsNull("EnqDt") ? string.Empty : Convert.ToString(dr1["EnqDt"]);
                                obj.EnqNo = dr1.IsNull("EnqNo") ? string.Empty : Convert.ToString(dr1["EnqNo"]);
                                obj.Subject = dr1.IsNull("Subject") ? string.Empty : Convert.ToString(dr1["Subject"]);
                                obj.DeliveryTerms = dr1.IsNull("DeliveryTerms") ? string.Empty : Convert.ToString(dr1["DeliveryTerms"]);

                                qED.lstQuoteEntity.Add(obj);
                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                qED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }

                return qED;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


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
                    Model.CustomerId = dt1.Rows[0]["VendorId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorId"]);
                    Model.CustomerName = dt1.Rows[0]["VendorName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorName"]);
                    Model.QuoteType = dt1.Rows[0]["QuoteType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteType"]);
                    Model.FileNo = dt1.Rows[0]["FileNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FileNo"]);
                    Model.EnqRef = dt1.Rows[0]["EnqRef"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqRef"]);
                    Model.EnqDt = dt1.Rows[0]["EnqDt"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqDt"]);
                    Model.QuoteNo = dt1.Rows[0]["QuoteNoView"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteNoView"]);
                    Model.QuoteDate = dt1.Rows[0]["QuoteDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteDate"]);
                    Model.QuoteValidity = dt1.Rows[0]["QuoteValidity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteValidity"]);
                    Model.BgReq = dt1.Rows[0]["BgReq"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["BgReq"]);
                    Model.Inspection = dt1.Rows[0]["Inspection"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Inspection"]);
                    Model.Remarks = dt1.Rows[0]["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Remarks"]);
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

        public List<DropDownEntity> GetProductLineList(string productLine)
        {
            try
            {
                List<DropDownEntity> lstCityName = new List<DropDownEntity>();
                DataTable dt = _repository.GetProductList(productLine);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("Id"))
                            entity.DataValueField = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("ProductName"))
                            entity.DataTextField = dr["ProductName"]?.ToString() ?? "";

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

        public String GetProductNumber(string productNameId, int productType)
        {
            try
            {
                List<DropDownEntity> lstCityName = new List<DropDownEntity>();
                DataTable dt = _repository.GetProductNumber(productNameId, productType);
                ProductEntity entity = new ProductEntity();

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dt.Columns.Contains("ProductNo"))
                        entity.ProductNo = dr["ProductNo"]?.ToString() ?? "";

                    //if (dt.Columns.Contains("CourseCode"))
                    //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";
                }

                return entity.ProductNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<DropDownEntity> GetEnqNoList(string vendorId)
        {
            try
            {
                List<DropDownEntity> lstEnqNo = new List<DropDownEntity>();
                DataTable dt = _repository.GetEnqNoList(vendorId);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("EnquiryId"))
                            entity.DataStringValueField = Convert.ToString(dr["EnquiryId"] ?? 0);

                        if (dt.Columns.Contains("ENQREF"))
                            entity.DataTextField = dr["ENQREF"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstEnqNo.Add(entity);
                    }
                }

                return lstEnqNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public string GetQuoteNo(string quotetypeId = null)
        {
            string quoteCode = "";
            try
            {
                quoteCode = _repository.GetQuoteNo(quotetypeId);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return quoteCode;
        }

        public List<DropDownEntity> GetQuoteItemSlNoList(string quotetypeId = "", string SoNo = null)
        {
            try
            {
                List<DropDownEntity> lstQuoteItemSlNo = new List<DropDownEntity>();
                DataTable dt = _repository.GetQuoteItemSlNoList(quotetypeId, SoNo);
                DropDownEntity entity;

                entity = new DropDownEntity();
                entity.DataStringValueField = "";
                entity.DataTextField = "Select";
                lstQuoteItemSlNo.Add(entity);


                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("Id"))
                            entity.DataStringValueField = Convert.ToString(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("ItemNo"))
                            entity.DataTextField = dr["ItemNo"]?.ToString() ?? "";


                        lstQuoteItemSlNo.Add(entity);
                    }
                }


                return lstQuoteItemSlNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<DropDownEntity> GetQuoteNoList(string quotetypeId = "", string SoNo = null)
        {
            try
            {
                List<DropDownEntity> lstEnqNo = new List<DropDownEntity>();
                DataTable dt = _repository.GetQuoteNoList(quotetypeId, SoNo);
                DropDownEntity entity;

                entity = new DropDownEntity();
                entity.DataStringValueField = "";
                entity.DataTextField = "Select";
                lstEnqNo.Add(entity);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("QuoteNo"))
                            entity.DataStringValueField = Convert.ToString(dr["QuoteNo"] ?? 0);

                        if (dt.Columns.Contains("QuoteTypeNo"))
                            entity.DataTextField = dr["QuoteTypeNo"]?.ToString() ?? "";


                        if (dt.Columns.Contains("CustomerName"))
                            entity.DataAltValueField = dr["CustomerName"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstEnqNo.Add(entity);
                    }
                }

                return lstEnqNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<DropDownEntity> GetQuoteEnqNoList(QuotationEntity qE)
        {
            try
            {
                List<DropDownEntity> lstEnqNo = new List<DropDownEntity>();
                DataTable dt = _repository.GetQuoteEnqNoList(qE);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("EnquiryId"))
                            entity.DataStringValueField = Convert.ToString(dr["EnquiryId"] ?? 0);

                        if (dt.Columns.Contains("ENQREF"))
                            entity.DataTextField = dr["ENQREF"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstEnqNo.Add(entity);
                    }
                }

                return lstEnqNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<ProductEntity> GetPrepProductNames(string productId, string casingSize, string type = null)
        {
            try
            {
                List<ProductEntity> lstProdEnt = new List<ProductEntity>();
                DataTable dt = _repository.GetPrepProductNames(productId, casingSize, type);
                ProductEntity entity;
                ProductDetails eDet;

                if (dt.Rows.Count > 0)
                {

                    entity = new ProductEntity();

                    //Start

                    if (dt.Columns.Contains("CasingSize"))
                        entity.CasingSize = dt.Rows[0]["CasingSize"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Connection"))
                        entity.Connection = dt.Rows[0]["Connection"]?.ToString() ?? "";
                    if (dt.Columns.Contains("MaterialGrade"))
                        entity.MaterialGrade = dt.Rows[0]["MaterialGrade"]?.ToString() ?? "";


                    if (dt.Columns.Contains("Pos1"))
                        entity.Pos1 = dt.Rows[0]["Pos1"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Pos2"))
                        entity.Pos2 = dt.Rows[0]["Pos2"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Pos3"))
                        entity.Pos3 = dt.Rows[0]["Pos3"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Pos4"))
                        entity.Pos4 = dt.Rows[0]["Pos4"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Pos5"))
                        entity.Pos5 = dt.Rows[0]["Pos5"]?.ToString() ?? "";
                    if (dt.Columns.Contains("Pos6"))
                        entity.Pos6 = dt.Rows[0]["Pos6"]?.ToString() ?? "";
                    //if (dt.Columns.Contains("Pos7"))
                    //    entity.Pos7 = dt.Rows[0]["Pos7"]?.ToString() ?? "";
                    //if (dt.Columns.Contains("Pos8"))
                    //    entity.Pos8 = dt.Rows[0]["Pos8"]?.ToString() ?? "";
                    //if (dt.Columns.Contains("Pos9"))
                    //    entity.Pos9 = dt.Rows[0]["Pos9"]?.ToString() ?? "";
                    //if (dt.Columns.Contains("Pos10"))
                    //    entity.Pos10 = dt.Rows[0]["Pos10"]?.ToString() ?? "";


                    if (dt.Columns.Contains("FieldName1"))
                        entity.FieldName1 = dt.Rows[0]["FieldName1"]?.ToString() ?? "";
                    if (dt.Columns.Contains("FieldName2"))
                        entity.FieldName2 = dt.Rows[0]["FieldName2"]?.ToString() ?? "";
                    if (dt.Columns.Contains("FieldName3"))
                        entity.FieldName3 = dt.Rows[0]["FieldName3"]?.ToString() ?? "";
                    if (dt.Columns.Contains("FieldName4"))
                        entity.FieldName4 = dt.Rows[0]["FieldName4"]?.ToString() ?? "";
                    if (dt.Columns.Contains("FieldName5"))
                        entity.FieldName5 = dt.Rows[0]["FieldName5"]?.ToString() ?? "";
                    if (dt.Columns.Contains("FieldName6"))
                        entity.FieldName6 = dt.Rows[0]["FieldName6"]?.ToString() ?? "";


                    if (dt.Columns.Contains("DES"))
                        entity.DES = dt.Rows[0]["DES"]?.ToString() ?? "";

                    if (dt.Columns.Contains("DESQuery"))
                        entity.DESQuery = dt.Rows[0]["DESQuery"]?.ToString() ?? "";

                    if (dt.Columns.Contains("NET_WEIGHT"))
                        entity.NetWeight = dt.Rows[0]["NET_WEIGHT"]?.ToString() ?? "";

                    if (dt.Columns.Contains("GROSS_Weight"))
                        entity.GrossWeight = dt.Rows[0]["GROSS_Weight"]?.ToString() ?? "";

                    //End

                    //StringBuilder sb = new StringBuilder();
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    ProductAccessories entity1 = new ProductAccessories();

                    //    if (dt.Columns.Contains("ProductNamePA"))
                    //        entity1.ProductNamePA = dr["ProductNamePA"]?.ToString() ?? "";

                    //    //if (dt.Columns.Contains("ProductCodePA"))
                    //    //    entity1.ProductCodePA = dr["ProductCodePA"]?.ToString() ?? "";

                    //    //if (dt.Columns.Contains("MasterProductName"))
                    //    //    entity1.MasterProductName = dr["MasterProductName"]?.ToString() ?? "";

                    //    //if (dt.Columns.Contains("Pos1PA"))
                    //    //    entity1.Pos1PA = dr["Pos1PA"]?.ToString() ?? "";
                    //    //if (dt.Columns.Contains("Pos2PA"))
                    //    //    entity1.Pos2PA = dr["Pos2PA"]?.ToString() ?? "";
                    //    //if (dt.Columns.Contains("Pos3PA"))
                    //    //    entity1.Pos3PA = dr["Pos3PA"]?.ToString() ?? "";
                    //    //if (dt.Columns.Contains("Pos4PA"))
                    //    //    entity1.Pos4PA = dr["Pos4PA"]?.ToString() ?? "";
                    //    //if (dt.Columns.Contains("Pos5PA"))
                    //    //    entity1.Pos5PA = dr["Pos5PA"]?.ToString() ?? "";

                    //    sb.Append(entity1.ProductNamePA + "\r\n");
                    //}

                    //entity.subProductDetails = sb.ToString();

                    lstProdEnt.Add(entity);
                }

                return lstProdEnt;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        //New Methods
        public string SaveOrderDetail(OrderEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveOrderDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveItemDetail(ItemEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveItemDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveItemDetailList(BulkUploadEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveItemDetailList(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public OrderEntity GetUserOrderDetails(string unitNo)
        {
            OrderEntity result = new OrderEntity();
            try
            {
                result = DT2VendorCust(_repository.GetUserOrderDetails(unitNo));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public OrderEntity GetVendorOrderDetails(string vendorId)
        {
            OrderEntity result = new OrderEntity();
            try
            {
                result = DT2OrderVendor(_repository.GetVendorOrderDetails(vendorId));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public OrderEntity DT2OrderVendor(DataTable dtRecord)
        {
            OrderEntity oVendor = new OrderEntity();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {
                    oVendor.UnitNo = Convert.ToString(dtRecord.Rows[0]["Id"]);
                    oVendor.CustomerId = Convert.ToString(dtRecord.Rows[0]["VENDORID"]);
                    oVendor.CustomerName = Convert.ToString(dtRecord.Rows[0]["VendorName"]);
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oVendor;
        }

        public OrderEntity DT2VendorCust(DataTable dtRecord)
        {
            OrderEntity oCust = new OrderEntity();
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

        public string DeleteOrderDetail(int Id)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteOrderDetail(Id);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }


        public OrderEntityDetails GetOrderDetails(int pageIndex, int pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            try
            {
                OrderEntityDetails qED = new OrderEntityDetails();
                qED.lstOrderEntity = new List<OrderEntity>();
                DataSet ds = _repository.GetOrderDetails(pageIndex, pageSize, SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms, SearchPODeliveryDate);
                //DataTable dt = _repository.GetOrderDetails();

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
                                OrderEntity obj = new OrderEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                //obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
                                //obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
                                //obj.VendorId = dr1.IsNull("VendorId") ? string.Empty : Convert.ToString(dr1["VendorId"]);
                                //obj.VendorName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                                //obj.QuoteType = dr1.IsNull("QuoteType") ? string.Empty : Convert.ToString(dr1["QuoteType"]);
                                obj.FileNo = dr1.IsNull("FILENO") ? string.Empty : Convert.ToString(dr1["FILENO"]);
                                obj.QuoteNo = dr1.IsNull("QuoteNo") ? string.Empty : Convert.ToString(dr1["QUOTENO"]);
                                //obj.QuoteDate = dr1.IsNull("QuoteDate") ? string.Empty : Convert.ToString(dr1["QuoteDate"]);
                                obj.SoNo = dr1.IsNull("SoNo") ? string.Empty : Convert.ToString(dr1["SoNo"]);
                                obj.SoNoView = dr1.IsNull("SoNoView") ? string.Empty : Convert.ToString(dr1["SoNoView"]);
                                obj.PoEntity = dr1.IsNull("PoEntity") ? string.Empty : Convert.ToString(dr1["PoEntity"]);
                                obj.PoLocation = dr1.IsNull("PoLocation") ? string.Empty : Convert.ToString(dr1["PoLocation"]);
                                obj.PoDor = dr1.IsNull("PoDor") ? string.Empty : Convert.ToString(dr1["PoDor"]);
                                obj.PoNo = dr1.IsNull("PoNo") ? string.Empty : Convert.ToString(dr1["PoNo"]);
                                obj.PoDate = dr1.IsNull("PoDate") ? string.Empty : Convert.ToString(dr1["PoDate"]);
                                //obj.PoSLNo = dr1.IsNull("PoSLNo") ? string.Empty : Convert.ToString(dr1["PoSLNo"]);
                                //obj.PoItemDescription = dr1.IsNull("PoItemDescription") ? string.Empty : Convert.ToString(dr1["PoItemDescription"]);
                                //obj.PoQty = dr1.IsNull("PoQty") ? string.Empty : Convert.ToString(dr1["PoQty"]);
                                obj.Curr = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
                                obj.ExWorkValue = dr1.IsNull("ExWorkValue") ? string.Empty : Convert.ToString(dr1["ExWorkValue"]);
                                //obj.UnitPrice = dr1.IsNull("UnitPrice") ? string.Empty : Convert.ToString(dr1["UnitPrice"]);
                                obj.PoDeliveryDate = dr1.IsNull("PoDeliveryDate") ? string.Empty : Convert.ToString(dr1["PoDeliveryDate"]);
                                //obj.PoTerms = dr1.IsNull("PoTerms") ? string.Empty : Convert.ToString(dr1["PoTerms"]);
                                //obj.SupplyTerms = dr1.IsNull("SupplyTerms") ? string.Empty : Convert.ToString(dr1["SupplyTerms"]);
                                //obj.LotWiseQty = dr1.IsNull("LotWiseQty") ? string.Empty : Convert.ToString(dr1["LotWiseQty"]);
                                //obj.LotWiseDate = dr1.IsNull("LotWiseDate") ? string.Empty : Convert.ToString(dr1["LotWiseDate"]);
                                obj.ConsigneeName = dr1.IsNull("ConsigneeName") ? string.Empty : Convert.ToString(dr1["ConsigneeName"]);
                                obj.ConsigneeLocation = dr1.IsNull("ConsigneeLocation") ? string.Empty : Convert.ToString(dr1["ConsigneeLocation"]);
                                obj.CustomerId = dr1.IsNull("CustomerId") ? string.Empty : Convert.ToString(dr1["CustomerId"]);
                                //obj.InspectionAgency = dr1.IsNull("InspectionAgency") ? string.Empty : Convert.ToString(dr1["InspectionAgency"]);
                                //obj.ModeOfShipment = dr1.IsNull("ModeOfShipment") ? string.Empty : Convert.ToString(dr1["ModeOfShipment"]);
                                //obj.PaymentTerms = dr1.IsNull("PaymentTerms") ? string.Empty : Convert.ToString(dr1["PaymentTerms"]);
                                obj.IsActive = dr1.IsNull("IsActive") ? string.Empty : Convert.ToString(dr1["IsActive"]);
                                obj.Remarks = dr1.IsNull("REMARKS") ? string.Empty : Convert.ToString(dr1["REMARKS"]);
                                obj.WADate = dr1.IsNull("WADate") ? string.Empty : Convert.ToString(dr1["WADate"]);
                                qED.lstOrderEntity.Add(obj);
                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                qED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }

                return qED;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public OrderEntity OrderDetailsPopup(OrderEntity Model)
        {
            try
            {
                DataSet ds = _repository.OrderDetailsPopup(Model);
                //Model = ExtractOrderDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);
                    Model.CustomerId = dt1.Rows[0]["CustomerId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerId"]);
                    Model.CustomerName = dt1.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerName"]);
                    Model.QuoteTypeValue = dt1.Rows[0]["QuoteTypeValue"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteTypeValue"]);
                    Model.QuoteQtyType = dt1.Rows[0]["QuoteQtyType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteQtyType"]);
                    Model.QuoteType = dt1.Rows[0]["QuoteType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteType"]);
                    Model.FileNo = dt1.Rows[0]["FileNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FileNo"]);
                    Model.QuoteNo = dt1.Rows[0]["QuoteNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteNo"]);
                    Model.QuoteDate = dt1.Rows[0]["QuoteDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteDate"]);
                    Model.SoNo = dt1.Rows[0]["SoNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SoNo"]);
                    Model.SoNoView = dt1.Rows[0]["SoNoView"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SoNoView"]);
                    Model.PoEntity = dt1.Rows[0]["PoEntity"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoEntity"]);
                    Model.PoLocation = dt1.Rows[0]["PoLocation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoLocation"]);
                    Model.PoDor = dt1.Rows[0]["PoDor"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoDor"]);
                    Model.PoNo = dt1.Rows[0]["PoNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoNo"]);
                    Model.PoDate = dt1.Rows[0]["PoDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoDate"]);
                    Model.Curr = dt1.Rows[0]["Currency"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Currency"]);
                    Model.PoDeliveryDate = dt1.Rows[0]["PoDeliveryDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoDeliveryDate"]);
                    Model.PoTerms = dt1.Rows[0]["PoTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PoTerms"]);
                    Model.SupplyTerms = dt1.Rows[0]["SupplyTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["SupplyTerms"]);

                    Model.ConsigneeName = dt1.Rows[0]["ConsigneeName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ConsigneeName"]);
                    Model.ConsigneeLocation = dt1.Rows[0]["ConsigneeLocation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ConsigneeLocation"]);
                    Model.InspectionAgency = dt1.Rows[0]["InspectionAgency"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["InspectionAgency"]);
                    Model.ModeOfShipment = dt1.Rows[0]["ModeOfShipment"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ModeOfShipment"]);
                    Model.PaymentTerms = dt1.Rows[0]["PaymentTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["PaymentTerms"]);
                    Model.DeliveryTerms = dt1.Rows[0]["DeliveryTerms"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DeliveryTerms"]);
                    Model.ExWorkValue = dt1.Rows[0]["ExWorkValue"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ExWorkValue"]);
                    Model.Inspection = dt1.Rows[0]["Inspection"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Inspection"]);
                    Model.EndUser = dt1.Rows[0]["EndUser"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EndUser"]);
                    Model.MainProdGrp = dt1.Rows[0]["ProductGroup"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ProductGroup"]);
                    Model.MultiQuoteNos = dt1.Rows[0]["MultiQuoteNos"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["MultiQuoteNos"]);
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }


        OrderEntity ExtractOrderDetailsEntity(DataSet ds, OrderEntity Model)
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

        public List<DropDownEntity> GetOrderQuoteDetails(string quoteType, string quoteNoId)
        {
            try
            {
                List<DropDownEntity> lstEnqNo = new List<DropDownEntity>();
                DataTable dt = _repository.GetOrderQuoteDetails(quoteType, quoteNoId);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        //if (dt.Columns.Contains("Id"))
                        //    entity.DataStringValueField = Convert.ToString(dr["Id"] ?? 0);

                        //if (dt.Columns.Contains("ViewProductDetails"))
                        //    entity.DataTextField = dr["ViewProductDetails"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ItemNo"))
                            entity.DataAltValueField = dr["ItemNo"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstEnqNo.Add(entity);
                    }
                }

                return lstEnqNo;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public OrderEntity GetQuoteOrderDetails(string quoteType, string quoteNoId)
        {
            try
            {
                OrderEntity orderEntity = new OrderEntity();
                DataTable dt = _repository.GetOrderDetailsForQuotes(quoteType, quoteNoId);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("Id"))
                            orderEntity.Id = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("CustomerID"))
                            orderEntity.CustomerId = dr["CustomerID"]?.ToString() ?? "";

                        if (dt.Columns.Contains("CustomerNAME"))
                            orderEntity.CustomerName = dr["CustomerNAME"]?.ToString() ?? "";

                        if (dt.Columns.Contains("FILENO"))
                            orderEntity.FileNo = dr["FILENO"]?.ToString() ?? "";

                        if (dt.Columns.Contains("SoNo"))
                            orderEntity.SoNo = dr["SoNo"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ProdGrp"))
                            orderEntity.MainProdGrp = dr["ProdGrp"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Country"))
                            orderEntity.PoLocation = dr["Country"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Currency"))
                            orderEntity.Curr = dr["Currency"]?.ToString() ?? "";

                        if (dt.Columns.Contains("DeliveryTerms"))
                            orderEntity.DeliveryTerms = dr["DeliveryTerms"]?.ToString() ?? "";
                    }
                }

                return orderEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public ItemEntity GetOrderDetailsFromSO(int SoNo)
        {
            try
            {
                ItemEntity itemEntity = new ItemEntity();
                DataTable dt = _repository.GetOrderDetailsFromSO(SoNo);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("Id"))
                            itemEntity.Id = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("ExWorkValue"))
                            itemEntity.ExWorkValue = dr["ExWorkValue"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PONO"))
                            itemEntity.PoNo = dr["PONO"]?.ToString() ?? "";

                        if (dt.Columns.Contains("CustomerID"))
                            itemEntity.CustomerId = dr["CustomerID"]?.ToString() ?? "";

                        if (dt.Columns.Contains("CustomerNAME"))
                            itemEntity.CustomerName = dr["CustomerNAME"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PODATE"))
                            itemEntity.PoDate = dr["PODATE"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PODELIVERYDATE"))
                            itemEntity.PoDeliveryDate = dr["PODELIVERYDATE"]?.ToString() ?? "";

                        if (dt.Columns.Contains("POPercent"))
                            itemEntity.POPercent = dr["POPercent"]?.ToString() ?? "";

                        if (dt.Columns.Contains("SUPPLYTERMS"))
                            itemEntity.SupplyTerms = dr["SUPPLYTERMS"]?.ToString() ?? "";

                        if (dt.Columns.Contains("QUOTENO"))
                            itemEntity.QuoteNo = dr["QUOTENO"]?.ToString() ?? "";

                        if (dt.Columns.Contains("SUPPLYTERMSTEXT"))
                            itemEntity.SupplyTermsText = dr["SUPPLYTERMSTEXT"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ProductGroup"))
                            itemEntity.ProductGroup = dr["ProductGroup"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoQty"))
                            itemEntity.PoQty = dr["PoQty"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoItemDescription"))
                            itemEntity.PoItemDescription = dr["PoItemDescription"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("PoSLNo"))
                        //    itemEntity.PoSLNo = dr["PoSLNo"]?.ToString() ?? "";


                    }
                }

                return itemEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }



        public DataTable GetDataForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            DataTable dt = _repository.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
            return dt;
        }

        public DataTable GetListForDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId)
        {
            DataTable dt = _repository.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
            return dt;
        }

        public string SaveRevisedQuotationDetails(QuotationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveRevisedQuotationDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DropDownEntity> GetVendorDetails(string quotetypeId)
        {
            try
            {
                List<DropDownEntity> lstDrpDwn = new List<DropDownEntity>();
                DataTable dt = _repository.GetVendorDetails(quotetypeId);
                DropDownEntity entity;

                DropDownEntity eDdl = new DropDownEntity();
                eDdl.DataTextField = "Select";
                lstDrpDwn.Add(eDdl);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        if (dt.Columns.Contains("Id"))
                            entity.DataStringValueField = Convert.ToString(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("CustomerID"))
                            entity.DataTextField = dr["CustomerID"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("ItemNo"))
                        //    entity.DataAltValueField = dr["ItemNo"]?.ToString() ?? "";

                        //if (dt.Columns.Contains("CourseCode"))
                        //    entity.DataCodeField = dr["CourseCode"]?.ToString() ?? "";

                        lstDrpDwn.Add(entity);
                    }
                }

                return lstDrpDwn;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public QuotationPreparationEntity GetQuotePrepDetails(int itemNoId, int quoteType, int quoteNo)
        {
            try
            {
                QuotationPreparationEntity quoteEntity = new QuotationPreparationEntity();
                DataTable dt = _repository.GetQuotePrepDetails(itemNoId, quoteType, quoteNo);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("Id"))
                            quoteEntity.Id = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("VENDORNAME"))
                            quoteEntity.CustomerName = dr["VENDORNAME"]?.ToString() ?? "";

                        if (dt.Columns.Contains("EnqSrNo"))
                            quoteEntity.EnqSrNo = dr["EnqSrNo"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ProductName"))
                            quoteEntity.ProductName = dr["ProductName"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ProductNo"))
                            quoteEntity.ProductNo = dr["ProductNo"]?.ToString() ?? "";

                        if (dt.Columns.Contains("CasingPpf"))
                            quoteEntity.CasingPpf = dr["CasingPpf"]?.ToString() ?? "";

                        if (dt.Columns.Contains("CasingSize"))
                            quoteEntity.CasingSize = dr["CasingSize"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Connection"))
                            quoteEntity.Connection = dr["Connection"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Qty"))
                            quoteEntity.Qty = dr["Qty"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Uom"))
                            quoteEntity.Uom = dr["Uom"]?.ToString() ?? "";

                        if (dt.Columns.Contains("MaterialGrade"))
                            quoteEntity.MaterialGrade = dr["MaterialGrade"]?.ToString() ?? "";

                        if (dt.Columns.Contains("UnitPrice"))
                            quoteEntity.UnitPrice = dr["UnitPrice"] == DBNull.Value ? 0 : Convert.ToInt32(dr["UnitPrice"]);

                        if (dt.Columns.Contains("RecordCount"))
                            quoteEntity.RecordCount = Convert.ToInt32(dr["RecordCount"]);
                    }
                }

                return quoteEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public string SaveClarificationData(ClarificationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveClarificationData(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveOrderClarificationData(ClarificationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveOrderClarificationData(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveQuoteNotes(ClarificationEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveQuoteNotes(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string DeleteClarificationMails(string[] param)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteClarificationMails(param);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }

        public string DeleteOrderClarifications(string[] param)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteOrderClarifications(param);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }

        public OrderEntity GetSoNoDetails(string soNo)
        {
            try
            {
                OrderEntity quoteEntity = new OrderEntity();
                DataTable dt = _repository.GetSoNoDetails(soNo);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Columns.Contains("Id"))
                            quoteEntity.Id = Convert.ToInt32(dr["Id"] ?? 0);

                        if (dt.Columns.Contains("VendorId"))
                            quoteEntity.CustomerId = dr["VendorId"]?.ToString() ?? "";

                        if (dt.Columns.Contains("VENDORNAME"))
                            quoteEntity.CustomerName = dr["VENDORNAME"]?.ToString() ?? "";

                        if (dt.Columns.Contains("QuoteNo"))
                            quoteEntity.QuoteNo = dr["QuoteNo"]?.ToString() ?? "";


                        if (dt.Columns.Contains("VendorName"))
                            quoteEntity.CustomerName = dr["VendorName"]?.ToString() ?? "";

                        if (dt.Columns.Contains("FileNo"))
                            quoteEntity.FileNo = dr["FileNo"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ProductGroup"))
                            quoteEntity.MainProdGrp = dr["ProductGroup"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoEntity"))
                            quoteEntity.PoEntity = dr["PoEntity"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoLocation"))
                            quoteEntity.PoLocation = dr["PoLocation"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoNo"))
                            quoteEntity.PoNo = dr["PoNo"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoDate"))
                            quoteEntity.PoDate = dr["PoDate"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoDor"))
                            quoteEntity.PoDor = dr["PoDor"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Curr"))
                            quoteEntity.Curr = dr["Curr"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ExWorkValue"))
                            quoteEntity.ExWorkValue = dr["ExWorkValue"]?.ToString() ?? "";

                        if (dt.Columns.Contains("POPercent"))
                            quoteEntity.POPercent = dr["POPercent"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PoDeliveryDate"))
                            quoteEntity.PoDeliveryDate = dr["PoDeliveryDate"]?.ToString() ?? "";

                        if (dt.Columns.Contains("DeliveryTerms"))
                            quoteEntity.DeliveryTerms = dr["DeliveryTerms"]?.ToString() ?? "";

                        if (dt.Columns.Contains("SupplyTerms"))
                            quoteEntity.SupplyTerms = dr["SupplyTerms"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ConsigneeName"))
                            quoteEntity.ConsigneeName = dr["ConsigneeName"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ConsigneeLocation"))
                            quoteEntity.ConsigneeLocation = dr["ConsigneeLocation"]?.ToString() ?? "";

                        if (dt.Columns.Contains("ModeOfShipment"))
                            quoteEntity.ModeOfShipment = dr["ModeOfShipment"]?.ToString() ?? "";

                        if (dt.Columns.Contains("PaymentTerms"))
                            quoteEntity.PaymentTerms = dr["PaymentTerms"]?.ToString() ?? "";

                        if (dt.Columns.Contains("Inspection"))
                            quoteEntity.Inspection = dr["Inspection"]?.ToString() ?? "";

                        if (dt.Columns.Contains("EndUser"))
                            quoteEntity.EndUser = dr["EndUser"]?.ToString() ?? "";


                    }
                }

                return quoteEntity;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public string SaveNewDescDetail(DescEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveNewDescDetail(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveOrderNote(OrderEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveOrderNote(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DescEntity> LoadDescDetail(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            try
            {
                List<DescEntity> lstDrpDwn = new List<DescEntity>();
                DataTable dt = _repository.LoadDescDetail(skip, pageSize, sortColumn, sortColumnDir, search);
                DescEntity Model;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model = new DescEntity();
                        Model.SNo = dr["SNo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SNo"]);
                        Model.Id = dr["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Id"]);
                        Model.MainPL = dr["MainPL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["MainPL"]);
                        Model.SubPL = dr["SubPL"] == DBNull.Value ? string.Empty : Convert.ToString(dr["SubPL"]);
                        Model.ProductName = dr["ProductName"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProductName"]);
                        Model.ProductNo = dr["ProductNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ProductNo"]);
                        Model.DESQuery = dr["DESQuery"] == DBNull.Value ? string.Empty : Convert.ToString(dr["DESQuery"]);
                        Model.TotalRecords = dr["TotalRecords"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TotalRecords"]);


                        lstDrpDwn.Add(Model);
                    }
                }

                return lstDrpDwn;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }
        
        public string GetWorkAuthReport(string SoNo, string FromDate, string ToDate,string ReportType)
        {

            DataSet result = new DataSet();
            string jdon = "";
            try
            {
                result =  _repository.GetWorkAuthReport(SoNo, FromDate, ToDate,ReportType);
                jdon = JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return jdon;
        }

        public string SaveRevisedOrderDetails(OrderEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveRevisedOrderDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public List<DropDownEntity> GetItemNosForEnqs(string EnqNo)
        {
            try
            {
                List<DropDownEntity> lstDrpDwn = new List<DropDownEntity>();
                DataTable dt = _repository.GetItemNosForEnqs(EnqNo);
                DropDownEntity Model;

                if (dt.Rows.Count > 0)
                {
                    BindDefault(lstDrpDwn);
                    foreach (DataRow dr in dt.Rows)
                    {
                        Model = new DropDownEntity();
                        Model.DataStringValueField = dr["Id"] == DBNull.Value ? string.Empty : Convert.ToString(dr["Id"]);
                        Model.DataTextField = dr["ItemNo"] == DBNull.Value ? string.Empty : Convert.ToString(dr["ItemNo"]);

                        lstDrpDwn.Add(Model);
                    }
                }

                return lstDrpDwn;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public DataTable GetDataForContractReview(string EnqNo, string ItemNo, string type)
        {
            DataTable dt = _repository.GetDataForContractReview(EnqNo, ItemNo, type);
            return dt;
        }

        public string SaveContractReviewData(ContractReview entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveContractReviewData(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;

        }
    }
}
