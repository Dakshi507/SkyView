using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace User_Service.Model
{
    public class UserContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<UserDetails> _userDetailsCollection;

        public UserContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("DatabaseSetting:ConnectionString").Value;
            string databaseName = configuration.GetSection("DatabaseSetting:DatabaseName").Value;
            string collectionName = configuration.GetSection("DatabaseSetting:CollectionName").Value;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            _userDetailsCollection = _database.GetCollection<UserDetails>(collectionName);
        }

        public IMongoCollection<UserDetails> UserDetailsCollection => _userDetailsCollection;
    }
}
