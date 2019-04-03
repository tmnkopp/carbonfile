using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls; 
using ASPXUtils;
using System.Web.Compilation;
using System.Reflection;
using System.Configuration;

namespace CarbonDash.Core 
{ 
    public class DDYears : DropDownList
    {
        public int From { get; set; }
        public int To { get; set; }
        protected override void OnInit(EventArgs e) 
        {
            if (From == 0)
                From = 2011;
            if (To == 0)
                To = System.DateTime.Now.Year + 2;
            LoadControl();
            base.OnInit(e);
            
        }
        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
     
        }  
        public  void LoadControl()
        {
            this.Items.Clear();
            for (int i = this.From; i <= this.To; i++) 
                this.Items.Add(new ListItem(i.ToString(), i.ToString()));
            
        }      

    } 
}
 

