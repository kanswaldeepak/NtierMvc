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
    public class EnquiryWorker : IEnquiryWorker, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        private Repository _repository;

        public EnquiryWorker()
        {
            _loggingHandler = new LoggingHandler();
            _repository = new Repository();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion

        public string SaveEnquiryDetails(EnquiryEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveEnquiryDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public EnquiryEntity GetUserCustDetails(string unitNo)
        {
            EnquiryEntity result = new EnquiryEntity();
            try
            {
                result = DT2Cust(_repository.GetUserCustomerDetails(unitNo));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }



        public EnquiryEntity DT2Cust(DataTable dtRecord)
        {
            EnquiryEntity oCust = new EnquiryEntity();
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

        public string DeleteEnquiryDetail(int EnquiryId)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteEnquiryDetail(EnquiryId);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }


        public EnquiryEntityDetails GetEnquiryDetails(int pageIndex, int pageSize, string SearchEQEnqType, string SearchCustomerName = null, string SearchEnqFor = null, string SearchEQDueDate = null, string SearchEOQ = null)
        {
            try
            {
                EnquiryEntityDetails eEd = new EnquiryEntityDetails();
                eEd.lstEnqEntity = new List<EnquiryEntity>();
                DataSet ds = _repository.GetEnquiryDetails(pageIndex, pageSize, SearchEQEnqType, SearchCustomerName, SearchEnqFor, SearchEQDueDate, SearchEOQ);

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
                                EnquiryEntity obj = new EnquiryEntity();

                                obj.EnquiryId = dr1.IsNull("EnquiryId") ? 0 : Convert.ToInt32(dr1["EnquiryId"]);
                                //obj.QuoteType = dr1.IsNull("QuoteType") ? string.Empty : Convert.ToString(dr1["QuoteType"]);
                                obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
                                obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
                                obj.CustomerName = dr1.IsNull("CustomerName") ? string.Empty : Convert.ToString(dr1["CustomerName"]);
                                //obj.VendorType = dr1.IsNull("VendorType") ? string.Empty : Convert.ToString(dr1["VendorType"]);
                                obj.City = dr1.IsNull("City") ? string.Empty : Convert.ToString(dr1["City"]);
                                obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.EnqRef = dr1.IsNull("EnqRef") ? string.Empty : Convert.ToString(dr1["EnqRef"]);
                                obj.EnqDt = dr1.IsNull("EnqDt") ? string.Empty : Convert.ToString(dr1["EnqDt"]);
                                obj.EnqType = dr1.IsNull("EnqType") ? string.Empty : Convert.ToString(dr1["EnqType"]);
                                obj.DueDate = dr1.IsNull("DueDate") ? string.Empty : Convert.ToString(dr1["DueDate"]);
                                obj.MainProdGrp = dr1.IsNull("ProdGrp") ? string.Empty : Convert.ToString(dr1["ProdGrp"]);
                                obj.EnqFor = dr1.IsNull("EnqFor") ? string.Empty : Convert.ToString(dr1["EnqFor"]);
                                obj.Eoq = dr1.IsNull("Eoq") ? string.Empty : Convert.ToString(dr1["Eoq"]);
                                obj.LeadTime = dr1.IsNull("LeadTime") ? 0 : Convert.ToInt32(dr1["LeadTime"]);
                                obj.TimeDuration = dr1.IsNull("LeadTimeDuration") ? string.Empty : Convert.ToString(dr1["LeadTimeDuration"]);
                                obj.EndUser = dr1.IsNull("EndUser") ? string.Empty : Convert.ToString(dr1["EndUser"]);
                                obj.ApiMonogramRequirement = dr1.IsNull("ApiMonogramRequirement") ? string.Empty : Convert.ToString(dr1["ApiMonogramRequirement"]);
                                obj.ReasonForRegret = dr1.IsNull("ReasonForRegret") ? string.Empty : Convert.ToString(dr1["ReasonForRegret"]);
                                obj.DateOfReceipt = dr1.IsNull("DateOfReceipt") ? string.Empty : Convert.ToString(dr1["DateOfReceipt"]);
                                obj.Remarks = dr1.IsNull("Remarks") ? string.Empty : Convert.ToString(dr1["Remarks"]);
                                obj.EnqMode = dr1.IsNull("EnqMode") ? string.Empty : Convert.ToString(dr1["EnqMode"]);
                                obj.EnqThru = dr1.IsNull("EnqThru") ? string.Empty : Convert.ToString(dr1["EnqThru"]);
                                obj.EnqBGreq = dr1.IsNull("EnqBGreq") ? 0 : Convert.ToInt32(dr1["EnqBGreq"]);
                                obj.AgentName = dr1.IsNull("AgentName") ? "" : Convert.ToString(dr1["AgentName"]);

                                eEd.lstEnqEntity.Add(obj);
                            }
                        }
                    }

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            eEd.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                        }
                    }
                }
                return eEd;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public EnquiryEntity EnquiryDetailsPopup(EnquiryEntity Model)
        {
            try
            {
                DataSet ds = _repository.EnquiryDetailsPopup(Model);
                //Model = ExtractEnquiryDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    Model.EnquiryId = dt1.Rows[0]["EnquiryId"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["EnquiryId"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);
                    Model.CustomerId = dt1.Rows[0]["CustomerId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerId"]);
                    Model.CustomerName = dt1.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerName"]);
                    //Model.QuoteType = dt1.Rows[0]["QuoteType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["QuoteType"]);
                    Model.City = dt1.Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["City"]);
                    Model.StateId = dt1.Rows[0]["StateId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["StateId"]);
                    Model.CountryId = dt1.Rows[0]["CountryId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CountryId"]);
                    Model.State = dt1.Rows[0]["State"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["State"]);
                    Model.Country = dt1.Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Country"]);
                    Model.EnqRef = dt1.Rows[0]["EnqRef"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqRef"]);
                    Model.EnqDt = dt1.Rows[0]["EnqDt"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqDt"]);
                    Model.EnqTypeId = dt1.Rows[0]["EnqType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqType"]);
                    Model.DueDate = dt1.Rows[0]["DueDate"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DueDate"]);
                    Model.MainProdGrp = dt1.Rows[0]["ProdGrp"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ProdGrp"]);
                    Model.EnqFor = dt1.Rows[0]["EnqFor"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqFor"]);
                    Model.Eoq = dt1.Rows[0]["Eoq"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Eoq"]);
                    Model.LeadTime = dt1.Rows[0]["LeadTime"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["LeadTime"]);
                    Model.LeadTimeDuration = dt1.Rows[0]["LeadTimeDuration"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["LeadTimeDuration"]);
                    Model.EndUser = dt1.Rows[0]["EndUser"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EndUser"]);
                    Model.ApiMonogramRequirement = dt1.Rows[0]["ApiMonogramRequirement"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ApiMonogramRequirement"]);
                    Model.ReasonForRegret = dt1.Rows[0]["ReasonForRegret"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["ReasonForRegret"]);
                    Model.DateOfReceipt = dt1.Rows[0]["DateOfReceipt"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DateOfReceipt"]);
                    Model.Remarks = dt1.Rows[0]["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Remarks"]);
                    Model.EnqMode = dt1.Rows[0]["EnqMode"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqMode"]);
                    Model.EnqThru = dt1.Rows[0]["EnqThru"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["EnqThru"]);
                    Model.EnqBGreq = dt1.Rows[0]["EnqBGreq"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["EnqBGreq"]);
                    Model.AgentName = dt1.Rows[0]["AgentName"] == DBNull.Value ? "" : Convert.ToString(dt1.Rows[0]["AgentName"]);
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }


        EnquiryEntity ExtractEnquiryDetailsEntity(DataSet ds, EnquiryEntity Model)
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

        public EnquiryEntity GetVendorDetailForEnquiry(string CustomerId)
        {
            EnquiryEntity result = new EnquiryEntity();
            try
            {
                result = DT2Vendor(_repository.GetVendorDetailForEnquiry(CustomerId));
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public EnquiryEntity DT2Vendor(DataTable dtRecord)
        {
            EnquiryEntity oVendor = new EnquiryEntity();
            try
            {
                if (dtRecord.Rows.Count > 0)
                {

                    oVendor.City = Convert.ToString(dtRecord.Rows[0]["City"]);
                    oVendor.Country = Convert.ToString(dtRecord.Rows[0]["Country"]);
                    oVendor.State = Convert.ToString(dtRecord.Rows[0]["State"]);
                    oVendor.EnqType = Convert.ToString(dtRecord.Rows[0]["EnqType"]);

                    //oVendor.CityId = Convert.ToString(dtRecord.Rows[0]["CityId"]);
                    oVendor.CountryId = Convert.ToString(dtRecord.Rows[0]["CountryId"]);
                    oVendor.StateId = Convert.ToString(dtRecord.Rows[0]["StateId"]);
                    oVendor.EnqTypeId = Convert.ToString(dtRecord.Rows[0]["EnqTypeId"]);

                    oVendor.CustomerName = Convert.ToString(dtRecord.Rows[0]["CustomerName"]);

                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return oVendor;
        }

        public List<DropDownEntity> GetDdlValueForEnquiry(string type, string EnqType = null, string CustomerId = null, string EnqFor = null, string DueDate = null)
        {
            try
            {
                List<DropDownEntity> lstDdl = new List<DropDownEntity>();
                DataTable dt = _repository.GetDdlValueForEnquiry(type, EnqType, CustomerId, EnqFor, DueDate);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        entity = new DropDownEntity();
                        entity.DataStringValueField = Convert.ToString(dr["DropDownID"] ?? "0");
                        entity.DataTextField = dr["DropDownValue"]?.ToString() ?? "";

                        lstDdl.Add(entity);
                    }

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
