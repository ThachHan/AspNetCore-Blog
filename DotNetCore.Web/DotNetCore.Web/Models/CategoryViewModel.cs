using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        public bool IsShowTopMenu { get; set; }
        public bool IsShowBotMenu { get; set; }
        public int? CategoryParentId { get; set; }
        public int? CategoryLayout { get; set; }
        public bool IsActive { get; set; }
        public int PostTotals { get; set; }
        public string Url { get; set; }
        public ContentViewModel NewestPost { get; set; }
        public IList<ContentViewModel> ListLastestPosts { get; set; }
    }    

    public class CategorySimpleViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int PostTotals { get; set; }
    }
}