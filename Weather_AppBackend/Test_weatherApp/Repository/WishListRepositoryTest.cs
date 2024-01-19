using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Test_WeatherApp.Infrasetup;
using Wishlist_Service.Model;
using Wishlist_Service.Repository;

namespace Wishlist_Service.Tests
{
    [TestFixture]
    public class WishlistRepositoryTest : IDisposable
    {
        private readonly WishListServiceDbFixture _wishlistDbFixture;
        private readonly WishlistRepository _wishlistRepository;

        public WishlistRepositoryTest()
        {
            _wishlistDbFixture = new WishListServiceDbFixture();
            _wishlistRepository = new WishlistRepository(_wishlistDbFixture.context);
        }

        [Test]
        public async Task CreateWishlist_WhenValidWishlistProvided_ShouldCreateWishlist()
        {
           
            var newWishlist = new Wishlist
            {
                CityId = 1002,
                City = "New City",
                Country = "New Country",
                Username = "user2"
            };

            
            await _wishlistRepository.CreateWishlist(newWishlist);

            var createdWishlist = await _wishlistRepository.GetCitybyName(newWishlist.City);
            Assert.NotNull(createdWishlist);
            Assert.AreEqual(newWishlist.CityId, createdWishlist.CityId);
           
        }

        [Test]
        public async Task DeleteCity_WhenExistingCityAndUsernameProvided_ShouldDeleteCity()
        {
            
            var cityToDelete = "Delhi";
            var username = "user1";

           
            await _wishlistRepository.DeleteCity(cityToDelete, username);

           
            var deletedCity = await _wishlistRepository.GetCitybyName(cityToDelete);
            Assert.Null(deletedCity);
        }

        [Test]
        public async Task GetAllCity_ShouldReturnAllCities()
        {
            
            var allCities = await _wishlistRepository.GetAllCity();

           
            Assert.NotNull(allCities);
            Assert.IsInstanceOf(typeof(List<Wishlist>), allCities);
            
        }

        

        public void Dispose()
        {
            _wishlistDbFixture.context = null;
        }
    }
}
