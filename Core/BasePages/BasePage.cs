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
    public abstract class BaseCarbonPage : Page
    {  
        public string _PDID = HTTPUtils.QString("pdid");
        public string _PNID = HTTPUtils.QString("pnid");
        public string _ID;
        public string _DY = HTTPUtils.QString("dy");
        public string _MT = HTTPUtils.QString("mt");
        public string _YR = HTTPUtils.QString("yr");
        public string _MODE = HTTPUtils.QString("mode");
        //private WebPage _WP;
        public User CurrentUser;
        public BaseCarbonPage() { }
        protected override void OnInit(EventArgs e)
        { 
            base.OnInit(e); 
        }
        protected override void OnLoad(  EventArgs e)
        { 
            base.OnLoad(e);
        } 
        protected override void OnPreRender(EventArgs e)
        { 
            base.OnPreRender(e);
        }
         
    }
}

