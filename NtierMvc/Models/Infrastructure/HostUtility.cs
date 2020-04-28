using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;

namespace NtierMvc.Models.Infrastructure
{
    public class HostUtility
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
        public static string GetMachineName()
        {
            string hostName = Dns.GetHostName();

            //// Issue #61: For dnxcore machine name does not have domain name like in full framework 
#if !NETSTANDARD1_6
            string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
            if (!hostName.EndsWith(domainName, StringComparison.OrdinalIgnoreCase))
            {
                hostName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", hostName, domainName);
            }
#endif
            return hostName;
        }

        public static bool IsValidIpAddress(string ipAddress)
        {
            IPAddress outParameter;
            ipAddress = ipAddress.Trim();

            if (IPAddress.TryParse(ipAddress, out outParameter))
            {
                if (outParameter.AddressFamily == AddressFamily.InterNetwork)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetSessionIdFromRequest(HttpContext platformContext, string cookieName)
        {
            var sessionId = string.Empty;

            if (platformContext.Request.Cookies != null && platformContext.Request.Cookies.AllKeys.Contains(cookieName))
            {
                var sessionCookieValue = platformContext.Request.Cookies[cookieName].Value;
                if (!string.IsNullOrEmpty(sessionCookieValue))
                {
                    var sessionCookieParts = ((string)sessionCookieValue).Split('|');
                    if (sessionCookieParts.Length > 0)
                    {
                        sessionId = sessionCookieParts[0];
                    }
                }
            }
            return sessionId;
        }
   }
}