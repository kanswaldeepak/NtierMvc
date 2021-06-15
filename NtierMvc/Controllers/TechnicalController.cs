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
using System.Text;
using System.Web;
using System.Web.Mvc;
using NtierMvc.ExcelProperty;

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
        //QuotationManager objManagerQuote;
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
            //objManagerQuote = new QuotationManager();
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
            ViewBag.ListDeliveryTerms = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "DeliveryTerms", ColumnNames.ObjectName);
            ViewBag.ListSubject = "";

            //Order
            ViewBag.ListPODeliveryDate = model.GetDateDropDownList(TableNames.Orders, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.PoDeliveryDate, "", "");

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
            ViewBag.ListQuoteNo = model.GetDropDownList(TableNames.QuotationRegister, GeneralConstants.ListTypeN, ColumnNames.QuoteNo, ColumnNames.QuoteNoView, "", "");
            ViewBag.YesNo = model.GetTaxonomyDropDownItems("", "YesNo");

            ViewBag.CasingSize = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CasingSize", ColumnNames.Property, true);
            ViewBag.CasingPpf = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "PPF", ColumnNames.Property, true);
            ViewBag.MaterialGrade = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "MatGrade", ColumnNames.Property, true);
            ViewBag.Connection = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Connection", ColumnNames.Property, true);
            ViewBag.ProductNameList = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListUom = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteUom", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListEnqNo = model.GetMasterTableStringList("EnquiryRegister", "EnquiryId", "EnqRef", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListOpenHoleSize = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "OpenHoleSize", ColumnNames.Property, true);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListBallSize = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "BallSize", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCasingWT = model.GetMasterTableStringList("CasingWeight", "Id", "CasingWT", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.ListItemNo = "";
            ViewBag.ListFinancialYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);

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
                data = GeneralConstants.NotSavedError + " .Reason:" + result;
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
            ViewBag.ListProdType = model.GetMasterTableStringList("ProductType", "Id", "TypeName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Currency", "Property");
            ViewBag.ListInspection = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Inspection", "Property");
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.ListModeOfDespatch = model.GetDropDownList("Master.Taxonomy", GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Transport", "Property");
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDeliveryTerms = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "DeliveryTerms", ColumnNames.ObjectName);
            ViewBag.ListFinYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);

            quotE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                //List<DropDownEntity> DT = new List<DropDownEntity>();
                //DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportQuote", "Property", GeneralConstants.ListTypeN);
                //List<DropDownEntity> DT1 = new List<DropDownEntity>();
                //DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticQuote", "Property", GeneralConstants.ListTypeN);

                //foreach (var item in DT1)
                //{
                //    if (!item.DataTextField.Equals("All"))
                //        DT.Add(item);
                //}


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

                ViewBag.ListEnqNo = SelectList;

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
        public ActionResult CreateDownloadDocument(string downloadTypeId, string quoteTypeId, string quoteNumberId, string quoteTypeText, string quoteNoForFileName)
        {
            //string quoteNoForFileName = "QTMOT"++"0";
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

            quoteNumberId = quoteNoForFileName;
            quoteNoForFileName = quoteNoForFileName + ".xlsx";
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]);
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), quoteNoForFileName);


            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = "";

            if (downloadTypeId == "xlsx")
                fileName = GenerateExcel(downloadTypeId, quoteTypeId, quoteNumberId, quoteNoForFileName, fullPath, fileName);
            else if (downloadTypeId == "docs")
                fileName = GenerateDoc(downloadTypeId, quoteTypeId, quoteNumberId, quoteNoForFileName, fullPath, fileName);

            //Download(fileName);
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


                System.Data.DataTable resultData = objManager.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
                System.Data.DataTable resultList = objManager.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId);

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
                decimal sum = 0;
                int NetWt = 0, GrWt = 0;
                double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count - 3; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    sum = sum + Convert.ToDecimal(dr[5]);
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

                ExcelInteropProperties exProp = new ExcelInteropProperties();
                exProp.AllBorders(range1.Borders);

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
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Sheet1"];

            try
            {
                System.Data.DataTable resultData = objManager.GetDataForDocument(downloadTypeId, quoteTypeId, quoteNumberId);
                System.Data.DataTable resultList = objManager.GetListForDocument(downloadTypeId, quoteTypeId, quoteNumberId);

                if (resultData.Rows.Count <= 0 || resultList.Rows.Count <= 0)
                {
                    fileName = "No Records Found For Selected Quote";
                    return fileName;
                }


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
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerAddress1", resultData.Rows[0]["CustomerAddress1"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerAddress2", resultData.Rows[0]["CustomerAddress2"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerEmail", resultData.Rows[0]["CustomerEmail"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Subject", resultData.Rows[0]["Subject"]);

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

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[15, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 15, resultList.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //Adding Table values in Excel
                double sum = 0;
                decimal NetWt = 0, GrWt = 0;
                double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count - 4; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    sum = sum + Convert.ToDouble(dr[6]);
                    NetWt = NetWt + Convert.ToDecimal(dr[8]);
                    GrWt = GrWt + Convert.ToDecimal(dr[9]);
                    CubMtr = CubMtr + Convert.ToDouble(dr[10]);
                }

                xlWorkbook.Worksheets[1].Cells.Replace("#TotalPrice", sum.ToString());

                xlWorkbook.Worksheets[1].Cells.Replace("#AmountInWords", resultData.Rows[0]["Currency"].ToString() == "INR" ? model.AmountToWordINR(sum) : model.AmountToWordsUSD(sum.ToString()));
                xlWorkbook.Worksheets[1].Cells.Replace("#NetWeight", NetWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#GrossWeight", GrWt.ToString());
                xlWorkbook.Worksheets[1].Cells.Replace("#CubicMeter", CubMtr.ToString());

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[15, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 15, resultList.Columns.Count];
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
                range1.Rows.AutoFit();
                range1.Value = arr;


                fileName = quoteNoForFileName;
                return fileName;
            }
            catch (Exception ex)
            {
                var response = ex.Message;
                return fileName;
            }
            finally
            {

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

            }


        }

        public ActionResult Download(string fileName, string path = "")
        {
            //Do not delete commented text
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);

            if (System.IO.File.Exists(fullPath))
            {
                ////Get the temp folder and file path in server
                byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
                System.IO.File.Delete(fullPath);
                return File(fileByteArray, "application/vnd.ms-excel", fileName);

            }

            else
                return Json(new { data = "", errorMessage = "Error While Generating Excel. Contact Support." }, JsonRequestBehavior.AllowGet);

            //string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);

            //string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);
            //var excelApp = new Microsoft.Office.Interop.Excel.Application();
            //excelApp.Visible = true;

            //if (System.IO.File.Exists(fullPath))
            //{
            //    ////Get the temp folder and file path in server
            //    Microsoft.Office.Interop.Excel.Workbooks books = excelApp.Workbooks;
            //    Microsoft.Office.Interop.Excel.Workbook sheet = books.Open(fullPath, 0, true, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlNormalLoad);
            //    System.IO.File.Delete(fullPath);
            //    //return Json(new { data = "", errorMessage = "" }, JsonRequestBehavior.AllowGet);
            //}
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

        public ActionResult GetDeliveryItems(string quotetypeId, string finYear = null)
        {
            QuoteTypeDetails qDetails = new QuoteTypeDetails();
            qDetails.lstQuoteTypeEntity = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", quotetypeId, "TaxonomyID", GeneralConstants.ListTypeN);
            qDetails.QuoteNo = objManager.GetQuoteNo(quotetypeId, finYear);
            qDetails.lstVendors = objManager.GetVendorDetails(quotetypeId);

            return new JsonResult { Data = qDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetPrepQuoteNo(string quotetypeId, string financialYr = null)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            //lstQuoteNo = objManager.GetQuoteNoList(quotetypeId, financialYr);
            if (string.IsNullOrEmpty(financialYr))
                lstQuoteNo = model.GetDropDownList(TableNames.QuotationRegister, GeneralConstants.ListTypeD, ColumnNames.QuoteNo, ColumnNames.QuoteNoView, quotetypeId, ColumnNames.QuoteType);
            else
                lstQuoteNo = model.GetDropDownList(TableNames.QuotationRegister, GeneralConstants.ListTypeD, ColumnNames.QuoteNo, ColumnNames.QuoteNoView, financialYr, ColumnNames.FinancialYear, false, "", "", quotetypeId, ColumnNames.QuoteType);
            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult GetSoNoList(string quotetypeId)
        {

            List<DropDownEntity> SoNoList = new List<DropDownEntity>();

            SoNoList = model.GetMasterTableStringList(TableNames.Orders, ColumnNames.SoNo, ColumnNames.SoNoView, "", "", GeneralConstants.ListTypeD);
            if (SoNoList.Count > 0)
                SoNoList.RemoveAt(0);
            return new JsonResult { Data = SoNoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public ActionResult GetOrderQuoteNo(string quotetypeId)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            List<DropDownEntity> lstSoNo = new List<DropDownEntity>();
            DropDownEntity objSelect = new DropDownEntity();
            objSelect.DataStringValueField = "";
            objSelect.DataTextField = "Select";
            lstQuoteNo = objManager.GetQuoteNoList(quotetypeId); // For Item PopUp Quote No details
            lstSoNo = model.GetMasterTableStringList(TableNames.Orders, ColumnNames.SoNo, ColumnNames.SoNoView, quotetypeId, "QuoteType", GeneralConstants.ListTypeD);
            lstQuoteNo.Insert(0, objSelect);
            var result = new { lstQuoteNo, lstSoNo };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult GetProductTypes()
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
            lstQuoteNo = model.GetMasterTableStringList(TableNames.QuotationRegister, ColumnNames.CustomerId, ColumnNames.CustomerName, "", "", GeneralConstants.ListTypeD);

            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult GetPrepProductNames(string productId, string casingSize, string type)
        {
            List<ProductEntity> lstProducts = new List<ProductEntity>();
            lstProducts = objManager.GetPrepProductNames(productId, casingSize, type);
            return new JsonResult { Data = lstProducts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult CreatePOReport(string ReportType, string pageIndex, string pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]);
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), ReportType);


            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fileName = "";

            ReportManager mgr = new ReportManager();
            fileName = mgr.PrepProductAuhtReport(fullPath, ReportType, pageIndex, pageSize, SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms, SearchPODeliveryDate);
            return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult FetchOrdersList(string pageIndex, string pageSize, string SearchQuoteType = null, string SearchCustomerID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            OrderEntityDetails orderEntity = new OrderEntityDetails();
            SearchQuoteType = SearchQuoteType == "undefined" ? string.Empty : SearchQuoteType;
            SearchCustomerID = SearchCustomerID == "undefined" ? string.Empty : SearchCustomerID;
            SearchProductGroup = SearchProductGroup == "undefined" ? string.Empty : SearchProductGroup;
            SearchDeliveryTerms = SearchDeliveryTerms == "undefined" ? string.Empty : SearchDeliveryTerms;
            SearchPODeliveryDate = SearchPODeliveryDate == "undefined" ? string.Empty : SearchPODeliveryDate;

            orderEntity = objManager.GetOrderDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchCustomerID, SearchProductGroup, SearchDeliveryTerms, SearchPODeliveryDate);
            return new JsonResult { Data = orderEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult PartialOrders()
        {
            return PartialView(techDetail);
        }
        public ActionResult PartialReportView()
        {
            return PartialView();
        }

        public ActionResult PartialOrderViewOnly()
        {
            return PartialView(techDetail);
        }

        [HttpPost]
        public ActionResult OrderPopup(string actionType, string Id)
        {
            model = new BaseModel();
            ViewBag.ListQuoteNo = model.GetMasterTableStringList(TableNames.QuotationRegister, ColumnNames.id, ColumnNames.QuoteNo, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListItemNo = DdlList();

            ViewBag.ListCustomerId = model.GetMasterTableStringList(TableNames.Customer, ColumnNames.id, ColumnNames.CustomerID, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListQuoteQtyType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "SingleMultiple", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListCurr = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Currency", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListOrderType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "SupplyTerms", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList(TableNames.Master_ProductLine, ColumnNames.id, ColumnNames.MainPLName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList(TableNames.SubProductLine, ColumnNames.id, ColumnNames.SubPLName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList(TableNames.Master_Product, ColumnNames.id, ColumnNames.ProductName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList(TableNames.Orders, ColumnNames.SoNo, ColumnNames.SoNoView, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSoNo.RemoveAt(0);
            ViewBag.ListModeOfDespatch = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Transport", ColumnNames.Property);
            ViewBag.ListFinancialYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);

            DropDownEntity obj = new DropDownEntity();

            obj.DataStringValueField = "";
            obj.DataTextField = "New";
            ViewBag.ListSoNo.Insert(0, obj);

            OrderEntity orderE = new OrderEntity();
            orderE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                //List<DropDownEntity> DT = new List<DropDownEntity>();
                //DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportorderE", "Property", GeneralConstants.ListTypeN);
                //List<DropDownEntity> DT1 = new List<DropDownEntity>();
                //DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticorderE", "Property", GeneralConstants.ListTypeN);

                //foreach (var item in DT1)
                //{
                //    if (!item.DataTextField.Equals("All"))
                //        DT.Add(item);
                //}
                ViewBag.ListDeliveryTerms = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "DeliveryTerms", ColumnNames.ObjectName);

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
            ViewBag.ListCustomerId = model.GetMasterTableStringList(TableNames.Customer, ColumnNames.id, ColumnNames.CustomerId, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = ViewBag.ListOrderType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListCurr = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Currency", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "SupplyTerms", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList(TableNames.Orders, ColumnNames.SoNo, ColumnNames.SoNoView, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteNo = model.GetMasterTableStringList(TableNames.QuotationRegister, ColumnNames.id, ColumnNames.QuoteNo, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListItems = model.GetMasterTableStringList(TableNames.QuotePreparationTbl, ColumnNames.id, ColumnNames.ItemNo, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListFinancialYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);


            ItemEntity itemE = new ItemEntity();
            List<ItemEntity> ItemList = new List<ItemEntity>();
            ItemList.Add(itemE);
            ViewBag.LotTable = ItemList;

            itemE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                //List<DropDownEntity> DT = new List<DropDownEntity>();
                //DT = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "ExportorderE", "Property", GeneralConstants.ListTypeN);
                //List<DropDownEntity> DT1 = new List<DropDownEntity>();
                //DT1 = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "DomesticorderE", "Property", GeneralConstants.ListTypeN);

                //foreach (var item in DT1)
                //{
                //    if (!item.DataTextField.Equals("All"))
                //        DT.Add(item);
                //}
                ViewBag.ListDeliveryTerms = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "DeliveryTerms", ColumnNames.ObjectName);

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
            soNo = model.GetDropDownList("Orders", GeneralConstants.ListTypeD, "SoNo", "SoNo", quoteType, "QuoteType", false, "", "", quoteNoId, "QuoteNo");
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
                    objBU.DataRecordTable = new System.Data.DataTable();

                    List<ItemEntity> itemList = new List<ItemEntity>();
                    List<ItemEntityBulkSave> itemListBulk = new List<ItemEntityBulkSave>();
                    itemList = ItemDetails.OfType<ItemEntity>().ToList();

                    foreach (var item in itemList)
                    {
                        ItemEntityBulkSave newObj = new ItemEntityBulkSave();

                        newObj.Id = item.Id;
                        newObj.UnitNo = Session["UserId"].ToString();
                        newObj.CustomerId = item.CustomerId;
                        newObj.CustomerName = item.CustomerName;
                        newObj.FinancialYear = item.FinancialYear;
                        newObj.QuoteNo = item.QuoteNo;
                        newObj.QuoteNoView = item.QuoteNoView;
                        newObj.QuoteType = item.QuoteType;
                        newObj.SoNo = item.SoNo;
                        newObj.SoNoView = item.SoNoView;
                        newObj.QuoteItemSlNo = item.QuoteItemSlNo;
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

        public ActionResult CheckDuplicateItemNo(int itemNoId, int quoteType, int quoteNo, int financialYear)
        {
            QuotationPreparationEntity qPEntity = new QuotationPreparationEntity();
            qPEntity = objManager.GetQuotePrepDetails(itemNoId, quoteType, quoteNo, 0, financialYear);
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
            ViewBag.ListCustomerId = model.GetMasterTableStringList(TableNames.Customer, ColumnNames.id, ColumnNames.CustomerId, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVENDORTYPE = model.GetMasterTableList(TableNames.Master_Vendor, ColumnNames.id, ColumnNames.VendorType);
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList(TableNames.Master_Vendor, ColumnNames.id, ColumnNames.VendorNature);
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList(TableNames.Master_FunctionalArea, ColumnNames.id, ColumnNames.FunctionArea);
            ViewBag.ListCountry = model.GetMasterTableList(TableNames.Master_Country, ColumnNames.id, ColumnNames.Country);
            ViewBag.ListProdType = model.GetMasterTableStringList(TableNames.ProductType, ColumnNames.id, ColumnNames.TypeName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, "dropdownId", "dropdownvalue", ColumnNames.QuoteType, ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Currency", ColumnNames.Property);
            ViewBag.ListInspection = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Inspection", ColumnNames.Property);
            ViewBag.ListLeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            ViewBag.ListModeOfDespatch = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, "dropdownId", "dropdownvalue", "Transport", ColumnNames.Property);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList(TableNames.Master_Taxonomy, "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDeliveryTerms = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "DeliveryTerms", ColumnNames.ObjectName);
            ViewBag.ListFinYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);
            ViewBag.ListQuoteNo = "";
            ViewBag.ListRevisedQuoteNo = model.GetDropDownList(TableNames.RevisedNoTbl, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.RevNo, "", "", true);

            //model = new BaseModel();
            //string countryId = "0";
            //ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value

            //ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            //ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            //ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            //ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");
            //ViewBag.ListCountry = model.GetMasterTableList("Master.Country", "Id", "Country");
            //ViewBag.ListMainProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "MainPLName", "", "", GeneralConstants.ListTypeN);
            //ViewBag.ListSubProdGrp = model.GetMasterTableStringList("SubProductLine", "Id", "SubPLName", "", "", GeneralConstants.ListTypeN);
            //ViewBag.ListProdName = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeN);
            //ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            //ViewBag.ListFinancialYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);

            quotE.UnitNo = Session["UserId"].ToString();
            quotE.UserInitial = Session["UserName"].ToString();
            List<DropDownEntity> SelectList = new List<DropDownEntity>();
            DropDownEntity objSelect = new DropDownEntity();
            objSelect.DataStringValueField = "";
            objSelect.DataTextField = "Select";
            SelectList.Add(objSelect);

            ViewBag.ListEnqNo = SelectList;

            if (actionType == "ADD")
            {
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

            string result = objManager.SaveRevisedQuotationDetails(cusE);

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
            objSingle = model.GetSingleColumnValues("QuotationRegister", "RevisedQuoteNo", "RevisedQuoteNo", "QuoteType", quoteTypeId, "QuoteDate", "", "", "QuoteNo", quoteNumberId, "", "QuoteValidity");

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
            NewStr = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.QuoteClarification, ColumnNames.QuoteNo, quoteNo, ColumnNames.QuoteType, quoteType, "", "", "", "", "", "", ColumnNames.id, ColumnNames.MailId);
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
            objSingle = model.GetSingleColumnValues(TableNames.Notes, ColumnNames.NoteMsg, ColumnNames.NoteMsg, ColumnNames.QuoteType, quoteType, "", ColumnNames.QuoteNo, quoteNo);

            objSingle.DataValueField1 = objSingle.DataValueField1 == null ? "" : objSingle.DataValueField1;
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

        public ActionResult GetVendorDetailsForQuote(string quoteNo, string quoteType, string financialYr)
        {
            //List<DropDownEntity> newList = new List<DropDownEntity>();
            //newList = model.GetMasterTableStringList("QuotationRegister", "Id", "VendorName", quoteNo, "QuoteNo", GeneralConstants.ListTypeN);
            //return new JsonResult { Data = newList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //SingleColumnEntity objSingle = new SingleColumnEntity();
            //objSingle = model.GetSingleColumnValues(TableNames.QuotationRegister, ColumnNames.CustomerName, ColumnNames.CustomerName, ColumnNames.QuoteType, quoteType, "Currency", ColumnNames.QuoteNoView, quoteNo);
            //return new JsonResult { Data = objSingle, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            QuotationEntity quotE = GetQuoteDetials(quoteNo, quoteType, financialYr);
            return new JsonResult { Data = quotE, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public ActionResult GetQuoteItemSlNos(string quoteType, string quoteNo, string finYear)
        {
            List<DropDownEntity> QuoteItemSlNoList = new List<DropDownEntity>();
            QuoteItemSlNoList = objManager.GetQuoteItemSlNos(quoteType, quoteNo, finYear);
            //QuoteItemSlNoList =  = model.GetDropDownList(TableNames.QuotePreparationTbl, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.ItemNo, quoteNo, ColumnNames.QuoteNoView);
            return new JsonResult { Data = QuoteItemSlNoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public ActionResult GetQuoteNoForSO(string SoNoId, string quoteTypeId)
        //{
        //    List<DropDownEntity> QuoteNoList = new List<DropDownEntity>();
        //    QuoteNoList = objManager.GetQuoteNoList(quoteTypeId, SoNoId);

        //    return new JsonResult { Data = QuoteNoList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public ActionResult GetOrderDetailsFromSO(string SoNoView, string quoteTypeId)
        {
            ItemEntity iEntity = new ItemEntity();
            iEntity = objManager.GetOrderDetailsFromSO(SoNoView);
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            List<DropDownEntity> lstQuoteItemSlNo = new List<DropDownEntity>();
            lstQuoteNo = objManager.GetQuoteNoList(quoteTypeId, SoNoView);
            lstQuoteItemSlNo = objManager.GetQuoteItemSlNoList(quoteTypeId, SoNoView);
            var result = new { iEntity, lstQuoteNo, lstQuoteItemSlNo };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetSoNoForClarification(string quotetypeId, string quoteNoId)
        {
            List<DropDownEntity> lstSoNos = new List<DropDownEntity>();

            lstSoNos = model.GetDropDownList("Orders", GeneralConstants.ListTypeD, "SoNo", "SoNo", quotetypeId, "QuoteType", false, "", "", quoteNoId, "QuoteNo");
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

        public ActionResult RevisedOrderDetailsPopup(string actionType, string OrderId)
        {
            ViewBag.ListQuoteNo = model.GetMasterTableStringList(TableNames.QuotationRegister, ColumnNames.id, ColumnNames.QuoteNo, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListItemNo = DdlList();

            ViewBag.ListCustomerId = model.GetMasterTableStringList(TableNames.Customer, ColumnNames.id, ColumnNames.CustomerID, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListQuoteQtyType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "SingleMultiple", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListCurr = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Currency", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListOrderType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "QuoteType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "SupplyTerms", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListMainProdGrp = model.GetMasterTableStringList(TableNames.Master_ProductLine, ColumnNames.id, ColumnNames.MainPLName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSubProdGrp = model.GetMasterTableStringList(TableNames.SubProductLine, ColumnNames.id, ColumnNames.SubPLName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdName = model.GetMasterTableStringList(TableNames.Master_Product, ColumnNames.id, ColumnNames.ProductName, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSoNo = model.GetMasterTableStringList(TableNames.Orders, ColumnNames.SoNo, ColumnNames.SoNoView, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSoNo.RemoveAt(0);
            ViewBag.ListModeOfDespatch = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeD, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Transport", ColumnNames.Property);
            ViewBag.ListFinancialYear = model.GetDropDownList(TableNames.FinancialYear, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.FinYear, "", "", true);

            model = new BaseModel();

            OrderEntity orderE = new OrderEntity();
            orderE.UnitNo = Session["UserId"].ToString();
            orderE.UserInitial = Session["UserName"].ToString();

            if (actionType == "ADD")
            {
                List<DropDownEntity> SelectList = new List<DropDownEntity>();
                DropDownEntity objSelect = new DropDownEntity();
                objSelect.DataStringValueField = "";
                objSelect.DataTextField = "Select";
                SelectList.Add(objSelect);

                ViewBag.ListQuoteNo = SelectList;

            }

            return base.PartialView("~/Views/Technical/_TechRevisedOrderDetails.cshtml", orderE);
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
            ViewBag.API = model.GetTaxonomyDropDownItems("", "APINonAPI");
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
                string msgCode = model.DeleteFromTable("EnquiryRegister", "EnquiryId", id.ToString());
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

            switch (type)
            {
                case "Customer":
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
            quote = objQuoteManager.GetQuoteRegList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchQuoteCustomerID, SearchSubject, SearchDeliveryTerms);
            return new JsonResult { Data = quote, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return QuotDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult DeleteQuotationDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = model.DeleteFromTable("QuotationRegister", "Id", id.ToString()); //objQuoteManager.DeleteQuotationDetail(id);
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
            var SubProdGrpList = model.GetDropDownList(TableNames.SubProductLine, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.SubPLName, MainProdGrpId, ColumnNames.MainPLId);
            return new JsonResult { Data = SubProdGrpList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult GetProdName(string MainProdGrpId, string SubProdGrpId)
        {
            var ProdNameList = model.GetDropDownList(TableNames.Master_Product, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.ProductName, MainProdGrpId, ColumnNames.PL, false, "", "", SubProdGrpId, ColumnNames.SubPL);
            return new JsonResult { Data = ProdNameList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult BindDescDetail(string actionType)
        {
            ViewBag.ListCustomerId = model.GetMasterTableStringList(TableNames.Customer, ColumnNames.id, ColumnNames.CustomerId, "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, ColumnNames.QuoteType, ColumnNames.Property, GeneralConstants.ListTypeN);

            ViewBag.ListMainPL = model.GetDropDownList(TableNames.Master_ProductLine, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.MainPLName, "", "");
            ViewBag.ListSubPL = model.GetDropDownList(TableNames.SubProductLine, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.SubPLName, "", "");
            ViewBag.ListPosition = model.GetDropDownList(TableNames.DescPosition, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.PosName, "", "", true);
            ViewBag.ListFieldName = model.GetDropDownList(TableNames.DescFieldName, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.FieldName, "", "", true);


            DescEntity dEN = new DescEntity();
            return base.PartialView("~/Views/Technical/_TechDescDetails.cshtml", dEN);
        }

        [HttpGet]
        public ActionResult BindPLDetails(string actionType)
        {
            ViewBag.ListMainPL = model.GetDropDownList(TableNames.Master_ProductLine, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.MainPLName, "", "");
            ViewBag.SubPLList = model.GetDropDownList(TableNames.SubProductLine, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.SubPLName, "", "");

            PLEntity pl = new PLEntity();
            return base.PartialView("~/Views/Technical/_TechPLDetails.cshtml", pl);
        }

        public JsonResult GetDescLists()
        {
            DescDetailLists dDetail = new DescDetailLists();
            dDetail.DescMailPL = model.GetDropDownList("Master.ProductLine", GeneralConstants.ListTypeD, "Id", "MainPLName", "", "");
            dDetail.DescSubPL = model.GetDropDownList("SubProductLine", GeneralConstants.ListTypeD, "Id", "SubPLName", "", "");
            dDetail.DescPosList = model.GetDropDownList("DescPosition", GeneralConstants.ListTypeD, "Id", "PosName", "", "");
            dDetail.FieldNameList = model.GetDropDownList("DescFieldName", GeneralConstants.ListTypeD, "Id", "FieldName", "", "");

            return new JsonResult { Data = dDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveNewDescDetail(DescEntity descE)
        {
            string data = string.Empty;

            try
            {
                string result = objManager.SaveNewDescDetail(descE);

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

        public ActionResult GetDisplayFieldsForQuotePrep(string productId, string casingSize, string type)
        {
            List<ProductEntity> lstProducts = new List<ProductEntity>();
            lstProducts = objManager.GetPrepProductNames(productId, casingSize, type);
            return new JsonResult { Data = lstProducts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetOrderNote(string soNo)
        {
            SingleColumnEntity objSingle = new SingleColumnEntity();
            objSingle = model.GetSingleColumnValues(TableNames.OrderClarification, ColumnNames.Notes, "", ColumnNames.SoNo, soNo);

            objSingle.DataValueField1 = objSingle.DataValueField1 == null ? "" : objSingle.DataValueField1;
            return new JsonResult { Data = objSingle.DataValueField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult SaveOrderNote(string soNo, string notes)
        {
            string result = "";
            OrderEntity oEntity = new OrderEntity();
            try
            {
                oEntity.SoNo = soNo;
                oEntity.Notes = notes;
                result = objManager.SaveOrderNote(oEntity);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

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

        public ActionResult LeadDetailsForQuote(string quoteNo, string quoteType)
        {
            List<TableRecordsEntity> ddE = new List<TableRecordsEntity>();
            ddE = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.QuotationRegister, ColumnNames.QuoteNo, quoteNo, ColumnNames.QuoteType, quoteType, "", "", "", "", "", "", ColumnNames.LeadTime, ColumnNames.LeadTimeDuration, ColumnNames.SupplyTerms);

            return new JsonResult { Data = ddE, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteQuotationPrepDetail(string ItemNo, string QuoteType, string QuoteNumber)
        {
            string msgCode = model.DeleteFromTable(TableNames.QuotePreparationTbl, ColumnNames.ItemNo, ItemNo, ColumnNames.QuoteType, QuoteType, ColumnNames.QuoteNo, QuoteNumber);
            if (msgCode == GeneralConstants.DeleteSuccess)
            {
                return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        [HttpPost]
        public ActionResult LoadDescDetail()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var totalRecords = 0;
            var objList = new List<DescEntity>();

            try
            {
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var search = Request.Form.GetValues("search[value]")[0];
                //int InstructorId = ((UserModel)Session["UserModel"]).ObjectId;
                //int UserId = ((UserModel)Session["UserModel"]).UserId;
                objList = objManager.LoadDescDetail(skip, pageSize, sortColumn, sortColumnDir, search);
                var v = (from a in objList select a);
                objList = v.ToList();
                totalRecords = Convert.ToInt32(objList[0].TotalRecords);
            }
            catch (Exception ex)
            {
                Logger.Error("TechnicalController/LoadDescDetail", ex);
            }
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = objList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveRevisedOrderDetails(OrderEntity cusE)
        {
            model = new BaseModel();
            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveRevisedOrderDetails(cusE);

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

        public ActionResult PartialContractReview()
        {
            return PartialView("~/Views/Technical/_TechContractReview.cshtml");
        }

        //[HttpPost]
        //public ActionResult SaveContractReviewDetails(ContractReview cusE)
        //{
        //    // string result = objEnquiryManager.SaveEnquiryDetails(cusE);
        //    string result = model.SaveTableData(TableNames.ContractReview, ColumnNames.Listing1, cusE.Listing1, ColumnNames.MainPL, cusE.MainPLId, ColumnNames.SubPL, cusE.SubPLId);

        //    string data = string.Empty;
        //    if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
        //    {
        //        data = GeneralConstants.SavedSuccess;
        //    }
        //    else
        //    {
        //        data = GeneralConstants.NotSavedError;
        //    }

        //    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public ActionResult GetCountry(string val)
        {
            SingleColumnEntity sE = new SingleColumnEntity();
            sE = model.GetSingleColumnValues(TableNames.Customer, ColumnNames.Country, ColumnNames.Country, ColumnNames.id, val);

            sE = model.GetSingleColumnValues(TableNames.Master_Country, ColumnNames.Country, ColumnNames.id, ColumnNames.id, sE.DataValueField1);

            return new JsonResult { Data = sE.DataValueField1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetEnqNo(string val)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();
            ddl = model.GetDropDownList(TableNames.EnquiryRegister, GeneralConstants.ListTypeD, ColumnNames.EnquiryId, ColumnNames.ENQREF, val, ColumnNames.CustomerId, false, "", "", "1", ColumnNames.IsActive);
            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetItemNosForEnqs(string EnqNo)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();
            ddl = objManager.GetItemNosForEnqs(EnqNo);
            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult GetExcelForContractReview(string customerId, string enqNo, string itemNo, string fileName)
        {
            fileName = fileName + ".xlsx";

            string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), "");
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), fileName);
            fileName = GenerateCRExcel(fullPath, fileName, enqNo, itemNo);
            var data = new { fileName = fileName, path = fullPath };
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private string GenerateCRExcel(string fullPath, string fileName, string EnqNo, string ItemNo = null)
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ContractReviewExcel"]);

                //if (!System.IO.Directory.Exists(path))
                //    System.IO.Directory.CreateDirectory(path);

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Contract Review Sheet"];

                System.Data.DataTable resultData = objManager.GetDataForContractReview(EnqNo, ItemNo, "Data");
                System.Data.DataTable resultList = objManager.GetDataForContractReview(EnqNo, ItemNo, "List");

                if (resultData.Rows.Count <= 0 || resultList.Rows.Count <= 0)
                {
                    fileName = "No Records Found For Selected Item";
                    return fileName;
                }

                //For Data
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerName", resultData.Rows[0]["CustomerName"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ContactPerson", resultData.Rows[0]["ContactPerson"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Email", resultData.Rows[0]["Email"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Country", resultData.Rows[0]["Country"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#DeliveryPoint", resultData.Rows[0]["DeliveryPoint"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustAddress", resultData.Rows[0]["CustAddress"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqNoDt", resultData.Rows[0]["EnqNoDt"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqRevNoDt", resultData.Rows[0]["EnqRevNoDt"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PONoDt", resultData.Rows[0]["PONoDt"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PORevNoDt", resultData.Rows[0]["PORevNoDt"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#FileNo", resultData.Rows[0]["FileNo"]);


                string EnqFor = "", MaterialGrade = "", CasingSize = "", ThreadType = "", BottomEnd = "", TopEnd = "", CasingPpf = "", Qty = "", DeliveryLeadTime = "",
                    DeliveryTerms = "", QuoteValidity = "", PaymentTerms = "", BgReq = "", ModeOfDespatch = "", RawMatSpec = "";

                int i = 1;
                foreach (DataRow row in resultList.Rows)
                {
                    if (i == 1)
                    {
                        EnqFor = EnqFor + (row.IsNull("EnqFor") ? string.Empty : (row["EnqFor"].ToString() + "\n"));
                        DeliveryTerms = DeliveryTerms + (row.IsNull("DeliveryTerms") ? string.Empty : (row["DeliveryTerms"].ToString() + "\n"));
                        DeliveryLeadTime = DeliveryLeadTime + (row.IsNull("DeliveryLeadTime") ? string.Empty : (row["DeliveryLeadTime"].ToString() + "\n"));
                        QuoteValidity = QuoteValidity + (row.IsNull("QuoteValidity") ? string.Empty : (row["QuoteValidity"].ToString() + "\n"));
                        PaymentTerms = PaymentTerms + (row.IsNull("PaymentTerms") ? string.Empty : (row["PaymentTerms"].ToString() + "\n"));
                        ModeOfDespatch = ModeOfDespatch + (row.IsNull("ModeOfDespatch") ? string.Empty : (row["ModeOfDespatch"].ToString() + "\n"));

                    }

                    MaterialGrade = MaterialGrade + (row.IsNull("MaterialGrade") ? string.Empty : (i.ToString() + ". " + row["MaterialGrade"].ToString() + "\n"));
                    CasingSize = CasingSize + (row.IsNull("CasingSize") ? string.Empty : (i.ToString() + ". " + row["CasingSize"].ToString() + "\n"));
                    ThreadType = ThreadType + (row.IsNull("ThreadType") ? string.Empty : (i.ToString() + ". " + row["ThreadType"].ToString() + "\n"));
                    BottomEnd = BottomEnd + (row.IsNull("BottomEnd") ? string.Empty : (i.ToString() + ". " + row["BottomEnd"].ToString() + "\n"));
                    TopEnd = TopEnd + (row.IsNull("TopEnd") ? string.Empty : (i.ToString() + ". " + row["TopEnd"].ToString() + "\n"));
                    CasingPpf = CasingPpf + (row.IsNull("CasingPpf") ? string.Empty : (i.ToString() + ". " + row["CasingPpf"].ToString() + "\n"));
                    Qty = Qty + (row.IsNull("Qty") ? string.Empty : (i.ToString() + ". " + row["Qty"].ToString() + "\n"));
                    BgReq = BgReq + (row.IsNull("BgReq") ? string.Empty : (i.ToString() + ". " + row["BgReq"].ToString() + "\n"));
                    RawMatSpec = RawMatSpec + (row.IsNull("RawMatSpec") ? string.Empty : (i.ToString() + ". " + row["RawMatSpec"].ToString() + "\n"));
                    i++;
                }

                //For List
                xlWorkbook.Worksheets[1].Cells.Replace("#EnqFor", EnqFor);
                xlWorkbook.Worksheets[1].Cells.Replace("#MaterialGrade", MaterialGrade);
                xlWorkbook.Worksheets[1].Cells.Replace("#CasingSize", CasingSize);
                xlWorkbook.Worksheets[1].Cells.Replace("#ThreadType", ThreadType);
                xlWorkbook.Worksheets[1].Cells.Replace("#BottomEnd", BottomEnd);
                xlWorkbook.Worksheets[1].Cells.Replace("#TopEnd", TopEnd);
                xlWorkbook.Worksheets[1].Cells.Replace("#CasingPpf", CasingPpf);
                xlWorkbook.Worksheets[1].Cells.Replace("#Qty", Qty);
                xlWorkbook.Worksheets[1].Cells.Replace("#DeliveryTerms", DeliveryTerms);
                xlWorkbook.Worksheets[1].Cells.Replace("#DeliveryLeadTime", DeliveryLeadTime);
                xlWorkbook.Worksheets[1].Cells.Replace("#QuoteValidity", QuoteValidity);
                xlWorkbook.Worksheets[1].Cells.Replace("#PaymentTerms", PaymentTerms);
                xlWorkbook.Worksheets[1].Cells.Replace("#BgReq", BgReq);
                xlWorkbook.Worksheets[1].Cells.Replace("#ModeOfDespatch", ModeOfDespatch);
                xlWorkbook.Worksheets[1].Cells.Replace("#RawMatSpec", RawMatSpec);

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Application.Quit();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                var response = ex.Message;
            }

            return fileName;
        }

        [HttpPost]
        public ActionResult CRFilesUpload()
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
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string Name = "";
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            string extension = System.IO.Path.GetExtension(file.FileName);
                            Name = file.FileName.Substring(0, file.FileName.Length - extension.Length);

                            fname = file.FileName;
                        }

                        //strB = strB.Append(fname + ",");
                        file.SaveAs(Path.Combine(Server.MapPath("~/Documents/ContractReviewUploads/"), fname));

                        string result = string.Empty;
                        ContractReview conRev = new ContractReview();
                        conRev.ENQNo = Request.Form["enquiryNo"];
                        conRev.FileName = fname;
                        //cObj.MailId = cObj.MailId.Remove(cObj.MailId.Length - 1, 1);
                        result = objManager.SaveContractReviewData(conRev);

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


        public JsonResult GetContractReviews(string customerId = null)
        {
            List<DropDownEntity> newLst = new List<DropDownEntity>();
            newLst = objManager.GetContractReviews(customerId);
            //List<TableRecordsEntity> NewStr = new List<TableRecordsEntity>();
            //newLst = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.ContractReviewDetails, ColumnNames.CustomerID, customerId, "", "", "", "", "", "", "", "", ColumnNames.id, ColumnNames.EnqNo, ColumnNames.filename);

            //return Json(NewStr);
            return new JsonResult { Data = newLst, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteContractReviews(List<string> EnqNoExcels)
        {
            string msgCode = "";
            try
            {
                if (EnqNoExcels != null)
                {
                    foreach (string i in EnqNoExcels)
                    {
                        if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Documents/ContractReviewUploads/"), i)))
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Documents/ContractReviewUploads/"), i));

                        msgCode = model.DeleteFromTable(TableNames.ContractReviewDetails, ColumnNames.filename, i);

                        if (msgCode != GeneralConstants.DeleteSuccess)
                        {
                            msgCode = GeneralConstants.NotDeletedSomeField;
                            break;
                        }
                    }
                }
                else
                    msgCode = GeneralConstants.NotDeletedSomeField + " .No Records Selected";
            }
            catch (Exception ex)
            {
                msgCode = GeneralConstants.NotDeletedSomeField + " .Error " + ex.Message;
            }


            return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult SavePLDetails(string type, string mainPLName = null, string mainPLNo = null, string mainPLDdl = null, string subPLtext = null)
        {

            string result = string.Empty;

            if (type == "MainPL")
                result = model.SaveTableData(TableNames.Master_ProductLine, ColumnNames.MainPL, mainPLNo, ColumnNames.MainPLName, mainPLName);
            else
                result = model.SaveTableData(TableNames.SubProductLine, ColumnNames.MainPLId, mainPLDdl, ColumnNames.SubPLName, subPLtext);

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
        public ActionResult LoadMasterPLlist()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var totalRecords = 0;
            var objList = new List<PLEntity>();

            try
            {
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var search = Request.Form.GetValues("search[value]")[0];
                //int InstructorId = ((UserModel)Session["UserModel"]).ObjectId;
                //int UserId = ((UserModel)Session["UserModel"]).UserId;
                objList = objManager.LoadMasterPLlist(skip, pageSize, sortColumn, sortColumnDir, search);
                var v = (from a in objList select a);
                objList = v.ToList();
                totalRecords = Convert.ToInt32(objList[0].TotalRecords);
            }
            catch (Exception ex)
            {
                Logger.Error("TechnicalController/LoadMasterPLlist", ex);
            }
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = objList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadQuotePrepListDetails(string quoteType = null, string quoteNo = null, string itemNo = null, string financialYear = null)
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var totalRecords = 0;
            var objList = new List<QuotationPreparationEntity>();

            try
            {
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var search = Request.Form.GetValues("search[value]")[0];
                //int InstructorId = ((UserModel)Session["UserModel"]).ObjectId;
                //int UserId = ((UserModel)Session["UserModel"]).UserId;
                objList = objManager.LoadQuotePrepListDetails(skip, pageSize, sortColumn, sortColumnDir, search, quoteType, quoteNo, itemNo, financialYear);
                var v = (from a in objList select a);
                objList = v.ToList();
                if (objList.Count > 0)
                    totalRecords = Convert.ToInt32(objList[0].TotalRecords);
            }
            catch (Exception ex)
            {
                Logger.Error("TechnicalController/LoadQuotePrepListDetails", ex);
            }
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = objList }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetItemNosForQuoteNos(string quoteNo)
        {
            List<DropDownEntity> ddE = new List<DropDownEntity>();
            ddE = model.GetDropDownList(TableNames.QuotePreparationTbl, GeneralConstants.ListTypeD, ColumnNames.ItemNo, ColumnNames.ItemNo, quoteNo, ColumnNames.QuoteNo);

            return new JsonResult { Data = ddE, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public ActionResult EditQuotePrep(string Id)
        {
            QuotationPreparationEntity en = new QuotationPreparationEntity();
            en = objManager.GetQuotePrepDetails(0, 0, 0, Convert.ToInt32(Id), 0);

            return new JsonResult { Data = en, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult QPGetQuoteNoFromFinYears(string finYear, string quoteType)
        {
            List<DropDownEntity> lstQuoteNo = GetRevisedAndOriginalQuotes(quoteType, finYear);

            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult LoadItemWiseOrders()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var totalRecords = 0;
            var objList = new List<ItemEntity>();

            try
            {
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var search = Request.Form.GetValues("search[value]")[0];
                //int InstructorId = ((UserModel)Session["UserModel"]).ObjectId;
                //int UserId = ((UserModel)Session["UserModel"]).UserId;
                objList = objManager.LoadItemWiseOrders(skip, pageSize, sortColumn, sortColumnDir, search);
                var v = (from a in objList select a);
                objList = v.ToList();
                if (objList.Count > 0)
                    totalRecords = Convert.ToInt32(objList[0].TotalRecords);
            }
            catch (Exception ex)
            {
                Logger.Error("TechnicalController/LoadItemWiseOrders", ex);
            }
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = objList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSoNosForFinancialYears(string FinYear, string quoteType = null)
        {
            List<DropDownEntity> lstOrder = new List<DropDownEntity>();
            if (string.IsNullOrEmpty(quoteType))
                lstOrder = model.GetDropDownList(TableNames.Orders, GeneralConstants.ListTypeN, ColumnNames.SoNo, ColumnNames.SoNoView, FinYear, ColumnNames.FinancialYear, false, "", "");
            else
                lstOrder = model.GetDropDownList(TableNames.Orders, GeneralConstants.ListTypeN, ColumnNames.SoNo, ColumnNames.SoNoView, FinYear, ColumnNames.FinancialYear, false, "", "", quoteType, ColumnNames.QuoteType);

            return new JsonResult { Data = lstOrder, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetQuoteNoDetailsforRevisedQuote(string quoteNoId, string quotetypeId, string financialYr)
        {
            QuotationEntity quotE = GetQuoteDetials(quoteNoId, quotetypeId, financialYr);
            return new JsonResult { Data = quotE, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private QuotationEntity GetQuoteDetials(string quoteNoId, string quotetypeId, string financialYr)
        {
            quotE = new QuotationEntity();
            quotE = objManager.GetQuoteNoDetailsforRevisedQuote(quoteNoId, quotetypeId, financialYr);
            return quotE;
        }

        public ActionResult GetRevAndOriginalQuotes(string quotetypeId, string financialYr = null)
        {
            List<DropDownEntity> lstQuoteNo = GetRevisedAndOriginalQuotes(quotetypeId, financialYr);

            return new JsonResult { Data = lstQuoteNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private List<DropDownEntity> GetRevisedAndOriginalQuotes(string quotetypeId, string financialYr)
        {
            List<DropDownEntity> lstQuoteNo = new List<DropDownEntity>();
            lstQuoteNo = objManager.GetRevAndOriginalQuotes(quotetypeId, financialYr);
            return lstQuoteNo;
        }
    }

}