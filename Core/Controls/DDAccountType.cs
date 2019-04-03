using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonDash
{
    public class DDAccountType : DropDownList
    { 
        public bool HideDefault{get;set;}
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            DataBind();
        }
        public override void DataBind()
        {
            Items.Clear();
            List<AccountType> oList = new AccountType().SelectAll();
            
            if(!HideDefault)
                Items.Add(new ListItem("", ""));

            foreach (AccountType item in oList)
                Items.Add(new ListItem(item.Name, item.ID.ToString())); 
            base.DataBind();
        }
    }
}

