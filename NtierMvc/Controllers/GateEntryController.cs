using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class GateEntryController : Controller
    {
        private GateEntryManager objManager;
        BaseModel model;

        public GateEntryController()
        {
            model = new BaseModel();
            objManager = new GateEntryManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GateEntryMaster()
        {
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
        public ActionResult PartialInbound()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InboundPopUp(string actionType)
        {
            ViewBag.ListPONo = model.GetMasterTableStringList("RMPO", "POSetno", "PONo", "", "", GeneralConstants.ListTypeD);            
            ViewBag.ListModeOfTransport = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Transport", "Property", GeneralConstants.ListTypeD);

            GateEntryEntity orderE = new GateEntryEntity();
            orderE.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                //if (!string.IsNullOrEmpty(Id))
                //    orderE.Id = Convert.ToInt32(Id);

                //orderE = objManager.OrderDetailsPopup(orderE);

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

            return base.PartialView("~/Views/GateEntry/_InboundPopUp.cshtml", orderE);
        }

        [HttpPost]
        public ActionResult SaveGateEntry(GateEntryEntity objGE)
        {
            ViewBag.ListVendorNature = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", "", "", GeneralConstants.ListTypeN);


            objGE.UserInitial = Session["UserName"].ToString();
            objGE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveGateEntry(objGE);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + " .Reason : " + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult GetVendorIds(string VendorNatureId)
        {
            BaseModel model = new BaseModel();
            List<DropDownEntity> lstVendors = new List<DropDownEntity>();
            lstVendors = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorID", VendorNatureId, "VendorNatureId", GeneralConstants.ListTypeD);

            return new JsonResult { Data = lstVendors, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult GetVendorDetails(string VendorId)
        {
            BaseModel model = new BaseModel();
            SingleColumnEntity objS = new SingleColumnEntity();
            objS = model.GetSingleColumnValues("Clientele_Master", "VendorName", "VendorName", "Id", VendorId, "City");

            return new JsonResult { Data = objS, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult ChangeEndUseNo(string EndUse)
        {
            BaseModel model = new BaseModel();
            List<DropDownEntity> lstVendors = new List<DropDownEntity>();

            if (EndUse == "SupplyOrder")
                lstVendors = model.GetMasterTableStringList("Orders", "Id", "SoNo", "", "", GeneralConstants.ListTypeD);
            else if (EndUse == "QuotationNumber")
                lstVendors = model.GetMasterTableStringList("QuotationRegister", "Id", "", "", "VendorNatureId", GeneralConstants.ListTypeD);
            else if (EndUse == "Non-PO")
                lstVendors = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "NonPO","Property", GeneralConstants.ListTypeD);            

            return new JsonResult { Data = lstVendors, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchInboundList(string pageIndex, string pageSize, string SearchType, string SearchVendorNature, string SearchVendorName, string SearchBillNo, string SearchBillDate, string SearchItemDescription, string SearchCurrency, string SearchApprovalStatus)
        {
            SearchType = SearchType == "-1" ? string.Empty : SearchType;
            SearchVendorNature = SearchVendorNature == "-1" ? string.Empty : SearchVendorNature;
            SearchVendorName = SearchVendorName == "-1" ? string.Empty : SearchVendorName;
            SearchBillNo = SearchBillNo == "-1" ? string.Empty : SearchBillNo;
            SearchBillDate = SearchBillDate == "-1" ? string.Empty : SearchBillDate;
            SearchItemDescription = SearchItemDescription == "-1" ? string.Empty : SearchItemDescription;
            SearchCurrency = SearchCurrency == "-1" ? string.Empty : SearchCurrency;
            SearchApprovalStatus = SearchApprovalStatus == "-1" ? string.Empty : SearchApprovalStatus;

            GateEntryEntityDetails vbmDetail = new GateEntryEntityDetails();
            vbmDetail = objManager.FetchInboundList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchType, SearchVendorNature, SearchVendorName, SearchBillNo, SearchBillDate, SearchItemDescription, SearchCurrency, SearchApprovalStatus);
            return new JsonResult { Data = vbmDetail.lstVBM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return techDetail.LstCusEnt;
        }

        public JsonResult GetDdlDetailsForList(string SelectedVal, string SelectedId)
        {
            BaseModel model = new BaseModel();
            List<DropDownEntity> lstDdl = new List<DropDownEntity>();
            if (SelectedId == "TypeList")
                lstDdl = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", SelectedVal, "Id", GeneralConstants.ListTypeN);
            else if(SelectedId == "VendorNatureList")
                lstDdl = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorName", SelectedVal, "VendorNatureId", GeneralConstants.ListTypeN);
            else if (SelectedId == "VendorNameList")
                lstDdl = model.GetMasterTableStringList("GateEntry", "Id", "BillNo", SelectedVal, "VendorId", GeneralConstants.ListTypeN);


            return new JsonResult { Data = lstDdl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetDetailsForGateEntry(string POSetno)
        {
            GateEntryEntityDetails poObj = new GateEntryEntityDetails();
            poObj = objManager.GetPOTableDetailsForGateEntry(POSetno);

            return new JsonResult { Data = poObj.lstVBM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}