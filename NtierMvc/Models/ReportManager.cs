using Newtonsoft.Json;
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
                if (ds1.Tables.Count > 0 && ds1.Tables!=null)
                {
                    dt1 = ds1.Tables[0];
                    //Dt2 = ds1.Tables[1];


                }
                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);


                //Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[5, 1];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 5, resultData.Columns.Count];
                ////Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                //Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                //range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);



                //Getting Single Fields
                //xlWorkbook.Worksheets[1].Cells.Replace("#Remarks", dt1.Rows[0]["FileNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#Qty", dt1.Rows[0]["FileNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#SrNo", dt1.Rows[0]["Id"]);



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

                string ds = GetWorkAuthReport("",FromDate, ToDate, ReportType);
                DataSet ds1 = JsonConvert.DeserializeObject<DataSet>(ds);
                DataTable dt1 = new DataTable();
                //DataTable Dt1 = new DataTable();
                if (ds1.Tables.Count > 0 && ds1.Tables != null)
                {
                    dt1 = ds1.Tables[0];
                    //Dt2 = ds1.Tables[1];


                }
                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(dt1.Rows.Count - 1) + 6, dt1.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);



                //Getting Single Fields

                //DateTime date1   =Convert.ToDateTime(FromDate);
                //DateTime date2 = Convert.ToDateTime(ToDate);
                //int year1 = date1.Year;
                //int year2 = date2.Year;
                xlWorkbook.Worksheets[1].Cells.Replace("#FinYear","2021-2022");

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

                DocumentName = DocumentName + ".Xlsx";
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
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["WorkAuthExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
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
                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);


                //var CompanyDetails = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.EnquiryRegister, ColumnNames.APIMONOGRAMREQUIREMENT, "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");



                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#FileNo", dt1.Rows[0]["FileNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#WAuthNo", dt1.Rows[0]["SoNo"]);
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
                //xlWorkbook.Worksheets[1].Cells.Replace("#APINONAPI", Convert.ToString(dt1.Rows[0]["API"])=="Yes" ?"API":"NON API");

                xlWorkbook.Worksheets[1].Cells.Replace("#PODeliverDate2", dt1.Rows[0]["PoDeliveryDate"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#Remarks", dt1.Rows[0]["FileNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#Qty", dt1.Rows[0]["FileNo"]);
                //xlWorkbook.Worksheets[1].Cells.Replace("#SrNo", dt1.Rows[0]["Id"]);



                object[,] arr = new object[Dt2.Rows.Count, Dt2.Columns.Count];
                for (int r = 0; r <= Dt2.Rows.Count - 1; r++)
                {
                    DataRow dr = Dt2.Rows[r];
                    for (int c = 0; c < Dt2.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }

                }
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[21, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(Dt2.Rows.Count - 1) + 21, Dt2.Columns.Count];
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

              //  range1.EntireRow.Font.Bold = true;
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
        public string PrepProductAuhtReport(string fullPath, string fileName, string pageIndex, string pageSize, string SearchQuoteType = null, string SearchVendorID = null, string SearchProductGroup = null, string SearchDeliveryTerms = null, string SearchPODeliveryDate = null)
        {
            string DocumentName = fileName;
            try
            {
                TechnicalManager objManager = new TechnicalManager(); ;



                var OrderDetails = objManager.GetOrderDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageIndex), SearchQuoteType, SearchVendorID, SearchProductGroup, SearchDeliveryTerms, SearchPODeliveryDate);
                //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["POPrepExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["POREPORT"];
                POReportDetails poReportDetails = new POReportDetails();
                List<POReportDetails> POrderList = new List<POReportDetails>();


                var CompanyDetails = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.CommonDetails, ColumnNames.IsActive, "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");

                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);
                // var CustDetails = model.GetTableDataList("Distinct", "Customer", "", "", "", "", "", "", "", "", "", "", "Id", "CustomerName", "CustomerID", "COUNTRY", "Address1", "ContactPerson", "Mob1", "Email1", "DateOfAssociation", "IsActive");
                int count = 1;
                if (OrderDetails != null)
                    
                    foreach (var order in OrderDetails.lstOrderEntity)
                    {
                     
                        poReportDetails.Id = Convert.ToString(count);
                        poReportDetails.PoEntity = order.PoEntity;
                        poReportDetails.PoNo = order.PoNo + " dtd. " + order.PoDate;
                        //  poReportDetails.PoDate = cust.Country == "India" ? "Domestic" : "Export";
                        poReportDetails.SoNoView = order.SoNoView;
                        poReportDetails.WADate = order.WADate;
                        poReportDetails.FileNo = order.FileNo + " / " + order.QuoteNo;
                        //poReportDetails.QuoteNo = cust.email1;
                        poReportDetails.Type = order.PoLocation == "India" ? "Domestic" : "Export";
                        poReportDetails.PoLocation = order.PoLocation;
                        poReportDetails.WorkReference = "";
                        poReportDetails.WAuthRecDate = "";
                        poReportDetails.IsActive = order.IsActive == "True" ? "Active" : "Inactive";
                        poReportDetails.Remarks = order.Remarks;
                        count = count + 1;
                        POrderList.Add(poReportDetails);
                       


                    }
                //}
                System.Data.DataTable resultData = ToDataTable(POrderList);
                System.Data.DataTable CompanyData = ToDataTable(CompanyDetails);

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[6, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 6, resultData.Columns.Count];
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


        public string PrepCustomerReport(string fullPath, string fileName, string pageIndex, string pageSize, string SearchCustomerName, string SearchCustomerID, string SearchCustomerIsActive)
        {
            string DocumentName = fileName;
            try
            {

                CustomerManager objManager = new CustomerManager();

                var CustDetails = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageIndex), SearchCustomerName, SearchCustomerID, SearchCustomerIsActive);
                //return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CustPrepExcel"]);
                //string path = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings["QuotePrepExcel"]);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Report"];
                CustomerReport CustReportDetails = new CustomerReport();
                List<CustomerReport> CustList = new List<CustomerReport>();

                var CompanyDetails = model.GetTableDataList(GeneralConstants.ListTypeD, TableNames.CommonDetails, ColumnNames.IsActive, "1", "", "", "", "", "", "", "", "", "Property", "Details", "", "", "", "", "", "", "", "");

                //      var name = CompanyDetails.Where(i => i.RequiredColumn1 == "CompanyName").Select(i => i.RequiredColumn2);
                // var CustDetails = model.GetTableDataList("Distinct", "Customer", "", "", "", "", "", "", "", "", "", "", "Id", "CustomerName", "CustomerID", "COUNTRY", "Address1", "ContactPerson", "Mob1", "Email1", "DateOfAssociation", "IsActive");
                int count = 1;
                if (CustDetails != null)
                {

                    foreach (var cust in CustDetails.LstCusEnt)
                    {
                        
                        CustReportDetails.Id = Convert.ToString(count);
                        CustReportDetails.CustomerName = cust.CustomerName;
                        CustReportDetails.CustomerId = cust.CustomerId;
                        CustReportDetails.CustomerType = cust.Country == "India" ? "Domestic" : "Export";
                        CustReportDetails.Address1 = cust.Address1 + "" + cust.Address2 + "" + cust.Address3;
                        CustReportDetails.ContactPerson = cust.ContactPerson;
                        CustReportDetails.mob1 = cust.mob1;
                        CustReportDetails.email1 = cust.email1;
                        CustReportDetails.DateOfAssociation = cust.DateOfAssociation;
                        CustReportDetails.Status = cust.Status == "Null" ? "Active" : "Inactive";
                        CustReportDetails.Remarks = "";
                        count=count+1;
                        CustList.Add(CustReportDetails);


                    }
                }
                System.Data.DataTable resultData = ToDataTable(CustList);
                System.Data.DataTable CompanyData = ToDataTable(CompanyDetails);

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[5, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 5, resultData.Columns.Count];
                //Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1, 1];
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
                xlWorkbook.Worksheets[1].Cells.Replace("#Emp1", "RamaPrakash");
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

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[5, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultData.Rows.Count - 1) + 5, resultData.Columns.Count];
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

                range1.EntireRow.Font.Bold = true;
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
    }
}