using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Technology { get; set; }

        public string Description { get; set; }

        public string AchievedGoals { get; set; }

        public string FutureGoals { get; set; }

        public int CvId { get; set; }

        public CV Cv { get; set; }
    }
}
   