using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("ContentCategoryMap")]
    public partial class ContentCategoryMap : BaseEntity
    {
        public virtual int ContentId { get; set; }
        public virtual int CategoryId { get; set; }

        public Category Category { get; set; }
        public Content Content { get; set; }
    }
}

