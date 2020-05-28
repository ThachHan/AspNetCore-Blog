using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class AccountViewModel
    {
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]  // kiểm tra độ dài chuỗi string : max 100 , min 5
        [Display(Name = "Username")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Not allow special character.")]
        [Remote("IsUserNameExist", "Account", AdditionalFields = "Id", ErrorMessage = "Username is exist. Please input username again.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]   // kiểm tra địa chỉ email có hợp lệ hay không 
        [Remote("IsEmailExist", "Account", AdditionalFields = "Id", ErrorMessage = "Email is exist. Please input email again.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "WorkPlace")]
        public string WorkPlace { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

    }
    public class AccountDetailViewModel
    {
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]  // kiểm tra độ dài chuỗi string : max 100 , min 5
        [Display(Name = "Username")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Not allow special character.")]
        [Remote("IsUserNameExist", "Account", AdditionalFields = "Id", ErrorMessage = "Username is exist. Please input username again.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]   // kiểm tra địa chỉ email có hợp lệ hay không 
        [Remote("IsEmailExist", "Account", AdditionalFields = "Id", ErrorMessage = "Email is exist. Please input email again.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "WorkPlace")]
        public string WorkPlace { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public string Role { get; set; }

        public int Roles { get; set; }

        public IEnumerable<SelectListItem> ListRole { get; set; }
    }
    public class LoginViewModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}