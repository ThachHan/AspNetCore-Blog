using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class TagViewModel
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public string TagUrl { get; set; }
    }   
}