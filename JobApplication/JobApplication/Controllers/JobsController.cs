using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using JobApplication.Data.Models;
using JobApplication.Data;
using JobApplication.Controllers.Interfaces;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller for the Job entity.
    /// It inherites the Controller class and provides the actions a certain job has.
    /// </summary>
    public class JobsController : Controller, ICheckLoggedUser
    {
        private IJobService JobsService;
        private IUserService UserService;
        private User loggedUser;
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the JobsController class.
        /// </summary>
        /// <param name="UserService">The service, responsible for the User</param>
        /// <param name="JobsService">The service, responsible for the Jobs</param>
        /// <param name="context">Data base context</param>
        public JobsController(IUserService UserService, IJobService JobsService, JobApplicationDbContext context)
        {
            this.JobsService = JobsService;
            this.UserService = UserService;
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
        /// and returns the CreateJob view where the user that is an employer can create a job.
        /// </summary>
        /// <returns>Aforementioned</returns>
        public IActionResult CreateJob()
        {
            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This HttpPost action uses the Job service to 
        /// create a job with the given parameters.
        /// All the validation is done by using ModelState
        /// </summary>
        /// <param name="name">The name of the job that is being created</param>
        /// <param name="salary">The monthly salary of the job</param>
        /// <param name="category">The category of the job i.e. programming, management, etc.</param>
        /// <param name="description">The description of the job so that users, applying for it, can have a better understanding of the work process</param>
        /// <param name="workPlace">The work place i.e. Sofiq,Bulgaria</param>
        /// <param name="requiredExperience">The experience, required for the job</param>
        /// <param name="requiredEducation">The education, required for the job</param>
        /// <returns>A RedirectToAction method which redirects the user to the 'Index' view in the 'Home' controller
        /// where he can continue searching for jobs.</returns>
        [HttpPost]
        public IActionResult
            CreateJob (string name, decimal salary, string category, string description, string workPlace, int requiredExperience, string requiredEducation){
            if (ModelState.IsValid)
            {
                JobsService.CreateJob(name, salary, category, description, workPlace, requiredExperience, requiredEducation);
            }
            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Using the Jobs service, this action takes the requested by id job and then puts it into the ViewData 
        /// and then checks the logged user 
        /// </summary>
        /// <param name="id">The id of the job the user wants to view in more detail</param>
        /// <returns>Returns the ViewJob view where the user can view the job in more detail</returns>
        public IActionResult ViewJob(int id)
        {
            ViewData["Job"] = JobsService.ViewJob(id);

            CheckLoggedUser();
            return View();
        }

        /// <summary>
        /// This action represents the applying for a job functionaity.
        /// If the user has already applied for the requested job,
        /// he is prompted that he has already applied and nothing happens.
        /// If the user is not logged in yet and tries to apply for a job, he is 
        /// redirected to the log in page where he can log in and then apply for the desired job.
        /// Otherwise (if the user has logged in and wants to apply for a job he has not already applied for), 
        /// the job application is successfull and that job is put in the ViewData 
        /// Depending on the outcome of the 'ApplyForJob' method inside the Jobs service
        /// the ViewBag message is different.
        /// The ViewBag message, alongside with the View data, is passed into the ViewJob view
        /// </summary>
        /// <param name="id">The id of the job the user wants to apply for</param>
        /// <returns>The ViewJob view where the user can view the job in more detail (so in fact - he stays in the same view)</returns>
        public IActionResult ApplyForJob(int id)
        {
            ViewBag.Message = "Successfully applied for job.";
            if (JobsService.ApplyForJob(id) == -1)
            {
                ViewBag.Message = "You have already applied for this job.";
            }else if (JobsService.ApplyForJob(id) == -2)
            {
                ViewBag.Message = "You can't apply for a job you have created.";
            }
            else if(JobsService.ApplyForJob(id) == 0)
            {
                return RedirectToAction("Login", "User");
            }

            ViewData["Job"] = JobsService.ViewJob(id);
            CheckLoggedUser();
            return View("ViewJob");
        }

        /// <summary>
        /// This action returns all the jobs that are created by the logged user (if he is an employer)
        /// It puts the user-created jobs and the DbContext in ViewData and checks the logged user
        /// </summary>
        /// <returns>ViewCreateJobs view where the employer can view the jobs that he created</returns>
        public IActionResult ViewCreatedJobs()
        {
            ViewData["CreatedJobs"] = JobsService.GetAllJobs(true);
            CheckLoggedUser();
            ViewData["Context"] = context;
            return View();
        }
    }

}