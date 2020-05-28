using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Data.Entity
{
    [Table("Subscribe")]
    public partial class Subscribe : BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
    }
}

