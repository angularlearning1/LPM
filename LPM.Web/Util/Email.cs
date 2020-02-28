using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LPM.Web.Util
{
    public class Email
    {

        public string SendEmail(EmailModel emailModel)
        {
            SmtpEmailConfig emailConfig = new SmtpEmailConfig();
            if (string.IsNullOrWhiteSpace(emailModel.Email))
            {
                emailModel.Email = emailConfig.Email;
            }
            if (string.IsNullOrWhiteSpace(emailModel.Password.ToString()))
            {
                emailModel.Password = emailConfig.Password;
            }
            string message = string.Empty;
            using (MailMessage mailMessage = new MailMessage(emailModel.Email, emailModel.To))
            {
                mailMessage.Subject = emailModel.Subject;
                mailMessage.Body = emailModel.Body;
                if (emailModel.Attachment.Length > 0)
                {
                    string fileName = Path.GetFileName(emailModel.Attachment.FileName);
                    mailMessage.Attachments.Add(new Attachment(emailModel.Attachment.OpenReadStream(), fileName));
                }
                mailMessage.IsBodyHtml = emailModel.IsBodyHtml;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = emailConfig.Host;
                    smtp.EnableSsl = emailConfig.EnableSsl;
                    NetworkCredential NetworkCred = new NetworkCredential(emailModel.Email, emailModel.Password);
                    smtp.UseDefaultCredentials = emailConfig.UseDefaultCredentials;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = emailConfig.Port;
                    smtp.Send(mailMessage);
                    message = "Email sent.";
                }
            }
            return message;
        }
    }
    
}
