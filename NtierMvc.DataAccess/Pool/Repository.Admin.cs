using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using NtierMvc.Common;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.DataAccess.Common;
using System.Data.Common;
using NtierMvc.Model.Admin;

namespace NtierMvc.DataAccess.Pool
{
    public partial class Repository
    {
        
        public DataTable GetRoleURLDetails(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null, string deptName = null, string mainMenu = null, string subMenu = null, string access = null)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@skip", skip);
            prams.Add("@pageSize", pageSize);
            prams.Add("@sortColumn", sortColumn);
            prams.Add("@sortColumnDir", sortColumnDir);
            prams.Add("@search", search);
            prams.Add("@deptName", deptName);
            prams.Add("@mainMenu", mainMenu);
            prams.Add("@subMenu", subMenu);
            prams.Add("@access", access);
            string spName = ConfigurationManager.AppSettings["GetRoleURLDetails"];
            return _dbAccess.GetDataTable(spName, prams);
        }


        public string SaveRoleAssigns(RoleAssignEntity objRA)
        {
            string msgCode = "";
            var Params = new Dictionary<string, object>();
            Params.Add("@Role", objRA.DeptName);
            Params.Add("@EmpId", objRA.EmpId);
            Params.Add("@MainMenu", objRA.MainMenu);
            Params.Add("@SubMenu", objRA.SubMenu);
            Params.Add("@ReadWrite", objRA.ReadWrite);

            var SPName = ConfigurationManager.AppSettings["SaveRoleAssigns"];
            _dbAccess.ExecuteNonQuery(SPName, Params, "@o_MsgCode", out msgCode);

            return msgCode;
        }

        public DataTable GetSubMenus(string mainMenu)
        {
            var prams = new Dictionary<string, object>();
            prams.Add("@MainMenu", mainMenu);
            string spName = ConfigurationManager.AppSettings["GetSubMenus"];
            return _dbAccess.GetDataTable(spName, prams);
        }


    }
}
