using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.Admin;
using NtierMvc.Model.DesignEng;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Web.Mvc;

namespace NtierMvc.Areas.Admin.Models
{
    public class AdminManager
    {
        public List<RoleAssignEntity> GetRoleURLDetails(int skip, int pageSize, string sortColumn, string sortColumnDir, string search)
        {
            List<RoleAssignEntity> prpList = new List<RoleAssignEntity>();
            var baseAddress = "AdminDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.GetAsync(baseAddress + "/GetRoleURLDetails?skip=" + skip + "&pageSize="+ pageSize+ "&sortColumn="+ sortColumn + "&sortColumnDir="+ sortColumnDir + "&search="+ search).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    prpList = JsonConvert.DeserializeObject<List<RoleAssignEntity>>(data);
                }
            }
            return prpList;
        }

        public string SaveRoleAssigns(RoleAssignEntity raEntity)
        {
            string result = "0";
            var baseAddress = "AdminDetails";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SaveRoleAssigns", raEntity).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    result = JsonConvert.DeserializeObject<string>(data);
                }
            }
            return result;
        }

        
    }
}