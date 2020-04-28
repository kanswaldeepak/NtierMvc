using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NtierMvc.Common;

namespace NtierMvc.BusinessLogic.Utility
{
    /// <summary>
    /// Summary description for NetMailer
    /// </summary>
    public class NetMailer : IDisposable
    {
        private MailMessage msgMail = new MailMessage();

        public async Task<bool> ComposeMail(EmailContent mo)
        //public bool ComposeMail(EmailContent mo)
        {
            //HttpContext.Current.Response.Write(filePath);
            //rds.ReadXml(filePath);
            try
            {
                if (mo.MailToAddresses != null)
                {
                    foreach (var toAddress in mo.MailToAddresses)
                    {
                        msgMail.To.Add(toAddress);
                    }
                }

                if (mo.CarbonCopyAddresses != null)
                {
                    foreach (var ccAddress in mo.CarbonCopyAddresses)
                    {
                        msgMail.CC.Add(ccAddress);
                    }
                }

                if (mo.BroadcastCarbonCopyAddresses != null)
                {
                    foreach (var bccAddress in mo.BroadcastCarbonCopyAddresses)
                    {
                        msgMail.Bcc.Add(bccAddress);
                    }
                }


                // this is now configured in mail config section
                //if (!string.IsNullOrEmpty(mo.SendersAddress) && !string.IsNullOrEmpty(mo.SendersDisplayName))
                //{
                //    msgMail.From = new MailAddress(mo.SendersAddress, mo.SendersDisplayName);
                //}

                msgMail.Priority = mo.MailPriority;
                msgMail.Subject = mo.Subject;
                msgMail.Body = mo.Body;
                msgMail.IsBodyHtml = true;
                msgMail.BodyEncoding = System.Text.Encoding.UTF8;

                if (mo.Attachments != null)
                {
                    foreach (var attachment in mo.Attachments)
                    {
                        msgMail.Attachments.Add(new Attachment(attachment));
                    }
                }


                SmtpClient client = new SmtpClient();
                //client.Send(msgMail);
                await client.SendMailAsync(msgMail);
                return true;
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                //return ex.Message;
                return false;
            }
        }

        public bool ComposeMailGriev(EmailContent mo)       
        {            
            try
            {
                if (mo.MailToAddresses != null)
                {
                    foreach (var toAddress in mo.MailToAddresses)
                    {
                        msgMail.To.Add(toAddress);
                    }
                }

                if (mo.CarbonCopyAddresses != null)
                {
                    foreach (var ccAddress in mo.CarbonCopyAddresses)
                    {
                        msgMail.CC.Add(ccAddress);
                    }
                }

                if (mo.BroadcastCarbonCopyAddresses != null)
                {
                    foreach (var bccAddress in mo.BroadcastCarbonCopyAddresses)
                    {
                        msgMail.Bcc.Add(bccAddress);
                    }
                }               
                msgMail.Priority = mo.MailPriority;
                msgMail.Subject = mo.Subject;
                msgMail.Body = mo.Body;
                msgMail.IsBodyHtml = true;
                msgMail.BodyEncoding = System.Text.Encoding.UTF8;

                if (mo.GrievDocs != null && mo.GrievDocs.Count > 0)
                {
                    foreach (var attach in mo.GrievDocs)
                    {
                        msgMail.Attachments.Add(new Attachment(attach.InputStream, attach.FileName));
                    }
                }

                SmtpClient client = new SmtpClient();

                client.Send(msgMail);
                
                //await client.SendMailAsync(msgMail);
                return true;
            }

            catch (Exception ex)
            {
                NtierMvc.DataAccess.ExceptionLogging.SendExcepToDBInnerException(ex);
                //return ex.Message;
                return false;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            msgMail.Dispose();
        }

        #endregion IDisposable Members
    }
}
