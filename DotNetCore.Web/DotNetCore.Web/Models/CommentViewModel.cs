using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class CommentViewModel
    {
        public int ContentId { get; set; }
        public int TotalComment { get; set; }
        public bool IsAllowComment { get; set; }
        public IList<UserCommentViewModel> UserComments { get; set; }
    }

    public class PostCommentViewModel
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int CommentParentId { get; set; }
        [Required]
        public string Body { get; set; }
        public bool IsAllowComment { get; set; }
    }

    public class UserCommentViewModel
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public int CommentParentId { get; set; }
        public string Body { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public AccountViewModel UserComment { get; set; }
        public IList<UserCommentViewModel> replyViewModels { get; set; }
    }

}