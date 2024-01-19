using NUnit.Framework;
using Wishlist_Service.Controllers;
using Wishlist_Service.Exceptions;
using Wishlist_Service.Model;
using Wishlist_Service.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User_Service.Exceptions;

namespace Wishlist_Service.Tests
{
    [TestFixture]
    public class WishListControllerTest
    {
        private WishListController _controller;
        private Mock<IWishlistService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IWishlistService>();
            _controller = new WishListController(_mockService.Object);
        }

        [Test]
        public async Task AddCityinWishList_ValidCity_ShouldReturnOk()
        {
            // Arrange
            var wishlist = new Wishlist
            {
                CityId = 1002,
                City = "New City",
                Country = "New Country",
                Username = "user2"
            };

            _mockService.Setup(service => service.CreateCity(It.IsAny<Wishlist>())).ReturnsAsync(wishlist);

            // Act
            var result = await _controller.AddCityinWishList(wishlist) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            // Add more assertions based on expected result data
        }

        [Test]
        public async Task GetAlCity_ShouldReturnAllCities()
        {
            // Arrange
            var cities = new List<Wishlist> {
                new Wishlist { CityId = 1, City = "City1", Country = "Country1", Username = "user1" },
                new Wishlist { CityId = 2, City = "City2", Country = "Country2", Username = "user2" }
            };
            _mockService.Setup(service => service.GetAllCity()).ReturnsAsync(cities);

            // Act
            var result = await _controller.GetAlCity() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            // Add more assertions based on expected result data
        }

        [Test]
        public async Task AddCityinWishList_AlreadyExistingCity_ShouldReturnConflict()
        {
            // Arrange
            var wishlist = new Wishlist
            {
                CityId = 1002,
                City = "Existing City", // Assume this city already exists
                Country = "Country",
                Username = "user"
            };

            _mockService.Setup(service => service.CreateCity(It.IsAny<Wishlist>()))
                .ThrowsAsync(new AlredyExistexception("City already exists in wishlist"));

            // Act
            var result = await _controller.AddCityinWishList(wishlist) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(409, result.StatusCode); // Conflict status code
                                                     // Add more assertions based on expected result data
        }

        [Test]
        public async Task GetCitybyUsername_ValidUsername_ShouldReturnCities()
        {
            // Arrange
            var username = "user1";
            var cities = new List<Wishlist> {
        new Wishlist { CityId = 1, City = "City1", Country = "Country1", Username = username },
        new Wishlist { CityId = 2, City = "City2", Country = "Country2", Username = username }
    };
            _mockService.Setup(service => service.GetCitybyUsername(username)).ReturnsAsync(cities);

            // Act
            var result = await _controller.GetCitybyUsername(username) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            // Add more assertions based on expected result data
        }

        [Test]
        public async Task DeleteCity_NonExistingCity_ShouldReturnNotFound()
        {
            // Arrange
            var cityName = "NonExistingCity"; // Assume this city doesn't exist
            var username = "user";

            _mockService.Setup(service => service.DeleteCity(cityName, username))
                .ThrowsAsync(new NotFoundException("City not found for deletion"));

            // Act
            var result = await _controller.DeleteCity(cityName, username) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode); // Not Found status code
                                                     // Add more assertions based on expected result data
        }


        [TearDown]
        public void Teardown()
        {
            _controller = null;
            _mockService = null;
        }
    }
}
