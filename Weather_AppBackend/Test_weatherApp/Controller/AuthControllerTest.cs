using Authentication_Service.Controllers;
using Authentication_Service.Exceptions;
using Authentication_Service.Model;
using Authentication_Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Serilog;
using System;

namespace Authentication_Service.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _authController;
        private Mock<IAuthService> _mockAuthService;

        [SetUp]
        public void Setup()
        {
            _mockAuthService = new Mock<IAuthService>();
            _authController = new AuthController(_mockAuthService.Object);
        }

        [Test]
        public void Login_WithValidModel_ShouldReturnOk()
        {
            // Arrange
            var validModel = new LoginRequestModel
            {
                Username = "validUser",
                Password = "validPassword"
            };

            var expectedToken = "mockTokenValue";
            var expectedUserId = 123;
            var expectedUsername = "validUser";

            var expectedAuthenticationResult = new AuthenticationResult
            {
                message = "Authentication successful",
                UserId = expectedUserId,
                Username = expectedUsername,
                TokenValue = expectedToken,
                ExpirationDate = DateTime.UtcNow.AddDays(1) 
            };

            // Act
            var result = _authController.Login(validModel);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            // Add more assertions based on the expected response
        }

        [Test]
        public void Login_WithInvalidModel_ShouldReturnBadRequest()
        {
            // Arrange
            var invalidModel = new LoginRequestModel
            {
                Username = "user123",
                Password = null // Null Password
            };

            _authController.ModelState.AddModelError("Password", "The Password field is required.");

            // Act
            var result = _authController.Login(invalidModel);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            // Add more assertions based on the expected response
        }
    

        [Test]
        public void Login_WithAuthenticationException_ShouldReturnBadRequest()
        {
            // Arrange
            var model = new LoginRequestModel
            {
                Username = "validUser",
                Password = "invalidPassword"
            };

            _mockAuthService.Setup(service => service.AuthenticateUser(model.Username, model.Password))
                            .Throws(new AuthenticationException("Invalid credentials"));

            // Act
            var result = _authController.Login(model);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            // Add more assertions based on the expected response
        }

        
    }
}
