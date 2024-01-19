using Authentication_Service.Model;

namespace Authentication_Service.Service
{
    public interface IAuthService
    {
        AuthenticationResult AuthenticateUser(string username, string password);
    }
}
