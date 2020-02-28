using LPM.Util.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LPM.Util
{
    public class SendEmail
    {
        public string Send(UtilEmail utilEmail)
        {
            SmtpEmailConfig emailConfig = new SmtpEmailConfig();
            string message = string.Empty;
            using (MailMessage mailMessage = new MailMessage(utilEmail.Email, utilEmail.To))
            {
                mailMessage.Subject = utilEmail.Subject;
                mailMessage.Body = utilEmail.Body;
                if (utilEmail.Attachment.Length > 0)
                {
                    string fileName = Path.GetFileName(utilEmail.Attachment.FileName);
                    mailMessage.Attachments.Add(new Attachment(utilEmail.Attachment.OpenReadStream(), fileName));
                }
                mailMessage.IsBodyHtml = utilEmail.IsBodyHtml;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = emailConfig.Host;
                    smtp.EnableSsl = emailConfig.EnableSsl;
                    NetworkCredential NetworkCred = new NetworkCredential(utilEmail.Email, utilEmail.Password);
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
