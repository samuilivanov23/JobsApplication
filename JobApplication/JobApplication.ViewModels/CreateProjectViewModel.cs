using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.ViewModels
{
    public class CreateProjectViewModel
    {
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A name should only contain letters")]
        public string Name { get; set; }

        [Display(Name = "Technology")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter technology")]
        [RegularExpression("[A-Za-z][A-Za-z0-9.,;/?_\\s*]{5,200}", ErrorMessage = "Technologies should not contain only numbers and must be between 5 and 100 characters")]
        public string Technology { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a description")]
        [RegularExpression("[A-Za-z][A-Za-z0-9.,;/?_\\s*]{10,200}", ErrorMessage = "A description should not contain only numbers and must be between 10 and 200 characters")]
        public string Description { get; set; }

        [Display(Name = "Achieved goals")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter achieved goals")]
        [RegularExpression("[A-Za-z][A-Za-z0-9.,/?;_\\s*]{5,200}", ErrorMessage = "This field should not contain only numbers and must be between 5 and 200 characters")]
        public string AchievedGoals { get; set; }

        [Display(Name = "Future goals")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your future goals")]
        [RegularExpression("[A-Za-z][A-Za-z0-9.,/?;_\\s*]{5,200}", ErrorMessage = "This field should not contain only numbers and must be between 5 and 200 characters")]
        public string FutureGoals { get; set; }
    }
}
