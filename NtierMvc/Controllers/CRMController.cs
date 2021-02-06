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
            return PartialView();
        }


        public JsonResult FetchCustomerList(string pageIndex, string pageSize, string SearchCustomerName, string SearchCustomerID)
        {
            SearchCustomerName = SearchCustomerName == "-1" ? string.Empty : SearchCustomerName;
            SearchCustomerID = SearchCustomerID == "-1" ? string.Empty : SearchCustomerID;

            custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCustomerName, SearchCustomerID);
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
            ViewBag.ListStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CustomerStatus", ColumnNames.Property, true);

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
                string msgCode = model.DeleteFormTable("Customer", "Id", id.ToString());

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
       

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        [HttpPost]
        public ActionResult CreateReport(string ReportType, string pageIndex, string pageSize,string SearchCustomerName, string SearchCustomerID)
        {
           
                 SearchCustomerName = SearchCustomerName == "-1" ? string.Empty : SearchCustomerName;
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
                //case "Customer":
                    fileName= GenerateReport(fullPath,fileName, pageIndex, pageSize, SearchCustomerName,SearchCustomerID);
                    //break;
            //    default:
            //       break;
            //}

            return new JsonResult { Data = fileName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        private string GenerateReport(string fullPath,  string DocumentName, string pageIndex, string pageSize, string SearchCustomerName, string SearchCustomerID)
        {

            string fileName = "";
                
            ReportManager Report = new ReportManager();
            fileName= Report.PrepCustomerReport(fullPath, DocumentName, pageIndex, pageSize, SearchCustomerName, SearchCustomerID);
            //try
            //{
                
            //    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //    // open the template in Edit mode
            //    string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustPrepExcel"]);
            //    //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
            //    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
            //    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
            //    CustomerReport CustReportDetails = new CustomerReport();
            //    List<CustomerReport> CustList = new List<CustomerReport>();

            //   var CompanyDetails = model.GetTableDataList("Distinct", "CommonDetails", "IsActive", "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");

            //    var name  = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i=>i.RequiredColumn2);
            //    var CustDetails = model.GetTableDataList("Distinct", "Customer", "", "", "", "", "", "", "", "", "", "", "Id", "CustomerName", "CustomerID", "COUNTRY", "Address1", "ContactPerson", "Mob1", "Email1", "DateOfAssociation", "IsActive");

            //    if(CustDetails.Count>0)
            //    {

            //        foreach (var cust in CustDetails)
            //        {
            //            CustReportDetails.Id = cust.RequiredColumn1;
            //            CustReportDetails.CustomerName = cust.RequiredColumn2;
            //            CustReportDetails.CustomerId = cust.RequiredColumn3;
            //            CustReportDetails.CustomerType = cust.RequiredColumn4== "1" ?"Domestic":"Export";
            //            CustReportDetails.Address1 = cust.RequiredColumn5;
            //            CustReportDetails.ContactPerson = cust.RequiredColumn6;
            //            CustReportDetails.mob1 = cust.RequiredColumn7;
            //            CustReportDetails.email1 = cust.RequiredColumn8;
            //            CustReportDetails.DateOfAssociation = cust.RequiredColumn9;
            //            CustReportDetails.Status = cust.RequiredColumn10=="1"? "Active":"Inactive";
            //            CustReportDetails.Remarks = "";
            //            CustList.Add(CustReportDetails);


            //        }
            //    }

            //  // custDetail = objManager.GetCustomerDetails(0, 0, "", "");

            // //   System.Data.DataTable resultData = objManager.GetDataForDocument();
     
               
            //    System.Data.DataTable resultData=  ToDataTable(CustList);
            //    System.Data.DataTable CompanyData = ToDataTable(CompanyDetails);

              
            //    //Getting Single Fields
            //    xlWorkbook.Worksheets[1].Cells.Replace("#CurrentDate", System.DateTime.Now.ToString("dd-MM-yyyy"));
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column1", "SL.No");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column2", "Customer Name");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column3", "Customer Code");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column4", "Domestic/Export");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column5", "Address");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column6", "Contact Person");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column7", "Contact Numbers");

            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column8", "Email ID");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column9", "Association Since");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_10","Status");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_11","Remarks");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_12","");

            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_13","");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_14","");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Column_15","");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#CompanyName", CompanyData.Rows[3]["RequiredColumn2"]);
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Heading",DocumentName);
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Emp1","RamPrakash");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Emp2","Kamalakar Pathak");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Designation1","Management Repersentative");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Designation2","Manaaging Director");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Sign1","");
            //    xlWorkbook.Worksheets[1].Cells.Replace("#Sign2","");
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#Logo","");

            //    //Removing for Table in Excel
            //    //result.Columns.Remove("vendorname");
            //    //result.Columns.Remove("VendorAddress");
            //    //result.Columns.Remove("enqno");
            //    //result.Columns.Remove("enqDt");
            //    //result.Columns.Remove("CompanyName");
            //    //result.Columns.Remove("QuotationNo");
            //    //result.Columns.Remove("Year");
            //    //result.Columns.Remove("CompanyInitial");
            //    //result.Columns.Remove("UserInitial");
            //    //result.Columns.Remove("FILENO");
            //    //result.Columns.Remove("ENQFOR");

            //    ////////////////For Image////////////////////
            //    //#region Image
            //    //Microsoft.Office.Interop.Excel.Range cells = xlWorkbook.Worksheets[1].Cells;

            //    ////ReqBy
            //    //Microsoft.Office.Interop.Excel.Range matchReqBy = cells.Find("#OwnerSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#OwnerSign", "");

            //    //string matchAdd = matchReqBy != null ? matchReqBy.Address : null;
            //    //if (matchReqBy != null)
            //    //{
            //    //    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchReqBy.Row, matchReqBy.Column];
            //    //    float Left = (float)((double)oRange.Left);
            //    //    float Top = (float)((double)oRange.Top);
            //    //    const float ImageSize = 40;
            //    //    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["OwnerSignImage"]));
            //    //    if (System.IO.File.Exists(filePath))
            //    //        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
            //    //}

            //    //#endregion
            //    ////////////////For Image////////////////////

            //    //Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[15, 1];
            //    //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 2) + 15, resultData.Columns.Count];
            //    ////Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
            //    //Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
            //    //range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

            //  //  Adding Table values in Excel
            //    //int sum = 0, NetWt = 0, GrWt = 0;
            //    //double CubMtr = 0;
            //    object[,] arr = new object[resultData.Rows.Count, resultData.Columns.Count];
            //    for (int r = 0; r <= resultData.Rows.Count-1; r++)
            //    {
            //        DataRow dr = resultData.Rows[r];
            //        for (int c = 0; c < resultData.Columns.Count; c++)
            //        {
            //            arr[r, c] = dr[c];
            //        }

            //    }

            //    //xlWorkbook.Worksheets[1].Cells.Replace("#TotalPrice", sum.ToString());
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#AmountInWords", model.NumberToWords(sum.ToString()));
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#NetWeight", NetWt.ToString());
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#GrossWeight", GrWt.ToString());
            //    //xlWorkbook.Worksheets[1].Cells.Replace("#CubicMeter", CubMtr.ToString());

            //    Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[5, 1];
            //    Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 5, resultData.Columns.Count];
            //    //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
            //    Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
            //    ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //    Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
            //    Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
            //    Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
            //    Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
            //    c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //    c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //    c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //    c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //    range1.EntireRow.Font.Bold = true;
            //    range1.WrapText = true;
            //    range1.Value = arr;

            //    xlWorkbook.SaveAs(fullPath);
            //    xlWorkbook.Close();
            //    excelApp.Quit();
            //      fileName = DocumentName+".Xlsx";
            //}
            //catch (Exception ex)
            //{
            //    var response = ex.Message;
            //}

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