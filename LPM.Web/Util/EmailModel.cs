using Microsoft.AspNetCore.Http;
using System.Net.Mail;
using System.Security;

namespace LPM.Web.Util
{
    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IFormFile Attachment { get; set; }
        public string Email { get; set; }
        public SecureString Password { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}