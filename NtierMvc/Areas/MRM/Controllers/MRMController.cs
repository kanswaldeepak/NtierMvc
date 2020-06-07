using NtierMvc.Areas.MRM.Models;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Account;
using NtierMvc.Model.Application;
using NtierMvc.Model.MRM;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace NtierMvc.Areas.MRM.Controllers
{
    [SessionExpire]
    public class MRMController : Controller
    {
        private MRMManager objManager;
        BaseModel model;

        public MRMController()
        {
            model = new BaseModel();
            objManager = new MRMManager();
        }

        [HttpGet]
        public ActionResult MRMMaster()
        {
            ViewBag.ListVendorType = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorType", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSupplierId = "";
            ViewBag.ListRMCategory = model.GetMasterTableStringList("Master.RMCategory", "Id", "CategoryName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDeliveryDate = model.GetMasterTableStringList("PurchaseRequest", "DeliveryDate", "DeliveryDate", "", "", GeneralConstants.ListTypeD);

            var UserDetails = (UserEntity)Session["UserModel"];
            ViewBag.DeptName = UserDetails.DeptName;
            return View();
        }

        [HttpGet]
        public ActionResult PRPlanning()
        {
            PRDetailEntity prObj = new PRDetailEntity();
            return PartialView(prObj);
        }

        public JsonResult FetchPRDetailsList(string pageIndex, string pageSize, string SearchTypeId = null, string SearchQuoteNo = null, string SearchSONo = null, string SearchVendorId = null, string SearchVendorName = null, string SearchProductGroup = null)
        {
            SearchTypeId = SearchTypeId == null ? string.Empty : SearchTypeId;
            SearchQuoteNo = SearchQuoteNo == null ? string.Empty : SearchQuoteNo;
            SearchSONo = SearchSONo == null ? string.Empty : SearchSONo;
            SearchVendorId = SearchVendorId == null ? string.Empty : SearchVendorId;
            SearchVendorName = SearchVendorName == null ? string.Empty : SearchVendorName;
            SearchProductGroup = SearchProductGroup == null ? string.Empty : SearchProductGroup;

            var UserDetails = (UserEntity)Session["UserModel"];

            PRDetailEntityDetails prEntity = new PRDetailEntityDetails();
            prEntity = objManager.GetPRDetailsList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), UserDetails.DeptName, SearchTypeId, SearchQuoteNo, SearchSONo, SearchVendorId, SearchVendorName, SearchProductGroup);
            return new JsonResult { Data = prEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult BindVendorBillPopup(string actionType, string Id)
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

            GateEntryEntity vMB = new GateEntryEntity();
            if (actionType == "VIEW" || actionType == "EDIT")
            {
                //if (!string.IsNullOrEmpty(Id))
                //bOM.Id = Convert.ToInt32(Id);
                //eOrder = objManager.BOMPopup(eOrder);
            }
            if (actionType == "ADD")
            {
                //eOrder = objManager.GetUserDetails(oeObj.UnitNo);
            }

            return base.PartialView("~/Areas/DesignEng/Views/DesignEng/_BOMDetails.cshtml");
        }

        public ActionResult PRDetailPopup(string actionType, string PRSetno)
        {
            ViewBag.ListSupplier = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListPriority = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Priority", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListRMcat = model.GetMasterTableStringList("Master.RMCategory", "CategoryName", "CategoryName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListUOM = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "UOM", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUse = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "EndUse", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCostCache = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListAcceptReject = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "AcceptReject", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCommunicate = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "YesNo", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUseNo = "";
            ViewBag.ListPRRequestedOn = "";

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

            var UserDetails = (UserEntity)Session["UserModel"];
            ViewBag.DeptName = UserDetails.DeptName;

            PRDetailEntity prObj = new PRDetailEntity();
            prObj.UserId = UserDetails.UserId;

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                prObj.PRSetno = Convert.ToInt32(PRSetno);
                prObj = objManager.GetSavedPRDetailsPopup(prObj);

                if (prObj.SignStatus == "Entry")
                    prObj.ApprovePerson1Sign = UserDetails.SignImage;
                else if (prObj.SignStatus == "Approved1")
                    prObj.ApprovePerson2Sign = UserDetails.SignImage;
                else if (prObj.SignStatus == "Approved2")
                    ViewBag.PurchaseRequest = UserDetails.SignImage;


                ViewBag.ListPRRequestedOn = "";
            }
            else if (actionType == "ADD")
            {
                prObj = objManager.GetPRDetailsPopup(prObj);
                prObj.ReqFrom = UserDetails.FirstName + " " + UserDetails.LastName;
                prObj.PRdate = DateTime.Now.ToShortDateString();
                prObj.DeptName = UserDetails.DeptName;
                prObj.EntryPersonSign = UserDetails.SignImage;
            }


            return base.PartialView("~/Areas/MRM/Views/MRM/_PRDetailPopup.cshtml", prObj);
        }

        [HttpPost]
        public JsonResult ChangeEndUseNoForQuoteOrSoNo(string EndUse, string quoteType = null)
        {
            List<DropDownEntity> lstEndUseNo = new List<DropDownEntity>();
            lstEndUseNo = model.GetSONoQuoteNoList(EndUse, quoteType);

            return new JsonResult { Data = lstEndUseNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult ChangeEndUseNoForNonPO(string EndUse)
        {
            List<DropDownEntity> lstEndUseNo = new List<DropDownEntity>();
            lstEndUseNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "NonPO", "Property", GeneralConstants.ListTypeD);

            return new JsonResult { Data = lstEndUseNo, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult ChangeRMCat(string CatType)
        {
            List<DropDownEntity> lstRMCat = new List<DropDownEntity>();

            if (CatType == "RM")
                lstRMCat = model.GetDropDownList("Master.RMCategory", GeneralConstants.ListTypeD, "Id", "CategoryName", "", "");
            else if (CatType == "BOI")
                lstRMCat = model.GetDropDownList("Master.BuyOut", GeneralConstants.ListTypeD, "Id", "CategoryName", "", "");

            return new JsonResult { Data = lstRMCat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SavePRDetailsList(PRDetailEntity[] PRDetails)
        {
            model = new BaseModel();
            //ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            //ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            //ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            try
            {
                if (PRDetails != null)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();

                    List<PRDetailEntity> prList = new List<PRDetailEntity>();
                    List<PRDetailEntityBulkSave> itemListBulk = new List<PRDetailEntityBulkSave>();
                    prList = PRDetails.OfType<PRDetailEntity>().ToList();

                    var UserDetails = (UserEntity)Session["UserModel"];

                    foreach (var item in prList)
                    {
                        PRDetailEntityBulkSave newObj = new PRDetailEntityBulkSave();

                        //newObj.Id = item.Id;

                        newObj.QuoteType = item.QuoteType;
                        newObj.PRSetno = item.PRSetno;
                        newObj.PRno = item.PRno;
                        newObj.ReqFrom = item.ReqFrom;
                        newObj.ReqTo = item.ReqTo;
                        newObj.DeptName = item.DeptName;
                        newObj.PRcat = item.PRcat;
                        newObj.Currency = item.Currency;
                        newObj.Priority = item.Priority;
                        newObj.EndUse = item.EndUse;
                        newObj.EndUseNo = item.EndUseNo;
                        newObj.CostCentre = item.CostCentre;
                        newObj.RMcat = item.RMcat;
                        newObj.RMdescription = item.RMdescription;
                        newObj.RMgrade = item.RMgrade;
                        newObj.RMHardness = item.RMHardness;
                        newObj.PSLlevel = item.PSLlevel;
                        newObj.UOM = item.UOM;
                        newObj.SN = item.SN;
                        newObj.OD = item.OD;
                        newObj.WT = item.WT;
                        newObj.Len = item.Len;
                        newObj.QtyReqd = item.QtyReqd;
                        newObj.QtyStock = item.QtyStock;
                        newObj.PRqty = item.PRqty;
                        newObj.UnitPrice = item.UnitPrice;
                        newObj.TotalPrice = item.TotalPrice;
                        newObj.DeliveryDate = item.DeliveryDate;
                        newObj.SupplyTerms = item.SupplyTerms;
                        newObj.DeliveryTerms = item.DeliveryTerms;
                        newObj.PaymentTerms = item.PaymentTerms;
                        newObj.Certificates = item.Certificates;
                        newObj.ApprovedSupplier1 = item.ApprovedSupplier1;
                        newObj.ApprovedSupplier2 = item.ApprovedSupplier2;
                        newObj.ApprovedReject = item.ApprovedReject;
                        newObj.Communicate = item.Communicate;
                        newObj.POno = item.POno;
                        newObj.ExpectedDeliveryDate = item.ExpectedDeliveryDate;
                        newObj.EntryDate = DateTime.Now;
                        newObj.SignStatus = item.SignStatus;
                        newObj.PRStatus = item.PRStatus;
                        newObj.EntryPerson = UserDetails.UserId.ToString();
                        newObj.TotalPRSetPrice = item.TotalPRSetPrice;
                        newObj.PRFavouredOn = item.PRFavouredOn;

                        if (item.DeptName == "Stores")
                        {
                            newObj.ApproveDate1 = DateTime.Now;
                            newObj.SignStatus = "Approved1";
                            newObj.ApprovePerson1 = UserDetails.UserId.ToString();
                        }
                        itemListBulk.Add(newObj);
                    }

                    string result = string.Empty;
                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);
                    objBU.IdentityNo = PRDetails[0].PRSetno;
                    result = objManager.SavePRDetailsList(objBU);

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
                return Json("Unable to save Item Details! Please Provide correct information", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Item Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateApproveReject(string PRSetno, string SignStatus, string PRFavouredOn, string PRStatus)
        {
            string msgCode = string.Empty;
            var UserDetails = (UserEntity)Session["UserModel"];
            msgCode = objManager.UpdateApproveReject(PRSetno, SignStatus, UserDetails.UserId.ToString(), PRFavouredOn, PRStatus);

            return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetPRTableDetails(string PRSetno)
        {
            List<PRDetailEntity> prObjList = new List<PRDetailEntity>();
            prObjList = objManager.GetPRTableDetails(PRSetno);

            return new JsonResult { Data = prObjList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SavePurchaseDetails(string PRSetno, string Communicate, string PONo, string ExpectedDeliveryDate)
        {
            string msgCode = string.Empty;
            var UserDetails = (UserEntity)Session["UserModel"];
            msgCode = objManager.SavePurchaseDetails(PRSetno, Communicate, PONo, ExpectedDeliveryDate, UserDetails.UserId.ToString());

            return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult CreateDownloadDocument(string PRSetNo)
        {
            string FileName = "";
            try
            {

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["PRExcel"]);
                FileName = Path.GetFileName(path);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Sheet1"];

                //string[] DataColumns = { "PRSetNo" };
                //string[] DataParam = { PRSetNo };
                //string[] RequiredColumn = { };

                //List<string> RC = new List<string>();
                //RC.Add("PRno");
                //RC.Add("PRdate");
                //RC.Add("DeliveryDate");
                //RC.Add("ApprovedSupplier1");
                //RC.Add("ApprovedSupplier2");
                //RC.Add("PRStatus");
                //RC.Add("Communicate");
                //RC.Add("POno");
                //RC.Add("ExpectedDeliveryDate");

                //RequiredColumn = RC.ToArray();

                //DataTable resultData = model.GetDataTableForDocument(GeneralConstants.ListTypeD, "PurchaseRequest", DataColumns, DataParam, RequiredColumn);

                //RC.Clear();
                //RC.Add("SN");
                //RC.Add("RMDescription");
                //RC.Add("OD");
                //RC.Add("WT");
                //RC.Add("Len");
                //RC.Add("QtyReqd");
                //RC.Add("QtyStock");
                //RC.Add("PRQty");

                DataTable resultData = objManager.GetPRDataForDocument(PRSetNo);
                DataTable resultList = objManager.GetPRListForDocument(PRSetNo);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#PRno", resultData.Rows[0]["PRno"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PRdate", resultData.Rows[0]["PRdate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#DeliveryDate", resultData.Rows[0]["DeliveryDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ApprovedSupplier1", resultData.Rows[0]["ApprovedSupplier1"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ApprovedSupplier2", resultData.Rows[0]["ApprovedSupplier2"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PRStatus", resultData.Rows[0]["PRStatus"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Communicate", resultData.Rows[0]["Communicate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#POno", resultData.Rows[0]["POno"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ExpectedDeliveryDate", resultData.Rows[0]["ExpectedDeliveryDate"]);

                ////////////////For Image////////////////////
                #region Image
                Microsoft.Office.Interop.Excel.Range cells = xlWorkbook.Worksheets[1].Cells;

                //ReqBy
                Microsoft.Office.Interop.Excel.Range matchReqBy = cells.Find("#RequestedBySign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#RequestedBySign", "");

                string matchAdd = matchReqBy != null ? matchReqBy.Address : null;
                if (matchReqBy != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchReqBy.Row, matchReqBy.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["RequestedBySign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Store
                Microsoft.Office.Interop.Excel.Range matchStore = cells.Find("#StoreSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#StoreSign", "");

                string matchStoreAdd = matchStore != null ? matchStore.Address : null;
                if (matchStoreAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchStore.Row, matchStore.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["StoreSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Approver
                Microsoft.Office.Interop.Excel.Range matchApprove = cells.Find("#ApproverSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#ApproverSign", "");

                string matchApproveAdd = matchApprove != null ? matchApprove.Address : null;
                if (matchApproveAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchApprove.Row, matchApprove.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["ApproverSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Purchase
                Microsoft.Office.Interop.Excel.Range matchPurchase = cells.Find("#PurchaseSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#PurchaseSign", "");

                string matchPurchaseAdd = matchPurchase != null ? matchPurchase.Address : null;
                if (matchPurchaseAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchPurchase.Row, matchPurchase.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["PurchaseSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }
                #endregion
                ////////////////For Image////////////////////


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[10, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 10, resultList.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                }


                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[9, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 9, resultList.Columns.Count];
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

        //Do Not Delete Commented Text Below
        //public ActionResult Download(string fileName)
        //{
        //    string fullPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["TempFolder"]), fileName);

        //    if (System.IO.File.Exists(fullPath))
        //    {
        //        ////Get the temp folder and file path in server
        //        byte[] fileByteArray = System.IO.File.ReadAllBytes(fullPath);
        //        System.IO.File.Delete(fullPath);
        //        return File(fileByteArray, "application/vnd.ms-excel", fileName);

        //    }

        //    else
        //        return Json(new { data = "", errorMessage = "Error While Generating Excel. Contact Support." }, JsonRequestBehavior.AllowGet);
        //}


        [HttpGet]
        public ActionResult POPlanning()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult PODetailsPopup(string actionType, string PRSetno)
        {
            ViewBag.ListPRno = model.GetMasterTableStringList("PurchaseRequest", "PRSetNo", "PRNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListPriority = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Priority", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListRMcat = model.GetMasterTableStringList("Master.RMCategory", "CategoryName", "CategoryName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListUOM = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "UOM", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUse = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "EndUse", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCostCache = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListAcceptReject = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "AcceptReject", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCommunicate = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "YesNo", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUseNo = "";
            ViewBag.ListPRRequestedOn = "";

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

            var UserDetails = (UserEntity)Session["UserModel"];
            ViewBag.DeptName = UserDetails.DeptName;

            PODetailEntity poObj = new PODetailEntity();
            poObj.UserId = UserDetails.UserId;

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                poObj.PRSetno = Convert.ToInt32(PRSetno);

                if (poObj.SignStatus == "Entry")
                    poObj.ApprovePerson1Sign = UserDetails.SignImage;
                else if (poObj.SignStatus == "Approved1")
                    poObj.ApprovePerson2Sign = UserDetails.SignImage;
                else if (poObj.SignStatus == "Approved2")
                    ViewBag.PurchaseRequest = UserDetails.SignImage;

            }
            else if (actionType == "ADD")
            {
                poObj.CompShortName = ConfigurationManager.AppSettings["CompanyShortName"];
                poObj = objManager.GetPODetailsForPopup(poObj);
                poObj.POdate = DateTime.Now.ToShortDateString();
            }

            return base.PartialView("~/Areas/MRM/Views/MRM/_PODetailsPopup.cshtml", poObj);
        }

        [HttpPost]
        public ActionResult SavePODetailsList(PODetailEntity[] PODetails)
        {
            model = new BaseModel();

            try
            {
                if (PODetails != null)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();

                    List<PODetailEntity> prList = new List<PODetailEntity>();
                    List<PODetailEntityBulkSave> itemListBulk = new List<PODetailEntityBulkSave>();
                    prList = PODetails.OfType<PODetailEntity>().ToList();

                    var UserDetails = (UserEntity)Session["UserModel"];

                    foreach (var item in prList)
                    {
                        PODetailEntityBulkSave newObj = new PODetailEntityBulkSave();

                        newObj.PRSetno = item.PRSetno;
                        newObj.PONo = item.PONo;
                        newObj.POSetno = item.POSetno;
                        newObj.POdate = item.POdate;
                        newObj.SN = item.SN;
                        newObj.RMdescription = item.RMdescription;
                        newObj.RMgrade = item.RMgrade;
                        newObj.RMHardness = item.RMHardness;
                        newObj.PSLlevel = item.PSLlevel;
                        newObj.QtyReqd = item.QtyReqd;
                        newObj.QtyStock = item.QtyStock;
                        newObj.PRqty = item.PRqty;
                        newObj.UnitPrice = item.UnitPrice;
                        newObj.TotalPrice = item.TotalPrice;
                        newObj.Discount = item.Discount;
                        newObj.FinalPrice = item.FinalPrice;
                        newObj.WorkNo = item.WorkNo;
                        newObj.DeliveryDate = item.DeliveryDate;
                        newObj.POValidity = item.POValidity;

                        newObj.TotalPRSetPrice = item.TotalPRSetPrice;

                        itemListBulk.Add(newObj);
                    }

                    string result = string.Empty;
                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);
                    objBU.IdentityNo = PODetails[0].PRSetno;
                    result = objManager.SavePODetailsList(objBU);

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
                return Json("Unable to save Item Details! Please Provide correct information", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Item Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FetchPODetailsList(string pageIndex, string pageSize)
        {
            //SearchTypeId = SearchTypeId == null ? string.Empty : SearchTypeId;
            //SearchQuoteNo = SearchQuoteNo == null ? string.Empty : SearchQuoteNo;
            //SearchSONo = SearchSONo == null ? string.Empty : SearchSONo;
            //SearchVendorId = SearchVendorId == null ? string.Empty : SearchVendorId;
            //SearchVendorName = SearchVendorName == null ? string.Empty : SearchVendorName;
            //SearchProductGroup = SearchProductGroup == null ? string.Empty : SearchProductGroup;

            PODetailEntityDetails prEntity = new PODetailEntityDetails();
            prEntity = objManager.GetPODetailsList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            return new JsonResult { Data = prEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult CreateDocumentForPO(string PRSetNo)
        {
            string FileName = "";
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                // open the template in Edit mode
                string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["POExcelFile"]);
                FileName = Path.GetFileName(path);
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = excelApp.Workbooks.Open(Filename: @path, Editable: true);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkbook.Sheets["Sheet1"];

                DataTable resultData = objManager.GetPODetailForDocument(PRSetNo);
                DataTable resultList = objManager.GetPOListDataForDocument(PRSetNo);

                //Getting Single Fields
                xlWorkbook.Worksheets[1].Cells.Replace("#PRno", resultData.Rows[0]["PRno"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PONo", resultData.Rows[0]["PONo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#POSetno", resultData.Rows[0]["POSetno"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#POdate", resultData.Rows[0]["POdate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#PRdate", resultData.Rows[0]["PRdate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#WorkNo", resultData.Rows[0]["WorkNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#DeliveryDate", resultData.Rows[0]["DeliveryDate"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#POValidity", resultData.Rows[0]["POValidity"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Curr", resultData.Rows[0]["Currency"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#EntryPerson", resultData.Rows[0]["EntryPerson"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ApprovePerson1", resultData.Rows[0]["ApprovePerson1"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#ApprovePerson2", resultData.Rows[0]["ApprovePerson2"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Supp1Name", resultData.Rows[0]["Supp1Name"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Supp1Address", resultData.Rows[0]["Supp1Address"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Supp1ContactPerson", resultData.Rows[0]["Supp1ContactPerson"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Supp1ContactNo", resultData.Rows[0]["Supp1ContactNo"]);
                xlWorkbook.Worksheets[1].Cells.Replace("#Supp1Email", resultData.Rows[0]["Supp1Email"]);

                ////////////////For Image////////////////////
                #region Image
                Microsoft.Office.Interop.Excel.Range cells = xlWorkbook.Worksheets[1].Cells;

                //ReqBy
                Microsoft.Office.Interop.Excel.Range matchReqBy = cells.Find("#RequestedBySign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#RequestedBySign", "");

                string matchAdd = matchReqBy != null ? matchReqBy.Address : null;
                if (matchReqBy != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchReqBy.Row, matchReqBy.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["RequestedBySign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Store
                Microsoft.Office.Interop.Excel.Range matchStore = cells.Find("#StoreSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#StoreSign", "");

                string matchStoreAdd = matchStore != null ? matchStore.Address : null;
                if (matchStoreAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchStore.Row, matchStore.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["StoreSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Approver
                Microsoft.Office.Interop.Excel.Range matchApprove = cells.Find("#ApproverSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#ApproverSign", "");

                string matchApproveAdd = matchApprove != null ? matchApprove.Address : null;
                if (matchApproveAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchApprove.Row, matchApprove.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["ApproverSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }

                //Purchase
                Microsoft.Office.Interop.Excel.Range matchPurchase = cells.Find("#PurchaseSign", LookAt: Microsoft.Office.Interop.Excel.XlLookAt.xlPart) as Microsoft.Office.Interop.Excel.Range;
                xlWorkbook.Worksheets[1].Cells.Replace("#PurchaseSign", "");

                string matchPurchaseAdd = matchPurchase != null ? matchPurchase.Address : null;
                if (matchPurchaseAdd != null)
                {
                    Microsoft.Office.Interop.Excel.Range oRange = (Microsoft.Office.Interop.Excel.Range)ws.Cells[matchPurchase.Row, matchPurchase.Column];
                    float Left = (float)((double)oRange.Left);
                    float Top = (float)((double)oRange.Top);
                    const float ImageSize = 40;
                    string filePath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["SignImagePath"]), resultData.Rows[0]["PurchaseSign"].ToString());
                    if (System.IO.File.Exists(filePath))
                        ws.Shapes.AddPicture(filePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, Left, Top, ImageSize, ImageSize);
                }
                #endregion
                ////////////////For Image////////////////////


                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[15, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 2) + 15, resultList.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range = ws.get_Range(c1, c2);
                range.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);

                double CubMtr = 0;
                object[,] arr = new object[resultList.Rows.Count, resultList.Columns.Count];
                for (int r = 0; r <= resultList.Rows.Count - 1; r++)
                {
                    DataRow dr = resultList.Rows[r];
                    for (int c = 0; c < resultList.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                    CubMtr = CubMtr + Convert.ToDouble(dr[7]);
                }

                xlWorkbook.Worksheets[1].Cells.Replace("#TotalValue", CubMtr);
                //xlWorkbook.Worksheets[1].Cells.Replace("#TotalValueWords", NtierMvc.Common.Helper.words(CubMtr, true));

                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[15, 1];
                Microsoft.Office.Interop.Excel.Range c4 = (Microsoft.Office.Interop.Excel.Range)ws.Cells[(resultList.Rows.Count - 1) + 15, resultList.Columns.Count];
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



    }
}