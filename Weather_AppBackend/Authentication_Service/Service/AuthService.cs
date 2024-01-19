using Authentication_Service.Exceptions;
using Authentication_Service.Model;
using Authentication_Service.Repository;

namespace Authentication_Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        public AuthenticationResult AuthenticateUser(string username, string password)
        {
            var authenticationResult = _authRepo.AuthenticateUser(username, password);

            if (authenticationResult != null)
            {
                return new AuthenticationResult
                {
                    message = authenticationResult.message,
                    UserId = authenticationResult.UserId,
                    Username = authenticationResult.Username,
                    
                    TokenValue = authenticationResult.TokenValue,
                    ExpirationDate = authenticationResult.ExpirationDate
                };
            }
            else
            {
                throw new AuthenticationException("Invalid username or password");
            }
        }
    }
}
