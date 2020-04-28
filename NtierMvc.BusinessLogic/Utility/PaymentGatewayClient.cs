using NtierMvc.Model;
using NtierMvc.Model.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;
using System.Net.Http.Headers;
using System.IO;

namespace NtierMvc.BusinessLogic.Utility
{
    public class PaymentGatewayClient
    {
        public NameValueCollection GetPaymentGatewayNameValueCollection(string strRegistrationNo, string strAmount, string strPaymentRTUURL)
        {
            NameValueCollection data = new NameValueCollection();
            try
            {
                var strPaymentCID = ConfigurationManager.AppSettings["Payment-CID"];
                //var strPaymentRTUURL = ConfigurationManager.AppSettings["Payment-RTUURL"];
                var strPaymentKey = ConfigurationManager.AppSettings["Payment-EncryptionDecryptionKey"];
                var strPaymentChecksumKey = ConfigurationManager.AppSettings["Payment-ChecksumKey"];
                var strPaymentVER = ConfigurationManager.AppSettings["Payment-VER"];
                var strPaymentTYP = ConfigurationManager.AppSettings["Payment-TYP"];
                var strPaymentCNY = ConfigurationManager.AppSettings["Payment-CNY"];
                var strPaymentRE1 = ConfigurationManager.AppSettings["Payment-RE1"];
                var strPaymentRE2 = ConfigurationManager.AppSettings["Payment-RE2"];
                var strPaymentRE3 = ConfigurationManager.AppSettings["Payment-RE3"];
                var strPaymentRE4 = ConfigurationManager.AppSettings["Payment-RE4"];
                var strPaymentRE5 = ConfigurationManager.AppSettings["Payment-RE5"];
                String strRID = string.Empty;
                String strCRN = string.Empty;
                strRID = strCRN = DateTime.Now.ToString("yyyyMMddhhmmss");
                string StrCheckSumString = strPaymentCID + strRID + strCRN + strAmount + strPaymentChecksumKey;
                string Checksum = sha256_hash(StrCheckSumString);
                string PlainText = "CID=" + strPaymentCID + "&RID=" + strRID + "&CRN=" + strCRN + "&AMT=" + strAmount +
                    "&VER=" + strPaymentVER + "&TYP=" + strPaymentTYP + "&CNY=" + strPaymentCNY + "&RTU=" + strPaymentRTUURL + "&PPI=" + strRegistrationNo + "|" + strAmount +
                    "&RE1=" + strPaymentRE1 + "&RE2=" + strPaymentRE2 + "&RE3=" + strPaymentRE3 + "&RE4=" + strPaymentRE4 + "&RE5=" + strPaymentRE5 + "&CKS=" + Checksum;
                string encryptedstring = Encrypt(PlainText, strPaymentKey);

               /* string strLogContent = "GetPaymentGatewayNameValueCollection is StrCheckSumString: " + StrCheckSumString + Environment.NewLine + " Checksum: " + Checksum + Environment.NewLine + " PlainText:" + PlainText + " Encrypted Text: " + encryptedstring + ".";
                if (ConfigurationManager.AppSettings["Enable_PaymentDetailsLogging"].ToUpper() == "YES")
                    NtierMvc.DataAccess.Source.ExceptionLogging.SendExcepMessageToDB(StrCheckSumString, Checksum, PlainText, encryptedstring, "PaymentGatewayClient", "GetPaymentGatewayNameValueCollection");
                    */
                data.Add("i", encryptedstring);
            }
            catch(Exception ex)
            {
                data = new NameValueCollection();
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
            }

            return data;
        }


        //public NameValueCollection GetPaymentGatewayEnquireURNNameValueCollection(string strRegistrationNo, string strURNNumber, PaymentGateWayResponse objPaymentGateWayResponse)
        //{
        //    NameValueCollection data = new NameValueCollection();
        //    try
        //    {
        //        var strPaymentVER = ConfigurationManager.AppSettings["Payment-VER"];
        //        var strPaymentCID = ConfigurationManager.AppSettings["Payment-CID"];
        //        var strPaymentTYP = ConfigurationManager.AppSettings["Payment-TYP"];
        //        var strPaymentRID = objPaymentGateWayResponse.RID;
        //        var strPaymentCRN = objPaymentGateWayResponse.CRN;
        //        var strPaymentBRN = ConfigurationManager.AppSettings["Payment-TYP"];
        //        var strPaymentKey = ConfigurationManager.AppSettings["Payment-EncryptionDecryptionKey"];
        //        var strPaymentChecksumKey = ConfigurationManager.AppSettings["Payment-ChecksumKey"];
        //        string StrCheckSumString = strPaymentCID + strPaymentRID + strPaymentCRN + strPaymentChecksumKey;
        //        string Checksum = sha256_hash(StrCheckSumString);

