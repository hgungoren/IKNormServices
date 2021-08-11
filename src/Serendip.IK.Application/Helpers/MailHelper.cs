using Serendip.IK.Emails.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Serendip.IK.Helpers
{
    public class MailHelper
    {
        public bool SendMail(string from, string to, string subject, string body, string bcc, List<EmailAttachmentDto> emailAttachments = null)
        {
            var pwd = "";
            if (String.IsNullOrEmpty(from))
            {
                from = "";
                pwd = "";
            }
            bool res = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.surat.com.tr";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(from, pwd);
            smtp.EnableSsl = false;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            if (emailAttachments != null)
            {
                foreach (var item in emailAttachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        var fileBytes = System.Convert.FromBase64String(item.Base64Data);
                        Attachment attachment = new Attachment(new MemoryStream(fileBytes), item.FileName);
                        mail.Attachments.Add(attachment);
                    }
                }
            }

            var toList = to.Split(';').ToList();
            for (int i = 0; i < toList.Count(); i++)
            {
                mail.To.Clear();
                mail.To.Add(toList[i].ToString());
            }
            var bccList = bcc.Split(';').ToList();
            for (int i = 0; i < bccList.Count(); i++)
            {
                mail.Bcc.Clear();
                mail.Bcc.Add(toList[i].ToString());
            }
            try
            {
                smtp.Send(mail);
            }
            catch (SmtpException ex)
            {
                string e = ex.Message;
                res = false;
            }
            return res;
        }
    }
}
