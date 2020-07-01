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
    public class ProjectServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateProjectTest()
        {

            var projectData = new List<Project>
            {
                new Project
                {
                    Name = "Qko ime",
                    Technology = "Django",
                    Description = "toq proekt e mn gotin",
                    AchievedGoals = "vsichko kato cqlo",
                    FutureGoals = "za sega nqma"
                }

            }.AsQueryable();

            var userData = new List<User>
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

            var cvData = new List<CV>
            {
                new CV
                {
                    Education = "Bachelors",
                    Experience = 5
                }

            }.AsQueryable();

            var projectMockSet = new Mock<DbSet<Project>>();
            projectMockSet.As<IQueryable<Project>>().Setup(p => p.Provider).Returns(projectData.Provider);
            projectMockSet.As<IQueryable<Project>>().Setup(p => p.Expression).Returns(projectData.Expression);
            projectMockSet.As<IQueryable<Project>>().Setup(p => p.ElementType).Returns(projectData.ElementType);
            projectMockSet.As<IQueryable<Project>>().Setup(p => p.GetEnumerator()).Returns(projectData.GetEnumerator());

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
            mockContext.Setup(c => c.Projects).Returns(projectMockSet.Object);
            mockContext.Setup(c => c.Users).Returns(userMockSet.Object);
            mockContext.Setup(c => c.CVs).Returns(cvMockSet.Object);

            var userService = new UserService(mockContext.Object);
            var cvService = new CvService(mockContext.Object, userService);
            var service = new ProjectService(mockContext.Object, userService, cvService);
            var projects = projectData.ToList();
            var users = userData.ToList();
            var cvs = cvData.ToList();

            users.ForEach(u => userService.Register(u.FirstName, u.LastName, u.Age.Value, u.Email, u.PhoneNumber, u.Username, u.Password, u.ConfirmPassword, u.IsEmployer));
            cvs.ForEach(c => cvService.CreateCv(c.Education, c.Experience, c.UserId));

            projects.ForEach(p => service.CreateProject(p.Name, p.Technology, p.Description, p.AchievedGoals, p.FutureGoals));
            projects.FirstOrDefault(p => p.Name == "Qko ime").CvId = cvs.FirstOrDefault(c => c.Education == "Bachelors").Id;
            projects.FirstOrDefault(p => p.Name == "Qko ime").Cv = cvs.FirstOrDefault(c => c.Education == "Bachelors");
            Assert.AreEqual("Qko ime", mockContext.Object.Projects.FirstOrDefault(p => p.Name == "Qko ime"));
        }
    }
}