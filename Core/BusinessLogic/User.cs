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
using CarbonDash.Utils;
using ASPXUtils;
using ASPXUtils.Controls;
using ASPXUtils.Data;

namespace CarbonDash
{



    [ASPXUtils.EntityAttribute("Users", TableName = "cd_Users")]
    [ASPXUtils.DataEntityAttribute( TableName = "cd_Users", TablePrefixAppKey="")]
    public class User  : ASPXUtils.IBusinessEntity
    {
         
        [EntityGridAttribute("ID", IsID = true)]
        [DatafieldAttribute("ID", DataType = SqlDbType.Int, IsPrimary = true)]
        public int ID { get; set; }

        private string _guid;
        [Formfield("Unique ID", ControlType = typeof(Label))]
        [DatafieldAttribute("GUID")]
        public string GUID
        {
            get
            {
                if (_guid == null || _guid == "")
                    _guid = System.Guid.NewGuid().ToString().ToUpper();
                return _guid;
            }
            set { _guid = value; }
        }

        [Formfield("RoleCode", ControlType = typeof(DDRole))]
        [DatafieldAttribute("RoleCode")]
        public string RoleCode { get; set; }

        [EntityGridAttribute("Email")]
        [DatafieldAttribute("Email")]
        public string Email { get; set; }

        [DatafieldAttribute("Username")]
        public string Username { get; set; }

        [DatafieldAttribute("PasswordHash")]
        public string PasswordHash { get; set; }

        [DatafieldAttribute("PasswordRecoveryToken")]
        public string PasswordRecoveryToken { get; set; }

        [DatafieldAttribute("SaltKey")]
        public string SaltKey { get; set; }

        [EntityGridAttribute("FirstName")]
        [Formfield("FirstName", ControlType = typeof(ExtendedTextBox))]
        [DatafieldAttribute("FirstName")]
        public string FirstName { get; set; }

        [EntityGridAttribute("LastName")]
        [Formfield("LastName", ControlType = typeof(ExtendedTextBox))]
        [DatafieldAttribute("LastName")]
        public string LastName { get; set; }

        [Formfield("DOB", ValidateDate = true, ControlType = typeof(ExtendedTextBox), ShowCalendar = true)]
        [DatafieldAttribute("DOB", DataType = SqlDbType.DateTime)]
        public DateTime DOB { get; set; }

        [DatafieldAttribute("PasswordQuestion")]
        public string PasswordQuestion { get; set; }

        [DatafieldAttribute("PasswordAnswer")]
        public string PasswordAnswer { get; set; }

        [EntityGridAttribute("IsApproved")]
        [Formfield("IsApproved", ControlType = typeof(DDTrueFalse))]
        [DatafieldAttribute("IsApproved")]
        public bool IsApproved { get; set; }

        [Formfield("IsLockedOut", ControlType = typeof(DDTrueFalse))]
        [DatafieldAttribute("IsLockedOut")]
        public bool IsLockedOut { get; set; }

        [Formfield("CreateDate", ValidateDate = true, ControlType = typeof(ExtendedTextBox), ShowCalendar = true)]
        [DatafieldAttribute("CreateDate", DataType = SqlDbType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Formfield("LastLoginDate",  ControlType = typeof(Label) )]
        [DatafieldAttribute("LastLoginDate", DataType = SqlDbType.DateTime)]
        public DateTime LastLoginDate { get; set; }

        [Formfield("AdminComment", ControlType = typeof(TextArea))]
        [DatafieldAttribute("AdminComment")]
        public string AdminComment { get; set; }

        [Formfield("Active", ControlType = typeof(DDTrueFalse))]
        [DatafieldAttribute("Active")]
        public bool Active { get; set; }

 
        [DatafieldAttribute("Deleted")]
        public bool Deleted { get; set; }
 

        



        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        private List<User> _UserCollection;
        public List<User> UserCollection
        {
            get { return _UserCollection; }
            set { _UserCollection = value; }
        }

        public int Insert()
        {
            return Insert(this);
        }
        public int Insert(string username, string email, string password, string passwordQuestion
            , string passwordAnswer, out MembershipCreateStatus status)
        {
            string saltKey = string.Empty;
            string passwordHash = string.Empty;
            string passwordAnswerHash = string.Empty;

            status = MembershipCreateStatus.UserRejected;
            if (SelectByEmail(email) != null)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return 0;
            }
            if (!CommonHelper.IsValidEmail(email))
            {
                status = MembershipCreateStatus.InvalidEmail;
                return 0;
            }

            saltKey = UserService.CreateSalt(5);
            passwordHash = UserService.CreatePasswordHash(password, saltKey);
            passwordAnswerHash = UserService.CreatePasswordHash(passwordAnswer, saltKey);

            User user = new User();
            user.Username = email;
            user.Email = email;
            user.PasswordHash = passwordHash;
            user.PasswordAnswer = passwordAnswerHash;
            user.PasswordQuestion = passwordQuestion;
            user.GUID = System.Guid.NewGuid().ToString();
            user.SaltKey = saltKey; 
            int userid = Insert(user);
            if (userid > 0)
            {
                status = MembershipCreateStatus.Success;
            }
            return userid;
        }
        public void ModifyPassword( string newPassword)
        {
            if (String.IsNullOrWhiteSpace(newPassword))
                throw new CDashException("Password Required");
            newPassword = newPassword.Trim();
 
            if (this != null)
            {
                string newPasswordSalt = UserService.CreateSalt(5);
                string newPasswordHash = UserService.CreatePasswordHash(newPassword, newPasswordSalt);

                this.PasswordHash = newPasswordHash;
                this.SaltKey = newPasswordSalt;
                this.Update(); 
            }
        }
        private int Insert(User user)
        {
            return GenericDAL<User>.Insert(user); 
        }
        public void Update()
        {
            Update(this);
        }
        public void Update(User obj)
        {
            GenericDAL<User>.Update(obj);
        }
        public void Delete()
        {
            Delete(this);
        }
        public void Delete(User obj)
        {
            //UserDAL.Delete(obj.ID);
        }
        public User Select(int ID)
        {
            List<User> oResult = ((from o in SelectAll() where o.ID == ID select o).Take(1)).ToList();
            return (oResult.Count > 0) ? oResult[0] : null;
        }
        public User SelectByEmail(string Email)
        {
            List<User> oResult = ((from o in SelectAll() where o.Email == Email select o).Take(1)).ToList();
            return (oResult.Count > 0) ? oResult[0] : null;
        }
        public User SelectByUsername(string Username)
        {
            List<User> oResult = ((from o in SelectAll() where o.Username == Username select o).Take(1)).ToList();
            return (oResult.Count > 0) ? oResult[0] : null;
        } 
        public List<User> SelectAllActive()
        {
            List<User> oResult = ((from o in SelectAll() where o.Active == true select o)).ToList();
            return oResult;
        }
        public List<User> SelectAll()
        {
            if (this.UserCollection == null)
                this.UserCollection = GenericDAL<User>.GetAll();
            return this.UserCollection;
        }
    }



}
