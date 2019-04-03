using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ASPXUtils;
using ASPXUtils.Controls;
 

namespace CarbonDash {

    [ASPXUtils.EntityAttribute("Company", TableName = "cd_Company")]
    [ASPXUtils.DataEntityAttribute(TableName = "cd_Company", TablePrefixAppKey = "")]
    public class Company: ASPXUtils.IBusinessEntity
    {
         
	[EntityGridAttribute("ID", IsID=true)]  
	[DatafieldAttribute("ID" , DataType = SqlDbType.Int   , IsPrimary=true  )] 
	public int ID{ get; set; }

    private string _guid;
    [Formfield("Unique ID", ControlType = typeof(Label))]
    [DatafieldAttribute("GUID")]
    public string GUID
    {
        get
        {
            if (_guid == null || _guid == "")
                _guid = System.Guid.NewGuid().ToString().ToUpper();
            return _guid;
        }
        set { _guid = value; }
    }
 
	[Formfield("Name"      )] 
	[DatafieldAttribute("Name"     )]
    [EntityGridAttribute("Name")]
	public string Name{ get; set; }
 
	[Formfield("Address"      )] 
	[DatafieldAttribute("Address"     )] 
	public string Address{ get; set; }
 
	[Formfield("City"      )] 
	[DatafieldAttribute("City"     )] 
	public string City{ get; set; }
 
	[Formfield("State"      )] 
	[DatafieldAttribute("State"     )] 
	public string State{ get; set; }
 
	[Formfield("Zip"      )] 
	[DatafieldAttribute("Zip"     )] 
	public string Zip{ get; set; }
 
	[Formfield("Phone"      )] 
	[DatafieldAttribute("Phone"     )]
    [EntityGridAttribute("Phone")]
	public string Phone{ get; set; }
 
	[Formfield("IsApproved"   , ControlType=typeof(DDBit)  )] 
	[DatafieldAttribute("IsApproved"     )]
    [EntityGridAttribute("IsApproved")]
	public bool IsApproved{ get; set; }
 
	[Formfield("CreateDate" , ValidateDate = true    , ControlType = typeof(ExtendedTextBox) , ShowCalendar = true    )] 
	[DatafieldAttribute("CreateDate" , DataType = SqlDbType.DateTime     )] 
	public DateTime CreateDate{ get; set; }
 
	[Formfield("ActivationDate" , ValidateDate = true    , ControlType = typeof(ExtendedTextBox) , ShowCalendar = true    )] 
	[DatafieldAttribute("ActivationDate" , DataType = SqlDbType.DateTime     )] 
	public DateTime ActivationDate{ get; set; }
 
	[Formfield("LastLoginDate" , ValidateDate = true    , ControlType = typeof(ExtendedTextBox) , ShowCalendar = true    )] 
	[DatafieldAttribute("LastLoginDate" , DataType = SqlDbType.DateTime     )]
    [EntityGridAttribute("LastLoginDate")]
	public DateTime LastLoginDate{ get; set; }
 
	[Formfield("AdminComment"      )] 
	[DatafieldAttribute("AdminComment"     )] 
	public string AdminComment{ get; set; }
 
	[Formfield("Active"   , ControlType=typeof(DDBit)  )] 
	[DatafieldAttribute("Active"     )]
    [EntityGridAttribute("Active")]
	public bool Active{ get; set; }
  
	[DatafieldAttribute("Deleted"     )] 
	public bool Deleted{ get; set; }
 



 

         private List<Company> _Collection;
         public List<Company> Collection  {
             get { return _Collection; }
             set { _Collection = value; }
         }
         public Company Select(int ID)
         {
             List<Company> oResult = SelectAll().Where(o => o.ID == ID).Take(1).ToList();
             return (oResult.Count > 0) ? oResult[0] : null;
         }
         public int Insert()
         {
             return ASPXUtils.Data.GenericDAL<Company>.Insert(this);
         } 
         public void Update()
         {
             ASPXUtils.Data.GenericDAL<Company>.Update(this);
         }
         public void Delete()
         {
             ASPXUtils.Data.GenericDAL<Company>.Delete(this);
         }
         public List<Company> SelectAll()
         {
             if (Collection == null)
                 Collection = ASPXUtils.Data.GenericDAL<Company>.GetAll();
             return Collection;
         }
      } 
}


