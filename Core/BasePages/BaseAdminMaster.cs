using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using ASPXUtils;
 
namespace CarbonDash.Core
{

    public abstract class BaseAdminMaster : MasterPage
    {

        public BaseAdminMaster() { }
        public bool IncludeJquery { get; set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
              
            //HttpContext.Current.Response.Write("BASE MASTER");

            StringBuilder SB = new StringBuilder();
            SB.Append("<style> ");
            if( ASPXUtils.Settings.GetSetting("tim") != ""){
                SB.Append(".fa_SiteConfig  {  visibility:visible;  } "); 
            }else{
                SB.Append(".fa_SiteConfig   { visibility:hidden;  } ");
            }
            SB.Append(" </style>");
            Page.Header.Controls.Add(new Literal() { Text=SB.ToString()});


            Page.Title = Request.ServerVariables["SERVER_NAME"] + " | Admin";


        }

    }
}

