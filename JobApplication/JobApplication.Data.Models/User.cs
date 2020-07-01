using System;
using System.Collections.Generic;

namespace JobApplication.Data.Models
{
    public class User
    {
        public User()
        {
            UserCreatedOn = DateTime.UtcNow;
        }


        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public bool IsEmployer { get; set; }

        public DateTime UserCreatedOn { get; set; }
    }
}
