using User_Service.Model;

namespace User_Service.Service
{
    public interface IUserService
    {
        Task <UserDetails> CreateUser(UserDetails user);
        Task<List<UserDetails>> GetAllUser();
        /*Task<UserDetails> GetUserById(int userId);*/
    }
}
