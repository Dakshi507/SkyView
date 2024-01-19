using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User_Service.Exceptions;
using User_Service.Model;
using User_Service.Repository;
using User_Service.Service;
using Moq;

namespace User_Service.Tests
{
    [TestFixture]
    public class UserServiceTest

    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        // Positive Test Cases

        [Test]
        public async Task CreateUser_Should_CreateNewUser()
        {
            // Arrange
            var user = new UserDetails
            {
                UserId = 1,
                FullName = "John Doe",
                Username = "johndoe",
                Password = "password",
                Email = "john@example.com",
                PhoneNumber = 1234567890
            };

            _userRepositoryMock.Setup(repo => repo.ExistsUsername(user.Username)).ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.ExistsUserId(user.UserId)).ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(Task.CompletedTask);

            // Act
            var createdUser = await _userService.CreateUser(user);

            // Assert
            Assert.IsNotNull(createdUser);
            // Add more assertions based on your user creation logic
        }

        [Test]
        public async Task GetAllUser_Should_ReturnListOfUsers()
        {
            // Arrange
            var userList = new List<UserDetails>
            {
                new UserDetails { UserId = 1, FullName = "User 1", Username = "User1", Email = "user1@gmail.com", Password = "12345", PhoneNumber = 1234567890},
                new UserDetails { UserId = 2, FullName = "User 2", Username = "User2", Email = "user2@gmail.com", Password = "12345", PhoneNumber = 1234560890},
                // Add more test user data as needed
            };
            _userRepositoryMock.Setup(repo => repo.GetAllUser()).ReturnsAsync(userList);

            // Act
            var users = await _userService.GetAllUser();

            // Assert
            Assert.IsNotNull(users);
            Assert.IsInstanceOf<List<UserDetails>>(users);
            // Add more specific assertions if required
        }

        // Negative Test Cases

        [Test]
        public void CreateUser_Should_ThrowException_When_UsernameExists()
        {
            // Arrange
            var existingUsername = "ExistingUser";
            var user = new UserDetails
            {
                UserId = 2,
                Username = existingUsername,
                // Add other necessary properties for the user
            };

            _userRepositoryMock.Setup(repo => repo.ExistsUsername(existingUsername)).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UsernameAlreadyExistException>(async () => await _userService.CreateUser(user));
            Assert.AreEqual("Username already exists", ex.Message);
        }

        [Test]
        public void CreateUser_Should_ThrowException_When_UserIdExists()
        {
            // Arrange
            var existingUserId = 3;
            var user = new UserDetails
            {
                UserId = existingUserId,

                FullName = "John Doe",
                Username = "johndoe",
                Password = "password",
                Email = "john@example.com",
                PhoneNumber = 1234567890
            };

            _userRepositoryMock.Setup(repo => repo.ExistsUsername(user.Username)).ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.ExistsUserId(existingUserId)).ReturnsAsync(true);

            // Act & Assert
            var ex = Assert.ThrowsAsync<UserIdAlredyExistexception>(async () => await _userService.CreateUser(user));
            Assert.AreEqual("UserId already exists", ex.Message);
        }

        // Additional Test Cases

   
    }
}
