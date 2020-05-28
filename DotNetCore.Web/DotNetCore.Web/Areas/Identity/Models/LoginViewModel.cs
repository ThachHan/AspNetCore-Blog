using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Identity.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string Error { get; set; }

    }
}