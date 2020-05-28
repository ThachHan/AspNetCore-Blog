using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class BannerViewModel
    {
        public ContentViewModel NewestPost { get; set; }
        public IList<ContentViewModel> TopNewestPosts { get; set; }
    }
}