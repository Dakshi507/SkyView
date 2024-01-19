using NUnit.Framework;
using Authentication_Service.Exceptions;
using Authentication_Service.Model;
using Authentication_Service.Repository;
using Authentication_Service.Service;
using Moq;

namespace Authentication_Service.Tests
{
    [TestFixture]
    public class AuthServiceTest
    {
        private Mock<IAuthRepo> _authRepoMock;
        private AuthService _authService;

        [SetUp]
        public void Setup()
        {
            _authRepoMock = new Mock<IAuthRepo>();
            _authService = new AuthService(_authRepoMock.Object);
        }

        // Positive Test Case

        [Test]
        public void AuthenticateUser_Should_ReturnAuthenticationResultForValidCredentials()
        {
            // Arrange
            var validUsername = "ValidUser";
            var validPassword = "ValidPassword";
            var expectedAuthResult = new AuthenticationResult
            {
                message = "Authenticated",
                UserId = 1,
                Username = validUsername,
                TokenValue = "ValidToken",
                ExpirationDate = new DateTime(2023, 12, 30)
        };
            _authRepoMock.Setup(repo => repo.AuthenticateUser(validUsername, validPassword)).Returns(expectedAuthResult);

            // Act
            var authResult = _authService.AuthenticateUser(validUsername, validPassword);

            // Assert
            Assert.IsNotNull(authResult);
            Assert.AreEqual(expectedAuthResult.message, authResult.message);
            Assert.AreEqual(expectedAuthResult.UserId, authResult.UserId);
            Assert.AreEqual(expectedAuthResult.Username, authResult.Username);
            Assert.AreEqual(expectedAuthResult.TokenValue, authResult.TokenValue);
            // Add more assertions as needed
        }

        // Negative Test Case

        [Test]
        public void AuthenticateUser_Should_ThrowExceptionForInvalidCredentials()
        {
            // Arrange
            var invalidUsername = "InvalidUser";
            var invalidPassword = "InvalidPassword";
            _authRepoMock.Setup(repo => repo.AuthenticateUser(invalidUsername, invalidPassword)).Returns((AuthenticationResult)null);

            // Act & Assert
            var ex = Assert.Throws<AuthenticationException>(() => _authService.AuthenticateUser(invalidUsername, invalidPassword));
            Assert.AreEqual("Invalid username or password", ex.Message);
        }
    }
}
