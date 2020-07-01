using JobApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobApplication.ViewModels
{
    public class CreateJobViewModel
    {
        public int Id { get; set; }

        public string Employer { get; set; }

        public ICollection<User> Applicants { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "A name should only contain letters")]
        public string Name { get; set; }

        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Please enter a salary")]
        public decimal Salary { get; set; }

        [Display(Name = "Category")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a category")]
        [RegularExpression(@"^[a-zA-Z.,;/''-'\s]{1,30}$", ErrorMessage = "A category should only contain letters")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a description")]
        [RegularExpression("[A-Za-z][A-Za-z0-9.,;/?_\\s*]{10,200}", ErrorMessage = "A description should not contain only numbers and must be between 10 and 200 characters")]
        public string Description { get; set; }

        [Display(Name = "WorkPlace")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a work place")]
        [RegularExpression(@"^[a-zA-Z,;/''-'\s]{1,30}$", ErrorMessage = "A work place should only contain letters")]
        public string WorkPlace { get; set; }

        [Display(Name = "Required Experience")]
        public int? RequiredExperience { get; set; }

        [Display(Name = "Required Education")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a required education")]
        [RegularExpression(@"^[a-zA-Z/''-'\s]{1,30}$", ErrorMessage = "An education should only contain letters")]
        public string RequiredEducation { get; set; }
    }
}
