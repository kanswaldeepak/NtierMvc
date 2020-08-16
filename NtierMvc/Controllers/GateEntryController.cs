using DocumentFormat.OpenXml.Office.CustomUI;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
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
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "SupplyType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListVendorNature = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorName = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorName", "", "", GeneralConstants.ListTypeD);            
            ViewBag.ListApprovalStatus = model.GetMasterTableStringList("ApproveStatus", "Id", "Status", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListPONo = model.GetMasterTableStringList("RMPO", "POSetno", "PONo", "", "", GeneralConstants.ListTypeD);

            return View();
        }

        [HttpGet]
        public ActionResult PartialInbound()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InboundPopUp(string actionType, string GateNo=null)
        {
            ViewBag.ListModeOfTransport = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Transport", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListPRCat = model.GetMasterTableStringList("Master.Taxonomy", "DropDownValue", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyType", "Property", GeneralConstants.ListTypeN);

            GateEntryEntity orderE = new GateEntryEntity();
            orderE.UnitNo = Session["UserId"].ToString();
            orderE.ViewType = actionType;

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                ViewBag.ListPONo = model.GetMasterTableStringList("RMPO", "POSetno", "PONo", "", "", GeneralConstants.ListTypeD);
                orderE = objManager.InboundDetailsPopup(GateNo);                
            }
            if (actionType == "ADD")
            {
                ViewBag.ListPONo = objManager.GetPoNoDetailsForGE();
            }

            return base.PartialView("~/Views/GateEntry/_InboundPopUp.cshtml", orderE);
        }

        [HttpPost]
        public ActionResult SaveGateEntryDetails(GateEntryEntity arrGE)
        {
            model = new BaseModel();
            
            try
            {
                if (arrGE != null)
                {
                    
                    string result = string.Empty;                    
                    result = objManager.SaveGateEntryDetails(arrGE);

                    string data = string.Empty;
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
                return Json("Unable to save Item Details! Empty Details Provided.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Gate Entry Details! Catch Exception. Please try again later.", JsonRequestBehavior.AllowGet);
            }

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

        public JsonResult FetchInboundList(string pageIndex, string pageSize, string SearchType, string SearchVendorNature, string SearchVendorName, string SearchPONo)
        {
            SearchType = SearchType == "-1" ? string.Empty : SearchType;
            SearchVendorNature = SearchVendorNature == "-1" ? string.Empty : SearchVendorNature;
            SearchVendorName = SearchVendorName == "-1" ? string.Empty : SearchVendorName;
            SearchPONo = SearchPONo == "-1" ? string.Empty : SearchPONo;

            GateEntryEntityDetails vbmDetail = new GateEntryEntityDetails();
            vbmDetail = objManager.FetchInboundList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchType, SearchVendorNature, SearchVendorName, SearchPONo);
            return new JsonResult { Data = vbmDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public JsonResult GetPOTableDetailsForGateEntry(string POSetno, string GateNo = null)
        {
            GateEntryEntityDetails poObj = new GateEntryEntityDetails();
            poObj = objManager.GetPOTableDetailsForGateEntry(POSetno, GateNo);

            return new JsonResult { Data = poObj.lstVBM, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetPODetailFromSupplyType(string SupplyType, string ViewType)
        {
            try
            {
                List<DropDownEntity> ddl = new List<DropDownEntity>();

                if(ViewType == "ADD")
                    ddl = objManager.GetPoNoDetailsForGE();
                else
                ddl = model.GetMasterTableStringList("RMPO", "POSetno", "PONo", SupplyType, "SupplyType", GeneralConstants.ListTypeD);

                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }

        }



    }
}