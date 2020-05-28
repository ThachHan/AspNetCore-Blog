using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int CommentParentId { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string TypeComment { get; set; }
        public AccountViewModel UserViewModel { get; set; }
    }   
}