using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Role")]
    public partial class Role : BaseEntity
    {
        [Key]
        public virtual int RoleId { get; set; }
        public virtual string RoleName { get; set; }
    }
}

