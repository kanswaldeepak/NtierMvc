﻿using NtierMvc.Common;
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
using System.Data;
using System.Web;
using System.Configuration;
using System.Text;

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

        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountController));
        // GET: Login

        [HttpGet]
        public ActionResult HRDepartmentMaster()
        {
            //HRDepartment
            ViewBag.ListEmployeeName = model.GetMasterTableStringList(TableNames.Master_Employee, ColumnNames.id, ColumnNames.EMPNAME, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDesignation = model.GetMasterTableStringList(TableNames.Master_Designation, ColumnNames.id, ColumnNames.DesignationName , "", "", GeneralConstants.ListTypeD);
            ViewBag.ListDepartment = model.GetDropDownList(TableNames.Master_Department, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.DeptName, "", "", true);
            ViewBag.ListEmployeeStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "EmployeeStatus", ColumnNames.Property);
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
        public JsonResult FetchEmployeeList(string pageIndex, string pageSize, string SearchEmployeeNameId = null, string SearchDesignation = null, string SearchDepartment = null, string SearchEmpStatus = null)
        {
            SearchEmployeeNameId = SearchEmployeeNameId == null ? string.Empty : SearchEmployeeNameId;
            SearchDesignation = SearchDesignation == null ? string.Empty : SearchDesignation;
            SearchDepartment = SearchDepartment == null ? string.Empty : SearchDepartment;
            SearchEmpStatus = SearchEmpStatus == null ? string.Empty : SearchEmpStatus;

            eEnDetail = objManager.GetEmployeeDetails(Convert.ToInt32(pageIndex), Convert.ToInt32(pageSize), SearchEmployeeNameId, SearchDesignation, SearchDepartment, SearchEmpStatus);
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
                    System.IO.Directory.CreateDirectory(Server.MapPath(ConfigurationManager.AppSettings["EmployeeImage"]));
                    file.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["EmployeeImage"]), fileName));
                    hrE.EmpImage = fileName;
                    flag = true;
                }
                catch (Exception)
                {
                    Message = " File upload failed! Please try again";
                }

            }
            //Image Upload End

            string result = string.Empty;
            string data = string.Empty;
            TempData["UserName"] = hrE.UserInitial;

            if (flag)
            {
                result = objManager.SaveEmployeeDetails(hrE);
                int sets;

                bool set = int.TryParse(result, out sets);

                if (set)
                {
                    if (Convert.ToInt32(result) > 0)
                        data = result;
                    else
                        data = "0";
                }
                else
                    data = result;

                //if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
                //{
                //    data = GeneralConstants.SavedSuccess;
                //}

            }
            else
                data = "0";

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ActionResult HREmpPopup(string actionType, string HREmpId)
        {
            string countryId = "0";
            ViewBag.ListState = model.GetMasterTableStringList(TableNames.Master_State, ColumnNames.id, ColumnNames.state); //Given wrong CountryId to not get any value
            ViewBag.ListEmpType = model.GetMasterTableList(TableNames.Master_EmpType, ColumnNames.id, ColumnNames.EmpTypeName);
            ViewBag.ListCountry = model.GetMasterTableStringList(TableNames.Master_Country, ColumnNames.id, ColumnNames.Country);
            ViewBag.ListGender = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "Gender", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.GenderTitleList = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "title", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListBloodGroup = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "BloodGroup", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListBloodGroupType = model.GetMasterTableStringList(TableNames.Master_Taxonomy, ColumnNames.DropDownID, ColumnNames.DropDownValue, "BloodGroupType", ColumnNames.Property, GeneralConstants.ListTypeN);
            ViewBag.ListDesignation = model.GetDropDownList(TableNames.Master_Designation, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.DesignationName, "", "", true);
            ViewBag.ListDepartment = model.GetMasterTableStringList(TableNames.Master_Department, ColumnNames.id, ColumnNames.DeptName, "", "", GeneralConstants.ListTypeD);
            ViewBag.ListEmployeeStatus = model.GetDropDownList(TableNames.Master_Taxonomy, GeneralConstants.ListTypeN, ColumnNames.DropDownID, ColumnNames.DropDownValue, "EmployeeStatus", ColumnNames.Property, true);

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

        [HttpPost]
        public ActionResult GetStateDetail(string countryId)
        {
            //List<GeographyEntity> StateList = new List<GeographyEntity>();
            //StateList = model.GetStateDetail(countryId);
            var StateList = model.GetDropDownList("Master.State", GeneralConstants.ListTypeD, "Id", "State", countryId, "CountryId");
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

        public ActionResult HRCertificatePopup(string actionType, string EmpId)
        {
            HRCertificatesEntity hrCE = new HRCertificatesEntity();
            if (actionType == "VIEW" || actionType == "EDIT")
            {
                if (!string.IsNullOrEmpty(EmpId))
                {
                    hrCE = objManager.HRCertificates(Convert.ToInt32(EmpId));
                }
            }

            return base.PartialView("~/Areas/HRDepartment/Views/HR/_HRCertificates.cshtml", hrCE);
        }

        [HttpPost]
        public ActionResult SaveExperienceDetails(HRExperienceEntity[] ExpDetails)
        {
            model = new BaseModel();

            try
            {
                if (ExpDetails != null && ExpDetails.Length > 1)
                {
                    BulkUploadEntity objBU = new BulkUploadEntity();
                    objBU.DataRecordTable = new DataTable();

                    List<HRExperienceEntity> prList = new List<HRExperienceEntity>();
                    List<HRExperienceEntityBulkSave> itemListBulk = new List<HRExperienceEntityBulkSave>();
                    prList = ExpDetails.OfType<HRExperienceEntity>().ToList();

                    foreach (var item in prList)
                    {
                        HRExperienceEntityBulkSave newObj = new HRExperienceEntityBulkSave();

                        newObj.EmpId = item.EmpId;
                        newObj.SN = item.SN;
                        newObj.Employer = item.Employer;
                        newObj.Designation = item.Designation;
                        newObj.PeriodFrom = item.PeriodFrom;
                        newObj.PeriodTo = item.PeriodTo;

                        itemListBulk.Add(newObj);
                    }

                    string result = string.Empty;
                    ExtensionMethods lsttodt = new ExtensionMethods();
                    objBU.DataRecordTable = lsttodt.ToDataTable(itemListBulk);
                    objBU.IdentityNo = ExpDetails[0].EmpId;
                    result = objManager.SaveExperienceDetailsList(objBU);

                    string data = string.Empty;
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
                return Json("No Data Entered Or Found for Experience Details. Employee Details Saved Successfully!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Unable to save your Experience Details! Please try again later.", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult SaveEmpCertificates(string EmpId)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["EmployeeCertificates"]);
            StringBuilder strB = new StringBuilder();
            HttpFileCollectionBase files = Request.Files;
            string result = string.Empty;
            string fname = string.Empty;
            string CertificateName = string.Empty;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    string extension = System.IO.Path.GetExtension(file.FileName);
                    string Name = file.FileName.Substring(0, file.FileName.Length - extension.Length);

                    fname = Name + DateTime.Now.Millisecond + extension;
                }

                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);

                file.SaveAs(path + fname);
                CertificateName = fname.ToString();
                result = objManager.SaveEmpCertificates(CertificateName, EmpId);

                if (string.IsNullOrEmpty(result) && (result != GeneralConstants.Inserted))
                {
                    if (System.IO.File.Exists(fname))
                        System.IO.File.Delete(fname);

                    result = GeneralConstants.NotSavedError + " DataBase Problem.";
                }
                else
                {
                    result = GeneralConstants.SavedSuccess + " File Saved.";
                }

            }
            return Json(result + files.Count + " Files Uploaded!");
        }

        public JsonResult GetEmpCertificates(string EmpId)
        {
            List<TableRecordsEntity> NewStr = new List<TableRecordsEntity>();
            NewStr = model.GetTableDataList(GeneralConstants.ListTypeD, "EmpCertificate", "EmpId", EmpId, "", "", "", "", "", "", "", "", "Id", "CertValue");
            return Json(NewStr);
        }

        [HttpPost]
        public JsonResult DeleteCertificates(string CertId)
        {
            string result = "";
            SingleColumnEntity scE = new SingleColumnEntity();

            try
            {
                scE = model.GetSingleColumnValues("EmpCertificate", "CertValue", "CertValue", "Id", CertId);

                if (!string.IsNullOrEmpty(scE.DataValueField1))
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["EmployeeCertificates"]);

                    if (System.IO.File.Exists(path + scE.DataValueField1))
                        System.IO.File.Delete(path + scE.DataValueField1);

                    result = model.DeleteFromTable("EmpCertificate", "Id", CertId);
                }


            }
            catch (Exception Ex)
            {
                result = GeneralConstants.NotDeletedError + " " + Ex.Message;
            }


            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetEmpExpDetails(string EmpId)
        {
    
            var masterList = model.GetTableDataList(GeneralConstants.ListTypeD, "EmployeeExperience", "EmpId", EmpId, "", "", "", "", "", "", "", "", "SN", "Employer", "Designation", "PeriodFrom", "PeriodTo");

            return new JsonResult { Data = masterList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}