using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NtierMvc.Model.Customer;
using NtierMvc.Infrastructure;

namespace NtierMvc.Controllers
{
    [SessionExpire]
    public class CustomerController : Controller
    {
        private LoggingHandler _loggingHandler;
        CustomerEntity cus;
        CustomerEntityDetails custDetail;
        private CustomerManager objManager;
        BaseModel model;

        #region Constructor
        public CustomerController()
        {
            _loggingHandler = new LoggingHandler();
            cus = new CustomerEntity();
            custDetail = new CustomerEntityDetails();
            objManager = new CustomerManager();
            model = new BaseModel();
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
        //        if (objManager != null)
        //        {
        //            objManager.Dispose();
        //            objManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}
        #endregion

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult CustomerMaster()
        {
            //Customer
            ViewBag.ListVendorName = model.GetMasterTableStringList("Clientele_Master", "VendorName", "VendorName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorId = model.GetMasterTableStringList("Clientele_Master", "VendorId", "VendorId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorNature = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListFunctionalArea = model.GetMasterTableStringList("Master.FunctionalArea", "Id", "FunctionArea", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListVendorType = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorType", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);


            //Enquiry
            ViewBag.ListProdGrp = model.GetMasterTableStringList("Master.ProductLine", "Id", "Product", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListEOQ = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Expression of Quote(EOQ)", "Property", GeneralConstants.ListTypeN);
            Dictionary<string, string> Months = new Dictionary<string, string>();
            Months.Add(Convert.ToString(DateTime.Now.Month), "Current Month");
            Months.Add(Convert.ToString(DateTime.Now.Month + 3), "Next 3 Months");

            List<DropDownEntity> ListMonths = new List<DropDownEntity>();
            DropDownEntity dDE0 = new DropDownEntity();
            dDE0.DataStringValueField = "";
            dDE0.DataTextField = "Select";
            ListMonths.Add(dDE0);
            DropDownEntity dDE = new DropDownEntity();
            dDE.DataStringValueField = Convert.ToString(DateTime.Now.Month);
            dDE.DataTextField = "Current Month";
            ListMonths.Add(dDE);
            DropDownEntity dDE1 = new DropDownEntity();
            dDE1.DataStringValueField = Convert.ToString(DateTime.Now.Month + 3);
            dDE1.DataTextField = "Next 3 Months";
            ListMonths.Add(dDE1);

            ViewBag.ListMonth = ListMonths;
            //Order
            ViewBag.ListQuoteNo = model.GetMasterTableStringList("QuotationRegister", "Id", "QUOTENO", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListEnqFor = model.GetMasterTableStringList("QuotationRegister", "EnqFor", "EnqFor", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);

            return View();
        }

        //public JsonResult DdlList()
        //{
        //    BaseModel model = new BaseModel();
        //    List<DropDownEntity> Vname = model.GetMasterTableStringList("Clientele_Master", "VendorName", "VendorName");
        //    List<DropDownEntity> Vid = model.GetMasterTableStringList("Clientele_Master", "VendorId", "VendorId");
        //    List<DropDownEntity> Vnature = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
        //    List<DropDownEntity> Farea = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");
        //    List<DropDownEntity> Vtype = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");

        //    var DdlData = Vname.Concat(Vid).Concat(Vnature).Concat(Farea).Concat(Vtype);

        //    return new JsonResult { Data = DdlData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        [HttpGet]
        public ActionResult PartialCustomer()
        {
            //custDetail.cusEnt.UnitNo = Session["UserId"].ToString();
            //custDetail.cusEnt = objManager.GetUserDetails(custDetail.cusEnt.UnitNo);
            //custDetail.LstCusEnt = FetchCustomerList(string.Empty);

            return PartialView(custDetail);
        }

        [HttpGet]
        public ActionResult Customer()
        {
            custDetail.cusEnt.UnitNo = Session["UserId"].ToString();
            custDetail.cusEnt = objManager.GetUserDetails(custDetail.cusEnt.UnitNo);
            //custDetail.LstCusEnt = FetchCustomerList(string.Empty);

            return View(custDetail);
        }

        public JsonResult FetchCustomerList(string pageIndex, string pageSize, string SearchCustName, string SearchCustVendorType, string SearchCustVendorID, string SearchCustVendorNature, string SearchCustFunctionalArea)
        {
            SearchCustName = SearchCustName == "-1" ? string.Empty : SearchCustName;
            SearchCustVendorID = SearchCustVendorID == "-1" ? string.Empty : SearchCustVendorID;
            SearchCustVendorType = SearchCustVendorType == "-1" ? string.Empty : SearchCustVendorType;
            SearchCustVendorNature = SearchCustVendorNature == "-1" ? string.Empty : SearchCustVendorNature;
            SearchCustFunctionalArea = SearchCustFunctionalArea == "-1" ? string.Empty : SearchCustFunctionalArea;

            custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCustName, SearchCustVendorID, SearchCustVendorType, SearchCustVendorNature, SearchCustFunctionalArea);
            return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult SaveCustomerDetails(CustomerEntity cusE)
        {
            ViewBag.ListVENDORTYPE = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveCustomerDetails(cusE);

            TempData["VendorName"] = cusE.VendorName;
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
                //TempData["StatusMsgBody"] = result;
                data = GeneralConstants.NotSavedError;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult CustomerPopup(string actionType, string CustomerId)
        {
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value

            ViewBag.ListVENDORTYPE = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListVENDOR_NATURE = model.GetMasterTableStringList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableStringList("Master.FunctionalArea", "Id", "FunctionArea");
            ViewBag.ListCountry = model.GetMasterTableStringList("Master.Country", "Id", "Country");


            cus.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(CustomerId))
                    cus.CustomerId = Convert.ToInt32(CustomerId);
                cus = objManager.CustomerDetailsPopup(cus);
            }
            if (actionType == "ADD")
            {
                cus = objManager.GetUserDetails(cus.UnitNo);
                //eModel = objManager.AddCustomerDetailsPopup(eModel);
            }

            return base.PartialView("~/Views/Customer/_CustomerDetails.cshtml", cus);
        }

        public ActionResult DeleteCustomerDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = objManager.DeleteCustomerDetail(id);
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

        public ActionResult GetStateDetail(string countryId)
        {
            List<GeographyEntity> StateList = new List<GeographyEntity>();
            StateList = model.GetStateDetail(countryId);
            return new JsonResult { Data = StateList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetVendorListForCust(string VendorNatureId)
        {
            var Ddl = model.GetDropDownList("Clientele_Master",GeneralConstants.ListTypeD,"Id","VendorID",VendorNatureId, "VendorNatureId");
            return new JsonResult { Data = Ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}