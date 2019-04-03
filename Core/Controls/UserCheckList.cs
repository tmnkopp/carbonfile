using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonDash.Core 
{ 
    public class UserCheckList : CheckBoxList
    {
        public RoleTypes RoleType { get; set; }
        protected override void OnLoad(System.EventArgs e)
        { 
            base.OnLoad(e); 
        }
        public override void DataBind()
        {
            base.DataBind();
            List<UserProfile> oList;
            oList = new UserProfile().SelectByRole(RoleType);
             
            foreach (UserProfile item in oList)
                Items.Add(new ListItem(item.FullName, item.ID.ToString()));
        
        }
    }
}
