using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("ContentTagMap")]
    public partial class ContentTagMap : BaseEntity
    {
        public virtual int ContentId { get; set; }
        public virtual int TagId { get; set; }

        public Tag Tag { get; set; }
        public Content Content { get; set; }
    }
}

