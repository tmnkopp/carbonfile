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


namespace CarbonDash
{
    enum RoleTypes {
        ATHLETE
    }

    [ASPXUtils.EntityAttribute("Role", TableName = "cd_Roles")]
    [ASPXUtils.DataEntityAttribute(TableName = "cd_Roles", TablePrefixAppKey = "")]
    public class Role : ASPXUtils.IBusinessEntity
    {
         
        [EntityGridAttribute("ID", IsID = true)] 
        [DatafieldAttribute("ID", DataType = SqlDbType.Int, IsPrimary = true)]
        public int ID { get; set; }
        public string Name { get { return RoleName; } }

        [EntityGridAttribute("RoleName")]
        [Formfield("RoleName")]
        [DatafieldAttribute("RoleName")]
        public string RoleName { get; set; }

        [EntityGridAttribute("RoleCode")]
        [Formfield("RoleCode")]
        [DatafieldAttribute("RoleCode")]
        public string RoleCode { get; set; }

        [EntityGridAttribute("Access")]
        [Formfield("Access", ValidateInteger = true)]
        [DatafieldAttribute("Access", DataType = SqlDbType.Int)]
        public int Access { get; set; }

        [EntityGridAttribute("Active")]
        [Formfield("Active", ControlType = typeof(DDTrueFalse))]
        [DatafieldAttribute("Active")]
        public bool Active { get; set; }
         
        [DatafieldAttribute("Deleted")]
        public bool Deleted { get; set; }

         
        private List<Role> _Collection;
        public List<Role> Collection
        {
            get { return _Collection; }
            set { _Collection = value; }
        }
        public Role Select(int ID)
        {
            List<Role> oResult = SelectAll().Where(o => o.ID == ID).Take(1).ToList();
            return (oResult.Count > 0) ? oResult[0] : null;
        }
        public int Insert()
        {
            return ASPXUtils.Data.GenericDAL<Role>.Insert(this);
        }
        public void Update()
        {
            ASPXUtils.Data.GenericDAL<Role>.Update(this);
        }
        public void Delete()
        {
            ASPXUtils.Data.GenericDAL<Role>.Delete(this);
        }
        public List<Role> SelectAll()
        {
            if (Collection == null)
                Collection = ASPXUtils.Data.GenericDAL<Role>.GetAll();
            return Collection;
        }
    }
}


