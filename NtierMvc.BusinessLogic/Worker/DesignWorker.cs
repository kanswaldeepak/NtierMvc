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
using NtierMvc.Model.HR;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;
using NtierMvc.Model.DesignEng;

namespace NtierMvc.BusinessLogic.Worker
{
    public class DesignWorker : IDesignWorker
    {
        Repository _repository = new Repository();

        public EmployeeEntity DT2Cust(DataTable dtRecord)
        {
            EmployeeEntity oCust = new EmployeeEntity();
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

        public string SaveBOMDetails(BOMEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveBOMDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public string SaveProductRealisationDetails(ProductRealisation entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveProductRealisationDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public ProductRealisation PRPPopup(ProductRealisation Model)
        {
            try
            {
                DataSet ds = _repository.PRPPopup(Model);
                //Model = ExtractEmployeeDetailsEntity(ds, Model);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];
                    //Model.Id = dt1.Rows[0]["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dt1.Rows[0]["Id"]);

                    Model.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                    Model.QuoteType = dr1.IsNull("QuoteType") ? "" : Convert.ToString(dr1["QuoteType"]);
                    Model.QuoteNo = dr1.IsNull("QuoteNo") ? "" : Convert.ToString(dr1["QuoteNo"]);
                    Model.QuoteDate = dr1.IsNull("QuoteDate") ? "" : Convert.ToString(dr1["QuoteDate"]);
                    Model.SONo = dr1.IsNull("SONo") ? "" : Convert.ToString(dr1["SONo"]);
                    Model.VendorID = dr1.IsNull("VendorID") ? "" : Convert.ToString(dr1["VendorID"]);
                    Model.VendorName = dr1.IsNull("VendorName") ? "" : Convert.ToString(dr1["VendorName"]);
                    Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? "" : Convert.ToString(dr1["SupplyTerms"]);
                    Model.PONo = dr1.IsNull("PONo") ? "" : Convert.ToString(dr1["PONo"]);
                    Model.POSlNo = dr1.IsNull("POSlNo") ? "" : Convert.ToString(dr1["POSlNo"]);
                    Model.PODate = dr1.IsNull("PODate") ? "" : Convert.ToString(dr1["PODate"]);
                    Model.ProductGroup = dr1.IsNull("ProductGroup") ? "" : Convert.ToString(dr1["ProductGroup"]);
                    Model.PODeliveryDate = dr1.IsNull("PODeliveryDate") ? "" : Convert.ToString(dr1["PODeliveryDate"]);

                    Model.POQty = dr1.IsNull("PoQty") ? 0 : Convert.ToInt32(dr1["PoQty"]);
                    Model.POItemDesc = dr1.IsNull("PoItemdesc") ? "" : Convert.ToString(dr1["PoItemdesc"]);
                    Model.OpnCode = dr1.IsNull("OpnCode") ? "" : Convert.ToString(dr1["OpnCode"]);
                    Model.ProductNo = dr1.IsNull("ProductNo") ? "" : Convert.ToString(dr1["ProductNo"]);
                    Model.ProductNoView = dr1.IsNull("ProductNoView") ? "" : Convert.ToString(dr1["ProductNoView"]);
                    Model.ProductName = dr1.IsNull("ProductName") ? "" : Convert.ToString(dr1["ProductName"]);
                    Model.PL = dr1.IsNull("PL") ? "" : Convert.ToString(dr1["PL"]);
                    Model.CasingSize = dr1.IsNull("CasingSize") ? "" : Convert.ToString(dr1["CasingSize"]);
                    Model.CasingPPF = dr1.IsNull("CasingPpf") ? "" : Convert.ToString(dr1["CasingPpf"]);
                    Model.Grade = dr1.IsNull("MaterialGrade") ? "" : Convert.ToString(dr1["MaterialGrade"]);
                    Model.OpenHoleSize = dr1.IsNull("OpenHoleSize") ? "" : Convert.ToString(dr1["OpenHoleSize"]);

                    Model.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                    Model.PartName = dr1.IsNull("PartName") ? "" : Convert.ToString(dr1["PartName"]);
                    Model.CommNo = dr1.IsNull("CommodityNo") ? "" : Convert.ToString(dr1["CommodityNo"]);
                    Model.COMMRevNo = dr1.IsNull("COMMRevNo") ? "" : Convert.ToString(dr1["COMMRevNo"]);
                    Model.Qty = dr1.IsNull("Qty") ? 0 : Convert.ToInt32(dr1["Qty"]);
                    Model.Length = dr1.IsNull("Length") ? "" : Convert.ToString(dr1["Length"]);
                    Model.OD = dr1.IsNull("OD") ? "" : Convert.ToString(dr1["OD"]);
                    Model.WT = dr1.IsNull("WT") ? "" : Convert.ToString(dr1["WT"]);
                    Model.UOM = dr1.IsNull("UOM") ? "" : Convert.ToString(dr1["UOM"]);
                    Model.RMTYPE = dr1.IsNull("RMTYPE") ? "" : Convert.ToString(dr1["RMTYPE"]);
                    Model.UploadFile = dr1.IsNull("UploadFile") ? "" : Convert.ToString(dr1["UploadFile"]);
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return Model;
        }


        public ProductRealisationDetails GetProductRealisationDetails(int pageIndex, int pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            try
            {
                ProductRealisationDetails eED = new ProductRealisationDetails();
                eED.ListPR = new List<ProductRealisation>();
                DataSet ds = _repository.GetProductRealisationDetails(pageIndex, pageSize, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup);

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
                                ProductRealisation obj = new ProductRealisation();

                                //obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.QuoteType = dr1.IsNull("QuoteType") ? "" : Convert.ToString(dr1["QuoteType"]);
                                obj.QuoteNo = dr1.IsNull("QuoteNo") ? "" : Convert.ToString(dr1["QuoteNo"]);
                                obj.QuoteDate = dr1.IsNull("QuoteDate") ? "" : Convert.ToString(dr1["QuoteDate"]);
                                obj.SONo = dr1.IsNull("SONo") ? "" : Convert.ToString(dr1["SONo"]);
                                obj.VendorID = dr1.IsNull("VendorID") ? "" : Convert.ToString(dr1["VendorID"]);
                                obj.VendorName = dr1.IsNull("VendorName") ? "" : Convert.ToString(dr1["VendorName"]);
                                obj.SupplyTerms = dr1.IsNull("SupplyTerms") ? "" : Convert.ToString(dr1["SupplyTerms"]);
                                obj.PONo = dr1.IsNull("PONo") ? "" : Convert.ToString(dr1["PONo"]);
                                //obj.POSlNo = dr1.IsNull("POSlNo") ? "" : Convert.ToString(dr1["POSlNo"]);
                                obj.PODate = dr1.IsNull("PODate") ? "" : Convert.ToString(dr1["PODate"]);
                                obj.PODeliveryDate = dr1.IsNull("PODeliveryDate") ? "" : Convert.ToString(dr1["PODeliveryDate"]);

                                eED.ListPR.Add(obj);
                            }
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                eED.totalcount = dr2.IsNull("totalCount") ? 0 : Convert.ToInt32(dr2["totalCount"]);
                            }
                        }
                    }
                }
                return eED;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<BOMEntity> GetBOMList(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null)
        {
            try
            {
                List<BOMEntity> listBOM = new List<BOMEntity>();
                DataSet ds = _repository.GetBOMList(ProductName, ProductCode, PL, ProductNo, CasingSize, CasingPPF, Grade, OpenHoleSize);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                BOMEntity obj = new BOMEntity();
                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                                obj.PartName = dr1.IsNull("PartName") ? string.Empty : Convert.ToString(dr1["PartName"]);
                                obj.CommodityNo = dr1.IsNull("CommodityNo") ? string.Empty : Convert.ToString(dr1["CommodityNo"]);
                                obj.PartName = dr1.IsNull("PartName") ? string.Empty : Convert.ToString(dr1["PartName"]);
                                obj.CommodityNo = dr1.IsNull("CommodityNo") ? string.Empty : Convert.ToString(dr1["CommodityNo"]);
                                obj.COMMRevNo = dr1.IsNull("COMMRevNo") ? string.Empty : Convert.ToString(dr1["COMMRevNo"]);
                                obj.Qty = dr1.IsNull("Qty") ? 0 : Convert.ToInt32(dr1["Qty"]);
                                obj.Length = dr1.IsNull("Length") ? 0 : Convert.ToDecimal(dr1["Length"]);
                                obj.OD = dr1.IsNull("OD") ? 0 : Convert.ToDecimal(dr1["OD"]);
                                obj.WT = dr1.IsNull("WT") ? 0 : Convert.ToDecimal(dr1["WT"]);

                                obj.UOM = dr1.IsNull("UOM") ? string.Empty : Convert.ToString(dr1["UOM"]);
                                obj.RMTYPE = dr1.IsNull("RMTYPE") ? string.Empty : Convert.ToString(dr1["RMTYPE"]);


                                listBOM.Add(obj);
                            }
                        }
                    }
                }
                return listBOM;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }

        public List<ProductRealisation> GetPoSLNoDetails(string POSlNo)
        {
            try
            {
                List<ProductRealisation> listPRP = new List<ProductRealisation>();
                DataSet ds = _repository.GetPoSLNoDetails(POSlNo);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                ProductRealisation obj = new ProductRealisation();
                                obj.POQty = dr1.IsNull("PoQty") ? 0 : Convert.ToInt32(dr1["PoQty"]);
                                obj.POItemDesc = dr1.IsNull("PoItemdesc") ? "" : Convert.ToString(dr1["PoItemdesc"]);
                                obj.OpnCode = dr1.IsNull("OpnCode") ? "" : Convert.ToString(dr1["OpnCode"]);
                                obj.ProductNo = dr1.IsNull("ProductNo") ? "" : Convert.ToString(dr1["ProductNo"]);
                                obj.ProductNoView = dr1.IsNull("ProductNoView") ? "" : Convert.ToString(dr1["ProductNoView"]);
                                obj.ProductName = dr1.IsNull("ProductName") ? "" : Convert.ToString(dr1["ProductName"]);
                                obj.PL = dr1.IsNull("PL") ? "" : Convert.ToString(dr1["PL"]);
                                obj.CasingSize = dr1.IsNull("CasingSize") ? "" : Convert.ToString(dr1["CasingSize"]);
                                obj.CasingPPF = dr1.IsNull("CasingPpf") ? "" : Convert.ToString(dr1["CasingPpf"]);
                                obj.Grade = dr1.IsNull("MaterialGrade") ? "" : Convert.ToString(dr1["MaterialGrade"]);
                                obj.OpenHoleSize = dr1.IsNull("OpenHoleSize") ? "" : Convert.ToString(dr1["OpenHoleSize"]);

                                obj.SN = dr1.IsNull("SN") ? 0 : Convert.ToInt32(dr1["SN"]);
                                obj.PartName = dr1.IsNull("PartName") ? "" : Convert.ToString(dr1["PartName"]);
                                obj.CommNo = dr1.IsNull("CommodityNo") ? "" : Convert.ToString(dr1["CommodityNo"]);
                                obj.COMMRevNo = dr1.IsNull("COMMRevNo") ? "" : Convert.ToString(dr1["COMMRevNo"]);
                                obj.Qty = dr1.IsNull("Qty") ? 0 : Convert.ToInt32(dr1["Qty"]);
                                obj.Length = dr1.IsNull("Length") ? "" : Convert.ToString(dr1["Length"]);
                                obj.OD = dr1.IsNull("OD") ? "" : Convert.ToString(dr1["OD"]);
                                obj.WT = dr1.IsNull("WT") ? "" : Convert.ToString(dr1["WT"]);
                                obj.UOM = dr1.IsNull("UOM") ? "" : Convert.ToString(dr1["UOM"]);
                                obj.RMTYPE = dr1.IsNull("RMTYPE") ? "" : Convert.ToString(dr1["RMTYPE"]);
                                obj.UploadFile = dr1.IsNull("UploadFile") ? "" : Convert.ToString(dr1["UploadFile"]);

                                listPRP.Add(obj);
                            }
                        }
                    }
                }
                return listPRP;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public BillMonitoringEntity BillDetailsPopup(BillMonitoringEntity obj)
        {
            try
            {
                DataSet ds = _repository.BillDetailsPopup(obj);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr1 = ds.Tables[0].Rows[0];
                    obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                    obj.Type = dr1.IsNull("Type") ? string.Empty : Convert.ToString(dr1["Type"]);
                    obj.VendorNature = dr1.IsNull("VendorNatureId") ? string.Empty : Convert.ToString(dr1["VendorNatureId"]);
                    obj.VendorId = dr1.IsNull("VendorId") ? string.Empty : Convert.ToString(dr1["VendorId"]);
                    obj.VendorName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                    obj.City = dr1.IsNull("City") ? string.Empty : Convert.ToString(dr1["City"]);
                    obj.EndUse = dr1.IsNull("EndUse") ? string.Empty : Convert.ToString(dr1["EndUse"]);
                    obj.FunctionalAreaId = dr1.IsNull("FunctionalAreaId") ? 0 : Convert.ToInt32(dr1["FunctionalAreaId"]);
                    obj.VendorPONO = dr1.IsNull("VendorPONO") ? string.Empty : Convert.ToString(dr1["VendorPONO"]);
                    obj.VendorPODate = dr1.IsNull("VENDORPODATE") ? string.Empty : Convert.ToString(dr1["VENDORPODATE"]);
                    obj.SupplyType = dr1.IsNull("SUPPLYTYPE") ? string.Empty : Convert.ToString(dr1["SUPPLYTYPE"]);
                    obj.BillNo = dr1.IsNull("BillNo") ? string.Empty : Convert.ToString(dr1["BillNo"]);
                    obj.BillDate = dr1.IsNull("BillDate") ? string.Empty : Convert.ToString(dr1["BillDate"]);
                    obj.BillAmount = dr1.IsNull("BillAmount") ? 0 : Convert.ToInt32(dr1["BillAmount"]);
                    obj.Currency = dr1.IsNull("Currency") ? string.Empty : Convert.ToString(dr1["Currency"]);
                    obj.UnitRate = dr1.IsNull("UnitRate") ? 0 : Convert.ToInt32(dr1["UnitRate"]);
                    obj.PaymentDueDate = dr1.IsNull("PaymentDueDate") ? string.Empty : Convert.ToString(dr1["PaymentDueDate"]);
                    obj.SCCNO = dr1.IsNull("SCCNO") ? string.Empty : Convert.ToString(dr1["SCCNO"]);

                    obj.GSTPercent = dr1.IsNull("GSTPERCENT") ? string.Empty : Convert.ToString(dr1["GSTPERCENT"]);
                    obj.GSTAmount = dr1.IsNull("GSTAMOUNT") ? string.Empty : Convert.ToString(dr1["GSTAMOUNT"]);
                    obj.PassedBy = dr1.IsNull("PASSEDBY") ? string.Empty : Convert.ToString(dr1["PASSEDBY"]);
                    obj.CostCenter = dr1.IsNull("CostCenter") ? string.Empty : Convert.ToString(dr1["CostCenter"]);
                    obj.ControlNo = dr1.IsNull("ControlNo") ? string.Empty : Convert.ToString(dr1["ControlNo"]);
                    obj.ApprovalDate = dr1.IsNull("ApprovalDate") ? string.Empty : Convert.ToString(dr1["ApprovalDate"]);
                    obj.VehicleNo = dr1.IsNull("VehicleNo") ? string.Empty : Convert.ToString(dr1["VehicleNo"]);
                    obj.DriverName = dr1.IsNull("DriverName") ? string.Empty : Convert.ToString(dr1["DriverName"]);
                    obj.DriverContactNo = dr1.IsNull("DriverContactNo") ? string.Empty : Convert.ToString(dr1["DriverContactNo"]);
                    obj.TimeIn = dr1.IsNull("TimeIn") ? string.Empty : Convert.ToString(dr1["TimeIn"]);
                    obj.TimeOut = dr1.IsNull("TimeOut") ? string.Empty : Convert.ToString(dr1["TimeOut"]);
                    obj.VehicleReleased = dr1.IsNull("VehicleReleased") ? string.Empty : Convert.ToString(dr1["VehicleReleased"]);
                    obj.ApprovedBy = dr1.IsNull("APPROVEDBY") ? string.Empty : Convert.ToString(dr1["APPROVEDBY"]);


                    obj.ApprovedStatus = dr1.IsNull("ApprovedStatus") ? string.Empty : Convert.ToString(dr1["ApprovedStatus"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return obj;
        }

        public string SaveBillMonitoringDetails(BillMonitoringEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveBillMonitoringDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public DataTable GetDataTablePRPData(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null)
        {
            DataSet result = new DataSet();

            try
            {
                result = _repository.GetDataTablePRPData(ReportType, DateFrom, DateTo, VendorId, SoNo);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }

            return result.Tables[0];
        }


        public List<DropDownEntity> GetVendorIdFromQuoteType(string ReportType = null)
        {
            try
            {
                List<DropDownEntity> listVendor = new List<DropDownEntity>();
                DataSet ds = _repository.GetVendorIdFromQuoteType(ReportType = null);

                if (ds.Tables.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                DropDownEntity obj = new DropDownEntity();
                                obj.DataStringValueField = dr1.IsNull("Id") ? "" : Convert.ToString(dr1["Id"]);
                                obj.DataTextField = dr1.IsNull("CustomerId") ? "" : Convert.ToString(dr1["CustomerId"]);

                                listVendor.Add(obj);
                            }
                        }
                    }
                }
                return listVendor;
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
                throw Ex;
            }
        }


        public OrderEntity GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId)
        {
            OrderEntity oE = new OrderEntity();
            try
            {
                DataSet ds = _repository.GetQuoteOrderDetailsForPRP(quoteType, quoteNoId);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr1 = ds.Tables[0].Rows[0];

                    oE.CustomerId = dr1.IsNull("VendorId") ? string.Empty : Convert.ToString(dr1["VendorId"]);
                    oE.CustomerName = dr1.IsNull("VendorName") ? string.Empty : Convert.ToString(dr1["VendorName"]);
                    oE.SoNo = dr1.IsNull("SoNo") ? string.Empty : Convert.ToString(dr1["SoNo"]);
                    oE.PoNo = dr1.IsNull("PoNo") ? string.Empty : Convert.ToString(dr1["PoNo"]);
                    oE.PoDate = dr1.IsNull("PoDate") ? string.Empty : Convert.ToString(dr1["PoDate"]);
                    oE.ProductGroup = dr1.IsNull("ProductGroup") ? string.Empty : Convert.ToString(dr1["ProductGroup"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return oE;
        }


    }

}
