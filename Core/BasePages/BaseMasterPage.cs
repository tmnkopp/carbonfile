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
    
    public abstract class BaseMasterPage : MasterPage
    {
        public string BodyClass = "";
        public string PageName = "";
        public string SystemPageName = "";
        public BaseMasterPage() { }
        public bool IncludeJquery{get; set;}
        
        //protected override void OnInit(EventArgs e)
        //{ 
        //    base.OnInit(e);
        //}
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);  
        //}
       
       
    }
}

