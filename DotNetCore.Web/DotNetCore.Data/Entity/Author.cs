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
    [Table("Author")]
    public partial class Author : BaseEntity
    {
        [Key]
        public virtual int AuthorId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string BannerUrl { get; set; }
        public virtual string Summary { get; set; }
        public virtual string FacebookLink { get; set; }
        public virtual string TwitterLink { get; set; }
        public virtual string GoogleLink { get; set; }
        public virtual string InstagramLink { get; set; }
    }
}