        //        //CID=2835&RID=123456&CRN=123456&VER=1.0&TYP=TEST&CRN=123456&CKS=60fe1f3b8a86d9477bf7722d444b5f14da303f65bd55bb4efb37ac4370e0178e
        //        string PlainText = "CID=" + strPaymentCID + "&RID=" + strPaymentRID + "&CRN=" + strPaymentCRN +
        //            "&VER=" + strPaymentVER + "&TYP=" + strPaymentTYP + "&CRN=" + strPaymentCRN + "&CKS=" + Checksum;
        //        string encryptedstring = Encrypt(PlainText, strPaymentKey);

        //        string strLogContent = "GetPaymentGatewayEnquireURNNameValueCollection is StrCheckSumString: " + StrCheckSumString + Environment.NewLine + " Checksum: " + Checksum + Environment.NewLine + " PlainText:" + PlainText + " Encrypted Text: " + encryptedstring + ".";
        //        if (ConfigurationManager.AppSettings["Enable_PaymentDetailsLogging"].ToUpper() == "YES")
        //            NtierMvc.DataAccess.ExceptionLogging.SendExcepMessageToDB(StrCheckSumString, Checksum, PlainText, encryptedstring, "PaymentGatewayClient", "GetPaymentGatewayEnquireURNNameValueCollection");

        //        data.Add("i", encryptedstring);
        //    }
        //    catch(Exception ex)
        //    {
        //        data = new NameValueCollection();
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
        //    }
        //    return data;
        //}


        //public string GetPaymentGatewayEnquireURNNameValueString(string strRegistrationNo, string strURNNumber, PaymentGateWayResponse objPaymentGateWayResponse)
        //{
        //    string encryptedstring = string.Empty;
        //    try
        //    {
        //        NameValueCollection data = new NameValueCollection();
        //        var strPaymentVER = ConfigurationManager.AppSettings["Payment-VER"];
        //        var strPaymentCID = ConfigurationManager.AppSettings["Payment-CID"];
        //        var strPaymentTYP = ConfigurationManager.AppSettings["Payment-TYP"];
        //        var strPaymentRID = objPaymentGateWayResponse.RID;
        //        var strPaymentCRN = objPaymentGateWayResponse.CRN;
        //        var strPaymentBRN = ConfigurationManager.AppSettings["Payment-TYP"];
        //        var strPaymentKey = ConfigurationManager.AppSettings["Payment-EncryptionDecryptionKey"];
        //        var strPaymentChecksumKey = ConfigurationManager.AppSettings["Payment-ChecksumKey"];
        //        string StrCheckSumString = strPaymentCID + strPaymentRID + strPaymentCRN + strPaymentChecksumKey;
        //        string Checksum = sha256_hash(StrCheckSumString);

        //        //CID=2835&RID=123456&CRN=123456&VER=1.0&TYP=TEST&CRN=123456&CKS=60fe1f3b8a86d9477bf7722d444b5f14da303f65bd55bb4efb37ac4370e0178e
        //        string PlainText = "CID=" + strPaymentCID + "&RID=" + strPaymentRID + "&CRN=" + strPaymentCRN +
        //            "&VER=" + strPaymentVER + "&TYP=" + strPaymentTYP + "&CRN=" + strPaymentCRN + "&CKS=" + Checksum;
        //        encryptedstring = Encrypt(PlainText, strPaymentKey);

        //        string strLogContent = "GetPaymentGatewayEnquireURNNameValueString is StrCheckSumString: " + StrCheckSumString + Environment.NewLine + " Checksum: " + Checksum + Environment.NewLine + " PlainText:" + PlainText + " Encrypted Text: " + encryptedstring + ".";
        //        if (ConfigurationManager.AppSettings["Enable_PaymentDetailsLogging"].ToUpper() == "YES")
        //            NtierMvc.DataAccess.ExceptionLogging.SendExcepMessageToDB(StrCheckSumString,Checksum,PlainText,encryptedstring, "PaymentGatewayClient", "GetPaymentGatewayEnquireURNNameValueString");

        //        return encryptedstring;
        //    }
        //    catch(Exception ex)
        //    {
        //        encryptedstring = string.Empty;
        //        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
        //    }
        //    return encryptedstring;
        //}

