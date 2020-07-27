using DocumentFormat.OpenXml.Office.CustomUI;
using NtierMvc.Areas.MRM.Models;
using NtierMvc.Areas.Stores.Models;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Account;
using NtierMvc.Model.Application;
using NtierMvc.Model.Stores;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace NtierMvc.Areas.Stores.Controllers
{
    [SessionExpire]
    public class StoresController : Controller
    {
        private StoresManager objManager;
        private MRMManager objMRMManager;
        BaseModel model;

        public StoresController()
        {
            model = new BaseModel();
            objManager = new StoresManager();
            objMRMManager = new MRMManager();
        }

        // GET: Stores/Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StoresMaster()
        {
            ViewBag.ListVendorType = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorType", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSupplierId = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListRMCategory = model.GetMasterTableStringList("Master.Taxonomy", "DropDownID", "ObjectName", "PRCat", "Property", GeneralConstants.ListTypeD);
            List<DropDownEntity> newlst = model.GetMasterTableStringList("PurchaseRequest", "DeliveryDate", "DeliveryDate", "", "", GeneralConstants.ListTypeD);

            ViewBag.ListSupplyTerms = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListStoresName = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoresName", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListBayNo = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "BAYNO", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListLocation = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Location", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListPostion = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Position", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListDirection = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Direction", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListStoreArea = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "StoreArea", "Property", GeneralConstants.ListTypeD);
            ViewBag.ListSN = "";

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTime;
            foreach (var item in newlst)
            {
                if (DateTime.TryParse(item.DataStringValueField, out dateTime))
                {
                    item.DataStringValueField = Convert.ToDateTime(item.DataStringValueField).ToString("MM-dd-yyyy");
                    item.DataTextField = Convert.ToDateTime(item.DataTextField).ToString("MM-dd-yyyy");
                }
            }

            ViewBag.ListDeliveryDate = newlst;

            return View();
        }


        [HttpGet]
        public ActionResult PartialGoodsReciept()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GRPopup(string actionType, string GRno = null)
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
                orderE = objManager.GetGRDetailsPopup(GRno);
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

            var UserDetails = (UserEntity)Session["UserModel"];
            poObj.lstGREntity[0].StoresIncharge = UserDetails.SignImage;

            return new JsonResult { Data = poObj.lstGREntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FetchGoodsRecieptList(string pageIndex, string pageSize, string SearchVendorTypeId = null, string SearchSupplierId = null, string SearchRMCategory = null, string SearchDeliveryDateFrom = null, string SearchDeliveryDateTo = null)
        {
            SearchVendorTypeId = SearchVendorTypeId == null ? string.Empty : SearchVendorTypeId;
            SearchSupplierId = SearchSupplierId == null ? string.Empty : SearchSupplierId;
            SearchRMCategory = SearchRMCategory == null ? string.Empty : SearchRMCategory;
            SearchDeliveryDateFrom = SearchDeliveryDateFrom == null ? string.Empty : SearchDeliveryDateFrom;
            SearchDeliveryDateTo = SearchDeliveryDateTo == null ? string.Empty : SearchDeliveryDateTo;

            GoodsRecieptEntityDetails grEntity = new GoodsRecieptEntityDetails();
            grEntity = objManager.FetchGoodsRecieptList(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchVendorTypeId, SearchSupplierId, SearchRMCategory, SearchDeliveryDateFrom, SearchDeliveryDateTo);
            return new JsonResult { Data = grEntity, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult SaveGoodsRecieptEntryDetails(GoodsRecieptBulkEntity[] StoreDetails)
        {
            string data = string.Empty;
            var UserDetails = (UserEntity)Session["UserModel"];
            try
            {
                if (StoreDetails != null)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();
                    List<GoodsRecieptBulkEntity> itemListBulk = new List<GoodsRecieptBulkEntity>();
                    itemListBulk = StoreDetails.OfType<GoodsRecieptBulkEntity>().ToList();

                    foreach(var Item in itemListBulk)
                    {
                        Item.StoresIncharge = UserDetails.UserId;
                    }
                    string result = string.Empty;
                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);
                    objBU.IdentityNo = StoreDetails[0].GRno;
                    result = objManager.SaveGoodsRecieptEntryDetails(objBU);

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
                return Json("Unable to save Item Details! Please Provide correct information", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Item Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetSuppliers(string VendorTypeId)
        {
            try
            {
                List<DropDownEntity> ddl = model.GetDropDownList("Clientele_Master", GeneralConstants.ListTypeD, "Id", "VendorId", VendorTypeId, "VendorTypeId");
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }

        }

        public JsonResult GetRMCategories(string SupplierId)
        {
            try
            {
                List<DropDownEntity> ddl = objMRMManager.GetRMCategories(SupplierId);
                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }

        }

        public JsonResult GetDeliveryDates(string RMCategory)
        {
            try
            {
                List<DropDownEntity> ddl = objMRMManager.GetDeliveryDates(RMCategory);

                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime dateTime;
                foreach (var item in ddl)
                {
                    if (DateTime.TryParse(item.DataStringValueField, out dateTime))
                    {
                        item.DataStringValueField = Convert.ToDateTime(item.DataStringValueField).ToString("MM-dd-yyyy");
                        item.DataTextField = Convert.ToDateTime(item.DataTextField).ToString("MM-dd-yyyy");
                    }
                }

                return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                throw;
            }
        }


        public ActionResult DeleteGoodsRecieptDetails(string id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = model.DeleteFormTable("GoodsReciept", "GRno", id);
                if (msgCode != "")
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
    }
}