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
            ViewBag.ListCountry = model.GetMasterTableList(TableNames.Master_Country, ColumnNames.id, ColumnNames.Country);
            ViewBag.ListState = model.GetMasterTableList(TableNames.Master_State, ColumnNames.id, ColumnNames.state);
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


        public ActionResult PartialAddState()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveStateDetails(string CountryId, string StateName)
        {
            string result = model.SaveTableData(TableNames.Master_State, ColumnNames.CountryId, CountryId, ColumnNames.state, StateName);

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

        public ActionResult DeleteTableDatas(int id, string type)
        {
            if (ModelState.IsValid)
            {
                string msgCode = string.Empty;
                if (type == "Country")
                    msgCode = model.DeleteFromTable(TableNames.Master_State, ColumnNames.id, id.ToString());
                else
                    msgCode = model.DeleteFromTable(TableNames.Master_State, ColumnNames.id, id.ToString());

                if (msgCode == GeneralConstants.DeleteSuccess)
                {
                    return new JsonResult { Data = msgCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public ActionResult GetStateForCountry(string countryId)
        {
            var StateList = model.GetDropDownList(TableNames.Master_State, GeneralConstants.ListTypeD, ColumnNames.id, ColumnNames.state, countryId, ColumnNames.CountryId);
            return new JsonResult { Data = StateList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}