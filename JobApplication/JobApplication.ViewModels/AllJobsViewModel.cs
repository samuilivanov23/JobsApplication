using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.ViewModels
{
    public class AllJobsViewModel
    {
        public IEnumerable<CreateJobViewModel> Jobs { get; set; }
    }
}
