using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class TechnicalController : Controller
    {
        private LoggingHandler _loggingHandler;
        QuotationEntity quotE;
        QuotationEntityDetails techDetail;
        QuotationPreparationEntity qPEntity;
        private TechnicalManager objManager;
        private QuotationManager objQuoteManager;
        private EnquiryManager objEnquiryManager;
        EnquiryEntityDetails enq;
        QuotationManager objManagerQuote;
        BaseModel model;

        #region Constructor
        public TechnicalController()
        {
            _loggingHandler = new LoggingHandler();
            quotE = new QuotationEntity();
            techDetail = new QuotationEntityDetails();
            qPEntity = new QuotationPreparationEntity();
            objManager = new TechnicalManager();
            objQuoteManager = new QuotationManager();
            objEnquiryManager = new EnquiryManager();
            model = new BaseModel();
            enq = new EnquiryEntityDetails();
            objManagerQuote = new QuotationManager();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_loggingHandler != null)
        //        {
        //            _loggingHandler.Dispose();
        //            _loggingHandler = null;
        //        }
        //        if (objManager != null)
        //        {
        //            objManager.Dispose();
        //            objManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
        #endregion

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult TechnicalMaster()
        {
            //Technical
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCustomerName = model.GetMasterTableStringList("Customer", "Id", "CustomerName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteNo = model.GetMasterTableStringList("QuotationRegister", "Id", "QUOTENO", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            //ViewBag.ListEnqFor = model.GetMasterTableStringList("QuotationRegister", "EnqFor", "EnqFor", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);


            //Enquiry
            ViewBag.ListEnqType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEOQ = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Expression of Quote(EOQ)", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEnqFor = model.GetMasterTableStringList("EnquiryRegister", "EnquiryId", "EnqFor", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDueDate = "";
            //Dictionary<string, string> Months = new Dictionary<string, string>();
            //Months.Add(Convert.ToString(DateTime.Now.Month), "Current Month");
            //Months.Add(Convert.ToString(DateTime.Now.Month + 3), "Next 3 Months");

            //List<DropDownEntity> ListMonths = new List<DropDownEntity>();
            //DropDownEntity dDE0 = new DropDownEntity();
            //dDE0.DataStringValueField = "";
            //dDE0.DataTextField = "Select";
            //ListMonths.Add(dDE0);
            //DropDownEntity dDE = new DropDownEntity();
            //dDE.DataStringValueField = Convert.ToString(DateTime.Now.Month);
            //dDE.DataTextField = "Current Month";
            //ListMonths.Add(dDE);
            //DropDownEntity dDE1 = new DropDownEntity();
            //dDE1.DataStringValueField = Convert.ToString(DateTime.Now.Month + 3);
            //dDE1.DataTextField = "Next 3 Months";
            //ListMonths.Add(dDE1);

            //ViewBag.ListMonth = ListMonths;

            //Quotation
            //ViewBag.ListDeliveryTerms = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "DeliveryTerms", "ObjectName", GeneralConstants.ListTypeN);
            ViewBag.ListDeliveryTerms = "";
            ViewBag.ListSubject = "";


            return View();
        }

        [HttpGet]
        public ActionResult PartialQuoteReg()
        {
            return PartialView(techDetail);
        }

        public List<DropDownEntity> DdlList()
        {
            List<DropDownEntity> DropList = new List<DropDownEntity>();
            DropDownEntity dE = new DropDownEntity();
            dE.DataTextField = "Select";
            dE.DataStringValueField = "";
            DropList.Add(dE);

            return DropList;
        }

        [HttpGet]
        public ActionResult PartialQuotePrep()
        {
            ViewBag.ListQuoteNo = DdlList(); //objManager.GetQuoteNoList(string.Empty); 

            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorName = model.GetMasterTableStringList("QuotationRegister", "CustomerId", "CustomerName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.CasingSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "CasingSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.CasingPpf = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Ppf", "Property", GeneralConstants.ListTypeN);
            ViewBag.MaterialGrade = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "MatGrade", "Property", GeneralConstants.ListTypeN);
            ViewBag.Connection = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Connection", "Property", GeneralConstants.ListTypeN);
            ViewBag.ProductNameList = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListUom = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Uom", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListEnqNo = model.GetMasterTableStringList("EnquiryRegister", "EnquiryId", "EnqRef", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListOpenHoleSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "OpenHoleSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListBallSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "BallSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCasingWT = model.GetMasterTableStringList("CasingWeight", "Id", "CasingWT", "", "", GeneralConstants.ListTypeN);

            return PartialView("~/Views/Technical/_TechQuotePrepDetails.cshtml", qPEntity);
        }

        [HttpGet]
        public ActionResult Technical()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SaveQuotationDetails(QuotationEntity cusE)
        {
            model = new BaseModel();
            //ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            //ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            //ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveQuotationDetails(cusE);

            TempData["VendorName"] = cusE.CustomerName;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveQuotePreparation(QuotationPreparationEntity quoteP)
        {
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            quoteP.UserInitial = Session["UserName"].ToString();
            quoteP.ipAddress = ERPContext.UserContext.IpAddress;
            quoteP.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveQuotePreparation(quoteP);

            TempData["UserName"] = quoteP.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
                TempData["StatusMsg"] = "Success";
            }
            else
            {
                data = GeneralConstants.NotSavedError;
                TempData["StatusMsg"] = "Failure";
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult QuotationPopup(string actionType, string QuotationId)
        {

            model = new BaseModel();
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");
            ViewBag.ListCountry = model.GetMasterTableList("Master.Country", "Id", "Country");
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdType = model.GetMasterTableStringList("ProductType", "Id", "TypeName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Currency", "Property");
            ViewBag.ListInspection = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Inspection", "Property");
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.ListModeOfDespatch = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Transport", "Property");

            quotE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                List<DropDownEntity> DT = new List<DropDownEntity>();
                DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportQuote", "Property", GeneralConstants.ListTypeN);
                List<DropDownEntity> DT1 = new List<DropDownEntity>();
                DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticQuote", "Property", GeneralConstants.ListTypeN);

                foreach (var item in DT1)
                {
                    if (!item.DataTextField.Equals("All"))
                        DT.Add(item);
                }
                ViewBag.ListDeliveryTerms = DT;

                if (!string.IsNullOrEmpty(QuotationId))
                    quotE.Id = Convert.ToInt32(QuotationId);
                //quotE = objManager.TechnicalDetailsPopup(quotE);

                ViewBag.ListEnqNo = objManager.GetQuoteEnqNoList(quotE);
                quotE = objQuoteManager.QuotationDetailsPopup(quotE);

            }
            if (actionType == "ADD")
            {
                List<DropDownEntity> SelectList = new List<DropDownEntity>();
                DropDownEntity objSelect = new DropDownEntity();
                objSelect.DataStringValueField = "";
                objSelect.DataTextField = "Select";
                SelectList.Add(objSelect);

                ViewBag.ListDeliveryTerms = ViewBag.ListEnqNo = SelectList;

                quotE = objManager.GetUserDetails(quotE.UnitNo);
            }

            return base.PartialView("~/Views/Technical/_TechQuotationDetails.cshtml", quotE);
        }

        public ActionResult DeleteTechnicalDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = objManager.DeleteTechnicalDetail(id);
                if (msgCode == "")
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

        public ActionResult GetQuoteVendorDetail(string vendorId)
        {
            QuotationEntity qEntity = new QuotationEntity();
            List<DropDownEntity> ListEnquiryNo = new List<DropDownEntity>();
            QuotationManager qM = new QuotationManager();
            QuotationEntityDetails qEDetail = new QuotationEntityDetails();

            qEntity = qM.GetVendorQuoteDetails(vendorId);

            qEDetail.quoteEntity = qEntity;
            qEDetail.lstDdEntity = objManager.GetEnqNoList(vendorId);
            //ListEnquiryNo = objManager.GetEnqNoList(vendorId);

            //List<string> stringColumnNames = new List<string>();
            //List<string> stringColumnValues = new List<string>();
            //List<int> intValueParams = new List<int>();
            //ListEnquiryNo = model.GetTableRecordsList(GeneralConstants.ListTypeN, stringColumnNames, stringColumnValues, intValueParams);


            return new JsonResult { Data = qEDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetProductLineList(string productLine)
        {
            List<DropDownEntity> ProductNameList = new List<DropDownEntity>();
            ProductNameList = objManager.GetProductLineList(productLine);
            return new JsonResult { Data = ProductNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetProductNumber(string productNameId)
        {
            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("Master.Product", "ProductNo", "ProductNo", "Id", productNameId);

            return new JsonResult { Data = objSingle.DataValueField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //ProductEntity pEntity = new ProductEntity();
            //pEntity.ProductNo = objManager.GetProductNumber(productNameId, productType);
            //return new JsonResult { Data = pEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpPost]
        public ActionResult CreateDownloadDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId, string quoteTypeText)
        {
            string quoteNoForFileName = "";
            //switch (quoteTypeText)
            //{
            //    case "Domestic":
            //        quoteNoForFileName = "DQ";
            //        break;
            //    case "Export":
            //        quoteNoForFileName = "EQ";
            //        break;
            //    case "Service":
            //        quoteNoForFileName = "SR";
            //        break;
            //    default:
            //        quoteNoForFileName = "SR";
            //        break;
            //}

            quoteNoForFileName = quoteNoForFileName + "-" + quoteNumberId + ".xlsx";
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), quoteNoForFileName);
            string fileName = "", FileQuoteNo = "";

            if (downloadTypeId == "xlsx")
                fileName = GenerateExcel(downloadTypeId, quoteTypeId, quoteNumberId, quoteNoForFileName, fullPath, fileName);
            else if (downloadTypeId == "docs")
                fileName = GenerateDoc(downloadTypeId, quoteTypeId, quoteNumberId, quoteNoForFileName, fullPath, fileName);

            Download(fileName);
            //return Json(new { fileName = fileName, errorMessage = "Error While Generating Excel. Contact Support." });
            return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        private string GenerateDoc(string downloadTypeId, string quoteTypeId, string quoteNumberId, string quoteNoForFileName, string fullPath, string fileName)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Sheet1"];


                DataTable resultData = objManager.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
                DataTable resultList = objManager.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#QuoteDate", resultData.Rows[0]["QuoteDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerName", "M/s." + resultData.Rows[0]["CustomerNAME"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerAddress", resultData.Rows[0]["CustomerAddress"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqNo", resultData.Rows[0]["enqno"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqDt", resultData.Rows[0]["enqDt"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CompanyName", resultData.Rows[0]["CompanyName"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#QuotationNo", resultData.Rows[0]["QuotationNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#Year", result.Rows[0]["Year"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CompanyInitial", resultData.Rows[0]["CompanyInitial"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#UserInitial", resultData.Rows[0]["UserInitial"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#FileNo", resultData.Rows[0]["FILENO"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqFor", resultData.Rows[0]["ENQFOR"]);

                //Removing for Table in Excel
                //result.Columns.Remove("vendorname");
                //result.Columns.Remove("VendorAddress");
                //result.Columns.Remove("enqno");
                //result.Columns.Remove("enqDt");
                //result.Columns.Remove("CompanyName");
                //result.Columns.Remove("QuotationNo");
                //result.Columns.Remove("Year");
                //result.Columns.Remove("CompanyInitial");
                //result.Columns.Remove("UserInitial");
                //result.Columns.Remove("FILENO");
                //result.Columns.Remove("ENQFOR");

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[21, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 21, resultList.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //Adding Table values in Excel
                int sum = 0, NetWt = 0, GrWt = 0;
                double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count - 3; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    sum = sum + Convert.ToInt32(dr[5]);
                    NetWt = NetWt + Convert.ToInt32(dr[6]);
                    GrWt = GrWt + Convert.ToInt32(dr[7]);
                    CubMtr = CubMtr + Convert.ToDouble(dr[8]);
                }

                xlWorkbook.Worksheets[1].Cells.Replace("#TotalPrice", sum.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#NetWeight", NetWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#GrossWeight", GrWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#CubicMeter", CubMtr.ToString());

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[21, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 21, resultList.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                range1.EntireRow.Font.Bold = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();
                fileName = quoteNoForFileName;
            }
            catch (Exception ex)
            {
                var response = ex.Message;
            }

            return fileName;
        }

        private string GenerateExcel(string downloadTypeId, string quoteTypeId, string quoteNumberId, string quoteNoForFileName, string fullPath, string fileName)
        {
            try
            {

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["MOT F SM 08 Rev.00"];


                DataTable resultData = objManager.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
                DataTable resultList = objManager.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#QuoteNoAndRev", resultData.Rows[0]["QuoteNoView"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#QuoteDate", resultData.Rows[0]["QuoteDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#FileNo", resultData.Rows[0]["FILENO"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqRef", resultData.Rows[0]["ENQNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#QuoteValidity", resultData.Rows[0]["QuoteValidity"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Currency", resultData.Rows[0]["Currency"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PaymentTerms", resultData.Rows[0]["PaymentTerms"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#SalesPerson", resultData.Rows[0]["SalesPerson"]);

                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerName", "M/s." + resultData.Rows[0]["CustomerNAME"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerAddress", resultData.Rows[0]["CustomerAddress"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerEmail", resultData.Rows[0]["CustomerEmail"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Subject", resultData.Rows[0]["Subject"]);

                //xlWorkbook.Worksheets[1].Cells.Replace("#EnqNo", resultData.Rows[0]["enqno"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#EnqDt", resultData.Rows[0]["enqDt"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#CompanyName", resultData.Rows[0]["CompanyName"]);
                ////xlWorkbook.Worksheets[1].Cells.Replace("#Year", result.Rows[0]["Year"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#CompanyInitial", resultData.Rows[0]["CompanyInitial"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#UserInitial", resultData.Rows[0]["UserInitial"]);

                //Removing for Table in Excel
                //result.Columns.Remove("vendorname");
                //result.Columns.Remove("VendorAddress");
                //result.Columns.Remove("enqno");
                //result.Columns.Remove("enqDt");
                //result.Columns.Remove("CompanyName");
                //result.Columns.Remove("QuotationNo");
                //result.Columns.Remove("Year");
                //result.Columns.Remove("CompanyInitial");
                //result.Columns.Remove("UserInitial");
                //result.Columns.Remove("FILENO");
                //result.Columns.Remove("ENQFOR");

                ////////////////For Image////////////////////
                #region Image
                Microsoft.Office.Interop.Excel.Range cells = xlWorkbook.Worksheets[1].Cells;

                //ReqBy
                Microsoft.Office.Interop.Excel.Range matchReqBy = cells.Find("#OwnerSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#OwnerSign", "");

                string matchAdd = matchReqBy != null ? matchReqBy.Address : null;
                if (matchReqBy != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchReqBy.Row, matchReqBy.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["OwnerSignImage"]));
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                #endregion
                ////////////////For Image////////////////////

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 16, resultList.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //Adding Table values in Excel
                int sum = 0, NetWt = 0, GrWt = 0;
                double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count - 4; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    sum = sum + Convert.ToInt32(dr[6]);
                    NetWt = NetWt + Convert.ToInt32(dr[8]);
                    GrWt = GrWt + Convert.ToInt32(dr[9]);
                    CubMtr = CubMtr + Convert.ToDouble(dr[10]);
                }

                xlWorkbook.Worksheets[1].Cells.Replace("#TotalPrice", sum.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#AmountInWords", model.NumberToWords(sum.ToString()));
                xlWorkbook.Worksheets[1].Cells.Replace("#NetWeight", NetWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#GrossWeight", GrWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#CubicMeter", CubMtr.ToString());

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 16, resultList.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                //Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                //Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
                //Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
                //Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
                //c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                //c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //range1.EntireRow.Font.Bold = true;
                range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();
                fileName = quoteNoForFileName;
            }
            catch (Exception ex)
            {
                var response = ex.Message;
            }

            return fileName;
        }

        private void Download(string fileName)
        {
            //Do not delete commented text
            //string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);

            //if (System.IO.File.Exists(fullPath))
            //{
            //    ////Get the temp folder and file path in server
            //    byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
            //    System.IO.File.Delete(fullPath);
            //    return File(fileByteArray, "application/vnd.ms-excel", fileName);

            //}

            //else
            //    return Json(new { data = "", errorMessage = "Error While Generating Excel. Contact Support." }, JsonRequestBehavior.AllowGet);

            //string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);

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
            //else
            //    return Json(new { data = "", errorMessage = "Error While Generating Excel. Contact Support." }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductNameForProductType(int productTypeId)
        {
            List<DropDownEntity> prList = new List<DropDownEntity>();

            switch (productTypeId)
            {
                case 1:
                    prList = model.GetMasterTableStringList("ProductOIL", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
                    break;
                case 2:
                    prList = model.GetMasterTableStringList("ProductONGC", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
                    break;
                case 3:
                    prList = model.GetMasterTableStringList("ProductOthers", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
                    break;
                case 4:
                    prList = model.GetMasterTableStringList("ProductStandard", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
                    break;
                default:
                    prList = model.GetMasterTableStringList("[Master.Product]", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
                    break;
            }

            return new JsonResult { Data = prList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetDeliveryItems(string quotetypeId)
        {
            QuoteTypeDetails qDetails = new QuoteTypeDetails();
            qDetails.lstQuoteTypeEntity = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", quotetypeId, "TaxonomyID", GeneralConstants.ListTypeN);
            qDetails.QuoteNo = objManager.GetQuoteNo(quotetypeId);
            qDetails.lstVendors = objManager.GetVendorDetails(quotetypeId);

            return new JsonResult { Data = qDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetPrepQuoteNo(string quotetypeId)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            lstQuoteNo = objManager.GetQuoteNoList(quotetypeId);
            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetOrderQuoteNo(string quotetypeId)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            List<DropDownEntity> lstSoNo = new List<DropDownEntity>();
            DropDownEntity objSelect = new DropDownEntity();
            objSelect.DataStringValueField = "";
            objSelect.DataTextField = "Select";
            //lstQuoteNo = objManager.GetQuoteNoList(quotetypeId);
            lstSoNo = model.GetMasterTableStringList("Orders", "SoNo", "SoNo", quotetypeId, "QuoteType", GeneralConstants.ListTypeD);
            //lstQuoteNo.Insert(0, objSelect);
            var result = new { lstQuoteNo, lstSoNo };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetProductTypes(string quotetypeId)
        {
            //List<TableRecordsEntity> lstQuoteNo = new List<TableRecordsEntity>();
            //TableRecordsEntity objTbl = new TableRecordsEntity();
            //objTbl.ListType = GeneralConstants.ListTypeD;
            //objTbl.TableName = "QuotationRegister";
            //objTbl.DataParamField1 = "QuoteType";
            //objTbl.DataParamValueField1 = quotetypeId;
            //objTbl.DataStringColumnField1 = "VENDORID";
            //objTbl.DataStringColumnField2 = "VENDORNAME";
            //objTbl.DataParamValueField1 = quotetypeId;
            //lstQuoteNo = model.GetTableDataList(objTbl);

            //objTbl.DataStringColumnField1 = "";
            //objTbl.DataStringColumnField2 = "Select";
            //lstQuoteNo.Insert(0, objTbl);

            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            lstQuoteNo = model.GetMasterTableStringList("QuotationRegister", "CustomerId", "CustomerName", "", "", GeneralConstants.ListTypeD);

            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetPrepProductNames(string productId, string casingSize)
        {
            List<ProductEntity> lstProducts = new List<ProductEntity>();
            lstProducts = objManager.GetPrepProductNames(productId, casingSize);
            return new JsonResult { Data = lstProducts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchOrdersList(string pageIndex, string pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null)
        {
            OrderEntityDetails orderEntity = new OrderEntityDetails();
            SearchQuoteType = SearchQuoteType == "undefined" ? string.Empty : SearchQuoteType;
            SearchVendorID = SearchVendorID == "undefined" ? string.Empty : SearchVendorID;
            SearchProductGroup = SearchProductGroup == "undefined" ? string.Empty : SearchProductGroup;
            SearchDeliveryTerms = SearchDeliveryTerms == "undefined" ? string.Empty : SearchDeliveryTerms;

            orderEntity = objManager.GetOrderDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms);
            return new JsonResult { Data = orderEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult PartialOrders()
        {
            return PartialView(techDetail);
        }

        public ActionResult PartialOrderViewOnly()
        {
            return PartialView(techDetail);
        }

        [HttpPost]
        public ActionResult OrderPopup(string actionType, string Id)
        {
            model = new BaseModel();
            ViewBag.ListQuoteNo = model.GetMasterTableStringList("QuotationRegister", "Id", "QUOTENO", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListItemNo = DdlList();
            ViewBag.ListCustomerId = model.GetMasterTableStringList("QuotationRegister", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteQtyType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SingleMultiple", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurr = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListOrderType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList("Orders", "SoNo", "SoNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSoNo.RemoveAt(0);
            DropDownEntity obj = new DropDownEntity();
            obj.DataStringValueField = "";
            obj.DataTextField = "New";
            ViewBag.ListSoNo.Insert(0, obj);

            OrderEntity orderE = new OrderEntity();
            orderE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                List<DropDownEntity> DT = new List<DropDownEntity>();
                DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportorderE", "Property", GeneralConstants.ListTypeN);
                List<DropDownEntity> DT1 = new List<DropDownEntity>();
                DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticorderE", "Property", GeneralConstants.ListTypeN);

                foreach (var item in DT1)
                {
                    if (!item.DataTextField.Equals("All"))
                        DT.Add(item);
                }
                ViewBag.ListDeliveryTerms = DT;

                if (!string.IsNullOrEmpty(Id))
                    orderE.Id = Convert.ToInt32(Id);

                orderE = objManager.OrderDetailsPopup(orderE);

                ViewBag.ListQuoteNo = objManager.GetQuoteNoList(orderE.QuoteType);
                ViewBag.ListEnqNo = "";

            }
            if (actionType == "ADD")
            {
                List<DropDownEntity> SelectList = new List<DropDownEntity>();
                DropDownEntity objSelect = new DropDownEntity();
                objSelect.DataStringValueField = "";
                objSelect.DataTextField = "Select";
                SelectList.Add(objSelect);

                ViewBag.ListQuoteNo = ViewBag.ListQuoteSlNo = SelectList;

            }

            return base.PartialView("~/Views/Technical/_TechOrderDetails.cshtml", orderE);
        }

        [HttpPost]
        public ActionResult SaveOrderDetails(OrderEntity orderE)
        {

            model = new BaseModel();
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            orderE.UserInitial = Session["UserName"].ToString();
            orderE.ipAddress = ERPContext.UserContext.IpAddress;
            orderE.UnitNo = Session["UserId"].ToString();

            string data = string.Empty;

            try
            {
                string result = objManager.SaveOrderDetail(orderE);

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

            TempData["VendorName"] = orderE.CustomerName;
            TempData["UserName"] = orderE.UserInitial;

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetOrderQuoteDetails(string quoteType, string quoteNoId)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            DropDownEntity objSelect = new DropDownEntity();
            objSelect.DataStringValueField = "";
            objSelect.DataAltValueField = "Select";
            lstQuoteNo = objManager.GetOrderQuoteDetails(quoteType, quoteNoId);
            lstQuoteNo.Insert(0, objSelect);
            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetItemQuoteDetails(string itemNo)
        {

            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("QuotePreparationTbl", "ViewProductDetails", "Id", "Id", itemNo, "Qty", "", "", "UnitPrice");

            return new JsonResult { Data = objSingle, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult ItemPopup(string actionType, string Id = null)
        {
            model = new BaseModel();
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurr = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListOrderType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList("Orders", "SoNo", "SONO", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteNo = DdlList();
            ViewBag.ListItems = model.GetMasterTableStringList("QuotePreparationTbl", "Id", "ItemNo", "", "", GeneralConstants.ListTypeD);


            ItemEntity itemE = new ItemEntity();
            List<ItemEntity> ItemList = new List<ItemEntity>();
            ItemList.Add(itemE);
            ViewBag.LotTable = ItemList;

            itemE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                List<DropDownEntity> DT = new List<DropDownEntity>();
                DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportorderE", "Property", GeneralConstants.ListTypeN);
                List<DropDownEntity> DT1 = new List<DropDownEntity>();
                DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticorderE", "Property", GeneralConstants.ListTypeN);

                foreach (var item in DT1)
                {
                    if (!item.DataTextField.Equals("All"))
                        DT.Add(item);
                }
                ViewBag.ListDeliveryTerms = DT;

                if (!string.IsNullOrEmpty(Id))
                    itemE.Id = Convert.ToInt32(Id);

                ViewBag.ListEnqNo = "";

            }
            if (actionType == "ADD")
            {

            }

            return base.PartialView("~/Views/Technical/_TechItemDetails.cshtml", itemE);
        }

        public ActionResult GetOrderDetailsForQuote(string quoteType, string quoteNoId)
        {
            OrderEntity oEntity = new OrderEntity();
            oEntity = objManager.GetQuoteOrderDetails(quoteType, quoteNoId);
            List<DropDownEntity> soNo = new List<DropDownEntity>();
            soNo = model.GetDropDownList("Orders", GeneralConstants.ListTypeD, "SoNo", "SoNo", quoteType, "QuoteType", "", "", quoteNoId, "QuoteNo");
            var result = new { oEntity, soNo };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveItemDetails(ItemEntity itemE)
        {
            model = new BaseModel();
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            itemE.ipAddress = ERPContext.UserContext.IpAddress;
            itemE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveItemDetail(itemE);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + ". Reason " + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveItemDetailList(ItemEntity[] ItemDetails)
        {
            model = new BaseModel();
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            try
            {
                if (ItemDetails != null)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();

                    List<ItemEntity> itemList = new List<ItemEntity>();
                    List<ItemEntityBulkSave> itemListBulk = new List<ItemEntityBulkSave>();
                    itemList = ItemDetails.OfType<ItemEntity>().ToList();

                    foreach (var item in itemList)
                    {
                        ItemEntityBulkSave newObj = new ItemEntityBulkSave();

                        newObj.Id = item.Id;
                        newObj.UnitNo = Session["UserId"].ToString();
                        newObj.VendorId = item.CustomerId;
                        newObj.VendorName = item.CustomerName;
                        newObj.QuoteNo = item.QuoteNo;
                        newObj.QuoteType = item.QuoteType;
                        newObj.SoNo = item.SoNo;
                        newObj.QuotePrepId = item.QuotePrepId;
                        newObj.PoSLNo = item.PoSLNo;
                        newObj.PoQty = item.PoQty;
                        newObj.UnitPrice = item.UnitPrice;
                        newObj.LotName = item.LotName;
                        newObj.LotWiseQty = item.LotWiseQty;
                        newObj.LotWiseDate = item.LotWiseDate;
                        newObj.PoRequirements = item.PoRequirements;
                        newObj.ipAddress = item.ipAddress;
                        newObj.IsActive = item.IsActive;

                        itemListBulk.Add(newObj);
                    }


                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);

                    string result = objManager.SaveItemDetailList(objBU);

                    string data = string.Empty;
                    if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                    {
                        //Payment Gateway
                        data = GeneralConstants.SavedSuccess;
                    }
                    else
                    {
                        data = GeneralConstants.NotSavedError + " Reason: " + result;
                    }

                    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                }
                return Json("Unable to save Item Details! Please Provice correct information", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Item Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckDuplicateItemNo(int itemNoId, int quoteType, int quoteNo)
        {
            QuotationPreparationEntity qPEntity = new QuotationPreparationEntity();
            qPEntity = objManager.GetQuotePrepDetails(itemNoId, quoteType, quoteNo);
            return new JsonResult { Data = qPEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //List<DropDownEntity> ddl = new List<DropDownEntity>();
            //ddl = model.GetMasterTableList("QuotePreparationTbl", "Id", "ItemNo", itemNoId, "ItemNo");
            //if (ddl.Count > 1)
            //    return new JsonResult { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //else
            //    return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult RevisedQuotationPopup(string actionType, string QuotationId)
        {

            model = new BaseModel();
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");
            ViewBag.ListCountry = model.GetMasterTableList("Master.Country", "Id", "Country");
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);

            quotE.UnitNo = Session["UserId"].ToString();
            quotE.UserInitial = Session["UserName"].ToString();

            if (actionType == "ADD")
            {
                List<DropDownEntity> SelectList = new List<DropDownEntity>();
                DropDownEntity objSelect = new DropDownEntity();
                objSelect.DataStringValueField = "";
                objSelect.DataTextField = "Select";
                SelectList.Add(objSelect);

                ViewBag.ListQuoteNo = SelectList;

            }

            return base.PartialView("~/Views/Technical/_TechRevisedQuotationDetails.cshtml", quotE);
        }

        [HttpPost]
        public ActionResult SaveRevisedQuotationDetails(QuotationEntity cusE)
        {
            model = new BaseModel();
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveRevisedTechnicalQuoteDetails(cusE);

            TempData["VendorName"] = cusE.CustomerName;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetQuoteNumbers(string quotetypeId)
        {
            QuoteTypeDetails qDetails = new QuoteTypeDetails();
            qDetails.lstQuoteNos = model.GetMasterTableStringList("QuotationRegister", "QuoteNo", "QuoteNo", quotetypeId, "QuoteType", GeneralConstants.ListTypeN);

            qDetails.lstQuoteNos.RemoveAt(0);
            qDetails.lstVendors = objManager.GetVendorDetails(quotetypeId);
            return new JsonResult { Data = qDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetRevisedQuoteNo(string quoteTypeId, string quoteNumberId)
        {
            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("QuotationRegister", "RevisedQuoteNo", "RevisedQuoteNo", "QuoteType", quoteTypeId, "QuoteDate", "","", "QuoteNo", quoteNumberId, "", "QuoteValidity");

            return new JsonResult { Data = objSingle, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult ClarificationFilesUpload(string quoteType, string quoteNo)
        {
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
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

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

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Documents/MailUploads/"), fname);
                        file.SaveAs(fname);

                        string result = string.Empty;
                        ClarificationEntity cObj = new ClarificationEntity();
                        cObj.QuoteNo = quoteNo;
                        cObj.QuoteType = quoteType;
                        cObj.MailId = strB.ToString();
                        cObj.MailId = cObj.MailId.Remove(cObj.MailId.Length - 1, 1);
                        result = objManager.SaveClarificationData(cObj);

                        if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted))
                        {
                            return Json("File Uploaded Successfully!");
                        }
                        else
                        {
                            if (System.IO.File.Exists(fname))
                                System.IO.File.Delete(fname);

                            return Json("Cannot Save Data!");
                        }
                    }
                    return Json("Cannot Save Data!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        [HttpPost]
        public ActionResult ClarificationOrderFileUpload(string SoNo)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    StringBuilder strB = new StringBuilder();
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    string result = string.Empty;
                    string fname = string.Empty;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];


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

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Documents/OrderUpload/"), fname);
                        file.SaveAs(fname);

                        fname = string.Empty;
                    }

                    ClarificationEntity cObj = new ClarificationEntity();
                    cObj.SoNo = SoNo;
                    cObj.OrderDocName = strB.ToString();
                    cObj.OrderDocName = cObj.OrderDocName.Remove(cObj.OrderDocName.Length - 1, 1);

                    result = objManager.SaveOrderClarificationData(cObj);

                    if (string.IsNullOrEmpty(result) && (result != GeneralConstants.Inserted))
                    {
                        if (System.IO.File.Exists(fname))
                            System.IO.File.Delete(fname);

                        return Json("Cannot Save Data!");
                    }
                    else
                    {
                        return Json("File Uploaded Successfully!");
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult PartialClarification()
        {
            ClarificationEntity cEntity = new ClarificationEntity();
            ViewBag.ListQuoteNo = DdlList(); //objManager.GetQuoteNoList(string.Empty);             
            ViewBag.ListVendorName = model.GetMasterTableStringList("QuotationRegister", "CustomerId", "CustomerName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList("Orders", "SoNo", "SoNo", "", "", GeneralConstants.ListTypeD);
            //qPEntity = new QuotationPreparationEntity();
            return PartialView("~/Views/Technical/_TechClarification.cshtml", cEntity);
        }

        public JsonResult GetClarificationMails(string quoteNo, string quoteType)
        {
            List<TableRecordsEntity> NewStr = new List<TableRecordsEntity>();
            NewStr = model.GetTableDataList(GeneralConstants.ListTypeD, "QuoteClarification", "QuoteNo", quoteNo, "QuoteType", quoteType, "", "", "", "", "", "", "Id", "MailId");
            return Json(NewStr);
        }

        //public JsonResult GetClarificationNotes(string quoteNo, string quoteType)
        //{
        //    List<TableRecordsEntity> NewStr = new List<TableRecordsEntity>();
        //    NewStr = model.GetTableRecordsList(GeneralConstants.ListTypeN, "Notes", "QuoteNo", quoteNo, "NoteMsg", "NoteMsg", "QuoteType", quoteType);
        //    return Json(NewStr);
        //}

        public ActionResult GetClarificationNotes(string quoteNo, string quoteType)
        {
            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("Notes", "NoteMsg", "NoteMsg", "QuoteType", quoteType, "", "", "QuoteNo", quoteNo);

            return new JsonResult { Data = objSingle.DataValueField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult SaveQuoteNotes(string quoteNo, string quoteType, string Notes)
        {
            ClarificationEntity objClar = new ClarificationEntity();
            objClar.QuoteNo = quoteNo;
            objClar.QuoteType = quoteType;
            objClar.Notes = Notes;

            string result = objManager.SaveQuoteNotes(objClar);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetVendorDetailsForQuote(string quoteNo, string quoteType)
        {
            //List<DropDownEntity> newList = new List<DropDownEntity>();
            //newList = model.GetMasterTableStringList("QuotationRegister", "Id", "VendorName", quoteNo, "QuoteNo", GeneralConstants.ListTypeN);

            //return new JsonResult { Data = newList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("QuotationRegister", "CustomerName", "CustomerName", "QuoteType", quoteType, "Currency", "QuoteNo", quoteNo);

            return new JsonResult { Data = objSingle, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteClarificationMails(List<int> Id)
        {
            StringBuilder strBuild = new StringBuilder();
            foreach (int i in Id)
                strBuild = strBuild.Append(i.ToString() + ",");

            string finalStr = strBuild.ToString().Remove(strBuild.ToString().Length - 1, 1);

            string msgCode = objManager.DeleteClarificationMails(finalStr);
            return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetQuoteItemPODetails(string quoteType, string quoteNo)
        {
            SingleColumnEntity singleCol = new SingleColumnEntity();
            singleCol = model.GetSingleColumnValues("QuotePreparationTbl", "UnitPrice", "UnitPrice", "QuoteType", quoteType, "Qty", "QuoteNo", quoteNo);
            return new JsonResult { Data = singleCol, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetSONoForOrder()
        {
            List<DropDownEntity> SONoList = new List<DropDownEntity>();
            SONoList = model.GetMasterTableStringList("Orders", "ID", "SoNo", "", "", GeneralConstants.ListTypeN);
            DropDownEntity obj = new DropDownEntity();
            obj.DataStringValueField = "";
            obj.DataTextField = "New";
            SONoList.RemoveAt(0);
            SONoList.Insert(0, obj);
            return new JsonResult { Data = SONoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetEnquiryDetailsForQuote(string enquiryId)
        {
            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues("EnquiryRegister", "EnqDt", "EnqDt", "EnquiryId", enquiryId, "EnqFor");

            return new JsonResult { Data = objSingle, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetSoNoDetails(string soNo)
        {
            OrderEntity objItem = new OrderEntity();
            objItem = objManager.GetSoNoDetails(soNo);
            return new JsonResult { Data = objItem, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetSONos(string quotetypeId)
        {
            List<DropDownEntity> lstSoNos = new List<DropDownEntity>();

            lstSoNos = model.GetMasterTableStringList("Orders", "SoNo", "SONo", quotetypeId, "QuoteType", GeneralConstants.ListTypeD);
            return new JsonResult { Data = lstSoNos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult GetQuoteItemSlNos(string quoteType, string quoteNo)
        {
            List<DropDownEntity> QuoteItemSlNoList = new List<DropDownEntity>();
            QuoteItemSlNoList = model.GetDropDownList("QuotePreparationTbl", GeneralConstants.ListTypeD, "Id", "ItemNo", quoteNo, "QuoteNo", "", "", quoteType, "QuoteType");

            return new JsonResult { Data = QuoteItemSlNoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetQuoteNoForSO(string SoNoId, string quoteTypeId)
        {
            List<DropDownEntity> QuoteNoList = new List<DropDownEntity>();
            QuoteNoList = objManager.GetQuoteNoList(quoteTypeId, SoNoId);

            return new JsonResult { Data = QuoteNoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetOrderDetailsFromSO(string SoNo, string quoteTypeId)
        {
            ItemEntity iEntity = new ItemEntity();
            iEntity = objManager.GetOrderDetailsFromSO(Convert.ToInt32(SoNo));
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            List<DropDownEntity> lstQuoteItemSlNo = new List<DropDownEntity>();
            lstQuoteNo = objManager.GetQuoteNoList(quoteTypeId, SoNo);
            lstQuoteItemSlNo = objManager.GetQuoteItemSlNoList(quoteTypeId, SoNo);
            var result = new { iEntity, lstQuoteNo, lstQuoteItemSlNo };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetSoNoForClarification(string quotetypeId, string quoteNoId)
        {
            List<DropDownEntity> lstSoNos = new List<DropDownEntity>();

            lstSoNos = model.GetDropDownList("Orders", GeneralConstants.ListTypeD, "SoNo", "SoNo", "QuoteType", quotetypeId, "", "", "QuoteNo", quoteNoId);
            return new JsonResult { Data = lstSoNos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetOrderClarifications(string soNo)
        {
            List<TableRecordsEntity> OrderClarList = new List<TableRecordsEntity>();
            OrderClarList = model.GetTableDataList(GeneralConstants.ListTypeD, "OrderClarification", "SoNo", soNo, "", "", "", "", "", "", "", "", "Id", "OrderDocName");
            return Json(OrderClarList);
        }

        public ActionResult DeleteOrderClarifications(List<int> Id)

        {
            StringBuilder strBuild = new StringBuilder();
            foreach (int i in Id)
                strBuild = strBuild.Append(i.ToString() + ",");

            string finalStr = strBuild.ToString().Remove(strBuild.ToString().Length - 1, 1);

            string msgCode = objManager.DeleteOrderClarifications(finalStr);
            return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //DeleteEntity dEn = new DeleteEntity();
            //string msgCode = string.Empty;
            //StringBuilder strBuild = new StringBuilder();

            //dEn.TableName = "OrderClarification";
            //dEn.ColumnName1 = "Id";
            //foreach (int i in Id)
            //{
            //    dEn.Param1 = Convert.ToString(i);
            //    msgCode = model.DeleteFormTable(dEn);

            //}

            //return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        #region Enquiry
        public JsonResult FetchEnquiryList(string pageIndex, string pageSize, string SearchEQEnqType, string SearchCustomerName = null, string SearchEnqFor = null, string SearchEQDueDate = null, string SearchEOQ = null)
        {

            SearchEQEnqType = SearchEQEnqType == "-1" ? string.Empty : SearchEQEnqType;
            SearchCustomerName = SearchCustomerName == "-1" ? string.Empty : SearchCustomerName;
            SearchEnqFor = SearchEnqFor == "-1" ? string.Empty : SearchEnqFor;
            SearchEQDueDate = SearchEQDueDate == "-1" ? string.Empty : SearchEQDueDate;
            SearchEOQ = SearchEOQ == "-1" ? string.Empty : SearchEOQ;

            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq = objEnquiryManager.GetEnquiryDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchEQEnqType, SearchCustomerName, SearchEnqFor, SearchEQDueDate, SearchEOQ);
            return new JsonResult { Data = enq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpGet]
        public ActionResult PartialEnquiry()
        {
            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq.enqEntity.UnitNo = Session["UserId"].ToString();
            enq.enqEntity = objEnquiryManager.GetUserDetailsForEnquiry(enq.enqEntity.UnitNo);

            return View(enq);
        }

        [HttpGet]
        public ActionResult EnquiryView()
        {
            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq.enqEntity.UnitNo = Session["UserId"].ToString();
            enq.enqEntity = objEnquiryManager.GetUserDetailsForEnquiry(enq.enqEntity.UnitNo);

            return View(enq);
        }

        public ActionResult EnquiryPopup(string actionType, string enquiryId)
        {
            ViewBag.ListQuoteType = this.model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEOQ = model.GetTaxonomyDropDownItems("", "Expression of Quote(EOQ)");
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.YesNo = model.GetTaxonomyDropDownItems("", "YesNo");
            ViewBag.ListEnqThru = model.GetTaxonomyDropDownItems("", "Enquiry Through");
            ViewBag.ListEnqType = model.GetTaxonomyDropDownItems("", "Domestic/International");
            ViewBag.ListCountry = model.GetMasterTableList("Master.Country", "Id", "Country");
            ViewBag.ListMainProdGrp = this.model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = this.model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = this.model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListEnqMode = model.GetTaxonomyDropDownItems("", "ContactType");

            EnquiryEntity eModel = new EnquiryEntity();
            eModel.UnitNo = Session["UserId"].ToString();
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", eModel.UnitNo, "UnitNo", GeneralConstants.ListTypeN);

            EnquiryManager objEnqManager = new EnquiryManager();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(enquiryId))
                    eModel.EnquiryId = Convert.ToInt32(enquiryId);
                eModel = objEnqManager.EnquiryDetailsPopup(eModel);
            }
            if (actionType == "ADD")
            {
                eModel = objEnqManager.GetUserDetailsForEnquiry(eModel.UnitNo);
                //eModel = objManager.AddEnquiryDetailsPopup(eModel);
            }

            return PartialView("~/Views/Technical/_EnquiryDetails.cshtml", eModel);
        }

        [HttpPost]
        public ActionResult SaveEnquiryDetails(EnquiryEntity cusE)
        {
            string result = objEnquiryManager.SaveEnquiryDetails(cusE);

            TempData["CustomerName"] = cusE.CustomerId;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                //TempData["StatusMsg"] = "Success";
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                //TempData["StatusMsg"] = "Error";
                TempData["StatusMsgBody"] = result;
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteEnquiryDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = model.DeleteFormTable("EnquiryRegister", "EnquiryId", id.ToString());
                if (msgCode == GeneralConstants.DeleteSuccess)
                {
                    return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //Response.StatusCode = 444;
                    //Response.StatusDescription = "Not Saved";
                    //return null;
                }

                //string msgCode = objEnquiryManager.DeleteEnquiryDetail(id);
                //if (msgCode == "")
                //{
                //    //return RedirectToAction("Customer");
                //    return new JsonResult { Data = GeneralConstants.DeleteSuccess, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                //}
                //else
                //{
                //    return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                //    //Response.StatusCode = 444;
                //    //Response.StatusDescription = "Not Saved";
                //    //return null;
                //}
            }
            else
            {
                Response.StatusCode = 444;
                Response.Status = "Not Saved";
                return null;
            }

        }

        [HttpPost]
        public ActionResult GetVendorDetailForEnquiry(string CustomerId)
        {
            EnquiryEntity eModel = new EnquiryEntity();
            eModel = objEnquiryManager.GetVendorDetailForEnquiry(CustomerId);
            return new JsonResult { Data = eModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetDdlValueForEnquiry(string type, string EnqType = null, string CustomerId = null, string EnqFor = null, string DueDate = null)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();
            //ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EOQId, ProductGroup, VendorId);

            switch(type)
            {
                case "Customer" :
                    ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EnqType);
                    break;
                case "EnqFor":
                    ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EnqType, CustomerId);
                    break;
                case "DueDate":
                    ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EnqType, CustomerId, EnqFor);
                    break;
                case "EOQ":
                    ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EnqType, CustomerId, EnqFor, DueDate);
                    break;
                default:
                    ddl = objEnquiryManager.GetDdlValueForEnquiry(type, EnqType);
                    break;
            }
            

            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion


        #region Quotation

        [HttpGet]
        public ActionResult PartialQuotation()
        {
            QuotationEntity quote = new QuotationEntity();
            quote.UnitNo = Session["UserId"].ToString();
            quote = objQuoteManager.GetUserQuoteDetails(quote.UnitNo);

            return View(quote);
        }


        public JsonResult FetchQuotationList(string pageIndex, string pageSize, string SearchQuoteType, string SearchQuoteCustomerID, string SearchSubject, string SearchDeliveryTerms)
        {
            SearchQuoteType = SearchQuoteType == "-1" ? string.Empty : SearchQuoteType;
            SearchQuoteCustomerID = SearchQuoteCustomerID == "-1" ? string.Empty : SearchQuoteCustomerID;
            SearchSubject = SearchSubject == "-1" ? string.Empty : SearchSubject;
            SearchDeliveryTerms = SearchDeliveryTerms == "-1" ? string.Empty : SearchDeliveryTerms;

            QuotationEntityDetails quote = new QuotationEntityDetails();
            quote = objQuoteManager.GetQuotationDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchQuoteCustomerID, SearchSubject, SearchDeliveryTerms);
            return new JsonResult { Data = quote, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return QuotDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult DeleteQuotationDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = model.DeleteFormTable("QuotationRegister", "Id", id.ToString()); //objQuoteManager.DeleteQuotationDetail(id);
                if (msgCode == GeneralConstants.DeleteSuccess)
                {
                    return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //Response.StatusCode = 444;
                    //Response.StatusDescription = "Not Saved";
                    //return null;
                }
            }
            else
            {
                Response.StatusCode = 444;
                Response.Status = "Not Saved";
                return null;
            }

        }


        public ActionResult GetDdlValueForQuote(string type, string CustomerId = null, string QuoteType = null, string SubjectId = null)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();
            ddl = objQuoteManager.GetDdlValueForQuote(type, CustomerId, QuoteType, SubjectId);
            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #endregion


        [HttpPost]
        public ActionResult GetSubProdGrp(string MainProdGrpId)
        {
            var SubProdGrpList = model.GetDropDownList("SubProductLine", GeneralConstants.ListTypeD, "Id", "SubPLName", MainProdGrpId, "MainPLId");
            return new JsonResult { Data = SubProdGrpList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult GetProdName(string MainProdGrpId, string SubProdGrpId)
        {
            var ProdNameList = model.GetDropDownList("Master.Product", GeneralConstants.ListTypeD, "Id", "ProductName", MainProdGrpId, "PL", "", "", SubProdGrpId, "SubPL");
            return new JsonResult { Data = ProdNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }

}