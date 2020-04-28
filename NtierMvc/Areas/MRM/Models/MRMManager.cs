using Newtonsoft.Json;
using NtierMvc.Common;
using NtierMvc.Infrastructure;
using NtierMvc.Model;
using NtierMvc.Model.MRM;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace NtierMvc.Areas.MRM.Models
{
    public class MRMManager
    {

        public PRDetailEntity GetPRDetailsPopup(PRDetailEntity Model)
        {
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/GetPRDetailsPopup", Model).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    Model = JsonConvert.DeserializeObject<PRDetailEntity>(data);
                }
            }
            return Model;
        }

        public string SavePRDetailsList(BulkUploadEntity objBU)
        {
            string result = "0";
            var baseAddress = "MRMDetail";
            using (HttpClient client = LocalUtility.InitializeHttpClient(baseAddress))
            {
                HttpResponseMessage response = client.PostAsJsonAsync(baseAddress + "/SavePRDetailsList", objBU).Result;
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