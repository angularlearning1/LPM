using System;
using System.Linq;
using System.Security;

namespace LPM.Web.Util
{
    internal class SmtpEmailConfig
    {
        

        public string Host => "";
        public bool EnableSsl => true;
        public bool UseDefaultCredentials => true;
        public int Port => 457;

        public string Email => "";
        public SecureString Password => SecureString("");


        private SecureString SecureString(string password)
        {
            var secure = new SecureString();
            foreach (var character in password.ToCharArray())
            {
                secure.AppendChar(character);
            }
            secure.MakeReadOnly();
            return secure;
        }
    }
}