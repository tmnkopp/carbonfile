using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using ASPXUtils;
using System.Reflection;
namespace CarbonDash.Core
{ 
    public class SiteSettingsForm : ASPXUtils.Controls.EntityForm
    {
        protected override void OnInit(EventArgs e)
        {
            base.EnableNewButton = false;
            base.OnInit(e);
        }
        
        protected override void DeleteForm(){}
        protected override void SaveForm()
        { 
            // ControlHolder.Controls.Add(new Literal() { Text = "Hello World" });
            SiteSettings.Instance.Title = Convert.ToString(GetValueFromControl("Title"));
            SiteSettings.Instance.ShowAthletesBMI = Convert.ToBoolean(GetValueFromControl("ShowAthletesBMI"));
            SiteSettings.Instance.RediruserToProfile = Convert.ToBoolean(GetValueFromControl("RediruserToProfile"));
            SiteSettings.Instance.ShowNoteCount = Convert.ToInt32(GetValueFromControl("ShowNoteCount"));
  
            SiteSettings.Instance.Save();
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl, true);
        }
        protected override void LoadForm()
        {
            
            SetControlValue("Title", SiteSettings.Instance.Title, null);
            SetControlValue("ShowAthletesBMI", SiteSettings.Instance.ShowAthletesBMI, null);
            SetControlValue("RediruserToProfile", SiteSettings.Instance.RediruserToProfile, null);
            SetControlValue("ShowNoteCount", SiteSettings.Instance.ShowNoteCount, null); 
        }  
    }
}
