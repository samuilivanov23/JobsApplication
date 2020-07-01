using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Controllers.Interfaces;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller for the CV entity.
    /// It inherites the Controller class and provides the actions a certain CV has.
    /// </summary>
    public class CvsController : Controller, ICheckLoggedUser
    {
        private IUserService UserService;
        private ICvService CVService;
        private User loggedUser;
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the CvsController class.
        /// </summary>
        /// <param name="UserService">The service, responsible for the User</param>
        /// <param name="CVService">The service, responsible for the CV</param>
        /// <param name="context">Data base context</param>
        public CvsController(IUserService UserService, ICvService CVService, JobApplicationDbContext context)
        {
            this.UserService = UserService;
            this.CVService = CVService;
            this.context = context;
        }

        /// <summary>
        /// This method checks if there is a logged user by getting the UserId property from the static class 'LoggedUserInfo'.
        /// If so, the logged user is put in the ViewData which is
        /// passed to the Index, Contact, About and Privacy views
        /// so dynamic user information (like the one used in the navbar) can be seen from all views
        /// </summary>
        public void CheckLoggedUser()
        {
            if (LoggedUserInfo.LoggedUserId != 0)
            {
                loggedUser = UserService.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
        }

        /// <summary>
        /// This action checks the logged user (for dynamic user information as mentioned before)
        /// and returns the CreateCV view where the user can create his CV.
        /// </summary>
        /// <returns>Aformentioned</returns>
        public IActionResult CreateCv()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the CV service to create
        /// a CV with the given parameters.
        /// All the validation is done by using ModelState
        /// </summary>
        /// <param name="education">The education of the user (Masters, Bachelor, etc.)</param>
        /// <param name="experience">The experience of the user expressed by years of labour</param>
        /// <param name="userId">The unique Id of the user whose CV is being created</param>
        /// <returns>A RedirectToAction method which redirects to the 'Profile' Action in the 'User' controller</returns>
        [HttpPost]
        public IActionResult CreateCv(string education, int experience, int userId)
        {
            if (ModelState.IsValid)
            {
                CVService.CreateCv(education, experience, userId);
            }

            CheckLoggedUser();
            return this.RedirectToAction("Profile", "User");
        }

        /// <summary>
        /// Using the CV service, this action takes the user's CV and then puts it into the ViewData 
        /// It then puts the data base context into the ViewData and gets the logged user by using the already mentioned 'CheckLoggedUser' method.
        /// All the ViewData parameters are then passed to the corresponding view.
        /// </summary>
        /// <returns>The ViewCv view where the user can view his CV details</returns>
        public IActionResult ViewCv()
        {
            var userCv = CVService.ViewCv();
            ViewData["UserCv"] = userCv;
            ViewData["Context"] = context;
            CheckLoggedUser();
            return View();
        }
    }
}