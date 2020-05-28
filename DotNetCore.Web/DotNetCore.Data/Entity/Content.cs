using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Content")]
    public partial class Content : BaseEntity
    {
        [Key]
        public virtual int ContentId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Source { get; set; }
        public virtual string Body { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string BannerUrl { get; set; }
        public virtual int PostStatus { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual bool IsAllowComment { get; set; }
        public virtual bool IsShowBanner { get; set; }
        public virtual int Counter { get; set; }
    }
}

