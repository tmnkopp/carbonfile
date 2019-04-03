using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls; 
using ASPXUtils;
using System.Web.Compilation;
using System.Reflection;
using System.Configuration;

namespace CarbonDash.Core 
{
    public  class LoginForm : UserControl
    {  

        protected Panel ControlHolder = new Panel() { CssClass = "LoginForm" };

        private Button cmdSubmit = new Button() { Text = "Login" };
        private ASPXUtils.Controls.ExtendedTextBox txtUsername = new ASPXUtils.Controls.ExtendedTextBox() { ID = "txtUsername", Text = "", Required = true };
        private ASPXUtils.Controls.ExtendedTextBox txtPassword = new ASPXUtils.Controls.ExtendedTextBox() { ID = "txtPassword", Text = "", Required = true, TextMode = TextBoxMode.Password };
        private Label lblFeedBack = new Label() { ID = "lblFeedBack" };
        protected override void OnInit(EventArgs e) 
        {
            base.OnInit(e); 
            cmdSubmit.Click += new EventHandler(cmdSubmit_Click); 
             
            Controls.Add(ControlHolder); 
            ControlHolder.Controls.Add(lblFeedBack);
            ControlHolder.Controls.Add(new Literal() { Text = "<br><label>Username</label>" }); 
            ControlHolder.Controls.Add(txtUsername);
            ControlHolder.Controls.Add(new Literal() { Text = "<br><label>Password</label>" });
            ControlHolder.Controls.Add(txtPassword);
            ControlHolder.Controls.Add(new Literal() { Text = "<br><label> </label>" });
            ControlHolder.Controls.Add(cmdSubmit);
            
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HTTPUtils.QString("logout") != "")
            {
                Security.Logout(); 
                Response.Redirect("/");
            }

            if (HTTPUtils.QString("log") == "1")
            {
                lblFeedBack.Visible = true;
                lblFeedBack.CssClass = "ok";
                lblFeedBack.Text = "<b>Login Successful</b>"; 
            } 
        }

        protected void cmdSubmit_Click(object source, EventArgs e)
        { 
            if ( txtUsername.Text != "" && txtPassword.Text != "" )
            {
                List<UserProfile> users = new UserProfile().SelectAll();
                List<UserProfile> oResult = ((from o in users where o.Email == txtUsername.Text && o.Password == txtPassword.Text select o)).ToList();

                if (oResult.Count > 0)
                { 
                    Security.Login(oResult); 
                    if (SiteSettings.Instance.RediruserToProfile && RoleTypes.ATHLETE.ToString() == Settings.GetSetting("ROLE"))
                    {
                        Response.Write("<script>window.location.href='/userprofile.aspx';</script>"); 
                    }
                    else
                    {
                        Response.Write("<script>window.location.href=''+window.location.href+'?log=1';</script>");
                    }
                    
                    Response.End(); 
                }
                else
                    SetInvalid();  
            }
            else 
                SetInvalid();
           
    }

    private void SetInvalid() {
        Security.Logout();
        lblFeedBack.Text = "<b>Invalid User Name / Password. Please try again</b>";
        lblFeedBack.CssClass = "ko";  
 
        }

    } 
}
 

