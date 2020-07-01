using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Models;
using JobApplication.Services;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using JobApplication.Controllers.Interfaces;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the home controller class.
    /// It inherites the Controller class and provides the initial actions a certain user has.
    /// </summary>
    public class HomeController : Controller, ICheckLoggedUser
    {
        private IUserService UserService;
        private IJobService JobsService;
        private User loggedUser;

        /// <summary>
        /// This is the constructor of the HomeController class.
        /// </summary>
        /// <param name="UserService">The service, responsible for the User</param>
        /// <param name="jobsService">The service, responsible for the Jobs</param>
        public HomeController(IUserService UserService, IJobService jobsService)
        {
            this.UserService = UserService;
            this.JobsService = jobsService;
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
        /// This action represents the Index page - the initial page that the user is put into
        /// It checks the logged user and puts all created jobs in the ViewData and 
        /// passes it to the Index view.
        /// </summary>
        /// <returns>The Index view</returns>
        public IActionResult Index()
        {
            var allJobs = JobsService.GetAllJobs(false);
            ViewData["AllJobs"] = allJobs;

            CheckLoggedUser();

            return View();
        }

        /// <summary>
        /// This action checks the logged user and puts a message in the ViewData
        /// that details the contents of the view.
        /// </summary>
        /// <returns>The About view</returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            CheckLoggedUser();

            return View();
        }

        /// <summary>
        /// This action checks the logged user and puts a message in the ViewData
        ///that details the contents of the view.
        /// </summary>
        /// <returns>The Contact view</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            CheckLoggedUser();

            return View();
        }

        /// <summary>
        /// This action checks if there is any error during the run of the application.
        /// </summary>
        /// <returns>Thew Error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
