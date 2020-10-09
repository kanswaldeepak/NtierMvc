using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Common;
using NtierMvc.DataAccess.Pool;
using NtierMvc.Model.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NtierMvc.BusinessLogic.Worker
{
    public class AdminWorker : IAdminWorker, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        public AdminWorker()
        {
            _loggingHandler = new LoggingHandler();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
        Repository _repository = new Repository();

        public List<RoleAssignEntity> GetRoleURLDetails(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null, string deptName = null, string mainMenu = null, string subMenu = null, string access = null)
        {
            var entity = new List<RoleAssignEntity>();
            try
            {
                var dt = _repository.GetRoleURLDetails(skip, pageSize, sortColumn, sortColumnDir, search, deptName, mainMenu, subMenu, access);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        RoleAssignEntity re = new RoleAssignEntity();
                        re.ID = dr.IsNull("Id") ? 0 : Convert.ToInt32(dr["Id"]);
                        re.SNo = dr.IsNull("SNo") ? 0 : Convert.ToInt32(dr["SNo"]);
                        re.EmpId = dr.IsNull("EmpId") ? 0 : Convert.ToInt32(dr["EmpId"]);
                        re.EmpCode = dr.IsNull("EmpCode") ? "" : Convert.ToString(dr["EmpCode"]);
                        re.EmpName = dr.IsNull("EmpName") ? "" : Convert.ToString(dr["EmpName"]);
                        re.DeptName = dr.IsNull("DeptName") ? "" : Convert.ToString(dr["DeptName"]);
                        re.MainMenu = dr.IsNull("MainMenu") ? "" : Convert.ToString(dr["MainMenu"]);
                        re.SubMenu = dr.IsNull("SubMenu") ? "" : Convert.ToString(dr["SubMenu"]);
                        re.Access = dr.IsNull("Access") ? "" : Convert.ToString(dr["Access"]);

                        re.totalcount = dr.IsNull("totalcount") ? 0 : Convert.ToInt32(dr["totalcount"]);
                        entity.Add(re);
                    }
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return entity;
        }

        public string SaveRoleAssigns(RoleAssignEntity objRA)
        {
            string result = string.Empty;
            try
            {
                result = _repository.SaveRoleAssigns(objRA);
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return result;
        }


        public List<DropDownEntity> GetSubMenus(string mainMenu)
        {
            var entity = new List<DropDownEntity>();
            try
            {
                var dt = _repository.GetSubMenus(mainMenu);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownEntity ddE = new DropDownEntity();                        
                        ddE.DataStringValueField = dr.IsNull("DataStringValueField") ? "" : Convert.ToString(dr["DataStringValueField"]);
                        ddE.DataTextField = dr.IsNull("DataTextField") ? "" : Convert.ToString(dr["DataTextField"]);
                        entity.Add(ddE);
                    }
                }
            }
            catch (Exception Ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(Ex);
            }
            return entity;
        }


    }
}
