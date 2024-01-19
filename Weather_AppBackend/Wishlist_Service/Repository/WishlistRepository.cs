using MongoDB.Driver;
using Wishlist_Service.Model;

namespace Wishlist_Service.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private WishListContext wishListcontext;
        public WishlistRepository(WishListContext _wishListcontext)
        {
            wishListcontext = _wishListcontext;
        }
        public async Task CreateWishlist(Wishlist wishlist)
        {
            var lastCityId = wishListcontext.CityCollection.AsQueryable().Max(r => r.CityId);
            wishlist.CityId = lastCityId + 1;
            await wishListcontext.CityCollection.InsertOneAsync(wishlist);
        }

        public async Task DeleteCity(string city, string username)
        {
            var filter = Builders<Wishlist>.Filter.And(
            Builders<Wishlist>.Filter.Eq(u => u.City, city),
            Builders<Wishlist>.Filter.Eq(u => u.Username, username)
        );

            await wishListcontext.CityCollection.DeleteOneAsync(filter);
        }

        public async Task<bool> ExistsCityForUser(string city, string username)
{
    var filter = Builders<Wishlist>.Filter.Eq(u => u.City, city) & Builders<Wishlist>.Filter.Eq(u => u.Username, username);
    var count = await wishListcontext.CityCollection.CountDocumentsAsync(filter);
    return count > 0;
}

        public async Task<bool> ExistsCityId(int cityId)
        {
            var filter = Builders<Wishlist>.Filter.Eq(u => u.CityId, cityId);
            var count = await wishListcontext.CityCollection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<List<Wishlist>> GetAllCity()
        {
            var filter = Builders<Wishlist>.Filter.Empty;
            return await wishListcontext.CityCollection.Find(filter).ToListAsync();
        }

        public async Task<Wishlist> GetCitybyName(string city)
        {
            var filter = Builders<Wishlist>.Filter.Eq(u => u.City, city);
            return await wishListcontext.CityCollection.Find(filter).FirstOrDefaultAsync();
        }

       /* public async Task<List<Wishlist>> GetCitybyUserName(string username)
        {
            var filter = Builders<Wishlist>.Filter.Eq(u => u.Username, username);
            return await wishListcontext.CityCollection.Find(filter).ToListAsync(); ;
        }*/
        public async Task<List<Wishlist>> GetCitybyUserName(string username)
        {
            var filter = Builders<Wishlist>.Filter.Eq(u => u.Username, username);
            return await wishListcontext.CityCollection.Find(filter).ToListAsync();
        }
    }
}
