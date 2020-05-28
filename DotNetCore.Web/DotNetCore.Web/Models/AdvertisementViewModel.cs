using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore.Web.Models
{
    public class AdvertisementViewModel
    {
        [Key]
        public int AdId { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string Link { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateExpired { get; set; }
    }
}
