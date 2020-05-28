using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.Data.Models
{
    public class NavigationItem
    {
        public int NavigationId { get; set; }
        public string NavigationName { get; set; }
        public string NavigatioUrl { get; set; }

        public IList<NavigationItem> NavigationItems { get; set; }
    }
}
