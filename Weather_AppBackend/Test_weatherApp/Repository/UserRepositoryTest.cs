using System;
using System.Threading.Tasks;
using InfraSetup;
using MongoDB.Driver;
using NUnit.Framework;
using User_Service.Model;
using User_Service.Repository;

namespace User_Service.Tests
{
    [TestFixture]
    public class UserRepositoryTest : IDisposable
    {
        private readonly UserDbFixture _userDbFixture;
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            _userDbFixture = new UserDbFixture();
            _userRepository = new UserRepository(_userDbFixture.context);
        }

        [Test]
        public async Task CreateUser_WhenValidUserProvided_ShouldCreateUser()
        {
            // Arrange
            var newUser = new UserDetails
            {
                // Create a new user object with valid details for testing
                UserId = 1002,
                FullName = "John Doe",
                Username = "johndoe",
                Password = "password123",
                Email = "john@example.com",
                PhoneNumber = 1234567890
            };

            // Act
            await _userRepository.CreateUser(newUser);

            // Assert
            var createdUser = await _userRepository.GetUserByUserId(newUser.UserId);
            Assert.NotNull(createdUser);
            Assert.AreEqual(newUser.UserId, createdUser.UserId);
            // Add more assertions based on your requirements
        }

        [Test]
        public async Task ExistsUsername_WhenExistingUsernameProvided_ShouldReturnTrue()
        {
            // Arrange
            var existingUsername = "Deeksha"; // Assuming "Deeksha" already exists

            // Act
            var exists = await _userRepository.ExistsUsername(existingUsername);

            // Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsUsername_WhenNonExistingUsernameProvided_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingUsername = "NonExistingUser123"; 

            // Act
            var exists = await _userRepository.ExistsUsername(nonExistingUsername);

            // Assert
            Assert.IsFalse(exists);
        }


        [Test]
        public async Task ExistsUserId_WhenExistingUserIdProvided_ShouldReturnTrue()
        {
            
            var existingUserId = 1001; 

            var exists = await _userRepository.ExistsUserId(existingUserId);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task GetAllUser_ShouldReturnAllUsers()
        {
            
            var allUsers = await _userRepository.GetAllUser();

            Assert.NotNull(allUsers);
            Assert.IsInstanceOf(typeof(List<UserDetails>), allUsers);
           
        }

       

      
        [Test]
        public async Task ExistsUsername_WhenNullUsernameProvided_ShouldReturnFalse()
        {
          
            string nullUsername = null;

            var exists = await _userRepository.ExistsUsername(nullUsername);

            Assert.IsFalse(exists);
        }

       

        public void Dispose()
        {
            _userDbFixture.context = null;
        }
    }
}