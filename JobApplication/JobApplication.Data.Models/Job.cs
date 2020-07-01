using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Data.Models
{
    public class Job
    {
        public Job()
        {
            Applicants = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public string Employer { get; set; }

        public string EmployerPhoneNumber { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string WorkPlace { get; set; }

        public int? RequiredExperience { get; set; }

        public string RequiredEducation { get; set; }

        public ICollection<User> Applicants { get; set; }
    }
}
