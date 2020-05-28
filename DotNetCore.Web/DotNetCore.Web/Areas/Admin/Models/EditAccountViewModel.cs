using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class EditAccountViewModel
    {
        public int AccountId { get; set; }
       
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must be minimum six characters, at least one uppercase letter, one lowercase letter, one number and one special character.")]
        public string NewPassword { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Password does not match. Please input again.")]
        public string PasswordConfirm { get; set; }      
    }
}