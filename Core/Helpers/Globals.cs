using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.Collections;
using System.Configuration;
using ASPXUtils;
namespace CarbonDash.Core
{
    public static class Globals
    {
        public static string ProQStr = "/UserProfile.aspx?id={0}&pdid={1}&pnid={2}&yr={3}";
        public static string qstrGUIDKey =  "GUID" ; 
        public static string qstrHREFKey =  "REFERTO" ;
        public static string qstrGUIDVal = EncodeDecode.EncodeURLSafe("5e8bb3ab-1917-4788-87b0-485e6499ef9c");
        public static string RemoteLink = "http://" + ServerVariable("SERVER_NAME") + "/l0g1n.aspx?" + Globals.qstrGUIDKey + "={0}&" + Globals.qstrHREFKey + "={1}";
        public static string ServerVariable(string ServerVariableName)
        {
            return HttpContext.Current.Request.ServerVariables[ServerVariableName];
        }
        public static string GetURL()
        {
            return HttpContext.Current.Request.ServerVariables["URL"] + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        } 
    }
}

namespace CarbonDash.Core.Helpers
{
    public static class Globals
    {
        public static string ProQStr = "/UserProfile.aspx?id={0}&pdid={1}&pnid={2}&yr={3}";
        public static string qstrGUIDKey = "GUID";
        public static string qstrHREFKey = "REFERTO";
        public static string qstrGUIDVal = EncodeDecode.EncodeURLSafe("5e8bb3ab-1917-4788-87b0-485e6499ef9c");
        public static string RemoteLink = "http://CarbonDash/l0g1n.aspx?" + Globals.qstrGUIDKey + "={0}&" + Globals.qstrHREFKey + "={1}";
        public static string ServerVariable(string ServerVariableName)
        {
            return HttpContext.Current.Request.ServerVariables[ServerVariableName];
        }
        public static string GetURLWithQuery()
        {
            return HttpContext.Current.Request.ServerVariables["URL"] + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"];
        }
        public static string GetURLWithoutQuery()
        {
            return HttpContext.Current.Request.ServerVariables["URL"];
        }
    }

}
