using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using User_Service.Exceptions;
using Wishlist_Service.Exceptions;
using Wishlist_Service.Model;
using Wishlist_Service.Repository;
using Wishlist_Service.Service;

namespace Test_WeatherApp.Service
{
    [TestFixture]
    internal class WishListServiceTest
    {
        private WishlistService wishlistService;
        private Mock<IWishlistRepository> mockWishlistRepo;

        [SetUp]
        public void Setup()
        {
            mockWishlistRepo = new Mock<IWishlistRepository>();
            wishlistService = new WishlistService(mockWishlistRepo.Object);
        }

        [Test]
        public async Task CreateCity_WhenCityDoesNotExist_ShouldCreateCity()
        {
            // Arrange
            var wishlist = new Wishlist { City = "NewCity", Username = "user123", CityId = 1 };
            mockWishlistRepo.Setup(r => r.ExistsCityForUser(wishlist.City, wishlist.Username)).ReturnsAsync(false);
            mockWishlistRepo.Setup(r => r.ExistsCityId(wishlist.CityId)).ReturnsAsync(false);
            mockWishlistRepo.Setup(r => r.CreateWishlist(wishlist)).Returns(Task.CompletedTask);

            // Act
            var result = await wishlistService.CreateCity(wishlist);

            // Assert
            Assert.AreEqual(wishlist, result);
            mockWishlistRepo.Verify(r => r.CreateWishlist(wishlist), Times.Once);
        }

        [Test]
        public void CreateCity_WhenCityExists_ShouldThrowAlreadyExistException()
        {
            // Arrange
            var wishlist = new Wishlist { City = "ExistingCity", Username = "user123", CityId = 1 };
            mockWishlistRepo.Setup(r => r.ExistsCityForUser(wishlist.City, wishlist.Username)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<AlredyExistexception>(async () => await wishlistService.CreateCity(wishlist));
            mockWishlistRepo.Verify(r => r.CreateWishlist(It.IsAny<Wishlist>()), Times.Never);
        }

        [Test]
        public void CreateCity_WhenCityIdExists_ShouldThrowAlreadyExistException()
        {
            // Arrange
            var wishlist = new Wishlist { City = "NewCity", Username = "user123", CityId = 1 };
            mockWishlistRepo.Setup(r => r.ExistsCityForUser(wishlist.City, wishlist.Username)).ReturnsAsync(false);
            mockWishlistRepo.Setup(r => r.ExistsCityId(wishlist.CityId)).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<AlredyExistexception>(async () => await wishlistService.CreateCity(wishlist));
            mockWishlistRepo.Verify(r => r.CreateWishlist(It.IsAny<Wishlist>()), Times.Never);
        }


        [Test]
        public void DeleteCity_WhenCityDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var city = "NonExistingCity";
            var username = "user123";
            mockWishlistRepo.Setup(r => r.ExistsCityForUser(city, username)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await wishlistService.DeleteCity(city, username));
            mockWishlistRepo.Verify(r => r.DeleteCity(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetAllCity_WhenWishlistIsNotEmpty_ShouldReturnListOfCities()
        {
            // Arrange
            var expectedWishlist = new List<Wishlist> { /* Populate with test data */ };
            mockWishlistRepo.Setup(r => r.GetAllCity()).ReturnsAsync(expectedWishlist);

            // Act
            var result = await wishlistService.GetAllCity();

            // Assert
            Assert.AreEqual(expectedWishlist, result);
        }
        [Test]
        public void GetCitybyName_WhenCityDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var city = "NonExistingCity";
            mockWishlistRepo.Setup(r => r.GetCitybyName(city)).ReturnsAsync((Wishlist)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await wishlistService.GetCitybyName(city));
        }

        [Test]
        public async Task GetCitybyUsername_WhenUserExists_ShouldReturnListOfCities()
        {
            // Arrange
            var username = "user123";
            var expectedWishlist = new List<Wishlist> { /* Populate with test data */ };
            mockWishlistRepo.Setup(r => r.GetCitybyUserName(username)).ReturnsAsync(expectedWishlist);

            // Act
            var result = await wishlistService.GetCitybyUsername(username);

            // Assert
            Assert.AreEqual(expectedWishlist, result);
        }

        [Test]
        public void GetCitybyUsername_WhenUserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var username = "NonExistingUser";
            mockWishlistRepo.Setup(r => r.GetCitybyUserName(username)).ReturnsAsync((List<Wishlist>)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await wishlistService.GetCitybyUsername(username));
        }


    }
}
