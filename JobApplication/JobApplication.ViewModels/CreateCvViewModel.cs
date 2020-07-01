using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.ViewModels
{
    public class CreateCvViewModel
    {
        [Display(Name = "Education")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an education")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "An education should only contain letters")]
        public string Education { get; set; }

        [Display(Name = "Experience")]
        public int Experience { get; set; }
    }
}
