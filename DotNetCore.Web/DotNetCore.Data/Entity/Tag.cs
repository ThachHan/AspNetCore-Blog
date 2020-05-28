using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Tag")]
    public partial class Tag : BaseEntity
    {
        [Key]
        public virtual int TagId { get; set; }
        public virtual string TagName { get; set; }
        public virtual string TagUrl { get; set; }
    }
}

