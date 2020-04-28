using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using NtierMvc.Infrastructure;
using NtierMvc.Areas.MRM.Models;
using System.Text;
using System.Web;
using NtierMvc.Model.DesignEng;
using NtierMvc.Model.Application;
using NtierMvc.Model.MRM;
using NtierMvc.Model.Account;
using System.Data;

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

            ViewBag.ListQuoteType = model.GetMasterTableStringList("DesignPRP", "Id", "QuoteType", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteNo = model.GetMasterTableStringList("DesignPRP", "Id", "QuoteNo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSONo = model.GetMasterTableStringList("DesignPRP", "Id", "SONo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorId = model.GetMasterTableStringList("DesignPRP", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorName = model.GetMasterTableStringList("DesignPRP", "Id", "VendorID", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListProductGroup = model.GetMasterTableStringList("Master.Product", "Id", "ProductName", "", "", GeneralConstants.ListTypeD);

            return View();
        }

        [HttpGet]
        public ActionResult PRPlanning()
        {
            GateEntryEntity objVNB = new GateEntryEntity();
            return PartialView(objVNB);
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

        public ActionResult PRDetailPopup()
        {
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
            PRDetailEntity prObj = new PRDetailEntity();
            prObj.UserId = UserDetails.UserId;
            prObj = objManager.GetPRDetailsPopup(prObj);

            prObj.ReqFrom = UserDetails.FirstName + " " + UserDetails.LastName;
            prObj.PRdate = DateTime.Now.ToShortDateString();
            prObj.DeptName = UserDetails.DeptName;
            

            //prObj.UserId = Convert.ToInt32(Session["UserId"]);
            //prObj = objManager.GetPRDetailsPopup(prObj);
            //prObj.ReqFrom = Session["UserName"].ToString();
            //prObj.PRdate = DateTime.Now.ToShortDateString();

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

                        
                        itemListBulk.Add(newObj);
                    }


                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);

                    string result = objManager.SavePRDetailsList(objBU);

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

    }
}