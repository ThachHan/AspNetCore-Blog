using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{    
    [Table("AccountRoleMap")]
    public partial class AccountRoleMap : BaseEntity
    {
        public virtual int AccountId { get; set; }
        public virtual int RoleId { get; set; }

        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
