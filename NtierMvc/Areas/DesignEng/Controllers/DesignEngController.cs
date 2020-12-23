using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model.DesignEng;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using NtierMvc.Infrastructure;
using NtierMvc.Areas.DesignEng.Models;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data;
using Microsoft.Office.Interop;
using Excel=Microsoft.Office.Interop.Excel;


namespace NtierMvc.Areas.DesignEng.Controllers
{
    [SessionExpire]
    public class DesignEngController : Controller
    {
        // GET: DesignEng/DesignEng        
        ProductRealisationDetails ePRPDetail;
        private DesignManager objManager;
        ProductRealisation ePRP;
        BaseModel model;

        public DesignEngController()
        {
            model = new BaseModel();
            objManager = new DesignManager();
        }

        [HttpGet]
        public ActionResult DesignEngMaster()
        {
            //Order Execution            
            ViewBag.ListQuoteType = model.GetMasterTableStringList("DesignPRP", "Id", "QuoteNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteNo = model.GetMasterTableStringList("DesignPRP", "Id", "QuoteNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSONo = model.GetMasterTableStringList("DesignPRP", "Id", "SONo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorId = model.GetMasterTableStringList("DesignPRP", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorName = model.GetMasterTableStringList("DesignPRP", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListProductGroup = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeD);

            ViewBag.ListDesignation = model.GetMasterTableStringList("Master.Designation", "Id", "DesignationName");
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);

            //Bill Monitoring
            ViewBag.ListType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "QuoteType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListVendorNature = "";
            ViewBag.ListVendorName = "";
            ViewBag.ListBillDate = model.GetMasterTableStringList("GateEntry", "Id", "BillDate", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListFunctionArea = "";
            ViewBag.ListFinancialYear = model.GetMasterTableStringList("GateEntry", "FinancialYear", "FinancialYear", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListApprovalStatus = model.GetMasterTableStringList("ApproveStatus", "Id", "Status", "", "", GeneralConstants.ListTypeN);

            return View();
        }

        [HttpGet]
        public ActionResult PartialProductRealisation()
        {
            ePRP = new ProductRealisation();
            return PartialView(ePRP);
        }

        public JsonResult FetchProductRealisationList(string pageIndex, string pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            SearchTypeId = SearchTypeId == null ? string.Empty : SearchTypeId;
            SearchQuoteNo = SearchQuoteNo == null ? string.Empty : SearchQuoteNo;
            SearchSONo = SearchSONo == null ? string.Empty : SearchSONo;
            SearchVendorId = SearchVendorId == null ? string.Empty : SearchVendorId;
            SearchVendorName = SearchVendorName == null ? string.Empty : SearchVendorName;
            SearchProductGroup = SearchProductGroup == null ? string.Empty : SearchProductGroup;

            ePRPDetail = objManager.FetchProductRealisationList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup);
            return new JsonResult { Data = ePRPDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchBillMonitoringList(string pageIndex, string pageSize, string SearchType = null, string SearchVendorNature = null, string SearchVendorName = null, string SearchBillNo = null, string SearchBillDate = null, string SearchItemDescription = null, string SearchCurrency = null, string SearchApprovalStatus = null)
        {
            SearchType = SearchType == null ? string.Empty : SearchType;
            SearchVendorNature = SearchVendorNature == null ? string.Empty : SearchVendorNature;
            SearchBillNo = SearchBillNo == null ? string.Empty : SearchBillNo;
            SearchBillDate = SearchBillDate == null ? string.Empty : SearchBillDate;
            SearchVendorName = SearchVendorName == null ? string.Empty : SearchVendorName;
            SearchItemDescription = SearchItemDescription == null ? string.Empty : SearchItemDescription;
            SearchCurrency = SearchCurrency == null ? string.Empty : SearchCurrency;
            SearchApprovalStatus = SearchApprovalStatus == null ? string.Empty : SearchApprovalStatus;

            //ePRPDetail = objManager.FetchProductRealisationList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchType, SearchVendorNature, SearchBillNo, SearchBillDate, SearchVendorName, SearchItemDescription, SearchCurrency, SearchApprovalStatus);

            return new JsonResult { Data = ePRPDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }



        [HttpPost]
        public ActionResult PRPPopup(string actionType, string SoNo)
        {
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            //ViewBag.ListQuoteNo = model.GetMasterTableStringList("QuotationRegister", "Id", "QUOTENO", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteNo = "";
            ViewBag.ListSONo = model.GetMasterTableStringList("Orders", "SoNo", "SoNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorId = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProductName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProductNo = model.GetMasterTableStringList("Master.Product", "Id", "ProductNo", "", "", GeneralConstants.ListTypeN);

            ViewBag.ListProductCode = model.GetMasterTableStringList("Master.Product", "Id", "ProductCode", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListPL = model.GetMasterTableStringList("Master.Product", "PL", "PL", "", "", GeneralConstants.ListTypeD);

            ViewBag.ListCasingSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "CasingSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCasingPPF = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Ppf", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListGrade = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "MatGrade", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListOpenHoleSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "OpenHoleSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListPOSlNo = model.GetMasterTableStringList("Items", "Id", "POSlNo", "", "", GeneralConstants.ListTypeD);

            ePRP = new ProductRealisation();
            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(SoNo))
                    ePRP.SONo = SoNo;

                ePRP = objManager.PRPPopup(ePRP);

                TechnicalManager techManager = new TechnicalManager();
                ViewBag.ListQuoteNo = techManager.GetQuoteNoList(ePRP.QuoteType);
            }
            if (actionType == "ADD")
            {
                //eOrder = objManager.GetUserDetails(PRPObj.UnitNo);
            }

            return base.PartialView("~/Areas/DesignEng/Views/DesignEng/_PRPDetails.cshtml", ePRP);
        }

        [HttpGet]
        public ActionResult PartialAdvanceProductRealisation()
        {
            ePRP = new ProductRealisation();
            return PartialView(ePRP);
        }

        [HttpPost]
        public ActionResult BOMPopup(string actionType, string BOMId)
        {
            ViewBag.ListProductName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProductCode = model.GetMasterTableStringList("Master.Product", "Id", "ProductCode", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListPL = model.GetMasterTableStringList("Master.Product", "PL", "PL", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListProductNo = model.GetMasterTableStringList("Master.Product", "Id", "ProductNo", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCasingSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "CasingSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCasingPPF = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Ppf", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListGrade = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "MatGrade", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListUom = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Uom", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListOpenHoleSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "OpenHoleSize", "Property", GeneralConstants.ListTypeN);

            BOMEntity bOM = new BOMEntity();
            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(BOMId))
                    bOM.Id = Convert.ToInt32(BOMId);
                //eOrder = objManager.BOMPopup(eOrder);
            }
            if (actionType == "ADD")
            {
                //eOrder = objManager.GetUserDetails(PRPObj.UnitNo);
            }

            return base.PartialView("~/Areas/DesignEng/Views/DesignEng/_BOMDetails.cshtml", bOM);
        }

        public JsonResult SearchForBOM(string ProductName = null, string ProductCode = null, string PL = null, string ProductNo = null, string CasingSize = null, string CasingPPF = null, string Grade = null, string OpenHoleSize = null)
        {
            var lstBOMDetails = objManager.GetBOMList(ProductName, ProductCode, PL, ProductNo, CasingSize, CasingPPF, Grade, OpenHoleSize);
            return new JsonResult { Data = lstBOMDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveBOMDetails(BOMEntity bomE)
        {
            string result = objManager.SaveBOMDetails(bomE);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + ". Reason: " + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveProductRealisationDetails(ProductRealisation objPRP)
        {
            string result = objManager.SaveProductRealisationDetails(objPRP);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                data = GeneralConstants.SavedSuccess;
            else
                data = "Error Message. " + result;

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetProductDetails(string ProductName, string ProductNo)
        {
            SingleColumnEntity objS = new SingleColumnEntity();
            objS = model.GetSingleColumnValues("BomMaster", "ProductCode", "", "ProductNo", ProductNo, "PL", "ProductName", ProductName, "CasingSize", "", "", "CasingPPF", "", "", "Grade", "OpenHoleSize", "UploadFile");

            return new JsonResult { Data = objS, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveAndUploadBOMDetails(int ProductName, int ProductCode, string PL, int ProductNo, int CasingSize, int CasingPPF, int Grade, int OpenHoleSize, int SN, string PartName, string CommodityNo, string COMMRevNo, int Qty, string Length, string OD, string WT, string UOM, string RMTYPE)
        {
            string result = string.Empty;
            string data = string.Empty;

            decimal tempD = 0;
            BOMEntity bomE = new BOMEntity();
            bomE.ProductName = ProductName;
            bomE.ProductCode = ProductCode;
            bomE.PL = PL;
            bomE.ProductNo = ProductNo;
            bomE.CasingSize = CasingSize;
            bomE.CasingPPF = CasingPPF;
            bomE.Grade = Grade;
            bomE.OpenHoleSize = OpenHoleSize;
            bomE.SN = SN;
            bomE.PartName = PartName;
            bomE.CommodityNo = CommodityNo;
            bomE.COMMRevNo = COMMRevNo;
            bomE.Qty = Qty;
            bomE.Length = decimal.TryParse(Length, out tempD) ? tempD : 0;
            bomE.OD = decimal.TryParse(OD, out tempD) ? tempD : 0;
            bomE.WT = decimal.TryParse(WT, out tempD) ? tempD : 0;
            bomE.UOM = UOM;
            bomE.RMTYPE = RMTYPE;


            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    StringBuilder strB = new StringBuilder();
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            string extension = System.IO.Path.GetExtension(file.FileName);
                            string Name = file.FileName.Substring(0, file.FileName.Length - extension.Length);

                            fname = Name + DateTime.Now.Millisecond + extension;
                            strB = strB.Append(fname + ",");
                        }

                        bomE.UploadFile = fname;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Documents/BOMUpload/"), fname);
                        file.SaveAs(fname);
                    }

                    result = objManager.SaveBOMDetails(bomE);

                    if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                    {
                        data = GeneralConstants.SavedSuccess;
                    }
                    else
                    {
                        data = GeneralConstants.NotSavedError + ". Reason: " + result;
                    }

                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }

                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                result = objManager.SaveBOMDetails(bomE);

                if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                {
                    data = GeneralConstants.SavedSuccess;
                }
                else
                {
                    data = GeneralConstants.NotSavedError + ". Reason: " + result;
                }

                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //For Bill Monitoring Starts
        [HttpGet]
        public ActionResult PartialBillMonitoring()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BillMonitoringPopUp(string actionType, string BillId)
        {
            BaseModel model = new BaseModel();
            ViewBag.ListType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "QuoteType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListVendorNature = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorId = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListEndUse = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "EndUse", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUseNo = "";
            ViewBag.ListUom = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Uom", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSCCNO = model.GetMasterTableStringList("SCCNO", "Id", "Heading", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCostCenter = "";
            ViewBag.ListApproveStatus = model.GetMasterTableStringList("ApproveStatus", "Id", "Status", "", "", GeneralConstants.ListTypeN);

            BillMonitoringEntity vmbE = new BillMonitoringEntity();
            vmbE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(BillId))
                    vmbE.Id = Convert.ToInt32(BillId);

                vmbE = objManager.BillDetailsPopup(vmbE);

            }
            if (actionType == "ADD")
            {
                //List<DropDownEntity> SelectList = new List<DropDownEntity>();
                //DropDownEntity objSelect = new DropDownEntity();
                //objSelect.DataStringValueField = "";
                //objSelect.DataTextField = "Select";
                //SelectList.Add(objSelect);

                //ViewBag.ListQuoteNo = ViewBag.ListQuoteSlNo = SelectList;

            }

            return base.PartialView("~/Areas/DesignEng/Views/DesignEng/_BillMonitoringPopUp.cshtml", vmbE);
        }

        [HttpPost]
        public ActionResult SaveBillMonitoringDetails(BillMonitoringEntity vmbE)
        {
            string result = objManager.SaveBillMonitoringDetails(vmbE);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + ". Reason: " + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetPoSLNoDetails(string POSlNo)
        {
            List<ProductRealisation> ePRP = new List<ProductRealisation>();
            ePRP = objManager.GetPoSLNoDetails(POSlNo);

            return new JsonResult { Data = ePRP, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetProductNoForProductName(string ProductNameId)
        {
            SingleColumnEntity objS = model.GetSingleColumnValues("Master.Product", "ProductNo", "Id", "Id", ProductNameId);

            return new JsonResult { Data = objS.DataValueField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult DeletePRPDetail(int id)
        {
            string TableName = "DesignPRP";
            string ColumnName1 = "SoNo";
            string Param1 = id.ToString();

            string msgCode = model.DeleteFormTable(TableName, ColumnName1, Param1);

            if (msgCode == GeneralConstants.DeleteSuccess)
                return new JsonResult { Data = GeneralConstants.DeleteSuccess, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            else
                return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetPlSoNoDetails(string SoNo)
        {
            List<DropDownEntity> ddE = new List<DropDownEntity>();
            ddE = model.GetDropDownList("Items", GeneralConstants.ListTypeD, "Id", "PoSlNo", SoNo, "SoNo", false, "asc", "PoSlNo");

            return new JsonResult { Data = ddE, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult PartialReports()
        {
            return View();
        }

        public ActionResult ReportsPopup()
        {
            ViewBag.ListVendorId = model.GetDropDownList("Clientele_Master", GeneralConstants.ListTypeN, "Id", "VendorId", "", "");
            ViewBag.ListSoNo = model.GetDropDownList("DesignPRP", GeneralConstants.ListTypeN, "SoNo", "SoNo", "", "");
            ViewBag.ListQuoteType = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeN, "DropDownId", "DropDownValue", "QuoteType", "Property");
            return PartialView("~/Areas/DesignEng/Views/DesignEng/_ReportsPopup.cshtml");
            //return PartialView("_ReportsPopup.cshtml");
        }

        public JsonResult GenerateExcelReport(string ReportType, string DateFrom, string DateTo, string VendorId = null, string SoNo = null)
        {
            var downloadFileName = "Detailed_PRP_Report" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            try
            {
                var path = Server.MapPath(ConfigurationManager.AppSettings["ReportsCreateExcelPRP"]);
                var fileName = "Detailed_PRP_Report" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                DateFrom = Convert.ToDateTime(DateFrom).ToString("yyyy-MM-dd");
                DateTo = Convert.ToDateTime(DateTo).ToString("yyyy-MM-dd");
                DataTable reportRecords = objManager.GetDataTablePRPData(ReportType, DateFrom, DateTo, VendorId, SoNo);

                if (reportRecords.Rows.Count > 0)
                {
                    string fullPath = Path.Combine(path, fileName);
                    CreateExcelFile.CreateExcelDocument(reportRecords, fullPath, includeAutoFilter: true);
                    openFile(fileName);
                }
                else
                    fileName = "";
                
                return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                //Logger.LogException(ex);
                return new JsonResult { Data = "Cannot download Excel " + ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        private void openFile(string fileName)
        {
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ReportsCreateExcelPRP"]), fileName);
            var excelApp = new Excel.Application();
            excelApp.Visible = true;

            if (System.IO.File.Exists(fullPath))
            {
                ////Get the temp folder and file path in server
                Excel.Workbooks books = excelApp.Workbooks;
                Excel.Workbook sheet = books.Open(fullPath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlExtractData);
                System.IO.File.Delete(fullPath);
            }
        }

        [HttpGet]
        [OutputCache(Duration = 10)]
        public ActionResult Download(string fileName)
        {

            var fullPath = Path.Combine(ConfigurationManager.AppSettings["ReportsCreateExcelPRP"].ToString(), fileName);

            if (System.IO.File.Exists(fullPath))
            {
                ////Get the temp folder and file path in server
                byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                System.IO.File.Delete(fullPath);        
                return File(fileByteArray, "application/vnd.ms-excel", fileName);
            }

            else
                return Json(new { data = "", errorMessage = "Error While Generating Excel. Contact Support." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVendorIdFromQuoteType(string ReportType=null)
        {
            var lstVendorId = objManager.GetVendorIdFromQuoteType(ReportType=null);

            return new JsonResult { Data = lstVendorId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetQuoteOrderDetailsForPRP(string quoteType, string quoteNoId)
        {
            OrderEntity oEntity = new OrderEntity();
            oEntity = objManager.GetQuoteOrderDetailsForPRP(quoteType, quoteNoId);
            //List<DropDownEntity> soNo = new List<DropDownEntity>();
            //soNo = model.GetDropDownList("Orders", GeneralConstants.ListTypeD, "SoNo", "SoNo", quoteType, "QuoteType", "", "", quoteNoId, "QuoteNo");
            //var result = new { oEntity, soNo };
            return new JsonResult { Data = oEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetSoNosFromVendorId(string VendorId)
        {
            var oEntity = model.GetDropDownList("DesignPRP",GeneralConstants.ListTypeD,"SoNo", "SoNo", VendorId, "VendorId");
            return new JsonResult { Data = oEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        
    }
}