        public string GetEnquireURNSuccessFailureMessage(string strEnquireURNRequest)
        {
            var strPaymentEnquireURNURL = ConfigurationManager.AppSettings["Payment-EnquireURN"];
            string strEnquireURNResponse = string.Empty;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    var uri = new Uri(strPaymentEnquireURNURL);
                    var response = httpClient.GetAsync(uri + "?&i=" + strEnquireURNRequest);//.PostAsync(uri, stringContent).Result;
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        strEnquireURNResponse = readTask.Result;
                    }
                }
            }
            catch (Exception ex)
            {
                strEnquireURNResponse = "Error";
            }
            return strEnquireURNResponse;
        }

    public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));
                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public string Encrypt(string input, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(input);
            Aes kgen = Aes.Create("AES");
            kgen.Mode = CipherMode.ECB;
            //kgen.Padding = PaddingMode.None;
            kgen.Key = keyArray;
            ICryptoTransform cTransform = kgen.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }



        //public PaymentGateWayResponse GetPaymentGatewayResponse(string strEncryptedResponse)
        //{
        //    PaymentGateWayResponse objPaymentGateWayResponse = new PaymentGateWayResponse();
        //    try
        //    {
        //        string strDecryptedResponse = DecryptEncryptedResponse(strEncryptedResponse);

        //        string[] ArrDecryptedResponse = strDecryptedResponse.Split('&');
        //        foreach (string strDecryptText in ArrDecryptedResponse)
        //        {
        //            if (strDecryptText.Contains("BRN"))
        //                objPaymentGateWayResponse.BRN = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("STC"))
        //            {
        //                objPaymentGateWayResponse.STC = Convert.ToString(strDecryptText.Split('=')[1]);
        //                if (objPaymentGateWayResponse.STC != string.Empty)
        //                {
        //                    switch (objPaymentGateWayResponse.STC)
        //                    {
        //                        case "000":
        //                            objPaymentGateWayResponse.STCStatus = "Success";
        //                            break;
        //                        case "101":
        //                            objPaymentGateWayResponse.STCStatus = "In process/Pending";
        //                            break;
        //                        case "111":
        //                            objPaymentGateWayResponse.STCStatus = "Failed";
        //                            break;
        //                    }
        //                }
        //            }
        //            else if (strDecryptText.Contains("RMK"))
        //                objPaymentGateWayResponse.RMK = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("TRN"))
        //                objPaymentGateWayResponse.TRN = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("TET"))
        //                objPaymentGateWayResponse.TET = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("PMD"))
        //            {
        //                objPaymentGateWayResponse.PMD = Convert.ToString(strDecryptText.Split('=')[1]);
        //                if (objPaymentGateWayResponse.PMD != string.Empty)
        //                {
        //                    switch (objPaymentGateWayResponse.PMD)
        //                    {
        //                        case "AIB":
        //                            objPaymentGateWayResponse.PaymentMode = "Axis Internet Banking";
        //                            break;
        //                        case "CD":
        //                            objPaymentGateWayResponse.PaymentMode = "Credit Card/Debit Card";
        //                            break;
        //                        case "NR":
        //                            objPaymentGateWayResponse.PaymentMode = "NEFT/RTGS";
        //                            break;
        //                        case "OIB":
        //                            objPaymentGateWayResponse.PaymentMode = "Other Internet Banking";
        //                            break;
        //                    }
        //                }
        //            }
        //            else if (strDecryptText.Contains("RID"))
        //                objPaymentGateWayResponse.RID = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("VER"))
        //                objPaymentGateWayResponse.VER = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("CID"))
        //                objPaymentGateWayResponse.CID = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("TYP"))
        //                objPaymentGateWayResponse.TYP = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("CRN"))
        //                objPaymentGateWayResponse.CRN = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("CNY"))
        //                objPaymentGateWayResponse.CNY = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("AMT"))
        //                objPaymentGateWayResponse.AMT = Convert.ToString(strDecryptText.Split('=')[1]);
        //            else if (strDecryptText.Contains("CKS"))
        //                objPaymentGateWayResponse.CKS = Convert.ToString(strDecryptText.Split('=')[1]);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        objPaymentGateWayResponse = new PaymentGateWayResponse();

        //    }
        //    return objPaymentGateWayResponse;
        //}

        public string DecryptEncryptedResponse(string input)
        {
            var strPaymentKey = ConfigurationManager.AppSettings["Payment-EncryptionDecryptionKey"];
            var strPaymentChecksumKey = ConfigurationManager.AppSettings["Payment-ChecksumKey"];
            byte[] inputArray = Convert.FromBase64String(input);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(strPaymentKey);
            Aes kgen = Aes.Create("AES");
            kgen.Mode = CipherMode.ECB;
            kgen.Key = keyArray;
            ICryptoTransform cTransform = kgen.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            kgen.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

    }


}
