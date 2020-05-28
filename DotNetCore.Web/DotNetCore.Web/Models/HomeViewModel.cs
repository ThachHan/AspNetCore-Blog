using DotNetCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Models
{
    public class HomeViewModel
    {
        public CategoryViewModel groupProject { get; set; }
        public List<ContentViewModel> ListContent { get; set; }
      

        public HomeViewModel()
       {
           groupProject = new CategoryViewModel();
           ListContent = new List<ContentViewModel>();
       }
    }
}