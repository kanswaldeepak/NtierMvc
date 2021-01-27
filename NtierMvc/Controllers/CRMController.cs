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
    public class CRMController : Controller
    {
        private LoggingHandler _loggingHandler;
        CustomerEntity cus;
        CustomerEntityDetails custDetail;
        private CustomerManager objManager;
        BaseModel model;

        #region Constructor
        public CRMController()
        {
            _loggingHandler = new LoggingHandler();
            cus = new CustomerEntity();
            custDetail = new CustomerEntityDetails();
            objManager = new CustomerManager();
            model = new BaseModel();
        }

        #endregion

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult CRMMaster()
        {
            //Customer
            ViewBag.ListCustomerName = model.GetMasterTableStringList("Customer", "CustomerName", "CustomerName", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCustomerId = model.GetMasterTableStringList("Customer", "Id", "CustomerId", "", "", GeneralConstants.ListTypeN);
            ViewBag.ListCustStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CustomerStatus", ColumnNames.Property,false,"asc",ColumnNames.DropDownValue);
            ViewBag.ListCountry = model.GetMasterTableStringList("Master.Country", "Id", "Country", "", "", GeneralConstants.ListTypeN);

            //Enquiry
            ViewBag.ListEnqType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListEnqFor = "";
            ViewBag.ListProdGrp = "";
            ViewBag.ListEOQ = ""; // model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Expression of Quote(EOQ)", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDueDate = "";
            ViewBag.ListDeliveryTerms = "";
            ViewBag.ListSubject = "";

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

            //Quotation
            ViewBag.ListDeliveryTerms = model.GetMasterTableStringList("Master.Taxonomy", "DropDownId", "DropDownValue", "DeliveryTerms", "ObjectName", GeneralConstants.ListTypeN);

            //Order
            ViewBag.ListQuoteNo = "";
            ViewBag.ListEnqFor = model.GetMasterTableStringList("QuotationRegister", "EnqFor", "EnqFor", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListQuoteType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "QuoteType", "Property", GeneralConstants.ListTypeN);

            return View();
        }

        [HttpGet]
        public ActionResult PartialCustomer()
        {
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

        public JsonResult FetchCustomerList(string pageIndex, string pageSize, string SearchCustomerName, string SearchCustomerID, string SearchCustomerIsActive)
        {
            SearchCustomerName = SearchCustomerName == "-1" ? string.Empty : SearchCustomerName;
            SearchCustomerID = SearchCustomerID == "-1" ? string.Empty : SearchCustomerID;
            SearchCustomerIsActive = SearchCustomerIsActive == "-1" ? string.Empty : SearchCustomerIsActive;

            custDetail = objManager.GetCustomerDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchCustomerName, SearchCustomerID, SearchCustomerIsActive);
            return new JsonResult { Data = custDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult SaveCustomerDetails(CustomerEntity cusE)
        {
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            cusE.UserInitial = Session["UserName"].ToString();
            cusE.ipAddress = ERPContext.UserContext.IpAddress;
            cusE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveCustomerDetails(cusE);

            TempData["VendorName"] = cusE.CustomerName;
            TempData["UserName"] = cusE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult CustomerPopup(string actionType, string CustomerId = null)
        {
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value

            ViewBag.ListCustomerType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Domestic/International", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListFUNCTION_AREA = model.GetMasterTableStringList(TableNames.Master_FunctionalArea, ColumnNames.id, ColumnNames.FunctionArea);
            ViewBag.ListCountry = model.GetMasterTableStringList(TableNames.Master_Country, ColumnNames.id, ColumnNames.Country);
            ViewBag.ListStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "CustomerStatus", ColumnNames.Property, false, "asc", ColumnNames.DropDownValue);
            

            if (!string.IsNullOrEmpty(Session["UserId"].ToString()))
                cus.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(CustomerId))
                    cus.Id = Convert.ToInt32(CustomerId);
                cus = objManager.CustomerDetailsPopup(cus);
            }
            if (actionType == "ADD")
            {
                cus = objManager.GetUserDetails(cus.UnitNo);
                //eModel = objManager.AddCustomerDetailsPopup(eModel);
            }

            return base.PartialView("~/Views/CRM/_CustomerDetails.cshtml", cus);
        }

        public ActionResult DeleteCustomerDetail(int id)
        {
            if (ModelState.IsValid)
            {
                //string msgCode = objManager.DeleteCustomerDetail(id);
                string msgCode = model.DeleteFormTable("Customer", "Id", id.ToString());

                if (msgCode == GeneralConstants.DeleteSuccess)
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
            List<DropDownEntity> StateList = new List<DropDownEntity>();
            //StateList = model.GetStateDetail(countryId);
            StateList = model.GetDropDownList(TableNames.Master_State, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.state, countryId, ColumnNames.CountryId,false);
            return new JsonResult { Data = StateList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetVendorListForCust(string VendorNatureId)
        {
            var Ddl = model.GetDropDownList("Clientele_Master", GeneralConstants.ListTypeD, "Id", "VendorID", VendorNatureId, "VendorNatureId");
            return new JsonResult { Data = Ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetDdlValueForCustomer(string type, string CountryId = null, string CustomerId = null)
        {
            List<DropDownEntity> ddl = new List<DropDownEntity>();

            if (type == "CountryId")
                ddl = objManager.GetDdlValueForCustomer(type, CountryId);
            //ddl = model.GetDropDownList("Customer", GeneralConstants.ListTypeD, "Id", "CustomerID", CountryId, "Country");
            else if (type == "CustomerName")
                ddl = objManager.GetDdlValueForCustomer(type, "", CustomerId);

            return new JsonResult { Data = ddl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



    }
}