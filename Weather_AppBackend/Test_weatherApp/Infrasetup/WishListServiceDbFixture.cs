using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Wishlist_Service.Model;

namespace Test_WeatherApp.Infrasetup
{
    internal class WishListServiceDbFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public WishListContext context;
        public WishListServiceDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new (configuration);

            context.CityCollection.DeleteMany(Builders<Wishlist>.Filter.Empty);
            context.CityCollection.InsertMany(new List<Wishlist>
            {
              
                new Wishlist{ CityId =1001, City = "Delhi", Country = "India", Username = "user1"}
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}
