using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCore.Data.Entity
{
    public partial class BaseEntity
    {
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual int CreatedUserId { get; set; }
        public virtual int UpdatedUserId { get; set; }
    }
}
