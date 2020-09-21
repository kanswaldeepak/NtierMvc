using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Infrastructure;
using System.Web.UI.WebControls;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class EnquiryController : Controller
    {
        private LoggingHandler _loggingHandler = new LoggingHandler();
        EnquiryManager objManager = new EnquiryManager();
        BaseModel model;

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
            enq.enqEntity = objManager.GetUserDetailsForEnquiry(enq.enqEntity.UnitNo);

            model = new BaseModel();
            return View(enq);
        }

        





    }
}