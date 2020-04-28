using NtierMvc.Common;
using NtierMvc.Models;
using NtierMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web.Mvc;
using NtierMvc.Infrastructure;
using NtierMvc.Model.HR;
using NtierMvc.Areas.HRDepartment.Models;
using NtierMvc.Controllers;

namespace NtierMvc.Areas.HRDepartment.Controllers
{
    [SessionExpire]
    public class HRController : Controller
    {
        // GET: HRDepartment/HR
        private LoggingHandler _loggingHandler;
        EmployeeEntity empObj;
        PayrollEntity payObj;
        EmployeeEntityDetails eEnDetail;
        private HRManager objManager;
        BaseModel model;

        #region Constructor
        public HRController()
        {
            _loggingHandler = new LoggingHandler();
            empObj = new EmployeeEntity();
            payObj = new PayrollEntity();
            eEnDetail = new EmployeeEntityDetails();
            objManager = new HRManager();
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
        public ActionResult HRDepartmentMaster()
        {
            //HRDepartment
            ViewBag.ListEmployeeName = model.GetMasterTableStringList("Master.Employee", "Id", "EmpName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDesignation = model.GetMasterTableStringList("Master.Designation", "Id", "DesignationName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);

            return View();
        }

        [HttpGet]
        public ActionResult PartialHRDepartment()
        {
            return PartialView(eEnDetail);
        }

        [HttpGet]
        public ActionResult HRDepartment()
        {
            eEnDetail.empEnt.UnitNo = Session["UserId"].ToString();
            eEnDetail.empEnt = objManager.GetUserDetails(eEnDetail.empEnt.UnitNo);

            return View(eEnDetail);
        }

        //Employee Start
        public JsonResult FetchEmployeeList(string pageIndex, string pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null)
        {
            SearchEmployeeNameId = SearchEmployeeNameId == null ? string.Empty : SearchEmployeeNameId;
            SearchDesignation = SearchDesignation == null ? string.Empty : SearchDesignation;
            SearchDepartment = SearchDepartment == null ? string.Empty : SearchDepartment;

            eEnDetail = objManager.GetEmployeeDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchEmployeeNameId, SearchDesignation, SearchDepartment);
            return new JsonResult { Data = eEnDetail, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        [HttpPost]
        public ActionResult SaveEmployeeDetails(EmployeeEntity hrE)
        {
            ViewBag.ListEmployeeName = model.GetMasterTableList("Master.Vendor", "Id", "VendorType");
            ViewBag.ListDesignation = model.GetMasterTableList("Master.Vendor", "Id", "VendorNature");
            ViewBag.ListDepartment = model.GetMasterTableList("Master.FunctionalArea", "Id", "FunctionArea");

            hrE.UserInitial = Session["UserName"].ToString();
            hrE.ipAddress = ERPContext.UserContext.IpAddress;
            hrE.UnitNo = Session["UserId"].ToString();

            //For Image Upload
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;

                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/Areas/HRDepartment/EmployeeImage"), fileName));
                    hrE.EmpImage = "/Areas/HRDepartment/EmployeeImage/" + fileName;
                    flag = true;
                }
                catch (Exception)
                {
                    Message = "File upload failed! Please try again";
                }

            }
            //Image Upload End

            string result = string.Empty;
            string data = string.Empty;
            TempData["UserName"] = hrE.UserInitial;

            if (flag)
            {
                result = objManager.SaveEmployeeDetails(hrE);
                if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                {
                    //TempData["StatusMsg"] = "Success";
                    data = GeneralConstants.SavedSuccess;
                }
                else
                {
                    //TempData["StatusMsg"] = "Error";
                    //TempData["StatusMsgBody"] = result;
                    data = GeneralConstants.NotSavedError;
                }
            }
            else
                data = GeneralConstants.NotSavedError;

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult HREmpPopup(string actionType, string HREmpId)
        {
            string countryId = "0";
            ViewBag.ListState = model.GetStateDetail(countryId); //Given wrong CountryId to not get any value
            ViewBag.ListEmpType = model.GetMasterTableList("Master.EmpType", "Id", "EmpTypeName");
            ViewBag.ListCountry = model.GetMasterTableList("Master.Country", "Id", "Country");
            ViewBag.ListGender = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "Gender", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListBloodGroup = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "BloodGroup", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListBloodGroupType = model.GetMasterTableStringList("Master.Taxonomy", "dropdownId", "dropdownvalue", "BloodGroupType", "Property", GeneralConstants.ListTypeN);
            ViewBag.ListDesignation = model.GetMasterTableStringList("Master.Designation", "Id", "DesignationName");
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);

            empObj.UnitNo = Session["UserId"].ToString();

            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(HREmpId))
                    empObj.Id = Convert.ToInt32(HREmpId);
                empObj = objManager.HREmpPopup(empObj);
            }
            if (actionType == "ADD")
            {
                empObj = objManager.GetUserDetails(empObj.UnitNo);
                //eModel = objManager.AddHRDepartmentDetailsPopup(eModel);
            }

