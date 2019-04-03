using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using ASPXUtils;
using System.Configuration;
using System.Diagnostics;
namespace CarbonDash.Core
{
    public static class Security
    { 
        public static bool IsAdmin()
        { 
             return  Settings.GetSetting("admin") == "1";  
        }
        public static void SetEditable()
        {
            Settings.SaveSetting("edit", "1"); 
        }
        public static void SetBrowse()
        {
            Settings.SaveSetting("browse", "1");
        }
        public static bool IsEditable()
        {
            return Settings.GetSetting("edit") == "1";  
        }
        public static bool IsBrowse()
        {
            return Settings.GetSetting("browse") == "1";
        }
        public static bool IsLogged()
        { 
            return Settings.GetSetting("id") != "";
        } 
        public static void SetAdmin()
        {
            Settings.SaveSetting("admin", "1");
            Settings.SaveSetting("edit", "1");
            Settings.SaveSetting("browse", "1");
        } 
        public static void Logout()
        {
            Settings.Clear();
        } 

      
    }
}
