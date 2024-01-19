namespace User_Service.Exceptions
{
    public class UserNotCreatedException : ApplicationException
    {
        public UserNotCreatedException(string v) { }
        public UserNotCreatedException(string message, Exception ex) : base(message) { }
    }
}
