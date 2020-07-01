using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplication.Controllers.Interfaces;
using JobApplication.Data.Models;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller class for the Project entity.
    /// It inherites the Controller class and provides the actions a certain project has.
    /// </summary>
    public class ProjectsController : Controller, ICheckLoggedUser
    {
        private IProjectService ProjectService;
        private IUserService UserService;
        private User loggedUser;

        /// <summary>
        /// This is the contructor of the ProjectController class
        /// </summary>
        /// <param name="UserService">The service, responsible for the User</param>
        /// <param name="ProjectService">The service, responsible for the Projects</param>
        public ProjectsController(IUserService UserService, IProjectService ProjectService)
        {
            this.ProjectService = ProjectService;
            this.UserService = UserService;
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
        /// and returns the CreateProject view where the user can create projects he has worked on.
        /// </summary>
        /// <returns>Aforementioned</returns>
        public IActionResult CreateProject()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the Project service
        /// to execute the functionality of creating a project the user has worked on
        /// with the given parameters.
        /// All the validation is done by using ModelState
        /// </summary>
        /// <param name="name">The name of the project</param>
        /// <param name="technology">A string that describes the technologies that were used in the project</param>
        /// <param name="description">A description of the project </param>
        /// <param name="achievedGoals">Goals and milestones that the user(s) have achieved</param>
        /// <param name="futureGoals">Goals and milestones that the user(s) have not yet achieved and want to do so in the future</param>
        /// <returns>A RedirectToAction method which redirects the user to the 'ViewCv' action in the Cvs controller where he can view his CV.
        /// </returns>
        [HttpPost]
        public IActionResult CreateProject(string name, string technology, string description, string achievedGoals, string futureGoals)
        {
            if (ModelState.IsValid)
            {
                ProjectService.CreateProject(name, technology, description, achievedGoals, futureGoals);
            }
            return RedirectToAction("ViewCv", "Cvs");
        }
    }
}