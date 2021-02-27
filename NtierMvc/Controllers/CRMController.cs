using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Model.Customer;
using NtierMvc.Infrastructure;
using System.Configuration;
using System.IO;
using System.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Reflection;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class CRMController : Controller
    {
        private LoggingHandler _loggingHandler;
        CustomerEntity cus;
        CustomerEntityDetails custDetail;
        private CustomerManager objManager;
        BaseModel model;

        #region Constructor
        public CRMController()
        {
            _loggingHandler = new LoggingHandler();
            cus = new CustomerEntity();
            custDetail = new CustomerEntityDetails();
            objManager = new CustomerManager();
            model = new BaseModel();
        }

        #endregion

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult CRMMaster()
        {
            //Customer
            ViewBag.ListCustomerName = model.GetMasterTableStringList("Customer", "CustomerName", "CustomerName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCustStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CustomerStatus", ColumnNames.Property,false,"asc",ColumnNames.DropDownValue);
            ViewBag.ListCountry = model.GetMasterTableStringList("Master.Country", "Id", "Country", "", "", GeneralConstants.ListTypeN);

            //Enquiry
            ViewBag.ListEnqType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEnqFor = "";
            ViewBag.ListProdGrp = "";
            ViewBag.ListEOQ = ""; // model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Expression of Quote(EOQ)", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDueDate = "";
            ViewBag.ListDeliveryTerms = "";
            ViewBag.ListSubject = "";

            Dictionary<string, string> Months = new Dictionary<string, string>();
            Months.Add(Convert.ToString(DateTime.Now.Month), "Current Month");
            Months.Add(Convert.ToString(DateTime.Now.Month + 3), "Next 3 Months");

            List<DropDownEntity> ListMonths = new List<DropDownEntity>();
            DropDownEntity dDE0 = new DropDownEntity();
            dDE0.DataStringValueField = "";
            dDE0.DataTextField = "Select";
            ListMonths.Add(dDE0);
            DropDownEntity dDE = new DropDownEntity();
            dDE.DataStringValueField = Convert.ToString(DateTime.Now.Month);
            dDE.DataTextField = "Current Month";
            ListMonths.Add(dDE);
            DropDownEntity dDE1 = new DropDownEntity();
            dDE1.DataStringValueField = Convert.ToString(DateTime.Now.Month + 3);
            dDE1.DataTextField = "Next 3 Months";
            ListMonths.Add(dDE1);

            ViewBag.ListMonth = ListMonths;

            //Quotation
            ViewBag.ListDeliveryTerms = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "DeliveryTerms", "ObjectName", GeneralConstants.ListTypeN);

            //Order
            ViewBag.ListQuoteNo = "";
            ViewBag.ListEnqFor = model.GetMasterTableStringList("QuotationRegister", "EnqFor", "EnqFor", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListPODeliveryDate = model.GetDateDropDownList(TableNames.Orders, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.PoDeliveryDate, "", "");

            return View();
        }

        [HttpGet]
        public ActionResult PartialCustomer()
        {
            return PartialView(custDetail);
        }

        [HttpGet]
        public ActionResult Customer()
        {
            custDetail.cusEnt.UnitNo = Session["UserId"].ToString();
            custDetail.cusEnt = objManager.GetUserDetails(custDetail.cusEnt.UnitNo);
            //custDetail.LstCusEnt = FetchCustomerList(string.Empty);

            return View(custDetail);
        }
        public ActionResult PartialReportView()
        {
            ViewBag.ListCustomer = model.GetDropDownList(TableNames.Customer, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.CustomerName, "", "");
            ViewBag.ListCountry = model.GetDropDownList(TableNames.Master_Country, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.Country, "", "");
            ViewBag.ListENQNo = model.GetDropDownList(TableNames.EnquiryRegister, GeneralConstants.ListTypeN, ColumnNames.EnquiryId, ColumnNames.ENQREF, "", "");
            ViewBag.ListItemNo = model.GetDropDownList(TableNames.Items, GeneralConstants.ListTypeN, ColumnNames.id, ColumnNames.PoSLNo, "", "");

            ContractReview CR = new ContractReview();
            return PartialView("~/Views/CRM/_CRMPartialReport.cshtml", CR);
            //return PartialView();
        }

        public JsonResult FetchCustomerList(string pageIndex, string pageSize, string SearchCountry, string SearchCustomerID, string SearchCustomerIsActive)
        {
            SearchCountry = SearchCountry == "-1" ? string.Empty : SearchCountry;
            SearchCustomerID = SearchCustomerID == "-1" ? string.Empty : SearchCustomerID;
            SearchCustomerIsActive = SearchCustomerIsActive == "-1" ? string.Empty : SearchCustomerIsActive;

            custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCountry, SearchCustomerID, SearchCustomerIsActive);
            return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult SaveCustomerDetails(CustomerEntity cusE)
        {
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveCustomerDetails(cusE);

            TempData["VendorName"] = cusE.CustomerName;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult CustomerPopup(string actionType, string CustomerId = null)
        {
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value

            ViewBag.ListCustomerType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Domestic/International", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableStringList(TableNames.Master_FunctionalArea, ColumnNames.id, ColumnNames.FunctionArea);
            ViewBag.ListCountry = model.GetMasterTableStringList(TableNames.Master_Country, ColumnNames.id, ColumnNames.Country);
            ViewBag.ListStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CustomerStatus", ColumnNames.Property, false, "asc", ColumnNames.DropDownValue);
            

            if (!string.IsNullOrEmpty(Session["UserId"].ToString()))
                cus.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(CustomerId))
                    cus.Id = Convert.ToInt32(CustomerId);
                cus = objManager.CustomerDetailsPopup(cus);
            }
            if (actionType == "ADD")
            {
                cus = objManager.GetUserDetails(cus.UnitNo);
                //eModel = objManager.AddCustomerDetailsPopup(eModel);
            }

            return base.PartialView("~/Views/CRM/_CustomerDetails.cshtml", cus);
        }

        public ActionResult DeleteCustomerDetail(int id)
        {
            if (ModelState.IsValid)
            {
                //string msgCode = objManager.DeleteCustomerDetail(id);
                string msgCode = model.DeleteFromTable("Customer", "Id", id.ToString());

                if (msgCode == GeneralConstants.DeleteSuccess)
                {
                    //return RedirectToAction("Customer");
                    return new JsonResult { Data = GeneralConstants.DeleteSuccess, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public ActionResult GetStateDetail(string countryId)
        {
            List<DropDownEntity> StateList = new List<DropDownEntity>();
            //StateList = model.GetStateDetail(countryId);
            StateList = model.GetDropDownList(TableNames.Master_State, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.state, countryId, ColumnNames.CountryId,false);
            return new JsonResult { Data = StateList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetVendorListForCust(string VendorNatureId)
        {
            var Ddl = model.GetDropDownList("Clientele_Master", GeneralConstants.ListTypeD, "Id", "VendorID", VendorNatureId, "VendorNatureId");
            return new JsonResult { Data = Ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetDdlValueForCustomer(string type, string CountryId = null, string CustomerId = null)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();

            if (type == "CountryId")

                ddl = objManager.GetDdlValueForCustomer(type, CountryId);
            //ddl = model.GetDropDownList("Customer", GeneralConstants.ListTypeD, "Id", "CustomerID", CountryId, "Country");
            else if (type == "CustomerName")
                ddl = objManager.GetDdlValueForCustomer(type, "", CustomerId);

            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
       
        [HttpGet]
        public ActionResult CreateReport(string ReportType, string pageIndex, string pageSize,string SearchCountry, string SearchCustomerID,string SearchCustomerIsActive)
        {

            SearchCountry = SearchCountry == "-1" ? string.Empty : SearchCountry;
            SearchCustomerID = SearchCustomerID == "-1" ? string.Empty : SearchCustomerID;

            //custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCustomerName, SearchCustomerID);
            //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            string fileName = ReportType;
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]);
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), fileName);


            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            //switch (ReportType)
            //{
            //    case "CUSTOMER":
                    fileName = GenerateReport(fullPath, fileName, pageIndex, pageSize, SearchCountry, SearchCustomerID, SearchCustomerIsActive);
                //    break;
                //default:
                //    break;
           // }

            return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        private string GenerateReport(string fullPath,  string DocumentName, string pageIndex, string pageSize, string SearchCountry, string SearchCustomerID,string SearchCustomerIsActive)
        {

            string fileName = "";
                ReportManager Report = new ReportManager();
            switch (DocumentName)
            {
                case "CUSTOMER":
                    fileName = Report.PrepCustomerReport(fullPath, DocumentName, pageIndex, pageSize, SearchCountry, SearchCustomerID, SearchCustomerIsActive);
                    break;
                //case "CUSTOMERFEEDBACK":
                //    fileName = Report.PrepCustomerFeedBackReport(fullPath, DocumentName);
                //    break;
                default:
                    break;
            }
            return fileName;
        }
        [HttpPost]
        public ActionResult CreateWAAuthReport( string SoNo, string FromDate, string ToDate, string ReportType)
        {
            //custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCustomerName, SearchCustomerID);
            //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            string fileName = ReportType;
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]);
            string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"].ToString()), fileName);


            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            //switch (ReportType)
            //{
            //    case "CUSTOMER":
            fileName = GenerateRAUtheport(fullPath, SoNo, FromDate, ToDate, ReportType);
            //    break;
            //default:
            //    break;
            // }

            return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        private string GenerateRAUtheport(string fullPath, string SoNo, string FromDate, string ToDate, string ReportType)
        {

            string fileName = "";
            ReportManager Report = new ReportManager();
            switch (ReportType)
            {
                case "WAuthReport":
                    fileName = Report.PrepCustomerReport(fullPath, SoNo, FromDate, ToDate, ReportType);
                    break;
                case "CUSTOMERFEEDBACK":
                    fileName = Report.PrepCustomerFeedBackReport(fullPath, SoNo, FromDate, ToDate, ReportType);
                    break;
                case "ProductPerformance":
                    fileName = Report.PrepProductPerformanceReport(fullPath, SoNo, FromDate, ToDate, ReportType);
                    break;
                case "EnquiryReport":
                    fileName = Report.PrepEnqandQuotoReport(fullPath, SoNo, FromDate, ToDate, ReportType);
                    break;
                default:
                    break;
            }
            return fileName;
        }

        public ActionResult Download(string fileName)
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





    }
}