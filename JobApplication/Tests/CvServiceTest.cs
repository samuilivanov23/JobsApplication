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
    public class CvServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateCvTest()
        {
            var userData = new List<User>
            {
                new User
                {
                    Id = 1,
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

            var cvData = new List<CV>
            {
                new CV
                {
                    Id = 1,
                    Education = "Bachelors",
                    Experience = 5
                }

            }.AsQueryable();

            var userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(u => u.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(u => u.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(u => u.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(u => u.GetEnumerator()).Returns(userData.GetEnumerator());

            var cvMockSet = new Mock<DbSet<CV>>();
            cvMockSet.As<IQueryable<CV>>().Setup(c => c.Provider).Returns(cvData.Provider);
            cvMockSet.As<IQueryable<CV>>().Setup(c => c.Expression).Returns(cvData.Expression);
            cvMockSet.As<IQueryable<CV>>().Setup(c => c.ElementType).Returns(cvData.ElementType);
            cvMockSet.As<IQueryable<CV>>().Setup(c => c.GetEnumerator()).Returns(cvData.GetEnumerator());

            var mockContext = new Mock<JobApplicationDbContext>();
            mockContext.Setup(c => c.Users).Returns(userMockSet.Object);
            mockContext.Setup(c => c.CVs).Returns(cvMockSet.Object);

            var userService = new UserService(mockContext.Object);
            var cvService = new CvService(mockContext.Object, userService);
            var users = userData.ToList();
            var cvs = cvData.ToList();

            users.ForEach(u => userService.Register(u.FirstName, u.LastName, u.Age.Value, u.Email, u.PhoneNumber, u.Username, u.Password, u.ConfirmPassword, u.IsEmployer));
            mockContext.Object.Users.FirstOrDefault(u => u.FirstName == "Stamat").Id = users[0].Id;
            cvs.ForEach(c => cvService.CreateCv(c.Education, c.Experience, users[0].Id));
            mockContext.Object.CVs.FirstOrDefault(c => c.Education == "Bachelors").Id = cvs[0].Id;

            Assert.AreEqual(1, mockContext.Object.CVs.FirstOrDefault(c => c.Education == "Bachelors").Id);

            //projects.ForEach(p => service.CreateProject(p.Name, p.Technology, p.Description, p.AchievedGoals, p.FutureGoals));
            //projects.FirstOrDefault(p => p.Name == "Qko ime").CvId = cvs.FirstOrDefault(c => c.Education == "Bachelors").Id;
            //projects.FirstOrDefault(p => p.Name == "Qko ime").Cv = cvs.FirstOrDefault(c => c.Education == "Bachelors");
            //Assert.AreEqual("Qko ime", mockContext.Object.Projects.FirstOrDefault(p => p.Name == "Qko ime"));
        }
    }
}