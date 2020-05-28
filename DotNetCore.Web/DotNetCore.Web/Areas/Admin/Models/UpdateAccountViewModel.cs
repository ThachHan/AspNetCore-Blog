
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetCore.Web.Areas.Admin.Models
{
    public class UpdateAccountViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fullname")]
        public string Name { get; set; }

        [Editable(false)]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "OldPassword")]
        public string OldPassword { get; set; }

        [StringLength(100, MinimumLength = 6)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Career")]
        public string Career { get; set; }

        [Required]
        [Display(Name = "Workplace")]
        public string Workplace { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; }

        public string DepartmentName { get; set; }
    }
}