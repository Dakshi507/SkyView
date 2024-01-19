using User_Service.Model;

namespace User_Service.Repository
{
    public interface IUserRepository
    {
        Task CreateUser(UserDetails user);
        Task <bool> ExistsUsername(string username);
        Task<bool> ExistsUserId(int userId);
        Task<List<UserDetails>> GetAllUser();
       /* Task<UserDetails> GetUserByUserId(int userId);*/
    }
}
