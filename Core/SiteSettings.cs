using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Web.Configuration;
using System.Web;
using ASPXUtils;
namespace CarbonDash.Core
{
    public class SiteSettings
    {
        public static string THUMBS_PATH = "th/";
        public static string SITE_ID = WebConfigurationManager.AppSettings["SITE_ID"];
        public static string VIR_GALLERY_PATH = WebConfigurationManager.AppSettings["GalleryPath"];
        public static string PHY_GALLERY_PATH =  HttpContext.Current.Server.MapPath(WebConfigurationManager.AppSettings["GalleryPath"]);
        public static string TABLE_PREFIX = WebConfigurationManager.AppSettings["TABLE_PREFIX"];
        public static string SYSFILE_TEMPLATE_PREFIX = WebConfigurationManager.AppSettings["SYSFILE_TEMPLATE_PREFIX"];
        public static string UPLOAD_PATH = WebConfigurationManager.AppSettings["UploadPath"];
        public static string SITE_U = WebConfigurationManager.AppSettings["SITEU"];
        public static string SITE_P = WebConfigurationManager.AppSettings["SITEP"];
        public static bool Debug = Convert.ToBoolean(WebConfigurationManager.AppSettings["Debugging"]);
        //public static bool UseCache = Convert.ToBoolean(WebConfigurationManager.AppSettings["UseCache"]);
        public static string FROM_EMAIL = WebConfigurationManager.AppSettings["FROM_EMAIL"];
        private static SiteSettings siteSettingsSingleton;
        public static SiteSettings Instance
        {
            get   {
                if (siteSettingsSingleton == null)  {
                    siteSettingsSingleton = new SiteSettings();
                }
                return siteSettingsSingleton;
            }
        } 
        //
        
        private string  _Title;
        [Formfield("", Caption = "Title")]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _ShowNoteCount = 2;
        [Formfield("", Caption = "Show Note Count", Width=25, ValidateInteger=true, Required=true)]
        public int ShowNoteCount
        {
            get { return _ShowNoteCount; }
            set { _ShowNoteCount = value; }
        }
       
        private bool _UseCache;
         [Formfield("", Caption = "Use Cache", FieldAccess = FormFieldAccess.fa_SiteConfig, ControlType = typeof(ASPXUtils.Controls.DDTrueFalse))]
        public bool UseCache
        {
            get    {   return _UseCache;    }
            set { _UseCache = value; }
        }

         private bool _ShowAthletesBMI;
         [Formfield("", Caption = "Show Athletes BMI", FieldAccess = FormFieldAccess.fa_General, ControlType = typeof(ASPXUtils.Controls.DDTrueFalse))]
         public bool ShowAthletesBMI
         {
             get { return _ShowAthletesBMI; }
             set { _ShowAthletesBMI = value; }
         }
         private bool _RediruserToProfile;
         [Formfield("", Caption = "Redirect Athlete To Profile On Login", FieldAccess = FormFieldAccess.fa_General, ControlType = typeof(ASPXUtils.Controls.DDTrueFalse))]
         public bool RediruserToProfile
         {
             get { return _RediruserToProfile; }
             set { _RediruserToProfile = value; }
         }         

        public SiteSettings() { 
            Load(); 
        }
  

        public void Save()
        {
            System.Collections.Specialized.StringDictionary dic = new System.Collections.Specialized.StringDictionary();
            Type settingsType = this.GetType();

            //------------------------------------------------------------
            //	Enumerate through settings properties
            //------------------------------------------------------------
            foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
            {

                if (propertyInformation.Name != "Instance" && propertyInformation.CanWrite)
                { 
                    object propertyValue = propertyInformation.GetValue(this, null); 
                    string valueAsString; 
                    if (propertyValue == null || propertyValue.Equals(Int32.MinValue) || propertyValue.Equals(Single.MinValue))
                    {
                        valueAsString = String.Empty;
                    }  else  {
                        valueAsString = propertyValue.ToString();
                    } 
                    dic.Add(propertyInformation.Name, valueAsString);
                }
            }
            SiteSettingsDAL.SaveSettings(dic);
            
        }
 

        private void Load()
        {
            Type settingsType = this.GetType();  
            System.Collections.Specialized.StringDictionary dic = SiteSettingsDAL.LoadSettings();

            foreach (string key in dic.Keys)
            { 
                string name = key;
                string value = dic[key]; 
                foreach (PropertyInfo propertyInformation in settingsType.GetProperties())
                { 
                    if (propertyInformation.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    { 
                        try  {
                            if (propertyInformation.CanWrite)
                            {
                                propertyInformation.SetValue(this, Convert.ChangeType(value, propertyInformation.PropertyType, CultureInfo.CurrentCulture), null);
                            }
                        }
                        catch
                        {
                            // TODO: Log exception to a common logging framework?
                        }
                        break;
                    }
                }
            }
             
        } 
    } 
}
