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

        public string SaveGateEntryDetails(GateEntryEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveGateEntryDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public GateEntryEntityDetails FetchInboundList(int pageIndex, int pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchPONo = null)
        {
            try
            {
                GateEntryEntityDetails vmbEnDetails = new GateEntryEntityDetails();
                vmbEnDetails.lstVBM = new List<GateEntryEntity>();

                DataSet ds = _repository.FetchInboundList(pageIndex, pageSize, SearchType, SearchVendorNature, SearchVendorName, SearchPONo);
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

                                obj.GateNo = dr1.IsNull("GateNo") ? "0" : Convert.ToString(dr1["GateNo"]);
                                obj.SupplyType = dr1.IsNull("SupplyType") ? string.Empty : Convert.ToString(dr1["SupplyType"]);
                                obj.GateControlNo = dr1.IsNull("GateControlNo") ? string.Empty : Convert.ToString(dr1["GateControlNo"]);
                                obj.VehicleNo = dr1.IsNull("VehicleNo") ? string.Empty : Convert.ToString(dr1["VehicleNo"]);
                                obj.GateDate = dr1.IsNull("GateDate") ? string.Empty : Convert.ToString(dr1["GateDate"]);
                                obj.TimeIn = dr1.IsNull("TimeIn") ? false : Convert.ToBoolean(dr1["TimeIn"]);
                                obj.TimeOut = dr1.IsNull("TimeOut") ? false : Convert.ToBoolean(dr1["TimeOut"]);
                                obj.TimeInDate = dr1.IsNull("TimeInDate") ? string.Empty : Convert.ToString(dr1["TimeInDate"]);
                                obj.TimeOutDate = dr1.IsNull("TimeOutDate") ? string.Empty : Convert.ToString(dr1["TimeOutDate"]);

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


        public GateEntryEntityDetails GetPOTableDetailsForGateEntry(string POSetno, string GateNo = null)
        {
            GateEntryEntityDetails objGE = new GateEntryEntityDetails();
            objGE.lstVBM = new List<GateEntryEntity>();

            try
            {
                DataSet ds = _repository.GetPOTableDetailsForGateEntry(POSetno, GateNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        GateEntryEntity Model = new GateEntryEntity();

                        Model.GateControlNo = dr1.IsNull("GateControlNo") ? "" : Convert.ToString(dr1["GateControlNo"]);
                        Model.GateNo = dr1.IsNull("GateNo") ? "0" : Convert.ToString(dr1["GateNo"]);

                        Model.PONo = dr1.IsNull("PONo") ? "" : Convert.ToString(dr1["PONo"]);
                        Model.POdate = dr1.IsNull("POdate") ? "" : Convert.ToString(dr1["POdate"]);
                        Model.PRCat = dr1.IsNull("PRCat") ? "" : Convert.ToString(dr1["PRCat"]);
                        Model.POValidity = dr1.IsNull("POValidity") ? "" : Convert.ToString(dr1["POValidity"]);
                        Model.WorkNo = dr1.IsNull("WorkNo") ? "" : Convert.ToString(dr1["WorkNo"]);
                        Model.PORevNo = dr1.IsNull("PORevNo") ? "" : Convert.ToString(dr1["PORevNo"]);
                        Model.ItemCategory = dr1.IsNull("ItemCategory") ? "" : Convert.ToString(dr1["ItemCategory"]);
                        Model.ModeOfTransport = dr1.IsNull("ModeOfTransport") ? "" : Convert.ToString(dr1["ModeOfTransport"]);

                        Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                        Model.RMdescription = dr1.IsNull("RMdescription") ? "" : Convert.ToString(dr1["RMdescription"]);
                        Model.PRqty = dr1.IsNull("PRqty") ? 0 : Convert.ToInt32(dr1["PRqty"]);
                        Model.UOM = dr1.IsNull("UOM") ? "" : Convert.ToString(dr1["UOM"]);
                        Model.UnitPrice = dr1.IsNull("UnitPrice") ? "" : Convert.ToString(dr1["UnitPrice"]);
                        Model.Discount = dr1.IsNull("Discount") ? "" : Convert.ToString(dr1["Discount"]);
                        Model.TotalPrice = dr1.IsNull("TotalPrice") ? "" : Convert.ToString(dr1["TotalPrice"]);
                        Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? "" : Convert.ToString(dr1["SupplyTerms"]);
                        Model.DeliveryDate = dr1.IsNull("DeliveryDate") ? "" : Convert.ToString(dr1["DeliveryDate"]);

                        objGE.lstVBM.Add(Model);
                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return objGE;
        }

        public GateEntryEntity InboundDetailsPopup(string GateNo)
        {
            GateEntryEntity Model = new GateEntryEntity();

            try
            {
                DataSet ds = _repository.InboundDetailsPopup(GateNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        Model.GateControlNo = dr1.IsNull("GateControlNo") ? "" : Convert.ToString(dr1["GateControlNo"]);

                        Model.VendorPONO = dr1.IsNull("VendorPONO") ? "" : Convert.ToString(dr1["VendorPONO"]);
                        Model.GateControlNo = dr1.IsNull("GateControlNo") ? "" : Convert.ToString(dr1["GateControlNo"]);
                        Model.GateDate = dr1.IsNull("GateDate") ? "" : Convert.ToString(dr1["GateDate"]);
                        Model.VehicleNo = dr1.IsNull("VehicleNo") ? "" : Convert.ToString(dr1["VehicleNo"]);
                        Model.ContainerNo = dr1.IsNull("ContainerNo") ? "" : Convert.ToString(dr1["ContainerNo"]);
                        Model.DriverName = dr1.IsNull("DriverName") ? "" : Convert.ToString(dr1["DriverName"]);
                        Model.DriverContactNo = dr1.IsNull("DriverContactNo") ? "" : Convert.ToString(dr1["DriverContactNo"]);
                        Model.TimeIn = dr1.IsNull("TimeIn") ? false : Convert.ToBoolean(dr1["TimeIn"]);
                        Model.TimeOut = dr1.IsNull("TimeOut") ? false : Convert.ToBoolean(dr1["TimeOut"]);
                        Model.TimeInDate = dr1.IsNull("TimeInDate") ? "" : Convert.ToString(dr1["TimeInDate"]);
                        Model.TimeOutDate = dr1.IsNull("TimeOutDate") ? "" : Convert.ToString(dr1["TimeOutDate"]);
                        Model.VehicleReleased = dr1.IsNull("VehicleReleased") ? "" : Convert.ToString(dr1["VehicleReleased"]);
                        Model.SupplyType = dr1.IsNull("SupplyType") ? "" : Convert.ToString(dr1["SupplyType"]);

                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }

            return Model;
        }

        public List<DropDownEntity> GetPoNoDetailsForGE()
        {
            List<DropDownEntity> lstDdl = new List<DropDownEntity>();
            try
            {
                lstDdl = _repository.GetPoNoDetailsForGE();
            }
            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return lstDdl;
        }








    }
}
