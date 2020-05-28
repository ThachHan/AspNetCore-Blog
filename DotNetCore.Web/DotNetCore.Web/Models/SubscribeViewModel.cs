using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Web.Models
{
    public class SubscribeViewModel
    {
        [Required(ErrorMessage = "This field cannot be left blank.")]
        [EmailAddress(ErrorMessage = "Please input correct email again !")]
        public string Email { get; set; }
    }   
}