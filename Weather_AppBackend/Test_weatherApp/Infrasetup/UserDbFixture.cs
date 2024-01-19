using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using User_Service.Model;

namespace InfraSetup
{
    public class UserDbFixture : IDisposable
    {
        private IConfigurationRoot configuration;
        public UserContext context;
        public UserDbFixture()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

            configuration = builder.Build();
            context = new UserContext(configuration);

            context.UserDetailsCollection.DeleteMany(Builders<UserDetails>.Filter.Empty);
            context.UserDetailsCollection.InsertMany(new List<UserDetails>
            {
               // new UserDetails{ UserId=101, FullName="Dakshi Shukla",Username="Dakshi", Password="12345", Email = "dakshi@gmail.com" , PhoneNumber = 9898898989 },
                new UserDetails{ UserId=1001, FullName="Deeksha Shukla",Username="Deeksha", Password="12345", Email = "deeksha@gmail.com" , PhoneNumber = 6567658909}
            });
        }
        public void Dispose()
        {
            context = null;
        }
    }
}