 

 
//------------------------------------------------------------------------------
// The contents of this file are subject to the nopCommerce Public License Version 1.0 ("License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at  http://www.nopCommerce.com/License.aspx. 
// 
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. 
// See the License for the specific language governing rights and limitations under the License.
// 
// The Original Code is nopCommerce.
// The Initial Developer of the Original Code is NopSolutions.
// All Rights Reserved.
// 
// Contributor(s): _______. 
//------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security; 
using CarbonDash.Utils;
using System.Collections.Generic;
using System.Linq;
 
namespace CarbonDash
{
    /// <summary>
    /// Manages storage of membership information for a nopCommerce application in a data source
    /// </summary>
    public partial class CDashMembershipProvider : MembershipProvider
    {
        #region Fields
        private string _appName;
        private bool _enablePasswordReset;
        private bool _enablePasswordRetrieval;
        private int _maxInvalidPasswordAttempts;
        private int _minRequiredNonalphanumericCharacters;
        private int _minRequiredPasswordLength;
        private int _passwordAttemptWindow;
        private string _passwordStrengthRegularExpression;
        private bool _requiresQuestionAndAnswer;
        private bool _requiresUniqueEmail;
        #endregion

        #region Methods
        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for. </param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>true if the password was updated successfully; otherwise, false.</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>true if the password question and answer are updated successfully; otherwise, false.</returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user.</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A MembershipCreateStatus enumeration value indicating whether the user was created successfully.</param>
        /// <returns>A MembershipUser object populated with the information for the newly created user.</returns>

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser oMembershipUser = null;
            
            bool UsernamesEnabled = false; 

            string _username = string.Empty;
            string _email = string.Empty;
            if (UsernamesEnabled)
            {
                _username = username;
                _email = email;
            }
            else
            {
                //little hack here. username variable was used to store customer email
                _username = username;
                _email = username;
            }
 
            User user = new User();
            user.Insert(_username, _email, password, passwordQuestion, passwordAnswer, out status);

            if (status == MembershipCreateStatus.Success)
            {
                var dt = DateTime.UtcNow;
                oMembershipUser = new MembershipUser(this.Name, _username, user.GUID, _email, string.Empty, null, true, false, dt, dt, dt, dt, dt);
            }
            return oMembershipUser;
        }
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
         public override MembershipUserCollection FindUsersByEmail(string EmailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        } 
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            User user = null;

            string _username = string.Empty;
            string _email = string.Empty;
 
            user = new User().SelectByUsername(username);
            if (user != null)
            {
                _username = user.Username;
                _email = user.Email;
            }

            if (user == null)
              return null;
            var dt = DateTime.UtcNow; 
            return new MembershipUser(this.Name, _username, user.GUID, _email, string.Empty, null, true, false, dt, dt, dt, dt, dt);
        }

        public override string GetUserNameByEmail(string email)
        {
            User user = new User().SelectByEmail(email);
            if (user == null)
                return null;
 
            return user.Username;
         
        }
       public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "MembershipProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Membership Provider");
            }
            base.Initialize(name, config);
            this._enablePasswordReset = CommonHelper.ConfigGetBooleanValue(config, "enablePasswordReset", true);
            this._enablePasswordRetrieval = CommonHelper.ConfigGetBooleanValue(config, "enablePasswordRetrieval", true);
            this._requiresQuestionAndAnswer = CommonHelper.ConfigGetBooleanValue(config, "requiresQuestionAndAnswer", true);
            this._requiresUniqueEmail = CommonHelper.ConfigGetBooleanValue(config, "requiresUniqueEmail", true);
            this._maxInvalidPasswordAttempts = CommonHelper.ConfigGetIntValue(config, "maxInvalidPasswordAttempts", 5, false, 0);
            this._passwordAttemptWindow = CommonHelper.ConfigGetIntValue(config, "passwordAttemptWindow", 10, false, 0);
            this._minRequiredPasswordLength = CommonHelper.ConfigGetIntValue(config, "minRequiredPasswordLength", 7, false, 0x80);
            this._minRequiredNonalphanumericCharacters = CommonHelper.ConfigGetIntValue(config, "minRequiredNonalphanumericCharacters", 1, true, 0x80);
            this._passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
            if (this._passwordStrengthRegularExpression != null)
            {
                this._passwordStrengthRegularExpression = this._passwordStrengthRegularExpression.Trim();
                if (this._passwordStrengthRegularExpression.Length != 0)
                {
                    try
                    {
                        new Regex(this._passwordStrengthRegularExpression);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new ProviderException(ex.Message, ex);
                    }
                }
            }
            this._passwordStrengthRegularExpression = string.Empty;
            if (this._minRequiredNonalphanumericCharacters > this._minRequiredPasswordLength)
            {
                throw new HttpException("MinRequiredNonalphanumericCharacters can not be more than MinRequiredPasswordLength");
            }
            this._appName = config["applicationName"];
            if (string.IsNullOrEmpty(this._appName))
            {
                this._appName = "CarbonDash";
            }
            if (this._appName.Length > 0x100)
            {
                throw new ProviderException("Provider application name too long");
            }

            string connectionStringName = config["connectionStringName"];
            if (string.IsNullOrEmpty(connectionStringName))
            {
                this._appName = "CDashSqlConnection";
            }

            config.Remove("enablePasswordReset");
            config.Remove("enablePasswordRetrieval");
            config.Remove("requiresQuestionAndAnswer");
            config.Remove("applicationName");
            config.Remove("requiresUniqueEmail");
            config.Remove("maxInvalidPasswordAttempts");
            config.Remove("passwordAttemptWindow");
            config.Remove("commandTimeout");
            config.Remove("name");
            config.Remove("minRequiredPasswordLength");
            config.Remove("minRequiredNonalphanumericCharacters");
            config.Remove("passwordStrengthRegularExpression");
            config.Remove("connectionStringName");
            if (config.Count > 0)
            {
                string key = config.GetKey(0);
                if (!string.IsNullOrEmpty(key))
                {
                    throw new ProviderException(string.Format("Provider unrecognized attribute {0}", key));
                }
            }
        }


        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        } 
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        } 
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
      
        } 
        public override bool ValidateUser(string username, string password)
        { 
            string _email = string.Empty;
            User user = new User().SelectByUsername(username);
             
            if (user != null)
            {
                _email = user.Email;
            }
          
            //return IoC.Resolve<ICustomerService>().Login(_email, password);
            return true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        public override string ApplicationName
        {
            get
            {
                return this._appName;
            }
            set
            {
                this._appName = value;
            }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        public override bool EnablePasswordReset
        {
            get
            {
                return this._enablePasswordReset;
            }
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return this._maxInvalidPasswordAttempts;
            }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return this._minRequiredNonalphanumericCharacters;
            }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        public override int MinRequiredPasswordLength
        {
            get
            {
                return this._minRequiredPasswordLength;
            }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        public override int PasswordAttemptWindow
        {
            get
            {
                return this._passwordAttemptWindow;
            }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return this._passwordStrengthRegularExpression;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return this._requiresQuestionAndAnswer;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get
            {
                return this._requiresUniqueEmail;
            }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                return this._enablePasswordRetrieval;
            }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Hashed;
            }
        }
        #endregion
    }
}