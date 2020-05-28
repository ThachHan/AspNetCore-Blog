using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNetCore.Web.Models;

namespace DotNetCore.Web.Models
{
    public class ContentViewModel
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Source { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        public int PostStatus { get; set; }
        public int AuthorId { get; set; }
        public bool IsAllowComment { get; set; }
        public bool IsShowBanner { get; set; }
        public string Url { get; set; }
        public string DatePublish { get; set; }
        public int TotalComments { get; set; }
        public int Counter { get; set; }
        public AuthorViewModel Author { get; set; }
        public ContentViewModel NextPost { get; set; }
        public ContentViewModel PreviousPost { get; set; }
        public IList<TagViewModel> Tags { get; set; }
        public IList<CategorySimpleViewModel> ListCategoryOfContent { get; set; }
    }   

    public class GalaryViewModel
    {
        public string Title { get; set; }
        public string BannerUrl { get; set; }
    }

    public class SearchContentViewModel
    {
        public string StringSearch { get; set; }
    }
}