using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Customer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace NtierMvc.Models
{
    public class ReportManager
    {
        BaseModel model = new BaseModel();

        public string GetWorkAuthReport(string SoNo, string FromDate, string ToDate, string ReportType)
        {
            var baseAddress = "TechnicalDetails";
            string itemEntity = "";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))

            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetWorkAuthReport?SoNo=" + SoNo + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&ReportType=" + ReportType).Result;

                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    itemEntity = JsonConvert.DeserializeObject<string>(data);
                }
            }

            return itemEntity;
        }
        //public string ReportGenrate(string fullPath, string DocumentName)
        //{
        //    string fileName = DocumentName;
        //    switch (DocumentName)
        //    {
        //        case "Customer":
        //            fileName = PrepCustomerReport(fullPath, fileName);
        //            break;
        //        default:
        //            break;
        //    }

        //    return fileName;
        //}
        public string PrepProductPerformanceReport(string fullPath, string SoNo, string FromDate, string ToDate, string ReportType)
        {
            string DocumentName = ReportType;
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ProductPerformance"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                CustomerReport CustReportDetails = new CustomerReport();
                List<CustomerReport> CustList = new List<CustomerReport>();

                string ds = GetWorkAuthReport(SoNo, "", "", ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                //DataTable Dt1 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                {
                    dt1 = ds1.Tables[0];
                    //Dt2 = ds1.Tables[1];


                }
                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[7, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 7, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                object[,] arr = new object[dt1.Rows.Count, dt1.Columns.Count];
                for (int r = 0; r <= dt1.Rows.Count - 1; r++)
                {
                    DataRow dr = dt1.Rows[r];
                    for (int c = 0; c < dt1.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[7, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 7, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
                Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
                Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
                c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //range1.EntireRow.Font.Bold = true;
                range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".Xlsx";
            }
            catch (Exception ex)
            {
                DocumentName = ex.Message;
            }
            return DocumentName;
        }
        public string PrepEnqandQuotoReport(string fullPath, string SoNo, string FromDate, string ToDate, string ReportType)
        {
            string DocumentName = ReportType;
            try
            {


                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["EnqandQutoRegExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                CustomerReport CustReportDetails = new CustomerReport();
                List<CustomerReport> CustList = new List<CustomerReport>();

                string ds = GetWorkAuthReport("", FromDate, ToDate, ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                //DataTable Dt1 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                {
                    dt1 = ds1.Tables[0];

                }
                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 6, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                FinancialYear fy = new FinancialYear(DateTime.Now);
                xlWorkbook.Worksheets[1].Cells.Replace("#FinYear", fy.ToString());

                xlWorkbook.Worksheets[1].Cells.Replace("#TotalEnquiryRecv", dt1.Rows.Count);
                xlWorkbook.Worksheets[1].Cells.Replace("#TotalQuoteSent", dt1.Rows.Count);
                xlWorkbook.Worksheets[1].Cells.Replace("#TotalConversionRate", "100%");


                object[,] arr = new object[dt1.Rows.Count, dt1.Columns.Count];
                for (int r = 0; r <= dt1.Rows.Count - 1; r++)
                {
                    DataRow dr = dt1.Rows[r];
                    for (int c = 0; c < dt1.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 6, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                //Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c4);
                //range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
                Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
                Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
                c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // range1.EntireRow.Font.Bold = true;
                range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".xlsx";
            }
            catch (Exception ex)
            {
                DocumentName = ex.Message;
            }
            return DocumentName;
        }

        public string PrepCustomerReport(string fullPath, string SoNo, string FromDate, string ToDate, string ReportType)
        {
            string DocumentName = ReportType;
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["WorkAuthExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                CustomerReport CustReportDetails = new CustomerReport();
                List<CustomerReport> CustList = new List<CustomerReport>();

                string ds = GetWorkAuthReport(SoNo, "", "", ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                DataTable Dt2 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                {
                    dt1 = ds1.Tables[0];
                    Dt2 = ds1.Tables[1];
                }

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#FileNo", dt1.Rows[0]["FileNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#WAuthNo", dt1.Rows[0]["SoNoView"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#WADate", dt1.Rows[0]["WADate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#SalesPerson", dt1.Rows[0]["CONSIGNEENAME"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PoNo", dt1.Rows[0]["PoNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PoDate", dt1.Rows[0]["PoDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerName", dt1.Rows[0]["PoEntity"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PORD", dt1.Rows[0]["PODOR"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#QuationNuber", dt1.Rows[0]["QuoteNoView"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ContactPerson", dt1.Rows[0]["CONSIGNEENAME"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ConsignmentLocation", dt1.Rows[0]["MODEOFSHIPMENT"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Inspection", dt1.Rows[0]["Inspection"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#APINONAPI", dt1.Rows[0]["APIMONOGRAMREQUIREMENT"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PODeliverDate2", dt1.Rows[0]["PoDeliveryDate"]);


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[22, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 22, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);


                object[,] arr = new object[Dt2.Rows.Count, Dt2.Columns.Count];
                for (int r = 0; r <= Dt2.Rows.Count - 1; r++)
                {
                    DataRow dr = Dt2.Rows[r];
                    for (int c = 0; c < Dt2.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[22, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(Dt2.Rows.Count - 1) + 22, Dt2.Columns.Count];
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

                //  range1.EntireRow.Font.Bold = true;
                //range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".Xlsx";
            }
            catch (Exception ex)
            {
                DocumentName = ex.Message;
            }
            return DocumentName;
        }
        public string PrepProductAuhtReport(string fullPath, string fileName, string pageIndex, string pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            string DocumentName = fileName;
            try
            {
                TechnicalManager objManager = new TechnicalManager(); ;



                var OrderDetails = objManager.GetOrderDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms, SearchPODeliveryDate);
                //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["POPrepExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["POREPORT"];
                List<POReportDetails> POrderList = new List<POReportDetails>();

                var CompanyDetails = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.CommonDetails, ColumnNames.IsActive, "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");

                int count = 1;
                if (OrderDetails != null)

                    foreach (var order in OrderDetails.lstOrderEntity)
                    {
                        POReportDetails poReportDetails = new POReportDetails();
                        poReportDetails.Id = Convert.ToString(count);
                        poReportDetails.PoEntity = order.PoEntity;
                        poReportDetails.PoNo = order.PoNo + " dtd. " + order.PoDate;
                        poReportDetails.SoNoView = order.SoNoView;
                        poReportDetails.WADate = order.WADate;
                        poReportDetails.FileNo = order.FileNo + " / " + order.QuoteNo;
                        //poReportDetails.QuoteNo = cust.email1;
                        poReportDetails.Type = order.PoLocation == "India" ? "Domestic" : "Export";
                        poReportDetails.PoLocation = order.PoLocation;
                        poReportDetails.WorkReference = order.Subject;
                        poReportDetails.WAuthRecDate = "";
                        poReportDetails.IsActive = order.IsActive == "True" ? "Active" : "Inactive";
                        poReportDetails.Remarks = order.Remarks;
                        count = count + 1;
                        POrderList.Add(poReportDetails);
                        poReportDetails = null;


                    }
                //}

                ExtensionMethods em = new ExtensionMethods();
                System.Data.DataTable resultData = em.ToDataTable(POrderList);
                System.Data.DataTable CompanyData = em.ToDataTable(CompanyDetails);

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[7, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 7, resultData.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);



                object[,] arr = new object[resultData.Rows.Count, resultData.Columns.Count];
                for (int r = 0; r <= resultData.Rows.Count - 1; r++)
                {
                    DataRow dr = resultData.Rows[r];
                    for (int c = 0; c < resultData.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[7, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 7, resultData.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
                Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
                Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
                c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // range1.EntireRow.Font.Bold = true;
                range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".Xlsx";
            }
            catch (Exception ex)
            {
                fileName = ex.Message;
            }
            return DocumentName;
        }

        public string PrepCustomerFeedBackReport(string fullPath, string SoNo, string FromDate, string ToDate, string ReportType)
        {

            string DocumentName = ReportType;
            try
            {

                CustomerManager objManager = new CustomerManager();
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustFeedbackExcel"]);

                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                string ds = GetWorkAuthReport(SoNo, FromDate, ToDate, ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                //DataTable Dt1 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                {
                    dt1 = ds1.Tables[0];
                    //Dt2 = ds1.Tables[1];


                }
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerName", dt1.Rows[0]["POENTITY"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustomerAddress", dt1.Rows[0]["Custaddress"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#CustPONO", dt1.Rows[0]["POdetail"]);


                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".Xlsx";
            }
            catch (Exception ex)
            {
                ReportType = ex.Message;
            }
            return DocumentName;
        }


        public string PrepCustomerReport(string fullPath, string fileName, string pageIndex, string pageSize, string SearchCountry, string SearchCustomerID, string SearchCustomerIsActive)
        {
            string DocumentName = fileName;
            try
            {

                CustomerManager objManager = new CustomerManager();

                var CustDetails = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCountry, SearchCustomerID, SearchCustomerIsActive);
                //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustPrepExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                List<CustomerReport> CustList = new List<CustomerReport>();

                var CompanyDetails = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.CommonDetails, ColumnNames.IsActive, "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");

                int count = 1;
                if (CustDetails != null)
                {

                    foreach (var cust in CustDetails.LstCusEnt)
                    {
                        CustomerReport CustReportDetails = new CustomerReport();
                        CustReportDetails.Id = Convert.ToString(count);
                        CustReportDetails.CustomerName = cust.CustomerName;
                        CustReportDetails.CustomerId = cust.CustomerId;
                        CustReportDetails.CustomerType = cust.Country == "India" ? "Domestic" : "Export";
                        CustReportDetails.Address1 = cust.Address1 + "," + cust.Address2 + "," + cust.Address3 + "," + cust.City + "," + cust.State;
                        CustReportDetails.ContactPerson = cust.ContactPerson;
                        CustReportDetails.mob1 = cust.mob1;
                        CustReportDetails.email1 = cust.email1;
                        CustReportDetails.DateOfAssociation = cust.DateOfAssociation;
                        CustReportDetails.Status = cust.Status;
                        CustReportDetails.Remarks = "";
                        count = count + 1;
                        CustList.Add(CustReportDetails);
                        CustReportDetails = null;

                    }
                }

                ExtensionMethods em = new ExtensionMethods();
                System.Data.DataTable resultData = em.ToDataTable(CustList);
                System.Data.DataTable CompanyData = em.ToDataTable(CompanyDetails);

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 6, resultData.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#CurrentDate", System.DateTime.Now.ToString("dd-MM-yyyy"));
                xlWorkbook.Worksheets[1].Cells.Replace("#Column1", "SL.No");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column2", "Customer Name");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column3", "Customer Code");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column4", "Domestic/Export");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column5", "Address");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column6", "Contact Person");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column7", "Contact Numbers");

                xlWorkbook.Worksheets[1].Cells.Replace("#Column8", "Email ID");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column9", "Association Since");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column_10", "Status");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column_11", "Remarks");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column_12", "");

                xlWorkbook.Worksheets[1].Cells.Replace("#Column_13", "");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column_14", "");
                xlWorkbook.Worksheets[1].Cells.Replace("#Column_15", "");
                xlWorkbook.Worksheets[1].Cells.Replace("#CompanyName", CompanyData.Rows[3]["RequiredColumn2"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Heading", DocumentName);
                xlWorkbook.Worksheets[1].Cells.Replace("#Emp1", "Ram Prakash");
                xlWorkbook.Worksheets[1].Cells.Replace("#Emp2", "Kamalakar Pathak");
                xlWorkbook.Worksheets[1].Cells.Replace("#Designation1", "Management Repersentative");
                xlWorkbook.Worksheets[1].Cells.Replace("#Designation2", "Manaaging Director");
                xlWorkbook.Worksheets[1].Cells.Replace("#Sign1", "");
                xlWorkbook.Worksheets[1].Cells.Replace("#Sign2", "");


                object[,] arr = new object[resultData.Rows.Count, resultData.Columns.Count];
                for (int r = 0; r <= resultData.Rows.Count - 1; r++)
                {
                    DataRow dr = resultData.Rows[r];
                    for (int c = 0; c < resultData.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 6, resultData.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range1 = ws.get_Range(c3, c4);
                ws.get_Range(c3, c4).Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                Microsoft.Office.Interop.Excel.Range c5 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 1];
                Microsoft.Office.Interop.Excel.Range c6 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 2];
                Microsoft.Office.Interop.Excel.Range c7 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 4];
                Microsoft.Office.Interop.Excel.Range c8 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[16, 5];
                c5.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c6.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c7.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                c8.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                range1.WrapText = true;
                range1.Value = arr;

                xlWorkbook.SaveAs(fullPath);
                xlWorkbook.Close();
                excelApp.Quit();

                DocumentName = DocumentName + ".Xlsx";
            }
            catch (Exception ex)
            {
                fileName = ex.Message;
            }
            return DocumentName;
        }

        public string PrepConsolidatedReport(string fullPath, string FromDate, string ToDate, string ReportType)
        {
            string DocumentName = ReportType + ".xlsx";
            fullPath = fullPath + ".xlsx";

            try
            {
                string ds = GetWorkAuthReport("", FromDate, ToDate, ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                    dt1 = ds1.Tables[0];

                if (dt1.Rows.Count > 0)
                    CreateExcelFile.CreateExcelDocument(dt1, fullPath, includeAutoFilter: true);
                else
                    DocumentName = "";

            }
            catch (Exception ex)
            {
                DocumentName = ex.Message;
            }
            return DocumentName;
        }

    }
}