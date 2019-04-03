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

    [ASPXUtils.EntityAttribute("AccountType", TableName = "cd_AccountType")]
    [ASPXUtils.DataEntityAttribute(TableName = "cd_AccountType", TablePrefixAppKey = "")]
    public class AccountType: ASPXUtils.IBusinessEntity
    {

        
	[EntityGridAttribute("ID", IsID=true)]  
	[DatafieldAttribute("ID" , DataType = SqlDbType.Int   , IsPrimary=true  )] 
	public int ID{ get; set; }

    private string _guid;
    [Formfield("Unique ID", ControlType = typeof(Label))]
    [DatafieldAttribute("GUID")] 
    public string GUID
    {
        get {
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
 
	[Formfield("Description"      )] 
	[DatafieldAttribute("Description"     )] 
	public string Description{ get; set; }
 
	[Formfield("BillCycleMonths" , ValidateInteger=true      )] 
	[DatafieldAttribute("BillCycleMonths" , DataType = SqlDbType.Int     )]
    [EntityGridAttribute("BillCycleMonths")]
	public int BillCycleMonths{ get; set; }
 
	[Formfield("CostPerMonth"      )] 
	[DatafieldAttribute("CostPerMonth"     )]
    [EntityGridAttribute("CostPerMonth")]
	public decimal CostPerMonth{ get; set; }
 
	[Formfield("StorageGIGs"      )] 
	[DatafieldAttribute("StorageGIGs"     )]
    [EntityGridAttribute("StorageGIGs")]
	public decimal StorageGIGs{ get; set; }

    [Formfield("AdminComment", ControlType = typeof(TextArea))] 
	[DatafieldAttribute("AdminComment"     )] 
	public string AdminComment{ get; set; }
 
	[Formfield("Active"   , ControlType=typeof(DDBit)  )] 
	[DatafieldAttribute("Active"     )]
    [EntityGridAttribute("Active")]
	public bool Active{ get; set; }
  
	[DatafieldAttribute("Deleted"     )] 
	public bool Deleted{ get; set; }
  

         private List<AccountType> _Collection;
         public List<AccountType> Collection  {
             get { return _Collection; }
             set { _Collection = value; }
         }
         public AccountType Select(int ID)
         {
             List<AccountType> oResult = SelectAll().Where(o => o.ID == ID).Take(1).ToList();
             return (oResult.Count > 0) ? oResult[0] : null;
         }
         public int Insert()
         {
             return ASPXUtils.Data.GenericDAL<AccountType>.Insert(this);
         } 
         public void Update()
         {
             ASPXUtils.Data.GenericDAL<AccountType>.Update(this);
         }
         public void Delete()
         {
             ASPXUtils.Data.GenericDAL<AccountType>.Delete(this);
         }
         public List<AccountType> SelectAll()
         {
             if (Collection == null)
                 Collection = ASPXUtils.Data.GenericDAL<AccountType>.GetAll();
             return Collection;
         }
      } 
}


