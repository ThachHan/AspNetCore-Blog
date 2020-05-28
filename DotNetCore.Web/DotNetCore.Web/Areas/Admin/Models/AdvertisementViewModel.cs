using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class AdvertisementViewModel
    {
        public virtual int AdId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public virtual string Title { get; set; }
        [Required]
        [Display(Name = "Banner Url")]
        public virtual string BannerUrl { get; set; }
        [Required]
        [Display(Name = "Link")]
        public virtual string Link { get; set; }
        [Required]
        [Display(Name = "Position")]
        public virtual string PositionAdvertisement { get; set; }
        [Required]
        [Display(Name = "Is Active")]
        public virtual bool IsActive { get; set; }

        [Display(Name = "Date Expired")]
        public virtual DateTime? DateExpired { get; set; }

        public IEnumerable<SelectListItem> AdvertisementPostions { get; set; }
        public string Status { get; set; }
    }
}
