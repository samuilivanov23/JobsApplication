using JobApplication.Controllers.Interfaces;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobApplication.Controllers
{
    /// <summary>
    /// This is the controller class for the user entity.
    /// It inherites the Controller class and provides the actions a certain user has.
    /// </summary>
    public class UserController : Controller, ICheckLoggedUser
    {
        private IUserService service;
        private User loggedUser;
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the UserController class
        /// </summary>
        /// <param name="service">The User service that this controller will use</param>
        /// <param name="context">Data base context</param>
        public UserController(IUserService service, JobApplicationDbContext context)
        {
            this.service = service;
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
                loggedUser = service.GetLoggedUser();
                ViewData["LoggedUser"] = loggedUser;
            }
        }

        /// <summary>
        /// This action returns the Register view in the User folder.
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public IActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// This HttpPost action uses the User service to create a user and save it in tha database.
        /// All the validation is done by using ModelState
        /// if the Register method inside the User service returns -1, 
        /// then the user is prompted (by using the ViewBag message that is passed to the view) 
        /// that a user with the same username/email/phone number 
        /// has already been created and the user stays in the same view until the registration is successful.
        /// When the registration is successful, the user is redirected to the 'Login' action inside the User controller
        /// </summary>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="age">The age of the user</param>
        /// <param name="email">The user's email for additional contact</param>
        /// <param name="phoneNumber">The user's phone number for additional contact</param>
        /// <param name="username">the user's username</param>
        /// <param name="password">the user's password</param>
        /// <param name="confirmPassword">a confirmation of the password (the two passwords should match)</param>
        /// <param name="isEmployer">if true, the user is an employer and can create jobs</param>
        /// <returns>Described above.</returns>
        [HttpPost]
        public IActionResult Register(string firstName, string lastName, int age, string email, 
            string phoneNumber,string username, string password,  string confirmPassword, bool isEmployer) {
            if (ModelState.IsValid)
            {
                if (service.Register(firstName, lastName, age, email, phoneNumber, username, password, confirmPassword, isEmployer) == -1) {
                    ViewBag.Message = "Username/Email or Phone Number already taken!";
                    return View();
                }
            }
            return RedirectToAction("Login", "User");
        }

        /// <summary>
        /// This action return the Login view in the User folder 
        /// and sets the id of the logged user to 0 (meaning there is no logged user).
        /// </summary>
        /// <returns>Aforementioned.</returns>
        public IActionResult Login()
        {
            LoggedUserInfo.LoggedUserId = 0;
            return this.View();
        }

        /// <summary>
        /// This HttpPost action checks if the user has logged in successfully.
        /// </summary>
        /// <param name="username">The username that is used in the login</param>
        /// <param name="password">The password, associated with the given username</param>
        /// <returns>If the user has logged in successfuly, this method redirects him to the index page where he can start listing through jobs.
        /// Otherwise, it redirects him back to the log in page.
        /// </returns>
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                if (service.Login(username, password) == -1)
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return RedirectToAction("Index","Home");
        }

        /// <summary>
        /// This action gets the logged user and his Cv passes them to the Profile view 
        /// </summary>
        /// <returns>The Profile view</returns>
        public IActionResult Profile()
        {
            var loggedUser = service.GetLoggedUser();
            ViewData["User"] = loggedUser;
            ViewData["UserCv"] = context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id);
            CheckLoggedUser();
            return View();
        }
    }
}
