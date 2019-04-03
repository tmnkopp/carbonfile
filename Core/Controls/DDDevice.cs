using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarbonDash
{
    public class DDDevice : DropDownList
    { 
        public bool HideDefault{get;set;}
        public override void DataBind()
        {
            Items.Clear();
            List<Device> oList = new Device().SelectAll();
            
            if(!HideDefault)
                Items.Add(new ListItem("", ""));

            foreach (Device item in oList)
                Items.Add(new ListItem(item.Name, item.ID.ToString())); 
            base.DataBind();
        }
    }
}

