using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace LPM.Util.Models
{
    public class UtilEmail
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
