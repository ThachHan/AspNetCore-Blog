using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    /// <summary>
    /// A class which represents the Account table.
    /// </summary>
    [Table("Advertisement")]
    public partial class Advertisement : BaseEntity
    {
        [Key]
        public virtual int AdId { get; set; }
        public virtual string Title { get; set; }
        public virtual string BannerUrl { get; set; }
        public virtual string Link { get; set; }
        public virtual int Position { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime? DateExpired { get; set; }
    }
}
