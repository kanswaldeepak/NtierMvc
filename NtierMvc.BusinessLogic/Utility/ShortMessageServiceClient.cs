using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NtierMvc.BusinessLogic.Utility
{
    public class ShortMessageServiceClient
    {
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ShortMessageServiceClient));  //Declaring Log4Net  
        /// <summary>
        /// _smsApiUri = "http://msdgweb.mgov.gov.in/esms/sendsmsrequest"
        /// </summary>
        string _smsApiUri = ConfigurationManager.AppSettings["SMS-API-URI"];

        public string SendMobileMessage(List<string> mobileNos, string msgBody)
        {
            try
            {
                var username = ConfigurationManager.AppSettings["SMS-Username"];
                var password = ConfigurationManager.AppSettings["SMS-Password"];
                var senderId = ConfigurationManager.AppSettings["SMS-SenderID"];
                var secureKey = ConfigurationManager.AppSettings["SMS-SecureKey"];
                msgBody = msgBody.Replace('\n', ' ').Replace('\r', ' ').Replace("  ", " ");
                msgBody = msgBody.Trim(' ');
                var output = SendBulkSms(username, password, senderId, string.Join(",", mobileNos), msgBody, secureKey);

                return output;

            }
            catch (Exception ex)
            {
                //Logger.Error("SMS Bulk", ex);
                return ex.Message;
                //throw ex;
            }

        }

        /// <summary>
        /// Method for sending single SMS.
        /// </summary>
        /// <param name="username"> Registered user name
        /// <param name="password"> Valid login password
        /// <param name="senderid">Sender ID
        /// <param name="mobileNo"> valid Single Mobile Number
        /// <param name="message">Message Content
        /// <param name="secureKey">Department generate key by login to services portal


        // Method for sending single SMS.

        public string SendSingleSms(string username, string password, string senderid, string mobileNo, string message,
            string secureKey)

        {
            //Latest Generated Secure Key
            Stream dataStream;
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(_smsApiUri);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            string encryptedPassword = EncryptedPasswod(password);
            string newsecureKey = HashGenerator(username, senderid, message, secureKey);
            string smsservicetype = "singlemsg"; //For single message.
            string query =
                $"username={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(encryptedPassword)}&smsservicetype={HttpUtility.UrlEncode(smsservicetype)}&content={HttpUtility.UrlEncode(message)}&mobileno={HttpUtility.UrlEncode(mobileNo)}&senderid={HttpUtility.UrlEncode(senderid)}&key={HttpUtility.UrlEncode(newsecureKey)}";



            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;



            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            string status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;

        }


        /// <summary>
        /// Method for sending bulk SMS.
        /// </summary>
        /// <param name="username"> Registered user name
        /// <param name="password"> Valid login password
        /// <param name="senderid">Sender ID
        /// <param name="mobileNo"> valid Mobile Numbers
        /// <param name="message">Message Content
        /// <param name="secureKey">Department generate key by login to services portal

        // method for sending bulk SMS

        public string SendBulkSms(string username, string password, string senderid, string mobileNos, string message,
            string secureKey)

        {
            Stream dataStream;
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(_smsApiUri);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            string encryptedPassword = EncryptedPasswod(password);
            string newsecureKey = HashGenerator(username, senderid, message, secureKey);
            Console.Write(newsecureKey);
            Console.Write(encryptedPassword);

            string smsservicetype = "bulkmsg"; // for bulk msg

            string query =
                $"username={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(encryptedPassword)}&smsservicetype={HttpUtility.UrlEncode(smsservicetype)}&content={HttpUtility.UrlEncode(message)}&bulkmobno={HttpUtility.UrlEncode(mobileNos)}&senderid={HttpUtility.UrlEncode(senderid)}&key={HttpUtility.UrlEncode(newsecureKey)}";
            Console.Write(query);

            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;

            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            string status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;

        }


        /// <summary>
        /// method for Sending unicode..
        /// </summary>
        /// <param name="username"> Registered user name
        /// <param name="password"> Valid login password
        /// <param name="senderid">Sender ID
        /// <param name="mobileNo"> valid Mobile Numbers
        /// <param name="unicodemessage">Unicodemessage Message Content
        /// <param name="secureKey">Department generate key by login to services portal

        //method for Sending unicode..

        public string SendUnicodeSms(string username, string password, string senderid, string mobileNos,
            string unicodemessage, string secureKey)

        {
            Stream dataStream;
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(_smsApiUri);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            string uConvertedmessage = "";

            foreach (char c in unicodemessage)
            {
                int j = (int)c;
                string sss = "&#" + j + ";";
                uConvertedmessage = uConvertedmessage + sss;
            }
            string encryptedPassword = EncryptedPasswod(password);
            string newsecureKey = HashGenerator(username, senderid, uConvertedmessage, secureKey);


            string smsservicetype = "unicodemsg"; // for unicode msg
            string query =
                $"username={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(encryptedPassword)}&smsservicetype={HttpUtility.UrlEncode(smsservicetype)}&content={HttpUtility.UrlEncode(uConvertedmessage)}&bulkmobno={HttpUtility.UrlEncode(mobileNos)}&senderid={HttpUtility.UrlEncode(senderid)}&key={HttpUtility.UrlEncode(newsecureKey)}";


            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }




        /// <summary>
        /// Method for sending OTP MSG.
        /// </summary>
        /// <param name="username"> Registered user name
        /// <param name="password"> Valid login password
        /// <param name="senderid">Sender ID
        /// <param name="mobileNo"> valid single  Mobile Number
        /// <param name="message">Message Content
        /// <param name="secureKey">Department generate key by login to services portal

        // Method for sending OTP MSG.
        public string SendMobileOTPMessage(string mobileNo, string msgBody)
        {
            try
            {
                var username = ConfigurationManager.AppSettings["SMS-Username"];
                var password = ConfigurationManager.AppSettings["SMS-Password"];
                var senderId = ConfigurationManager.AppSettings["SMS-SenderID"];
                var secureKey = ConfigurationManager.AppSettings["SMS-SecureKey"];
                msgBody = msgBody.Replace('\n', ' ').Replace('\r', ' ').Replace("  ", " ");
                msgBody = msgBody.Trim(' ');
                var output = SendOtpmsg(username, password, senderId, string.Join(",", mobileNo), msgBody, secureKey);

                return output;

            }
            catch (Exception ex)
            {
                //Logger.Error("SMS Bulk", ex);
                return ex.Message;
                //throw ex;
            }

        }
        public string SendOtpmsg(string username, string password, string senderid, string mobileNo, string message,
            string secureKey)

        {
            Stream dataStream;
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(_smsApiUri);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            string encryptedPassword = EncryptedPasswod(password);
            string key = HashGenerator(username, senderid, message, secureKey);

            string smsservicetype = "otpmsg"; //For OTP message.

            string query =
                $"username={HttpUtility.UrlEncode(username)}&password={HttpUtility.UrlEncode(encryptedPassword)}&smsservicetype={HttpUtility.UrlEncode(smsservicetype)}&content={HttpUtility.UrlEncode(message)}&mobileno={HttpUtility.UrlEncode(mobileNo)}&senderid={HttpUtility.UrlEncode(senderid)}&key={HttpUtility.UrlEncode(key)}";



            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;



            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            string status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;

        }

        /// <summary>
        /// Method to get Encrypted the password
        /// </summary>
        /// <param name="password"> password as String"

        protected string EncryptedPasswod(string password)
        {

            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
            byte[] pp = sha1.ComputeHash(encPwd);
            // static string result = System.Text.Encoding.UTF8.GetString(pp);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {

                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();

        }

        /// <summary>
        /// Method to Generate hash code 
        /// </summary>
        /// <param name="secureKey">your last generated Secure_key

        protected string HashGenerator(string username, string senderId, string message, string secureKey)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(username).Append(senderId).Append(message).Append(secureKey);
            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
            byte[] secKey = sha1.ComputeHash(genkey);

            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < secKey.Length; i++)
            {
                sb1.Append(secKey[i].ToString("x2"));
            }
            return sb1.ToString();
        }

    }
}
