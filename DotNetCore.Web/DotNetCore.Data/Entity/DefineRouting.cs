using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Data.Entity
{
    [Table("DefineRouting")]
    public partial class DefineRouting : BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string FriendlyUrlLv1 { get; set; }
        public virtual string FriendlyUrlLv2 { get; set; }
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual int EntityId { get; set; }
    }
}
