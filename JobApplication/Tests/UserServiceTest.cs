using NUnit.Framework;
using JobApplication.Data;
using JobApplication.Data.Models;
using JobApplication.Services;
using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void RegisterUser()
        {

            var data = new List<User>
            {
                new User
                {
                    FirstName = "Stamat",
                    LastName = "Stamatov",
                    Age = 13,
                    Email = "stamat@gmial.com",
                    PhoneNumber = "0877253698",
                    Username = "stamat13",
                    Password = "1234",
                    ConfirmPassword = "1234",
                    IsEmployer = false
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(u => u.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(u => u.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(u => u.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<JobApplicationDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new UserService(mockContext.Object);
            var users = data.ToList();

            users.ForEach(u => service.Register(u.FirstName, u.LastName, u.Age.Value, u.Email, u.PhoneNumber, u.Username, u.Password, u.ConfirmPassword, u.IsEmployer));

            Assert.AreEqual("Stamat", mockContext.Object.Users.FirstOrDefault(u => u.FirstName == "Stamat").FirstName);
        }

        [Test]
        public void LogInUser()
        {
            var data = new List<User>
            {
                new User
                {
                    FirstName = "Stamat",
                    LastName = "Stamatov",
                    Age = 13,
                    Email = "stamat@gmial.com",
                    PhoneNumber = "0877253698",
                    Username = "stamat13",
                    Password = "1234",
                    ConfirmPassword = "1234",
                    IsEmployer = false
                }

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(u => u.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(u => u.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(u => u.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<JobApplicationDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var service = new UserService(mockContext.Object);
            var users = data.ToList();

            users.ForEach(u => service.Register(u.FirstName, u.LastName, u.Age.Value, u.Email, u.PhoneNumber, u.Username, u.Password, u.ConfirmPassword, u.IsEmployer));
            int logInResult = service.Login(users[0].Username, users[0].Password);

            Assert.AreEqual(0, logInResult);
        }
    }
}