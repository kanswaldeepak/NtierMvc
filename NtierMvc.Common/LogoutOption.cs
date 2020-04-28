using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtierMvc.Common
{
    [Serializable]
    public class LogoutOption
    {
        public enum LogoutCode
        {
            Normal = 1,
            SessionTimeOut = 2,
            ForceLogout = 3,
            NotNormal = 4

        }
    }
}
