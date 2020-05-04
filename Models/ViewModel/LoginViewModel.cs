using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Art_ShopVF.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter Email .")]
        //[EmailAddress(ErrorMessage = "Enter correct e.mail shap")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter correct Password .")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
