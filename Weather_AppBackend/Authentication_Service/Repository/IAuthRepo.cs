using Authentication_Service.Model;

namespace Authentication_Service.Repository
{
    public interface IAuthRepo
    {
        AuthenticationResult AuthenticateUser(string username, string password);
    }
}
