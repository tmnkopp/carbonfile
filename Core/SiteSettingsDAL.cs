using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using System.Web;
 
namespace CarbonDash.Core
{
    public static class SiteSettingsDAL
    {
        private static string CacheSettingName = "DASH_SiteSettings_{0}";
        public static void ResetCache()  {
            ASPXUtils.SiteCache.Remove(string.Format(SiteSettingsDAL.CacheSettingName, "LoadSettings"));
        }
        public static StringDictionary LoadSettings()
        {
     
            StringDictionary dic = new StringDictionary();
            DataSet objDataSet = ASPXUtils.SiteCache.GetDataSet(string.Format(SiteSettingsDAL.CacheSettingName, "LoadSettings")); 
            if (objDataSet == null)  {
                SqlHelper objSqlHelper = new SqlHelper();
                objDataSet = objSqlHelper.GetDatasetByCommand("Select SettingName, SettingValue From  SiteSettings  WHERE SITE_ID = '" + SiteSettings.SITE_ID + "'   Order By SettingName");
                objSqlHelper.Dispose();
                ASPXUtils.SiteCache.Add(string.Format(SiteSettingsDAL.CacheSettingName, "LoadSettings"), objDataSet); 
            } 
            foreach (DataRow oDataRow in objDataSet.Tables[0].Rows) {
                string name = Convert.ToString(oDataRow["SettingName"]);
                string value = Convert.ToString(oDataRow["SettingValue"]); 
                dic.Add(name, value);
            }
            return dic;
        }
        public static void SaveSettings(StringDictionary dic) { 
            ResetCache();
            string sSql = "Delete From  SiteSettings   WHERE SITE_ID = '" + SiteSettings.SITE_ID + "'  ;";
            SqlHelper objSqlHelper = new SqlHelper();
            int rec = 0;
            foreach (string key in dic.Keys)  {
                sSql += " INSERT INTO  SiteSettings ( SITE_ID,SettingName, SettingValue) VALUES (@SITE_ID" + rec.ToString() + ", @SettingName" + rec.ToString() + ", @SettingValue" + rec.ToString() + ");";
                objSqlHelper.AddParameterToSQLCommand("@SettingName" + rec.ToString(), SqlDbType.VarChar, Convert.ToString(key));
                objSqlHelper.AddParameterToSQLCommand("@SettingValue" + rec.ToString(), SqlDbType.VarChar, Convert.ToString(dic[key]));
                objSqlHelper.AddParameterToSQLCommand("@SITE_ID" + rec.ToString(), SqlDbType.VarChar, SiteSettings.SITE_ID);
                rec++;
            }
            objSqlHelper.GetExecuteNonQueryByCommand(sSql);
            objSqlHelper.Dispose(); 
        }
    }
}
 