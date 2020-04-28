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
    public class EnquiryController : Controller
    {
        private LoggingHandler _loggingHandler = new LoggingHandler();
        EnquiryManager objManager = new EnquiryManager();
        BaseModel model = new BaseModel();

        //public EnquiryController()
        //{
        //    _loggingHandler = new LoggingHandler();
        //    objManager = new EnquiryManager();
        //}

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
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult Enquiry()
        {
            
            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq.enqEntity.UnitNo = Session["UserId"].ToString();
            enq.enqEntity = objManager.GetUserDetails(enq.enqEntity.UnitNo);

            //BaseModel model = new BaseModel();
            //ViewBag.EOQ = model.GetTaxonomyDropDownItems("", "Expression of Quote(EOQ)");
            //ViewBag.LeadTimeDuration = model.GetTaxonomyDropDownItems("", "Time");
            //ViewBag.YesNo = model.GetTaxonomyDropDownItems("", "YesNo");
            //ViewBag.EnqThru = model.GetTaxonomyDropDownItems("", "Enquiry Through");

            return View(enq);
        }

        public JsonResult FetchEnquiryList(string pageIndex, string pageSize, string SearchEnqName, string SearchEnqVendorID = null, string SearchProductGroup = null, string SearchMonth = null, string SearchEOQ = null)
        {
            
            SearchEnqName = SearchEnqName == "-1" ? string.Empty : SearchEnqName;
            SearchEnqVendorID = SearchEnqVendorID == "-1" ? string.Empty : SearchEnqVendorID;
            SearchProductGroup = SearchProductGroup == "-1" ? string.Empty : SearchProductGroup;
            SearchMonth = SearchMonth == "-1" ? string.Empty : SearchMonth;
            SearchEOQ = SearchEOQ == "-1" ? string.Empty : SearchEOQ;

            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq = objManager.GetEnquiryDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchEnqName, SearchEnqVendorID, SearchProductGroup, SearchMonth, SearchEOQ);
            return new JsonResult { Data = enq, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpGet]
        public ActionResult PartialEnquiry()
        {
            EnquiryEntityDetails enq = new EnquiryEntityDetails();
            enq.enqEntity.UnitNo = Session["UserId"].ToString();
            enq.enqEntity = objManager.GetUserDetails(enq.enqEntity.UnitNo);

            return View(enq);
        }

        [HttpPost]
        public ActionResult SaveEnquiryDetails(EnquiryEntity cusE)
        {
            string result = objManager.SaveEnquiryDetails(cusE);

            TempData["VendorName"] = cusE.VendorId;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                //TempData["StatusMsg"] = "Success";
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                //TempData["StatusMsg"] = "Error";
                TempData["StatusMsgBody"] = result;
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult EnquiryPopup(string actionType, string enquiryId)
        {

            BaseModel bModel = new BaseModel();
            ViewBag.ListEOQ = bModel.GetTaxonomyDropDownItems("", "Expression of Quote(EOQ)");
            ViewBag.ListLeadTimeDuration = bModel.GetTaxonomyDropDownItems("", "Time");
            ViewBag.YesNo = bModel.GetTaxonomyDropDownItems("", "YesNo");
            ViewBag.ListEnqThru = bModel.GetTaxonomyDropDownItems("", "Enquiry Through");
            ViewBag.ListEnqType = bModel.GetTaxonomyDropDownItems("", "Domestic/International");
            ViewBag.ListCountry = bModel.GetMasterTableList("Master.Country", "Id","Country");
            ViewBag.ListProdGrp = bModel.GetMasterTableList("Master.ProductLine", "Id", "Product");
            ViewBag.ListEnqMode = bModel.GetTaxonomyDropDownItems("", "ContactType"); 

            EnquiryEntity eModel = new EnquiryEntity();
            eModel.UnitNo = Session["UserId"].ToString();
            ViewBag.ListVendorId = bModel.GetMasterTableStringList("Clientele_Master", "Id", "VendorName", eModel.UnitNo, "UnitNo",GeneralConstants.ListTypeN);
            ViewBag.ListCity = objManager.GetCityName();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(enquiryId))
                    eModel.EnquiryId = Convert.ToInt32(enquiryId);
                eModel = objManager.EnquiryDetailsPopup(eModel);
            }
            if (actionType == "ADD")
            {
                eModel = objManager.GetUserDetails(eModel.UnitNo);
                //eModel = objManager.AddEnquiryDetailsPopup(eModel);
            }

            return PartialView("~/Views/Enquiry/_EnquiryDetails.cshtml", eModel);
        }

        public ActionResult DeleteEnquiryDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = objManager.DeleteEnquiryDetail(id);
                if (msgCode == "")
                {
                    //return RedirectToAction("Customer");
                    return new JsonResult { Data = GeneralConstants.DeleteSuccess, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    return new JsonResult { Data = GeneralConstants.NotDeletedError, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    //Response.StatusCode = 444;
                    //Response.StatusDescription = "Not Saved";
                    //return null;
                }
            }
            else
            {
                Response.StatusCode = 444;
                Response.Status = "Not Saved";
                return null;
            }

        }

        [HttpPost]
        public ActionResult GetVendorDetailForEnquiry(string vendorId)
        {
            EnquiryEntity eModel = new EnquiryEntity();
            eModel = objManager.GetVendorDetailForEnquiry(vendorId);
            return new JsonResult { Data = eModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}