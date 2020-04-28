using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;

namespace NtierMvc.Common
{
    [Serializable]
    public class EmailContent
    {
        public List<string> CarbonCopyAddresses { get; set; }
        public List<string> MailToAddresses { get; set; }
        public List<string> BroadcastCarbonCopyAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailPriority MailPriority { get; set; }
        public List<string> Attachments { get; set; }
        public List<GrievScreenShot> GrievDocs { get; set; }
    }
    public class GrievScreenShot
    {
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
    }
}
