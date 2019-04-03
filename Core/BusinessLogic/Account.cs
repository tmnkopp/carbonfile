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

    [ASPXUtils.EntityAttribute("Account", TableName = "cd_Account")]
    [ASPXUtils.DataEntityAttribute(TableName = "cd_Account", TablePrefixAppKey = "")]
    public class Account: ASPXUtils.IBusinessEntity
    {

        
	[EntityGridAttribute("ID", IsID=true)]  
	[DatafieldAttribute("ID" , DataType = SqlDbType.Int   , IsPrimary=true  )] 
	public int ID{ get; set; }

    private string _guid;
    [Formfield("Unique ID", ControlType = typeof(Label))]
    [DatafieldAttribute("GUID")]
    [EntityGridAttribute("AccountID")]
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

    public string Name { get { return this.GUID; } }

	[DatafieldAttribute("CompanyID" , DataType = SqlDbType.Int     )] 
	public int CompanyID{ get; set; }

    [Formfield("AccountType", ValidateInteger = true, ControlType = typeof(DDAccountType))] 
	[DatafieldAttribute("AccountType" , DataType = SqlDbType.Int     )] 
	public int AccountType{ get; set; }
 
	[Formfield("IsApproved"   , ControlType=typeof(DDBit)  )] 
	[DatafieldAttribute("IsApproved"     )]
    [EntityGridAttribute("IsApproved")]
	public bool IsApproved{ get; set; }
 
	[Formfield("CreateDate" , ValidateDate = true    , ControlType = typeof(ExtendedTextBox) , ShowCalendar = true    )] 
	[DatafieldAttribute("CreateDate" , DataType = SqlDbType.DateTime     )] 
	public DateTime CreateDate{ get; set; }
 
	[Formfield("ActivationDate" , ValidateDate = true    , ControlType = typeof(ExtendedTextBox) , ShowCalendar = true    )] 
	[DatafieldAttribute("ActivationDate" , DataType = SqlDbType.DateTime     )]
    [EntityGridAttribute("ActivationDate")]
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
 



 

         private List<Account> _Collection;
         public List<Account> Collection  {
             get { return _Collection; }
             set { _Collection = value; }
         }
         public Account Select(int ID)
         {
             List<Account> oResult = SelectAll().Where(o => o.ID == ID).Take(1).ToList();
             return (oResult.Count > 0) ? oResult[0] : null;
         }
         public int Insert()
         {
             return ASPXUtils.Data.GenericDAL<Account>.Insert(this);
         } 
         public void Update()
         {
             ASPXUtils.Data.GenericDAL<Account>.Update(this);
         }
         public void Delete()
         {
             ASPXUtils.Data.GenericDAL<Account>.Delete(this);
         }
         public List<Account> SelectAll()
         {
             if (Collection == null)
                 Collection = ASPXUtils.Data.GenericDAL<Account>.GetAll();
             return Collection;
         }
      } 
}


