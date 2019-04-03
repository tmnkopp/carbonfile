using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarbonDash.Core;
using ASPXUtils;
namespace CarbonDash.Core
{

    public class BaseUserControl: UserControl
    {
        public string _PDID = HTTPUtils.QString("pdid");
        public string _PNID = HTTPUtils.QString("pnid");
        public string _ID = Utils.GetUserID(  true);
        public string _DY = HTTPUtils.QString("dy");
        public string _MT = HTTPUtils.QString("mt");
        public string _YR = HTTPUtils.QString("yr");
        public string _MODE = HTTPUtils.QString("mode");
        //private WebPage _WP; 
        //protected override void OnInit(EventArgs e)
        //{
        //    base.OnInit(e);
        //}
        //protected override void OnLoad(  EventArgs e)
        //{ 
        //    base.OnLoad(e);
        //} 
        //protected override void OnPreRender(EventArgs e)
        //{ 
        //    base.OnPreRender(e);
        //}
    }
}
