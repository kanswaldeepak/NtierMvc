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


    }
}