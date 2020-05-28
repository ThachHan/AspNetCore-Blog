using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"^.*\.(jpg|gif|jpeg|png|bmp|JPG|GIF|JPEG|PNG|BMP)$", ErrorMessage = "File type is not allow")]
        [Display(Name = "Image Url")]
        [Required]
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        [Display(Name = "Summary")]
        [Required]
        public string Summary { get; set; }
        [Display(Name = "Facebook")]
        public string FacebookLink { get; set; }
        [Display(Name = "Twitter")]
        public string TwitterLink { get; set; }
        [Display(Name = "Google")]
        public string GoogleLink { get; set; }
        [Display(Name = "Instagram")]
        public string InstagramLink { get; set; }
    }    
}