            return base.PartialView("~/Areas/HRDepartment/Views/HR/_HREmpDetails.cshtml", empObj);
        }

        public ActionResult DeleteEmployeeDetail(int id)
        {
            if (ModelState.IsValid)
            {
                string msgCode = objManager.DeleteEmployeeDetail(id);
                if (msgCode == "Deleted")
                {
                    //return RedirectToAction("HRDepartment");
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

        //Employee End

        //Salary Start
        [HttpGet]
        public ActionResult PartialHRPayroll()
        {
            int j = 1900;
            List<DropDownEntity> yr = new List<DropDownEntity>();
            for (int i = 0; i <= 199; i++)
            {
                DropDownEntity year = new DropDownEntity();
                year.DataStringValueField = j.ToString();
                year.DataTextField = j.ToString();
                yr.Add(year);
                j++;
            }

            List<DropDownEntity> mnth = new List<DropDownEntity>();
            for (int i = 1; i <= 12; i++)
            {
                DropDownEntity mnthObj = new DropDownEntity();
                mnthObj.DataStringValueField = i.ToString();
                mnthObj.DataTextField = i.ToString();
                mnth.Add(mnthObj);
            }

            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);
            ViewBag.Year = yr;
            ViewBag.Month = mnth;
            //ViewBag.ListEmpCode = model.GetMasterTableStringList("Master.Employee", "Id", "EmpCode", "", "", GeneralConstants.ListTypeD);
            return PartialView("~/Areas/HRDepartment/Views/HR/_HRPayrollDetails.cshtml", payObj);
        }

        [HttpPost]
        public ActionResult SavePayrollDetails(PayrollEntity payE)
        {
            payE.UserInitial = Session["UserName"].ToString();
            payE.ipAddress = ERPContext.UserContext.IpAddress;
            payE.UnitNo = Session["UserId"].ToString();

            string result = objManager.SavePayrollDetails(payE);

            TempData["UserName"] = payE.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
                TempData["StatusMsg"] = "Success";
            }
            else
            {
                data = GeneralConstants.NotSavedError;
                TempData["StatusMsg"] = "Failure";
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetEmpPayrollData(int EmpId, int Yr = 0, int mnth = 0)
        {
            payObj = objManager.GetEmpPayrollData(EmpId, Yr, mnth);
            return new JsonResult { Data = payObj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        public JsonResult GetEmpDetailsForPayroll(string Data, string Type)
        {
            List<DropDownEntity> ListEmp = new List<DropDownEntity>();
            if (Type == "EmpName")
            {
                ListEmp = model.GetMasterTableStringList("Master.Employee", "EmpName", "EmpName", Data, "Dept", GeneralConstants.ListTypeD);
            }
            else if (Type == "EmpCode")
            {
                ListEmp = model.GetMasterTableStringList("Master.Employee", "Id", "EmpCode", Data, "EmpName", GeneralConstants.ListTypeN);
            }
            return new JsonResult { Data = ListEmp, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //return custDetail.LstCusEnt;
        }

        //Salary End

        //Leave Management Start

        [HttpGet]
        public ActionResult PartialLeaveManagement()
        {
            LeaveManagement lMgmt = new LeaveManagement();
            ViewBag.ListDepartment = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);
            //ViewBag.ListEmpCode = model.GetMasterTableStringList("Master.Employee", "Id", "EmpCode", "", "", GeneralConstants.ListTypeD);
            return PartialView("~/Areas/HRDepartment/Views/HR/_HrLeaveManagement.cshtml", lMgmt);
        }


        [HttpPost]
        public ActionResult SaveEmpLeaveDetails(LeaveManagement lvMgmt)
        {
            lvMgmt.UserInitial = Session["UserName"].ToString();
            lvMgmt.ipAddress = ERPContext.UserContext.IpAddress;
            lvMgmt.UnitNo = Session["UserId"].ToString();

            string result = objManager.SaveEmpLeaveDetails(lvMgmt);

            TempData["UserName"] = lvMgmt.UserInitial;
            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                //Payment Gateway
                data = GeneralConstants.SavedSuccess;
                TempData["StatusMsg"] = "Success";
            }
            else
            {
                data = GeneralConstants.NotSavedError;
                TempData["StatusMsg"] = "Failure";
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult GetEmpLeaveDetails(string empId = null)
        {
            LeaveManagementEntityDetails lvMgmt = new LeaveManagementEntityDetails();
            lvMgmt = objManager.GetEmpLeaveList(Convert.ToInt32(empId));
            return new JsonResult { Data = lvMgmt, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Leave Management End
    }
}