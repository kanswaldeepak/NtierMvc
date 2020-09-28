using NtierMvc.Areas.Admin.Models;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Admin;
using NtierMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NtierMvc.Areas.Admin.Controllers
{
    [SessionExpire]
    public class AdminController : Controller
    {
        BaseModel model;
        private AdminManager objManager;

        public AdminController()
        {
            model = new BaseModel();
            objManager = new AdminManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminMaster()
        {
            ViewBag.ListRole = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListMainMenu = model.GetMasterTableStringList("MenuTable", "Id", "UrlName", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListSubMenu = model.GetMasterTableStringList("SubMenuTable", "Id", "Name", "", "", GeneralConstants.ListTypeD);
            ViewBag.ListReadWrite = model.GetDropDownList("Master.Taxonomy",GeneralConstants.ListTypeD,"DropDownId", "DropDownValue", "ReadWrite", "Property");

            return View();
        }

        [HttpGet]
        public ActionResult PartialAdminDashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRoleURLDetails(string AMRoleID = null, string AMMainMenuID = null, string AMSubMenuID = null)
        {
            var draw = Request.Form["draw"];
            var start = Request.Form["start"];
            var length = Request.Form["length"];
            var totalRecords = 0;
            var objList = new List<RoleAssignEntity>();
            try
            {
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];
                var sortColumnDir = Request.Form["order[0][dir]"];
                var pageSize = length != "0" ? Convert.ToInt32(length) : 0;
                var skip = start != "0" ? Convert.ToInt32(start) : 0;
                var search = Request.Form["search[value]"];


                objList = objManager.GetRoleURLDetails(skip, pageSize, sortColumn, sortColumnDir, search);
                totalRecords = objList[0].totalcount;
                var v = (from a in objList select a);
                objList = v.ToList();
            }
            catch (Exception ex)
            {
                //ex.Message;
            }

            return new JsonResult { Data = objList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };            
        }

        [HttpPost]
        public ActionResult SaveRoleAssigns(string Role, string MainMenu, string SubMenu, string ReadWrite)
        {
            RoleAssignEntity raEntity = new RoleAssignEntity();
            raEntity.DeptName = Role;
            raEntity.MainMenu = MainMenu;
            raEntity.SubMenu = SubMenu;
            raEntity.ReadWrite = ReadWrite;
            string result = objManager.SaveRoleAssigns(raEntity);

            string data = string.Empty;
            if (!string.IsNullOrEmpty(result) && (result == GeneralConstants.Inserted || result == GeneralConstants.Updated))
            {
                data = GeneralConstants.SavedSuccess;
            }
            else
            {
                data = GeneralConstants.NotSavedError + ". Reason: " + result;
            }

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



    }
}