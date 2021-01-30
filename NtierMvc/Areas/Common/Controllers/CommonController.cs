using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Models;
using System.Web.Mvc;

namespace NtierMvc.Areas.Common.Controllers
{
    [SessionExpire]
    public class CommonController : Controller
    {
        BaseModel model;
        //private AdminManager objManager;

        public CommonController()
        {
            model = new BaseModel();
//            objManager = new CommonManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CommonMaster()
        {
            //ViewBag.ListDeptName = model.GetMasterTableStringList("Master.Department", "Id", "DeptName", "", "", GeneralConstants.ListTypeD);
            //ViewBag.ListEmployee = model.GetMasterTableStringList("Master.Employee", "Id", "EmpName", "", "", GeneralConstants.ListTypeD);
            //ViewBag.ListMainMenu = model.GetMasterTableStringList("MenuTable", "Id", "UrlName", "", "", GeneralConstants.ListTypeD);
            //ViewBag.ListSubMenu = model.GetMasterTableStringList("SubMenuTable", "Id", "Name", "", "", GeneralConstants.ListTypeD);
            //ViewBag.ListReadWrite = model.GetDropDownList("Master.Taxonomy",GeneralConstants.ListTypeD,"DropDownId", "DropDownValue", "ReadWrite", "Property");

            return View();
        }

        [HttpGet]
        public ActionResult PartialCommonDashboard()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult GetRoleURLDetails(string deptName = null, string mainMenu = null, string subMenu = null, string access = null)
        //{
        //    var draw = Request.Form.GetValues("draw").FirstOrDefault();
        //    var start = Request.Form.GetValues("start").FirstOrDefault();
        //    var length = Request.Form.GetValues("length").FirstOrDefault();
        //    var totalRecords = 0;
        //    var objList = new List<RoleAssignEntity>();
        //    try
        //    {
        //        var sortColumn = Request.Form.GetValues("columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]").FirstOrDefault();
        //        var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
        //        var pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        var skip = start != null ? Convert.ToInt32(start) : 0;
        //        var search = Request.Form.GetValues("search[value]").FirstOrDefault();


        //        objList = objManager.GetRoleURLDetails(skip, pageSize, sortColumn, sortColumnDir, search, deptName, mainMenu, subMenu, access);
        //        totalRecords = objList[0].totalcount;
        //        var v = (from a in objList select a);
        //        objList = v.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        //ex.Message;
        //    }

        //    return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = objList }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult SaveCountryDetails(string CountryName, string GeoArea, string GeoCode)
        {
            string result = model.SaveTableData(TableNames.Master_Country, ColumnNames.Country, CountryName, ColumnNames.GEO_AREA, GeoArea, ColumnNames.GEO_CODE, GeoCode);

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