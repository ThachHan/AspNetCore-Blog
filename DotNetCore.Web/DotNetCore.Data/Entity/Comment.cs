using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Comment")]
    public partial class Comment : BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int ContentId { get; set; }
        public virtual int UserId { get; set; }
        public virtual string Body { get; set; }
        public virtual int? CommentParentId { get; set; }
        public virtual bool IsActive { get; set; }
    }
}

