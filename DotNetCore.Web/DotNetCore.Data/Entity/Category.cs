using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Category")]
    public partial class Category : BaseEntity
    {
        [Key]
        public virtual int CategoryId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string BannerUrl { get; set; }
        public virtual bool IsShowTopMenu { get; set; }
        public virtual bool IsShowBotMenu { get; set; }
        public virtual int? CategoryParentId { get; set; }
        public virtual int? CategoryLayoutId { get; set; }
        public virtual bool IsActive { get; set; }
    }
}

