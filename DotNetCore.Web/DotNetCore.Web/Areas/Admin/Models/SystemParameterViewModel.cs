using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DotNetCore.Web.Areas.Admin.Models
{
    public class SystemParameterViewModel
    {
        public int Id { get; set; }
        public string SystemParameterName { get; set; }
        public string SystemParameterValue { get; set; }
        public string Description { get; set; }

        public IEnumerable<SelectListItem> SystemParamaterNames { get; set; }
    }
}