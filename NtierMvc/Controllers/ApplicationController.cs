using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Data;
using log4net.Repository.Hierarchy;
using NtierMvc.Models;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Application;
using NtierMvc.Common;

namespace DVETPrivateITI.Web.Controllers
{
    [ActionModel]
    [SessionExpire]
    public partial class ApplicationController : Controller
    {
        // GET: Application
        BaseModel model;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Home()
        {
            //AccountManager accountMgr = new AccountManager();
            return View("~/Views/Application/Dashboard.cshtml");

            //ApplicationComplStatusEntity statusEntity = accountMgr.GetApplicationComplStatus(ERPContext.UserContext.RegistrationID);
            //ERPContext.ApplicationComplStatus = statusEntity;

            //if (ERPContext.UserContext.UserRoles[0].RoleId == 5)
            //{
            //    return View("~/Views/Application/ResearcherHome.cshtml");
            //}
            //else
            //{
            //    return View("~/Views/Application/NewInstitute.cshtml");
            //}


            //if (MahaITContext.UserContext.AffiliationType == AffiliationType.NEW)
            //{
            //    return View("~/Views/Application/NewInstitute.cshtml");
            //}
            //else if (MahaITContext.UserContext.AffiliationType !="")
            //{
            //    return View("~/Views/Application/OtherInstitute.cshtml");
            //}            
            //else
            //{
            //    return View();
            //}

        }

        [HttpPost]
        public ActionResult SaveNewItemInDdl(string type, string Nametbl, string Value, string Property = null, string ColumnName = null, string Value1=null, string ColumnName1 = null)
        {
            model = new BaseModel();
            AddDdlEntity ddlEntity = new AddDdlEntity();
            ddlEntity.type = type;
            ddlEntity.Nametbl = Nametbl;
            ddlEntity.Value = Value;
            ddlEntity.Property = Property;
            ddlEntity.ColumnName = ColumnName;
            ddlEntity.Value1 = Value1;
            ddlEntity.ColumnName1 = ColumnName1;

            List<DropDownEntity> result = model.SaveNewItemInDdl(ddlEntity);
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}