using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string BannerUrl { get; set; }
        public string Summary { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GoogleLink { get; set; }
        public string InstagramLink { get; set; }
        public string Url { get; set; }
    }   
}