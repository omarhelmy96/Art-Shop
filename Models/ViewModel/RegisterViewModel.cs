using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="your Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Group")]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Adress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public List<IFormFile> Photos { get; set; }
        public string Gender { get; set; }
        
    }
}

