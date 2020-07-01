using JobApplication.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.ViewModels
{
    public class LoginUserViewModel
    {
        
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a username")]
        [RegularExpression("[A-Za-z][A-Za-z0-9._]{4,14}", ErrorMessage = "Invalid username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Invalid password")]
        public string Password { get; set; }

    }
}
