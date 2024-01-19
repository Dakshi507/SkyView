using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User_Service.Exceptions;
using User_Service.Model;
using User_Service.Service;

namespace User_Service.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _userController;
        private Mock<IUserService> _mockUserService;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Test]
        public async Task CreateUser_WithValidUser_ShouldReturnOk()
        {
            // Arrange
            var validUser = new UserDetails
            {
                UserId = 1,
                FullName = "John Doe",
                Username = "john.doe",
                Password = "password123",
                Email = "john@example.com",
                PhoneNumber = 1234567890
                // Set other properties as needed
            };

            _mockUserService.Setup(service => service.CreateUser(validUser))
                            .ReturnsAsync(validUser);

            // Act
            var result = await _userController.CreateUser(validUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            // Add more assertions based on the expected response
        }

        [Test]
        public async Task CreateUser_WithExistingUsername_ShouldReturnConflict()
        {
            // Arrange
            var existingUser = new UserDetails
            {
                // Existing user with the same username
                UserId = 1,
                FullName = "John Doe",
                Username = "john.doe",
                Password = "password123",
                Email = "john@example.com",
                PhoneNumber = 1234567890
            };

            _mockUserService.Setup(service => service.CreateUser(existingUser))
                            .ThrowsAsync(new UsernameAlreadyExistException("Username already exists"));

            // Act
            var result = await _userController.CreateUser(existingUser);

            // Assert
            Assert.IsInstanceOf<ConflictObjectResult>(result);
            var conflictResult = result as ConflictObjectResult;
            Assert.IsNotNull(conflictResult);
            Assert.AreEqual(StatusCodes.Status409Conflict, conflictResult.StatusCode);
            // Add more assertions based on the expected response
        }

        // Similar tests for handling UserNotCreatedException, generic Exception, etc.

        [Test]
        public async Task GetAllUser_ShouldReturnOk()
        {
            // Arrange
            var expectedUsers = new List<UserDetails>
            {
                // Simulated list of users
            };

            _mockUserService.Setup(service => service.GetAllUser())
                            .ReturnsAsync(expectedUsers);

            // Act
            var result = await _userController.GetAllUser();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            // Add more assertions based on the expected response
        }

       
    }
}
