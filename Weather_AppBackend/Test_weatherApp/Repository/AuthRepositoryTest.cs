using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication_Service.Model;
using Authentication_Service.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Authentication_Service.Repository;
using Authentication_Service.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace Test_WeatherApp.Repository
{
   
        [TestFixture]
        public class AuthRepositoryTest
        {
            private AuthRepo authRepo;
            private UserDbContext dbContext;

            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<UserDbContext>()
                    .UseInMemoryDatabase(databaseName: "Test_User_Database")
                    .Options;

                dbContext = new UserDbContext(options);
                dbContext.Database.EnsureCreated(); // Ensure the in-memory database is created

                authRepo = new AuthRepo(dbContext, Mock.Of<IConfiguration>());
            }

            [TearDown]
            public void Cleanup()
            {
                dbContext.Database.EnsureDeleted(); // Clean up the in-memory database after each test
            }

            [Test]
            public void AuthenticateUser_WhenValidCredentials_ShouldReturnAuthenticationResult()
            {
            // Arrange
            var user = new UserDetail
            {
                _id = "100",
                UserId = 1,
                FullName = "Test User",
                Username = "testuser",
                Password = "password123",
                Email = "test@example.com",
                PhoneNumber = 1234567890
            };
                dbContext.UserDetails.Add(user);
                dbContext.SaveChanges();


            // Act
            var authResult = authRepo.AuthenticateUser("testuser", "password123");

            // Assert
            Assert.NotNull(authResult);
            Assert.AreEqual("testuser", authResult.Username);
                // Add more assertions based on the expected result
            }

            [Test]
            public void AuthenticateUser_WhenInvalidCredentials_ShouldReturnNull()
            {
                // Arrange
                var user = new UserDetail
                {
                    _id ="100",
                    UserId = 1,
                    FullName = "Test User",
                    Username = "testuser",
                    Password = "password123",
                    Email = "test@example.com",
                    PhoneNumber = 1234567890
                };
                dbContext.UserDetails.Add(user);
                dbContext.SaveChanges();

                // Act
                var authResult = authRepo.AuthenticateUser("testuser", "wrongpassword");

                // Assert
                Assert.Null(authResult);
            }

        [Test]
        public void AuthenticateUser_WhenUsernameNullOrEmpty_ShouldReturnNull()
        {
            // Act
            var authResultNullUsername = authRepo.AuthenticateUser(null, "password123");
            var authResultEmptyUsername = authRepo.AuthenticateUser("", "password123");

            // Assert
            Assert.Null(authResultNullUsername);
            Assert.Null(authResultEmptyUsername);
            // Add more assertions if needed
        }

        [Test]
        public void AuthenticateUser_WhenNonExistentUser_ShouldReturnNull()
        {
            // Act
            var authResult = authRepo.AuthenticateUser("nonexistentuser", "password123");

            // Assert
            Assert.Null(authResult);
        }

        [Test]
        public void AuthenticateUser_WhenIncorrectPassword_ShouldReturnNull()
        {
            // Act
            var authResult = authRepo.AuthenticateUser("testuser", "wrongpassword");

            // Assert
            Assert.Null(authResult);
        }

        


    }
}
