using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonDash.Core 
{
    public class DDRUserProfile : DropDownList
    {
        public RoleTypes RoleType { get; set; } 
        protected override void OnInit(System.EventArgs e)
        {
            Items.Clear();
            List<User> oList;
            oList = new User().SelectByRole(RoleType);
 
            Items.Add(new ListItem("", ""));
            foreach (User item in oList)
                Items.Add(new ListItem( item.FullName, item.ID.ToString()));

            base.OnInit(e);
        }
    }
}
