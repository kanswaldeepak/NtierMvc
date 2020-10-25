using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Model.Account;
using NtierMvc.Model;
using NtierMvc.Common;
using NtierMvc.Model.Application;
using NtierMvc.Model.Admin;

namespace NtierMvc.BusinessLogic.Interface
{
    public interface IAdminWorker
    {
        
        List<RoleAssignEntity> GetRoleURLDetails(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null, string deptName = null, string mainMenu = null, string subMenu = null, string access = null);

        string SaveRoleAssigns(RoleAssignEntity objBill);
        string SaveAdminAssigns(RoleAssignEntity objBill);

        List<DropDownEntity> GetSubMenus(string mainMenu);

        List<RoleAssignEntity> GetAdminAssigns(string skip = null, string pageSize = null, string sortColumn = null, string sortColumnDir = null, string search = null);

    }
}
