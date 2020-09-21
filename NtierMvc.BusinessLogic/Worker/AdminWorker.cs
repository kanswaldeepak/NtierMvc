using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using NtierMvc.DataAccess.Pool;
using NtierMvc.BusinessLogic.Utility;
using System.Configuration;
using System.IO;
using NtierMvc.BusinessLogic.Interface;
using NtierMvc.Model.Account;
using NtierMvc.Common;
using NtierMvc.Model;
using System.Web;
using Helper = NtierMvc.BusinessLogic.Utility.Helper;
using NtierMvc.Model.Application;
using NtierMvc.Model.Admin;

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

        public List<RoleAssignEntity> GetRoleURLDetails(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null)
        {
            var entity = new List<RoleAssignEntity>();
            try
            {
                var dt = _repository.GetRoleURLDetails(skip, pageSize, sortColumn, sortColumnDir, search);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        RoleAssignEntity re = new RoleAssignEntity();
                        re.ID = dr.IsNull("Id") ? 0 : Convert.ToInt32(dr["Id"]);
                        re.SNo = dr.IsNull("SNo") ? 0 : Convert.ToInt32(dr["SNo"]);
                        re.DeptName = dr.IsNull("DeptName") ? "" : Convert.ToString(dr["DeptName"]);
                        re.MainMenu = dr.IsNull("MainMenu") ? "" : Convert.ToString(dr["MainMenu"]);
                        re.SubMenu = dr.IsNull("SubMenu") ? "" : Convert.ToString(dr["SubMenu"]);

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
    }
}
