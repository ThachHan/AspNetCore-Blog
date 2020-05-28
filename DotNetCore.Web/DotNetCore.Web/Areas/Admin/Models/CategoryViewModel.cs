using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        [Required]
        [Remote("IsExistCategoryName", "Category", AdditionalFields ="Id", ErrorMessage = "Category name is exist. Please input category name again.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Image")]
        //[RegularExpression(@"^.*\.(jpg|gif|jpeg|png|bmp|JPG|GIF|JPEG|PNG|BMP)$", ErrorMessage = "File type is not allow")]
        public string ImageUrl { get; set; }

        [Display(Name = "Banner")]
        [Required]
        [RegularExpression(@"^.*\.(jpg|gif|jpeg|png|bmp|JPG|GIF|JPEG|PNG|BMP)$", ErrorMessage = "File type is not allow")]
        public string BannerUrl { get; set; }

        [Display(Name = "Show Top Navigation")]
        public bool IsShowTopMenu { get; set; }

        [Display(Name = "Show Bottom Navigation")]
        public bool IsShowBotMenu { get; set; }

        [Display(Name = "Parent")]
        public int? CategoryParentId { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public string CategoryParentName { get; set; }

        public int? CategoryLayout { get; set; }

        public string CategoryLayoutName { get; set; }

        public string Status { get; set; }

        public IEnumerable<SelectListItem> ListCategory { get; set; }

        public IEnumerable<SelectListItem> ListCategoryLayout { get; set; }
    }    
}