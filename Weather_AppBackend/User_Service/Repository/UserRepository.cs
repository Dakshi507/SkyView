using MongoDB.Driver;
using User_Service.Model;

namespace User_Service.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _user;

        public UserRepository(UserContext userContext)
        {
           _user = userContext;

        }
        public async Task CreateUser(UserDetails user)
        {
            var lastUserId = _user.UserDetailsCollection.AsQueryable().Max(r => r.UserId);
            user.UserId = lastUserId + 1;
            await _user.UserDetailsCollection.InsertOneAsync(user);
        }

        public async Task<bool> ExistsUsername(string username)
        {
            var filter = Builders<UserDetails>.Filter.Eq(u => u.Username, username);
            var count = await _user.UserDetailsCollection.CountDocumentsAsync(filter);
            return count > 0;
        }
        public async Task<bool> ExistsUserId(int userId)
        {
            var filter = Builders<UserDetails>.Filter.Eq(u => u.UserId, userId);
            var count = await _user.UserDetailsCollection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<List<UserDetails>> GetAllUser()
        {
            var filter = Builders<UserDetails>.Filter.Empty; 
            return await _user.UserDetailsCollection.Find(filter).ToListAsync();
        }
        public async Task<UserDetails> GetUserByUserId(int userId)
        {
            var filter = Builders<UserDetails>.Filter.Eq(u => u.UserId, userId);
            return await _user.UserDetailsCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
