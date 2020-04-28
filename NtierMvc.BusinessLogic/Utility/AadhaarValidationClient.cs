using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace NtierMvc.BusinessLogic.Utility
{
    public class AadhaarValidationClient
    {
        public AadhaarResponse GetAadhaarSuccessFailureMessage(string strAadharNumber, string strFirstName, string strLastName, string strIsFatherName, string strFatherName, long strMobileNumber, string strDOBDay, string strDOBMonth, string strDOBYear, string strGender, string strIsOnlyYear)
        {
            AadhaarResponse objAadhaarResponse = new AadhaarResponse();
            var AadhaarUserName = ConfigurationManager.AppSettings["Aadhaar-UserName"];
            var AadharPassword = ConfigurationManager.AppSettings["Aadhaar-Password"];
            var AadharSaltKey = ConfigurationManager.AppSettings["Aadhaar-SaltKey"];
            var AadharSecretKey = ConfigurationManager.AppSettings["Aadhaar-SecretKey"];
            var AadharEncryptionKey = ConfigurationManager.AppSettings["Aadhaar-EncryptionKey"];
            var AadharEncryptionIV = ConfigurationManager.AppSettings["Aadhaar-EncryptionIV"];
            var strAadhaarApiUri = ConfigurationManager.AppSettings["Aadhaar-API-URI"];


            CryptLib _crypt = new CryptLib();
            String cypherAadharNumber = _crypt.encrypt(strAadharNumber, AadharEncryptionKey, AadharEncryptionIV);
            String cypherAadharUserName = _crypt.encrypt(AadhaarUserName, AadharEncryptionKey, AadharEncryptionIV);
            String cypherAadharPassword = _crypt.encrypt(AadharPassword, AadharEncryptionKey, AadharEncryptionIV);

            APIAuthData objAPIAuthData = new APIAuthData()
            {
                username = cypherAadharUserName,
                password = cypherAadharPassword,
                salt_key = AadharSaltKey,
                secret_key = AadharSecretKey
            };
            AadhaarRequest objAadhaarRequest = new AadhaarRequest()
            {
                apiAuthData = objAPIAuthData,
                first_name = strFirstName,
                last_name = strLastName,
                is_fathers_name = strIsFatherName,
                fathers_name = strFatherName,
                aadhar_no = cypherAadharNumber,
                mobile_no = strMobileNumber,
                dobday = strDOBDay,
                dobmonth = strDOBMonth,
                dobyear = strDOBYear,
                gender = strGender,
                is_only_year = strIsOnlyYear
            };
            try
            {
                using (var httpClient = new HttpClient())
                {

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var uri = new Uri(strAadhaarApiUri);
                    var stringContent = new StringContent(JsonConvert.SerializeObject(objAadhaarRequest), UnicodeEncoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(uri, stringContent).Result;
                    string res = "";
                    using (HttpContent content = response.Content)
                    {
                        Task<string> result = content.ReadAsStringAsync();
                        res = result.Result;
                    }
                    objAadhaarResponse = JsonConvert.DeserializeObject<AadhaarResponse>(res);
                }
            }
            catch(Exception ex)
            {
                objAadhaarResponse.msg = ex.Message;
            }
            return objAadhaarResponse;
        }
    }

    public class AadhaarResponse
    {
        public string DeviceId { get; set; }
        public string SubAUAtransId { get; set; }
        public string Ret { get; set; }
        public string ResponseTs { get; set; }
        public string ResponseCode { get; set; }
        public string msg { get; set; }
    }


    public class APIAuthData
    {
        public string username { get; set; }
        public string password { get; set; }
        public string secret_key { get; set; }
        public string salt_key { get; set; }
    }


    public class AadhaarRequest
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string is_fathers_name { get; set; }
        public string fathers_name { get; set; }
        public string aadhar_no { get; set; }
        public long mobile_no { get; set; }
        public string dobday { get; set; }
        public string dobmonth { get; set; }
        public string dobyear { get; set; }
        public string gender { get; set; }
        public string is_only_year { get; set; }

        public APIAuthData apiAuthData { get; set; }

    }
}
