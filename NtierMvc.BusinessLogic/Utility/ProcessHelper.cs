using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;

namespace NtierMvc.BusinessLogic.Utility
{
    /// <summary>
    /// Delegated async helper
    /// </summary>
    public class ProcessHelper
    {
        private delegate void Mail(EmailContent mo);

        public static void MailHelper(EmailContent mo)
        {
            var process = new Mail(MailByThread);
            process.BeginInvoke(mo, null, null);
        }

        private static void MailByThread(EmailContent mo)
        {
            var nm = new NetMailer();
            var mailStatus = nm.ComposeMail(mo);
        }
    }
}
