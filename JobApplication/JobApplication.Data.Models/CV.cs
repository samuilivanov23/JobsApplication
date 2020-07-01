using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Data.Models
{
    public class CV
    {
        public CV()
        {
            Projects = new List<Project>();
        }

        public int Id { get; set; }

        public string Education { get; set; }

        public int Experience { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
