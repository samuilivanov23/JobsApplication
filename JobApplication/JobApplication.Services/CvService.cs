using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobApplication.Services
{
    /// <summary>
    ///  This is a class implements the ICvService interface and is used
    ///  to ensure the functionalities that a certain CV has.
    /// </summary>
    public class CvService : ICvService
    {
        private JobApplicationDbContext context;
        private IUserService userService;

        /// <summary>
        /// This is the constructor of the CvService class
        /// </summary>
        /// <param name="context">Data base context</param>
        /// <param name="userService">The service that is responsible for the user</param>
        public CvService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        /// <summary>
        /// This method creates a cv with the given parameters in the database 
        /// by first getting the logged user
        /// </summary>
        /// <param name="education">The education of the user i.e. Masters, Bachelor, etc.</param>
        /// <param name="experience">The experience of the user (in years)</param>
        /// <param name="userId">The id of the user associated with the current cv.</param>
        /// <returns>The id of the created cv.</returns>
        public int CreateCv(string education, int experience, int userId)
        {
            User loggedUser = userService.GetLoggedUser();
            var cv = new CV
            {
                Education = education,
                Experience = experience,
                UserId = loggedUser.Id
            };

            context.CVs.Add(cv);
            context.SaveChanges(); //dont ask plz

            context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id).User = loggedUser;
            context.SaveChanges();

            return cv.Id;
        }

        /// <summary>
        /// This method returns a Cv that will be used in the ViewCv view
        /// so the user can view his CV in detail.
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public CV ViewCv()
        {
            return context.CVs.FirstOrDefault(c => c.UserId == LoggedUserInfo.LoggedUserId);
        }
    }
}
