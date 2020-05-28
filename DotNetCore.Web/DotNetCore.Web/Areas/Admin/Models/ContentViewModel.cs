using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class ContentViewModel
    {
        public int ContentId { get; set; }

        [Display(Name = "Head Line")]
        [Required]
        [Remote("IsExistContentName", "Content", AdditionalFields = "Id", ErrorMessage = "Category name is exist. Please input category name again.")]
        public string Title { get; set; }

        [Display(Name = "Summary")]
        [Required]
        public string Summary { get; set; }

        [Display(Name = "Source")]
        public string Source { get; set; }

        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Display(Name = "Body")]
        public string Body { get; set; }

        //[RegularExpression(@"^.*\.(jpg|gif|jpeg|png|bmp|JPG|GIF|JPEG|PNG|BMP)$", ErrorMessage = "File type is not allow")]
        //[Required]
        public string ImageUrl { get; set; }

        [RegularExpression(@"^.*\.(jpg|gif|jpeg|png|bmp|JPG|GIF|JPEG|PNG|BMP)$", ErrorMessage = "File type is not allow")]
        [Required]
        public string BannerUrl { get; set; }

        public int PostStatus { get; set; }

        [Display(Name = "Allow Comment")]
        public bool IsAllowComment { get; set; }

        [Display(Name = "Show Banner")]
        public bool IsShowBanner { get; set; }

        public string NameStatus { get; set; }

        [Display(Name = "Tag")]
        public int[] Tag { get; set; }

        [Display(Name = "Tag")]
        public string Tags { get; set; }

        public int Counter { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CreatedUserId { get; set; }

        public int UpdatedUserId { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<SelectListItem> ListTag { get; set; }

        public IEnumerable<SelectListItem> ListAuthor { get; set; }

        public IEnumerable<SelectListItem> ListCategory { get; set; }

        [Display(Name = "Categories")]
        public int[] Categories { get; set; }

    }

    public class PublishingContentViewModel
    {
        public int ContentId { get; set; }

        [Display(Name = "Headline")]
        public string Title { get; set; }

        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Display(Name = "Category Publish")]
        [Required]
        public int[] Categories { get; set; }

        public IEnumerable<SelectListItem> ListCategory { get; set; }
    }
}