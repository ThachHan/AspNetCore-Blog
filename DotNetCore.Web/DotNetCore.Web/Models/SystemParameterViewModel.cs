using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class SystemParameterViewModel
    {
        public virtual int Id { get; set; }
        public virtual string SystemParameterName { get; set; }
        public virtual string SystemParameterValue { get; set; }
        public virtual string Description { get; set; }
    }

    public class SummaryWebsiteViewModel
    {
        public string SiteLogoUrl { get; set; }
        public string SiteDescription { get; set; }
        public string CopyRight { get; set; }
    }
    public class SocialViewModel
    {
        public string FacebookLink { get; set; }
        public string FacebookFollowers { get; set; }
        public string TwitterLink { get; set; }
        public string TwitterFollowers { get; set; }
        public string GoogleLink { get; set; }
        public string GoogleFollowers { get; set; }
        public string InstagramLink { get; set; }
        public string InstagramFollowers { get; set; }
    }

    public class HeaderViewModel
    {
        public string SiteLogoUrl { get; set; }
    }
}