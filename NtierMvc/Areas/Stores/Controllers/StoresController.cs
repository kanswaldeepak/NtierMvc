using NtierMvc.Areas.Stores.Models;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Application;
using NtierMvc.Model.Stores;
using NtierMvc.Models;
using System;
using System.Web.Mvc;

namespace NtierMvc.Areas.Stores.Controllers
{
    [SessionExpire]
    public class StoresController : Controller
    {
        private StoresManager objManager;
        BaseModel model;

        public StoresController()
        {
            model = new BaseModel();
            objManager = new StoresManager();
        }

        // GET: Stores/Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StoresMaster()
        {
            //ViewBag.ListGateControlNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "QuoteType", "Property", GeneralConstants.ListTypeD);            
            //ViewBag.ListRMCat = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeD);

            return View();
        }


        [HttpGet]
        public ActionResult PartialGoodsReciept()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GRPopup(string actionType)
        {

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


            ViewBag.ListGateControlNo = model.GetMasterTableStringList("GateEntry", "GateNo", "GateControlNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListPRCat = model.GetMasterTableStringList("Master.Taxonomy", "DropDownValue", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "SupplyType", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListStoresName = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoresName", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListBayNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "BAYNO", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListLocation = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Location", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListPostion = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Position", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListDirection = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Direction", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListStoreArea = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoreArea", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSN = "";


            GoodsRecieptEntity orderE = new GoodsRecieptEntity();

            if (actionType == "VIEW" || actionType == "EDIT")
            {

            }
            if (actionType == "ADD")
            {
                orderE = objManager.GetGRDetailsPopup();

            }

            return base.PartialView("~/Areas/Stores/Views/Stores/_GoodsRecieptPopUp.cshtml", orderE);
        }

        [HttpPost]
        public JsonResult GetGRValuesFromSupplyType(string SupplyType)
        {
            try
            {
                var ddl = model.GetDropDownList("GateEntry", GeneralConstants.ListTypeD, "GateNo", "GateControlNo", SupplyType, "SupplyType");
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }
        }

        public JsonResult GetDetailForGateControlNo(string GateControlNo)
        {
            GoodsRecieptEntityDetails poObj = new GoodsRecieptEntityDetails();
            poObj = objManager.GetDetailForGateControlNo(GateControlNo);

            return new JsonResult { Data = poObj.lstGREntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchGoodsRecieptList(string pageIndex, string pageSize)
        {
            GoodsRecieptEntityDetails grEntity = new GoodsRecieptEntityDetails();
            grEntity = objManager.FetchGoodsRecieptList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize));
            return new JsonResult { Data = grEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveGoodsRecieptEntryDetails(GoodsRecieptEntity grE)
        {
            string data = string.Empty;

            try
            {
                string result = objManager.SaveGoodsRecieptEntryDetails(grE);

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

    }
}