using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace CarbonDash.Core 
{
    public class UserProfileList : Control
    {

        #region Properties
        public string CssClass { get; set; }
        private StringBuilder HTML = new StringBuilder();
        public RoleTypes RoleType { get; set; }
        public bool OmitOuterTags { get; set; }

        private bool _Active = true;
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        } 


        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<UserProfile> oList;
            if (Active)
            {
                oList = new UserProfile().SelectByRole(RoleType).Where(o => o.Active == "True").ToList();
            }
            else {
                oList = new UserProfile().SelectByRole(RoleType).Where(o => o.Active != "True").ToList();
            }
            

            if (!OmitOuterTags)
                HTML.AppendFormat("<ul class='UserProfileList'>", "");

            foreach (UserProfile item in oList)
            {
                string link = string.Format(Globals.ProQStr, item.ID.ToString(), "", "", ""); 
                HTML.Append("<li>"); 
                HTML.Append(string.Format("<a href='{0}'>{1}</a>", link, item.FullName)); 
                HTML.Append("</li>");
            }
            if (!OmitOuterTags)
                HTML.AppendFormat("</ul>", "");
            
        }
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteLine(HTML.ToString());
            base.Render(writer);
        }
    }
}
