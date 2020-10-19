using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Newtonsoft.Json;
using NtierMvc.Model.Account;

namespace NtierMvc.Infrastructure
{
    public class LocalUtility
    {

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }


        public static HttpClient InitializeHttpClient(string baseAddress)
        {
            string url = ConfigurationManager.AppSettings["WebAPIURL"];
            baseAddress = url + baseAddress;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("nl-NL"));
            //string token = param.token.Value;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public static bool CheckUrlForPermission(string area, string controller, string master, string action)
        {
            var user = (UserEntity)HttpContext.Current.Session["UserModel"];
            if (user != null)
            {
                if (user.DeptName == "Administration")
                    return true;
                else if (!string.IsNullOrEmpty(area))
                    return user.Permissions.Any(x => x.PermissionRoute == $"{area}-{controller}-{master}-{action}");
                else
                    return user.Permissions.Any(x => x.PermissionRoute == $"{controller}-{master}-{action}");
            }
            else
            {
                return false;
            }
        }

        public static string CheckForWrite(string area, string controller, string master, string action)
        {
            var user = (UserEntity)HttpContext.Current.Session["UserModel"];
            if (user != null)
            {
                if (!string.IsNullOrEmpty(area) && user.Permissions.Any(x => x.PermissionRoute == $"{area}-{controller}-{master}-{action}"))
                {
                    var res = (from a in user.Permissions
                               where a.PermissionRoute == $"{area}-{controller}-{master}-{action}"
                               select a.PermissionRouteWrite).FirstOrDefault();

                    return res.ToString();
                }
                else if (user.Permissions.Any(x => x.PermissionRoute == $"{controller}-{master}-{action}"))
                {
                    var res = (from a in user.Permissions
                               where a.PermissionRoute == $"{controller}-{master}-{action}"
                               select a.PermissionRouteWrite).FirstOrDefault();

                    return res.ToString();
                }
                else
                    return "Read";
            }
            else
            {
                return "Read";
            }
        }



    }
}