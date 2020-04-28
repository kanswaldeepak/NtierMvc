using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Infrastructure;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class QuotationController : Controller
    {
        private LoggingHandler _loggingHandler;
        QuotationManager objManager = new QuotationManager();
        BaseModel bModel = new BaseModel();
        QuotationEntity eModel = new QuotationEntity();
        QuotationEntityDetails quote = new QuotationEntityDetails();

        #region Constructor
        public QuotationController()
        {
            //_loggingHandler = new LoggingHandler();
            //objManager = new QuotationManager();
            //bModel = new BaseModel();
            //eModel = new QuotationEntity();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_loggingHandler != null)
        //        {
        //            _loggingHandler.Dispose();
        //            _loggingHandler = null;
        //        }
        //        else if (objManager != null)
        //        {
        //            objManager.Dispose();
        //            objManager = null;
        //            bModel = null;
        //            eModel = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
        #endregion

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult Quotation()
        {
            QuotationEntityDetails quote = new QuotationEntityDetails();
            quote.quoteEntity.UnitNo = Session["UserId"].ToString();
            quote.quoteEntity = objManager.GetUserQuoteDetails(quote.quoteEntity.UnitNo);

            //BaseModel model = new BaseModel();
            //ViewBag.EOQ = model.GetTaxonomyDropDownItems("", "Expression of Quote(EOQ)");
            //ViewBag.LeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            //ViewBag.YesNo = model.GetTaxonomyDropDownItems("", "YesNo");
            //ViewBag.quoteThru = model.GetTaxonomyDropDownItems("", "Quotation Through");

            //quote.lstQuoteEntity = objManager.GetQuotationDetails();

            return View(quote);
        }

        [HttpGet]
        public ActionResult PartialQuotation()
        {
            quote.quoteEntity.UnitNo = Session["UserId"].ToString();
            quote.quoteEntity = objManager.GetUserQuoteDetails(quote.quoteEntity.UnitNo);
            
            //quote.lstQuoteEntity = objManager.GetQuotationDetails();

            return View(quote);
        }

        public JsonResult FetchQuotationList(string pageIndex, string pageSize, string SearchQuoteType, string SearchQuoteNo, string SearchQuoteVendorID, string SearchQuoteVendorName, string SearchQuoteProductGroup, string SearchQuoteEnqFor)
        {
            SearchQuoteType = SearchQuoteType == "-1" ? string.Empty : SearchQuoteType;
            SearchQuoteNo = SearchQuoteNo == "-1" ? string.Empty : SearchQuoteNo;
            SearchQuoteVendorID = SearchQuoteVendorID == "-1" ? string.Empty : SearchQuoteVendorID;
            SearchQuoteVendorName = SearchQuoteVendorName == "-1" ? string.Empty : SearchQuoteVendorName;
            SearchQuoteProductGroup = SearchQuoteProductGroup == "-1" ? string.Empty : SearchQuoteProductGroup;
            SearchQuoteEnqFor = SearchQuoteEnqFor == "-1" ? string.Empty : SearchQuoteEnqFor;

            quote = objManager.GetQuotationDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchQuoteType, SearchQuoteNo, SearchQuoteVendorID, SearchQuoteVendorName, SearchQuoteProductGroup, SearchQuoteEnqFor);
            return new JsonResult { Data = quote, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return QuotDetail.LstCusEnt;
        }

        //[HttpPost]
        //public ActionResult SaveQuotationDetails(QuotationEntity quoteE)
        //{
        //    string result = objManager.SaveQuotationDetails(quoteE);

        //    TempData["VendorName"] = quoteE.VendorName;
        //    TempData["UserName"] = quoteE.UserInitial;
        //    string data = string.Empty;
        //    if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
        //    {
        //        //Payment Gateway
        //        //TempData["StatusMsg"] = "Success";
        //        data = GeneralConstants.SavedSuccess;
        //    }
        //    else
        //    {
        //        //TempData["StatusMsg"] = "Error";
        //        //TempData["StatusMsgBody"] = result;
        //        data = GeneralConstants.NotSavedError;
        //    }

        //    return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        [HttpPost]
        public ActionResult QuotationPopup(string actionType, string QuotationId)
        {
            string countryId = "0";
            ViewBag.ListState = bModel.GetStateDetail(countryId); //Given wrong CountryId to not get any value
            ViewBag.ListVendorId = bModel.GetMasterTableStringList("Clientele_Master", "Id", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVENDORTYPE = bModel.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = bModel.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = bModel.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");
            ViewBag.ListCountry = bModel.GetMasterTableList("Master.Country", "Id", "Country");
            ViewBag.ListProdGrpId = bModel.GetMasterTableStringList("Master.ProductLine", "Id", "Product", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListProdType = bModel.GetMasterTableStringList("ProductType", "Id", "TypeName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCurrency = bModel.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Currency", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = bModel.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);

            eModel.UnitNo = Session["UserId"].ToString();


            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(QuotationId))
                    eModel.Id = Convert.ToInt32(QuotationId);
                eModel = objManager.QuotationDetailsPopup(eModel);
            }
            if (actionType == "ADD")
            {
                eModel = objManager.GetUserQuoteDetails(eModel.UnitNo);
                //eModel = objManager.AddQuotationDetailsPopup(eModel);
            }

            return PartialView("~/Views/Quotation/QuotationPopup.cshtml", eModel);
        }

        public ActionResult GetQuoteVendorDetail(string vendorId)
        {
            eModel = objManager.GetVendorQuoteDetails(vendorId);
            return new JsonResult { Data = eModel.VendorName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult DeleteQuotationDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = objManager.DeleteQuotationDetail(id);
                if (msgCode == "")
                {
                    return RedirectToAction("Quotation");
                }
                else
                {
                    Response.StatusCode = 444;
                    Response.StatusDescription = "Not Saved";
                    return null;
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