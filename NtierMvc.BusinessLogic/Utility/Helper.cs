using NtierMvc.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NtierMvc.BusinessLogic.Utility
{
    public class Helper
    {
        private static Random random = new Random();
        public static string RandomString(int size)
        {
           
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, size)
                .Select(x => input[random.Next(0, input.Length)]);
            return new string(chars.ToArray()).ToUpperInvariant();
        }

        public static void SendMailMessage(EmailContent mo)
        {
            ProcessHelper.MailHelper(mo);
        }

        public static string ConvertObjectToXML<T>(T Source)
        {
            string xml = "";
            var serializer = new XmlSerializer(typeof(T),new XmlRootAttribute("root"));
            using (var stream = new StringWriter())
            {
                serializer.Serialize(stream, Source);
                xml = stream.ToString();
            }

            return xml;
        }
    }
}
