using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("CategoryLayout")]
    public partial class CategoryLayout : BaseEntity
    {
        [Key]
        public virtual int CategoryLayoutId { get; set; }
        public virtual string CategoryLayoutName { get; set; }
    }
}

