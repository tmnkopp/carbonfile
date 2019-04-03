using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CarbonDash
{
    public static class UserService
    {
        public static bool Login(string email, string password)
        {
            if (email == null)
                email = string.Empty;
            email = email.Trim();

            User user = new User().SelectByEmail(email);

            if (user == null)
                return false;

            if (!user.Active)
                return false;

            if (user.Deleted)
                return false;
              
            string passwordHash = CreatePasswordHash(password, user.SaltKey);
            bool result = user.PasswordHash.Equals(passwordHash);
            return true;
        }
        public static string CreatePasswordHash(string password, string salt)
        {
            //MD5, SHA1
            string passwordFormat = "SHA1";
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, passwordFormat);
        }
        public static string CreateSalt(int size)
        {
            var provider = new RNGCryptoServiceProvider();
            byte[] data = new byte[size];
            provider.GetBytes(data);
            return Convert.ToBase64String(data);
        }      
    } 
}
