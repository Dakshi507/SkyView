using MongoDB.Driver;
using User_Service.Exceptions;
using User_Service.Model;
using User_Service.Repository;

namespace User_Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepo;
        public UserService(IUserRepository _userRepo)
        {
            userRepo = _userRepo;
        }
        public async Task<UserDetails> CreateUser(UserDetails user)
        {
            var existusername = await userRepo.ExistsUsername(user.Username);
            var existuserId = await userRepo.ExistsUserId(user.UserId);
            if(existusername)
            {
                throw new UsernameAlreadyExistException("Username already exists");
            }
            else if (existuserId)
            {
                throw new UserIdAlredyExistexception("UserId already exists");
            }
            else 
            {
                try
                {
                    await userRepo.CreateUser(user);
                    return user; 
                }
                catch (Exception ex)
                {
                    throw new UserNotCreatedException("Failed to create user", ex);              
                }
            }               

        }

        public async Task<List<UserDetails>> GetAllUser()
        {
            try
            {
                var users = await userRepo.GetAllUser();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("some exception error", ex);
            }
        }

       /* public async Task<UserDetails> GetUserById(int userId)
        {
            try
            {
                var user = await userRepo.GetUserByUserId(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("some exception error", ex);
            }
        }*/
    }
}
