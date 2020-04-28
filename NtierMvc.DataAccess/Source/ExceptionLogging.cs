using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.DataAccess.Source;

namespace NtierMvc.DataAccess
{
    public static class ExceptionLogging
    {

        public static void SendExcepToDB(Exception exdb)
        {
            
            DatabaseAccess _dAccess = new DatabaseAccess();
            var parms = new Dictionary<string, object>();
            parms.Add("@ExceptionMsg", exdb.Message.ToString());
            parms.Add("@ExceptionType", exdb.GetType().Name.ToString());
            parms.Add("@ExceptionSource", exdb.StackTrace?.ToString() ?? "");
            parms.Add("@ExceptionURL", "");
            var spName = "[ExceptionLoggingToDataBase]";
            _dAccess.ExecuteNonQuery(spName, parms);
        }

        public static void SendExcepToDBInnerException(Exception exdb)
        {
            string ErrorMessage = string.Empty;
            if (exdb.InnerException != null && !string.IsNullOrEmpty(exdb.InnerException.Message))
            {
                ErrorMessage = "Error Message: " + exdb.Message + " Inner Exception: " + exdb.InnerException.Message;
            }
            else
            {
                ErrorMessage = "Error Message: " + exdb.Message;
            }

            DatabaseAccess _dAccess = new DatabaseAccess();
            var parms = new Dictionary<string, object>();
            parms.Add("@ExceptionMsg", ErrorMessage);
            parms.Add("@ExceptionType", exdb.GetType().Name.ToString());
            parms.Add("@ExceptionSource", exdb.StackTrace.ToString());
            parms.Add("@ExceptionURL", "");
            var spName = "[ExceptionLoggingToDataBase]";
            _dAccess.ExecuteNonQuery(spName, parms);
        }

        public static void SendExcepMessageToDB(string StrCheckSumString,string strChecksum,string strPlainText, string strencryptedstring, string strSource, string strType)
        {
            DatabaseAccess _dAccess = new DatabaseAccess();
            var parms = new Dictionary<string, object>();
            parms.Add("@CheckSumString", StrCheckSumString);
            parms.Add("@Checksum", strChecksum);
            parms.Add("@PlainText", strPlainText);
            parms.Add("@EncryptedString", strencryptedstring);
            parms.Add("@ExceptionType", strType);
            parms.Add("@ExceptionSource", strSource);
            parms.Add("@ExceptionURL", "");
            var spName = "[PaymentDetailsLoggingToDataBase]";
            _dAccess.ExecuteNonQuery(spName, parms);
        }

    }
}
