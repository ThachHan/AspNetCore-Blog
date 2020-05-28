using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Identity.Models
{
    public class RegisterViewModel
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
        [Remote("IsUserNameExist", "Account", ErrorMessage = "Username is exist. Please input username again.")]  
        public string UserName { get; set; }

        [Required]
        [EmailAddress]   // kiểm tra địa chỉ email có hợp lệ hay không 
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be minimum six characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]    
        [Compare("Password", ErrorMessage = "Password does not match. Please input correct password again.")]  // So sánh với password đã nhập hay không 
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

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
}