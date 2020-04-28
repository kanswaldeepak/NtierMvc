using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    public class GetLocalIPAddress
    {
        public static string GetIPAddress()
        {
            try
            {
                return Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception)
            {
                return "";
            }   
        }
    }
}
