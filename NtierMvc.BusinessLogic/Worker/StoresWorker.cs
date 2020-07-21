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
using NtierMvc.Model.Stores;

namespace NtierMvc.BusinessLogic.Worker
{
    public class StoresWorker : IStoresWorker
    {
        #region Class Declarations

        private Repository _repository = new Repository();

        #endregion

        public string SaveGoodsRecieptEntryDetails(GoodsRecieptEntity entity)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveGoodsRecieptEntryDetails(entity);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }

        public GoodsRecieptEntityDetails FetchGoodsRecieptList(int pageIndex, int pageSize)
        {
            try
            {
                GoodsRecieptEntityDetails vmbEnDetails = new GoodsRecieptEntityDetails();
                vmbEnDetails.lstGREntity = new List<GoodsRecieptEntity>();

                DataSet ds = _repository.FetchGoodsRecieptList(pageIndex, pageSize);
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
                                GoodsRecieptEntity obj = new GoodsRecieptEntity();

                                obj.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                                obj.GRno = dr1.IsNull("GRno") ? 0 : Convert.ToInt32(dr1["GRno"]);

                                obj.GRDate = dr1.IsNull("GRDate") ? string.Empty : Convert.ToString(dr1["GRDate"]);
                                //obj.PRCat = dr1.IsNull("PRCat") ? string.Empty : Convert.ToString(dr1["PRCat"]);
                                obj.GoodRecieptNo = dr1.IsNull("GoodRecieptNo") ? string.Empty : Convert.ToString(dr1["GoodRecieptNo"]);
                                obj.SupplierName = dr1.IsNull("SupplierName") ? string.Empty : Convert.ToString(dr1["SupplierName"]);
                                obj.SupplierInvNo = dr1.IsNull("SupplierInvNo") ? string.Empty : Convert.ToString(dr1["SupplierInvNo"]);
                                obj.PoNo = dr1.IsNull("PoNo") ? string.Empty : Convert.ToString(dr1["PoNo"]);
                                obj.SupplierLocation = dr1.IsNull("SupplierLocation") ? string.Empty : Convert.ToString(dr1["SupplierLocation"]);
                                obj.SupplierDate = dr1.IsNull("SupplierDate") ? string.Empty : Convert.ToString(dr1["SupplierDate"]);
                                //obj.PoDate = dr1.IsNull("PoDate") ? string.Empty : Convert.ToString(dr1["PoDate"]);
                                obj.BatchNo = dr1.IsNull("BatchNo") ? string.Empty : Convert.ToString(dr1["BatchNo"]);
                                obj.HeatNo = dr1.IsNull("HeatNo") ? string.Empty : Convert.ToString(dr1["HeatNo"]);
                                obj.QtyReqd = dr1.IsNull("QtyReqd") ? 0 : Convert.ToInt32(dr1["QtyReqd"]);
                                obj.RejTeqNo = dr1.IsNull("RejTeqNo") ? 0 : Convert.ToInt32(dr1["RejTeqNo"]);
                                obj.InspectionReportNo = dr1.IsNull("InspectionReportNo") ? string.Empty : Convert.ToString(dr1["InspectionReportNo"]);
                                obj.LocationDetail = dr1.IsNull("LocationDetail") ? string.Empty : Convert.ToString(dr1["LocationDetail"]);
                                obj.PreparedBy = dr1.IsNull("PreparedBy") ? string.Empty : Convert.ToString(dr1["PreparedBy"]);
                                obj.StoresIncharge = dr1.IsNull("StoresIncharge") ? string.Empty : Convert.ToString(dr1["StoresIncharge"]);
                                obj.TestCertificationNo = dr1.IsNull("TestCertificationNo") ? string.Empty : Convert.ToString(dr1["TestCertificationNo"]);
                                obj.SupplyType = dr1.IsNull("SupplyType") ? string.Empty : Convert.ToString(dr1["SupplyType"]);
                                obj.SupplyTerms = dr1.IsNull("SupplyTerms") ? string.Empty : Convert.ToString(dr1["SupplyTerms"]);

                                vmbEnDetails.lstGREntity.Add(obj);
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

        public GoodsRecieptEntityDetails GetDetailForGateControlNo(string GateControlNo)
        {
            GoodsRecieptEntityDetails objGE = new GoodsRecieptEntityDetails();
            objGE.lstGREntity = new List<GoodsRecieptEntity>();

            try
            {
                DataSet ds = _repository.GetDetailForGateControlNo(GateControlNo);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];

                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        GoodsRecieptEntity Model = new GoodsRecieptEntity();

                        //Model.Id = dr1.IsNull("Id") ? 0 : Convert.ToInt32(dr1["Id"]);
                        //Model.GRno = dr1.IsNull("GRno") ? 0 : Convert.ToInt32(dr1["GRno"]);
                        //Model.GoodRecieptNo = dr1.IsNull("GoodRecieptNo") ? string.Empty : Convert.ToString(dr1["GoodRecieptNo"]);
                        //Model.SupplierInvNo = dr1.IsNull("SupplierInvNo") ? string.Empty : Convert.ToString(dr1["SupplierInvNo"]);
                        //Model.SupplierDate = dr1.IsNull("SupplierDate") ? string.Empty : Convert.ToString(dr1["SupplierDate"]);
                        //Model.GRDate = dr1.IsNull("GRDate") ? string.Empty : Convert.ToString(dr1["GRDate"]);
                        Model.SupplierName = dr1.IsNull("SupplierName") ? string.Empty : Convert.ToString(dr1["SupplierName"]);
                        Model.SupplierLocation = dr1.IsNull("SupplierLocation") ? string.Empty : Convert.ToString(dr1["SupplierLocation"]);
                        Model.PRCat = dr1.IsNull("PRCat") ? string.Empty : Convert.ToString(dr1["PRCat"]);
                        Model.GateControlNo = dr1.IsNull("GateControlNo") ? string.Empty : Convert.ToString(dr1["GateControlNo"]);
                        Model.PoNo = dr1.IsNull("PoNo") ? string.Empty : Convert.ToString(dr1["PoNo"]);

                        Model.PoDate = dr1.IsNull("PoDate") ? string.Empty : Convert.ToString(dr1["PoDate"]);
                        Model.SupplyTerms = dr1.IsNull("SupplyTerms") ? string.Empty : Convert.ToString(dr1["SupplyTerms"]);

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
                        Model.PreparedBy = dr1.IsNull("PreparedBy") ? "" : Convert.ToString(dr1["PreparedBy"]);
                        Model.StoresIncharge = dr1.IsNull("StoresIncharge") ? "" : Convert.ToString(dr1["StoresIncharge"]);

                        objGE.lstGREntity.Add(Model);
                    }
                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return objGE;
        }

        public GoodsRecieptEntity GetGRDetailsPopup()
        {
            GoodsRecieptEntity objGR = new GoodsRecieptEntity();
            try
            {
                DataSet ds = _repository.GetGRDetailsPopup();

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = ds.Tables[0];
                    DataRow dr1 = dt1.Rows[0];

                    objGR.GRno = dr1.IsNull("GRno") ? 0 : Convert.ToInt32(dr1["GRno"]);
                    objGR.GoodRecieptNo = dr1.IsNull("GoodRecieptNo") ? string.Empty : Convert.ToString(dr1["GoodRecieptNo"]);

                }
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }
            return objGR;
        }


    }
}
