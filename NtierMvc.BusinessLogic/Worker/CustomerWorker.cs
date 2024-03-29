﻿using System;
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
using NtierMvc.Model.Customer;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;

namespace NtierMvc.BusinessLogic.Worker
{
    public class CustomerWorker : ICustomerWorker, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;
        private Repository _repository;

        public CustomerWorker()
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

        public string SaveCustomerDetails(CustomerEntity entity)
        {
            string result = "";
            try
            {
                result = _repository.SaveCustomerDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public CustomerEntity GetUserCustDetails(string unitNo)
        {
            CustomerEntity result = new CustomerEntity();
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

        public CustomerEntity DT2Cust(DataTable dtRecord)
        {
            CustomerEntity oCust = new CustomerEntity();
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

        public string DeleteCustomerDetail(int CustomerId)
        {
            string msgCode = "";
            try
            {
                msgCode = _repository.DeleteCustomerDetail(CustomerId);
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                throw;
            }
            return msgCode;
        }


        public CustomerEntityDetails GetCustomerDetails(int pageIndex, int pageSize, string SearchCountry = null, string SearchCustomerID = null, string SearchCustomerIsActive = null)
        {
            try
            {
                CustomerEntityDetails cED = new CustomerEntityDetails();
                cED.LstCusEnt = new List<CustomerEntity>();
                DataSet ds = _repository.GetCustomerDetails(pageIndex, pageSize, SearchCountry, SearchCustomerID, SearchCustomerIsActive);

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
                                CustomerEntity obj = new CustomerEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.UserInitial = dr1.IsNull("UserInitial") ? string.Empty : Convert.ToString(dr1["UserInitial"]);
                                obj.UnitNo = dr1.IsNull("UnitNo") ? string.Empty : Convert.ToString(dr1["UnitNo"]);
                                obj.CustomerId = dr1.IsNull("CustomerId") ? string.Empty : Convert.ToString(dr1["CustomerId"]);
                                //obj.VendorTypeId = dr1.IsNull("VendorTypeId") ? string.Empty : Convert.ToString(dr1["VendorTypeId"]);
                                obj.DateOfAssociation = dr1.IsNull("DateOfAssociation") ? string.Empty : Convert.ToString(dr1["DateOfAssociation"]);
                                obj.Address2 = dr1.IsNull("Address2") ? string.Empty : Convert.ToString(dr1["Address2"]);
                               obj.Address1 = dr1.IsNull("Address1") ? string.Empty : Convert.ToString(dr1["Address1"]);
                             // obj.FunctionArea = dr1.IsNull("FunctionArea") ? string.Empty : Convert.ToString(dr1["FunctionArea"]);
                                obj.CustomerName = dr1.IsNull("CustomerName") ? string.Empty : Convert.ToString(dr1["CustomerName"]);
                                obj.City = dr1.IsNull("City") ? string.Empty : Convert.ToString(dr1["City"]);
                                obj.State = dr1.IsNull("State") ? string.Empty : Convert.ToString(dr1["State"]);
                                obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.ContactPerson = dr1.IsNull("ContactPerson") ? string.Empty : Convert.ToString(dr1["ContactPerson"]);
                                obj.Designation = dr1.IsNull("Designation") ? string.Empty : Convert.ToString(dr1["Designation"]);
                                obj.Country = dr1.IsNull("Country") ? string.Empty : Convert.ToString(dr1["Country"]);
                                obj.mob1 = dr1.IsNull("MOB1") ? string.Empty : Convert.ToString(dr1["MOB1"]);
                                obj.email1 = dr1.IsNull("EMAIL1") ? string.Empty : Convert.ToString(dr1["EMAIL1"]);
                                obj.Status = dr1.IsNull("CustomerStatus") ? string.Empty : Convert.ToString(dr1["CustomerStatus"]);

                                cED.LstCusEnt.Add(obj);
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


        public CustomerEntity CustomerDetailsPopup(CustomerEntity Model)
        {
            try
            {
                DataSet ds = _repository.CustomerDetailsPopup(Model);
                //Model = ExtractCustomerDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);
                    Model.UserInitial = dt1.Rows[0]["UserInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UserInitial"]);
                    Model.UnitNo = dt1.Rows[0]["UnitNo"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["UnitNo"]);
                    Model.CustomerId = dt1.Rows[0]["CustomerId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerId"]);
                    //Model.VendorTypeId = dt1.Rows[0]["VendorTypeId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorTypeId"]);
                    //Model.VendorType = dt1.Rows[0]["VendorType"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorType"]);
                    //Model.VendorNatureId = dt1.Rows[0]["VendorNatureId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorNatureId"]);
                    //Model.VendorNature = dt1.Rows[0]["VendorNature"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["VendorNature"]);
                    //Model.FunctionAreaId = dt1.Rows[0]["FunctionAreaId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FunctionAreaId"]);
                    //Model.FunctionArea = dt1.Rows[0]["FunctionArea"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["FunctionArea"]);
                    Model.CustomerName = dt1.Rows[0]["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerName"]);
                    Model.Address1 = dt1.Rows[0]["Address1"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address1"]);
                    Model.Address2 = dt1.Rows[0]["Address2"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address2"]);
                    Model.Address3 = dt1.Rows[0]["Address3"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Address3"]);
                    Model.City = dt1.Rows[0]["City"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["City"]);
                    Model.State = dt1.Rows[0]["StateId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["StateId"]);
                    Model.Country = dt1.Rows[0]["Country"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["Country"]);
                    Model.CountryId = dt1.Rows[0]["CountryId"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CountryId"]);

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
                    Model.CustomerInitial = dt1.Rows[0]["CustomerInitial"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerInitial"]);
                    Model.DateOfAssociation = dt1.Rows[0]["DateOfAssociation"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["DateOfAssociation"]);
                    Model.Status = dt1.Rows[0]["CustomerStatus"] == DBNull.Value ? string.Empty : Convert.ToString(dt1.Rows[0]["CustomerStatus"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }

        public List<DropDownEntity> GetDdlValueForCustomer(string type, string CountryId, string CustomerId = null)
        {
            try
            {
                List<DropDownEntity> lstDdl = new List<DropDownEntity>();
                DataTable dt = _repository.GetDdlValueForCustomer(type, CountryId, CustomerId);
                DropDownEntity entity;

                if (dt.Rows.Count > 0)
                {
                    if (type == "CustomerName")
                        foreach (DataRow dr in dt.Rows)
                        {
                            entity = new DropDownEntity();
                            if (dt.Columns.Contains("Id"))
                                entity.DataStringValueField = Convert.ToString(dr["Id"] ?? "0");

                            if (dt.Columns.Contains("CustomerName"))
                                entity.DataTextField = dr["CustomerName"]?.ToString() ?? "";

                            lstDdl.Add(entity);
                        }

                    else if (type == "CountryId")
                        foreach (DataRow dr in dt.Rows)
                        {
                            entity = new DropDownEntity();
                            if (dt.Columns.Contains("Id"))
                                entity.DataStringValueField = Convert.ToString(dr["Id"] ?? "0");

                            if (dt.Columns.Contains("CustomerID"))
                                entity.DataTextField = dr["CustomerID"]?.ToString() ?? "";

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
