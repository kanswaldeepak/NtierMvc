using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMvc.Areas.Stores.Controllers
{
    [SessionExpire]
    public class StoresController : Controller
    {
        //private StoresManager objManager;
        BaseModel model;

        public StoresController()
        {
            model = new BaseModel();
            //objManager = new StoresManager();
        }

        // GET: Stores/Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StoresMaster()
        {
            ViewBag.ListType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "QuoteType", "Property",
                GeneralConstants.ListTypeD);
            ViewBag.ListVendorNature = "";
            ViewBag.ListVendorName = "";
            ViewBag.ListBillDate = model.GetMasterTableStringList("GateEntry", "Id", "BillDate", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListFunctionArea = "";
            ViewBag.ListFinancialYear = model.GetMasterTableStringList("GateEntry", "FinancialYear", "FinancialYear", "", "",
                GeneralConstants.ListTypeN);
            ViewBag.ListApprovalStatus = model.GetMasterTableStringList("ApproveStatus", "Id", "Status", "", "", GeneralConstants.ListTypeN);

            return View();
        }


        [HttpGet]
        public ActionResult PartialStoresInbound()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InboundPopUp(string actionType)
        {
            ViewBag.ListPONo = model.GetMasterTableStringList("RMPO", "POSetno", "PONo", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListVendorNature = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorId = model.GetMasterTableStringList("Clientele_Master", "Id", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListEndUse = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "EndUse", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEndUseNo = "";
            ViewBag.ListUom = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Uom", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListSupplyType = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "SupplyTerms", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListSCCNO = model.GetMasterTableStringList("SCCNO", "Id", "Heading", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCostCentre = "";
            ViewBag.ListApproveStatus = model.GetMasterTableStringList("ApproveStatus", "Id", "Status", "", "", GeneralConstants.ListTypeN);

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

            return base.PartialView("~/Areas/Views/Stores/_InboundStoresPopUp.cshtml", orderE);
        }



    }
}