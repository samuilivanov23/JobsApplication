using JobApplication.Data;
using JobApplication.Data.Models;
using System;
using System.Linq;

namespace JobApplication.Services
{
    /// <summary>
    /// This class implements the IUserService interface and is used 
    /// to ensure the user functionalities (To be able to log in, to be able to register in the data base and 
    /// to get the user that is currently logged in)
    /// </summary>
    public class UserService : IUserService
    {
        private JobApplicationDbContext context;

        /// <summary>
        /// This is the constructor of the UserService class
        /// </summary>
        /// <param name="context">Data base context</param>
        public UserService(JobApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// This method adds a user with the given parameters in the User table of the database.
        /// First it checks if the there has already been a user created with the same username/email or phone number.
        /// If that is true, the method returns -1.
        /// Otherwise, the user gets successfuly created and added into the database
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
        /// <returns>The Id of the registered user or, as mentioned above, -1 if the the registration failed</returns>
        public int Register(string firstName,
                            string lastName,
                            int age,
                            string email,
                            string phoneNumber,
                            string username,
                            string password,
                            string confirmPassword,
                            bool isEmployer) {

            bool takenInfo = context.Users.FirstOrDefault(x => x.Username == username) != null 
                || context.Users.FirstOrDefault(x => x.Email == email) != null 
                ||context.Users.FirstOrDefault(x => x.PhoneNumber == phoneNumber) != null;

            if (takenInfo)
            {
                return -1;
            }
            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                Email = email,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword,
                IsEmployer = isEmployer
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        /// <summary>
        /// This methods logs the user in by first checking if a user with the given username/pass exists in the database
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password, associated with the given username</param>
        /// <returns>This method returns the user Id if it exists in the data base.</returns>
        public int Login(string username, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null)
            {
                LoggedUserInfo.LoggedUserId = user.Id;
                return user.Id;
            }
            return -1;
        }

        /// <summary>
        /// This method returns the user that is currently logged in.
        /// If the user doesn't have a CV, it returns the user straight from the database (idk either)
        /// </summary>
        /// <returns>The logged in user.</returns>
        public User GetLoggedUser()
        {
            var loggedUser = context.Users.FirstOrDefault(x => x.Id == LoggedUserInfo.LoggedUserId);
            if (context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id) != null)
            {
                return context.CVs.FirstOrDefault(c => c.UserId == loggedUser.Id).User;
            }
            return loggedUser;
        }
    }
}
