using System;
using System.Collections.Generic;
using System.Text;
using JobApplication.Data.Models;

namespace JobApplication.Services
{
    /// <summary>
    /// The interface that is implemented by the User service
    /// </summary>
    public interface IUserService
    {
        int Register(string firstName, 
                     string lastName, 
                     int age, 
                     string email, 
                     string phoneNumber,
                     string username, 
                     string password, 
                     string confirmPassword, 
                     bool isEmployer);
        int Login(string username, string password);
        User GetLoggedUser();
    }
}
