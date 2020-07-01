using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    /// <summary>
    /// The interface that is implemented by the Project service
    /// </summary>
    public interface IProjectService
    {
        int CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals);
    }
}
