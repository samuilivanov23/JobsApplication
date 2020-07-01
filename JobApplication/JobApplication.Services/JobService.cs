using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services.Interfaces;
using JobApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobApplication.Services
{
    /// <summary>
    /// This class implements the IJobService interface and is used
    /// to ensure the functionalities that a certain job has.
    /// </summary>
    public class JobService : IJobService
    {
        private JobApplicationDbContext context;
        private IUserService userService;

        /// <summary>
        /// This is the constructor of the JobService class
        /// </summary>
        /// <param name="context">Data base context</param>
        /// <param name="userService">The service that is responsible for the user</param>
        public JobService(JobApplicationDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        /// <summary>
        /// This method creates a job with the given parameters and adds it
        /// to the database.
        /// </summary>
        /// <param name="name">The name of the job that is being created</param>
        /// <param name="salary">The monthly salary of the job</param>
        /// <param name="category">The category of the job i.e. programming, management, etc.</param>
        /// <param name="description">The description of the job so that users, applying for it, can have a better understanding of the work process</param>
        /// <param name="workPlace">The work place i.e. Sofiq,Bulgaria</param>
        /// <param name="requiredExperience">The experience, required for the job</param>
        /// <param name="requiredEducation">The education, required for the job</param>
        /// <returns>the id of the newly created job.</returns>
        public int CreateJob(string name, 
                             decimal salary, 
                             string category, 
                             string description, 
                             string workPlace,
                             int requiredExperience, 
                             string requiredEducation){

            var loggedUser = userService.GetLoggedUser(); 

            var job = new Job
            {
                Name = name,
                Salary = salary,
                Employer = loggedUser.FirstName + " " + loggedUser.LastName, 
                EmployerPhoneNumber = loggedUser.PhoneNumber,
                Category = category,
                Description = description,
                WorkPlace = workPlace,
                RequiredExperience = requiredExperience,
                RequiredEducation = requiredEducation
            };

            context.Jobs.Add(job);
            context.SaveChanges();

            return job.Id;
        }

        /// <summary>
        /// This method is responsible for getting all the available jobs so the user can browse through them.
        /// If we want to show the jobs that the current user has created (if he is an employer)
        /// the method's only parameter is set to true and if we want to show all available jobs, it is set to false.
        /// </summary>
        /// <param name="viewCreatedJobs">view only created jobs</param>
        /// <returns>A new AllJobs view model for the jobs we have sellected based on the viewCreatedJobs parameter</returns>
        public AllJobsViewModel GetAllJobs(bool viewCreatedJobs)
        {
            var jobs = context.Jobs.Select(j => new CreateJobViewModel()
            {
                Id = j.Id,
                Name = j.Name,
                Employer = j.Employer,
                Salary = j.Salary,
                Category = j.Category,
                WorkPlace = j.WorkPlace,
                Applicants = j.Applicants
            });

            if (viewCreatedJobs)
            {
                var loggedUser = userService.GetLoggedUser();
                var employer = $"{loggedUser.FirstName} {loggedUser.LastName}";
                jobs = jobs.Where(j => j.Employer == employer);
            }
            var model = new AllJobsViewModel() { Jobs = jobs };

            return model;
        }

        /// <summary>
        /// This method returns the requested job based on the given id.
        /// </summary>
        /// <param name="id">the id of the requested job</param>
        /// <returns>Aforementioned</returns>
        public Job ViewJob(int id)
        {
            return context.Jobs.FirstOrDefault(j => j.Id == id);
        }

        /// <summary>
        /// This method allows the user to apply for the listed jobs.
        /// </summary>
        /// <param name="id">The id of the job that the user wants to apply for.</param>
        /// <returns>If there isn't a logged user the method returns 0.
        /// if a user is logged in, but tries to apply for a job that he has already applied for, the method returns -1.
        /// if a user is trying to apply for a job he has created, the method returns -2.
        /// Eventually, if a user is logged in and applies for a job he has not applied yet or created, the method returns 1;
        /// </returns>
        public int ApplyForJob(int id)
        {
            if (LoggedUserInfo.LoggedUserId == 0)
            {
                return 0; //we need to log in
            }

            var loggedUser = userService.GetLoggedUser();

            if (context.Jobs.FirstOrDefault(j => j.Id == id).Applicants.Contains(loggedUser))
            {
                return -1;
            }

            var employer = $"{loggedUser.FirstName} {loggedUser.LastName}";
            if (context.Jobs.FirstOrDefault(j => j.Id == id).Employer == employer) 
            {
                return -2;
            }

            context.Jobs.FirstOrDefault(j => j.Id == id).Applicants.Add(loggedUser);
            context.SaveChanges();
            return 1;
        }
    }
}
