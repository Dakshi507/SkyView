using MongoDB.Driver;

namespace Wishlist_Service.Model
{
    public class WishListContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Wishlist> _cityCollection;
        public WishListContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("DatabaseSetting:ConnectionString").Value;
            string databaseName = configuration.GetSection("DatabaseSetting:DatabaseName").Value;
            string collectionName = configuration.GetSection("DatabaseSetting:CollectionName").Value;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            _cityCollection = _database.GetCollection<Wishlist>(collectionName);
        }

        public IMongoCollection<Wishlist> CityCollection => _cityCollection;
    }
}
