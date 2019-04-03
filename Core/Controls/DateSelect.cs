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
    public enum DateSpans
    {
        BIWEEKLY,
        MONTHLY, 
        WEEKLY 
    }

    public class DateSelect : UserControl
    {
        public DateSpans DateSpan { get; set; }
        protected Panel ControlHolder = new Panel() { CssClass = "date-select" }; 
        private DropDownList DDMonths = new DropDownList() { ID = "ddMonths" };
        private DropDownList DDDays = new DropDownList() { ID = "ddDays" };
        private DropDownList DDyears = new DropDownList() { ID = "ddYears" };
       
 
        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set {
                _SelectedDate = value;
               
            }
        }

 
        public DateTime GetSelectedDate
        {
            get
            {
                return
                    Convert.ToDateTime(string.Format("{0}/{1}/{2}", DDMonths.SelectedValue, DDDays.SelectedValue, DDyears.SelectedValue));
            }
         
        } 

        private StringBuilder HTML = new StringBuilder();
        protected override void OnInit(EventArgs e) 
        {
            base.OnInit(e);

            Controls.Add(DDMonths);
            Controls.Add(DDDays);
            Controls.Add(DDyears);
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
     
        }

        protected void Page_Load(object sender, EventArgs e)
        {     
        }

        protected void LoadDays()
        {
            DDDays.Items.Clear();
            int day = SelectedDate.Day;
            switch (this.DateSpan)
            {
                case DateSpans.MONTHLY:
                    DDDays.Items.Add(new ListItem("Month (1-*)", "1"));
                    DDDays.SelectedValue = "1";
                    break;

                case DateSpans.WEEKLY:
                  DDDays.Items.Add(new ListItem("Week 1 (1-7)", "1"));
                  DDDays.Items.Add(new ListItem("Week 2 (8-14)", "8"));
                  DDDays.Items.Add(new ListItem("Week 3 (15-21)", "15"));
                  DDDays.Items.Add(new ListItem("Week 3 (22-*)", "22")); 
                    if (day < 15)
                        DDDays.SelectedValue = "1";
                    if (day >= 15)
                        DDDays.SelectedValue = "15";
                    break;

                default:
                    DDDays.Items.Add(new ListItem("Month Beginning (1-14)", "1"));
                    DDDays.Items.Add(new ListItem("Month End (15-*)", "15")); 
                    if (day < 15)
                        DDDays.SelectedValue = "1";
                    if (day >= 15)
                        DDDays.SelectedValue = "15";
                    break;
            }      
                            

        }
        protected void LoadMonth()
        {
            //HTML.AppendFormat("11- {0}<br>", SelectedDate.ToString());
            DDMonths.Items.Clear(); 
            for (int i = 1; i < 13; i++)
            {
                string monthname = DateUtils.GetMonthName(i);
                DDMonths.Items.Add(new ListItem(monthname, i.ToString()));
            }
            DDMonths.SelectedValue = SelectedDate.Month.ToString();
            //HTML.AppendFormat("12 - {0}<br>", SelectedDate.Month.ToString());
        }
        protected void LoadYear()
        {
            DDyears.Items.Clear();
            for (int i = 2012 - 1; i <= System.DateTime.Now.Year + 1; i++)
            {
                DDyears.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            DDyears.SelectedValue = SelectedDate.Year.ToString();
        }
        public void LoadControl( )
        {
            //if (SelectedDate == null)
            //{
            //    if (GetSelectedDate != null)
            //        SelectedDate = GetSelectedDate;
            //    else
            //        SelectedDate = System.DateTime.Now;
            //}
            //HTML.AppendFormat("1 - {0}<br>", SelectedDate.ToString());
            LoadMonth();
            LoadDays();
            LoadYear();
            Response.Write(HTML.ToString());
        }         

    } 
}
 

