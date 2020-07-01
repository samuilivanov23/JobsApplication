using JobApplication.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Services.Interfaces
{
    /// <summary>
    /// The interface that is implemented by the CV service
    /// </summary>
    public interface ICvService
    {
        int CreateCv(string education, int experience, int userId);

        CV ViewCv();
    }
}
