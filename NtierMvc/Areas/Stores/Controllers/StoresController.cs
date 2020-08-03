using DocumentFormat.OpenXml.Office.CustomUI;
using NtierMvc.Areas.MRM.Models;
using NtierMvc.Areas.Stores.Models;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Account;
using NtierMvc.Model.Application;
using NtierMvc.Model.Stores;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace NtierMvc.Areas.Stores.Controllers
{
    [SessionExpire]
    public class StoresController : Controller
    {
        private StoresManager objManager;
        private MRMManager objMRMManager;
        BaseModel model;

        public StoresController()
        {
            model = new BaseModel();
            objManager = new StoresManager();
            objMRMManager = new MRMManager();
        }

        // GET: Stores/Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StoresMaster()
        {
            ViewBag.ListVendorType = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorType", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSupplierId = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListRMCategory = model.GetMasterTableStringList("Master.Taxonomy", "DropDownID", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeD);
            List<DropDownEntity> newlst = model.GetMasterTableStringList("PurchaseRequest", "DeliveryDate", "DeliveryDate", "", "", GeneralConstants.ListTypeD);

            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListStoresName = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoresName", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListBayNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "BAYNO", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListLocation = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Location", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListPostion = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Position", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListDirection = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Direction", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListStoreArea = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoreArea", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSN = "";

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTime;
            foreach (var item in newlst)
            {
                if (DateTime.TryParse(item.DataStringValueField, out dateTime))
                {
                    item.DataStringValueField = Convert.ToDateTime(item.DataStringValueField).ToString("MM-dd-yyyy");
                    item.DataTextField = Convert.ToDateTime(item.DataTextField).ToString("MM-dd-yyyy");
                }
            }

            ViewBag.ListDeliveryDate = newlst;

            return View();
        }


        [HttpGet]
        public ActionResult PartialGoodsReciept()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GRPopup(string actionType, string GRno = null)
        {

            if (Session["CommonDetails"] != null)
            {
                var CommonDetails = (CommonDetailsEntity)Session["CommonDetails"];

                ViewBag.CompanyName = CommonDetails.CompanyName;
                ViewBag.Logo = CommonDetails.Logo;
                ViewBag.Address1 = CommonDetails.Address1;
                ViewBag.Address2 = CommonDetails.Address2;
                ViewBag.Address3 = CommonDetails.Address3;
                ViewBag.Phone = CommonDetails.Phone;
                ViewBag.Mobile = CommonDetails.Mobile;
                ViewBag.Website = CommonDetails.Website;
                ViewBag.Email1 = CommonDetails.Email1;
                ViewBag.Email2 = CommonDetails.Email2;
                ViewBag.Email3 = CommonDetails.Email3;
                ViewBag.Notation = CommonDetails.Notation;
            }


            ViewBag.ListGateControlNo = model.GetMasterTableStringList("GateEntry", "GateNo", "GateControlNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListPRCat = model.GetMasterTableStringList("Master.Taxonomy", "DropDownValue", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "SupplyType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListStoresName = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoresName", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListBayNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "BAYNO", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListLocation = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Location", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListPostion = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Position", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListDirection = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Direction", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListStoreArea = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoreArea", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSN = "";

            GoodsRecieptEntity orderE = new GoodsRecieptEntity();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                orderE = objManager.GetGRDetailsPopup(GRno);
            }
            if (actionType == "ADD")
            {
                orderE = objManager.GetGRDetailsPopup();
            }

            return base.PartialView("~/Areas/Stores/Views/Stores/_GoodsRecieptPopUp.cshtml", orderE);
        }

        [HttpPost]
        public JsonResult GetGRValuesFromSupplyType(string SupplyType)
        {
            try
            {
                var ddl = model.GetDropDownList("GateEntry", GeneralConstants.ListTypeD, "GateNo", "GateControlNo", SupplyType, "SupplyType");
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }
        }

        public JsonResult GetDetailForGateControlNo(string GateControlNo, string GRNo=null)
        {
            GoodsRecieptEntityDetails poObj = new GoodsRecieptEntityDetails();
            poObj = objManager.GetDetailForGateControlNo(GateControlNo, GRNo);

            if (poObj.lstGREntity.Count > 0)
            {
                var UserDetails = (UserEntity)Session["UserModel"];
                poObj.lstGREntity[0].StoresInchargeSign = UserDetails.SignImage;
            }

            return new JsonResult { Data = poObj.lstGREntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchGoodsRecieptList(string pageIndex, string pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            SearchVendorTypeId = SearchVendorTypeId == null ? string.Empty : SearchVendorTypeId;
            SearchSupplierId = SearchSupplierId == null ? string.Empty : SearchSupplierId;
            SearchRMCategory = SearchRMCategory == null ? string.Empty : SearchRMCategory;
            SearchDeliveryDateFrom = SearchDeliveryDateFrom == null ? string.Empty : SearchDeliveryDateFrom;
            SearchDeliveryDateTo = SearchDeliveryDateTo == null ? string.Empty : SearchDeliveryDateTo;

            GoodsRecieptEntityDetails grEntity = new GoodsRecieptEntityDetails();
            grEntity = objManager.FetchGoodsRecieptList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDateFrom, SearchDeliveryDateTo);
            return new JsonResult { Data = grEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveGoodsRecieptEntryDetails(GoodsRecieptBulkEntity[] StoreDetails)
        {
            string data = string.Empty;
            var UserDetails = (UserEntity)Session["UserModel"];
            try
            {
                if (StoreDetails != null)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();
                    List<GoodsRecieptBulkEntity> itemListBulk = new List<GoodsRecieptBulkEntity>();
                    itemListBulk = StoreDetails.OfType<GoodsRecieptBulkEntity>().ToList();

                    foreach(var Item in itemListBulk)
                    {
                        Item.StoresIncharge = UserDetails.UserId;
                    }
                    string result = string.Empty;
                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);
                    objBU.IdentityNo = StoreDetails[0].GRno;
                    result = objManager.SaveGoodsRecieptEntryDetails(objBU);

                    if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                    {
                        data = GeneralConstants.SavedSuccess;
                    }
                    else
                    {
                        data = GeneralConstants.NotSavedError + " Reason: " + result;
                    }

                    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                return Json("Unable to save Item Details! Please Provide correct information", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Item Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSuppliers(string VendorTypeId)
        {
            try
            {
                List<DropDownEntity> ddl = model.GetDropDownList("Clientele_Master", GeneralConstants.ListTypeD, "Id", "VendorId", VendorTypeId, "VendorTypeId");
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }

        }

        public JsonResult GetRMCategories(string SupplierId)
        {
            try
            {
                List<DropDownEntity> ddl = objMRMManager.GetRMCategories(SupplierId);
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }

        }

        public JsonResult GetDeliveryDates(string RMCategory)
        {
            try
            {
                List<DropDownEntity> ddl = objMRMManager.GetDeliveryDates(RMCategory);

                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime dateTime;
                foreach (var item in ddl)
                {
                    if (DateTime.TryParse(item.DataStringValueField, out dateTime))
                    {
                        item.DataStringValueField = Convert.ToDateTime(item.DataStringValueField).ToString("MM-dd-yyyy");
                        item.DataTextField = Convert.ToDateTime(item.DataTextField).ToString("MM-dd-yyyy");
                    }
                }

                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }
        }


        public ActionResult DeleteGoodsRecieptDetails(string id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = model.DeleteFormTable("GoodsReciept", "GRno", id);
                if (msgCode != "")
                {
                    //return RedirectToAction("Technical");
                    return new JsonResult { Data = GeneralConstants.DeleteSuccess, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            else
            {
                Response.StatusCode = 444;
                Response.Status = "Not Saved";
                return null;
            }

        }


        [HttpPost]
        public ActionResult CreateDocumentForGR(string GRno)
        {
            string FileName = "";
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["GRExcelFile"]);
                FileName = Path.GetFileName(path);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Sheet1"];

                DataTable resultData = objManager.GetGoodsDetailForDocument(GRno);
                DataTable resultList = objManager.GetGoodsListDataForDocument(GRno);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#GoodRecieptNo", resultData.Rows[0]["GoodRecieptNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#SupplierName", resultData.Rows[0]["SupplierName"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#SupplierInvNo", resultData.Rows[0]["SupplierInvNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#SupplierDate", resultData.Rows[0]["SupplierDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#GRDate", resultData.Rows[0]["GRDate"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#VehicleNo", resultData.Rows[0]["VehicleNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#ContainerNo", resultData.Rows[0]["ContainerNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PONo", resultData.Rows[0]["PONo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PODate", resultData.Rows[0]["PODate"]);

                ////////////////For Image////////////////////
                #region Image
                Microsoft.Office.Interop.Excel.Range cells = xlWorkbook.Worksheets[1].Cells;

                //PreparedBy Sign
                Microsoft.Office.Interop.Excel.Range matchReqBy = cells.Find("#PreparedBySign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#PreparedBySign", "");

                string matchAdd = matchReqBy != null ? matchReqBy.Address : null;
                if (matchReqBy != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchReqBy.Row, matchReqBy.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["PreparedBySign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //ApprovedBy Sign
                Microsoft.Office.Interop.Excel.Range matchStore = cells.Find("#StoresInchargeSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#StoresInchargeSign", "");

                string matchStoreAdd = matchStore != null ? matchStore.Address : null;
                if (matchStoreAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchStore.Row, matchStore.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["StoresInchargeSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                #endregion
                ////////////////For Image////////////////////


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[10, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 10, resultList.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    //CubMtr = CubMtr + Convert.ToDouble(dr[7]);
                }

                //xlWorkbook.Worksheets[1].Cells.Replace("#TotalValue", CubMtr);
                //string TotalWords = model.NumberToWords(CubMtr.ToString());
                //xlWorkbook.Worksheets[1].Cells.Replace("#ValueInWords", TotalWords);

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[10, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 10, resultList.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                range1.Value = arr;

                string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), FileName);
                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                Download(FileName);
            }
            catch (Exception ex)
            {
                var response = ex.Message;
            }

            return Json(new { fileName = FileName, errorMessage = "Error While Generating Excel. Contact Support." });
        }

        private void Download(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);
            var excelApp = new Excel.Application();
            excelApp.Visible = true;

            if (System.IO.File.Exists(fullPath))
            {
                ////Get the temp folder and file path in server
                Excel.Workbooks books = excelApp.Workbooks;
                Excel.Workbook sheet = books.Open(fullPath, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlNormalLoad);
                System.IO.File.Delete(fullPath);
                //return Json(new { data = "", errorMessage = "" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}